clearInterval(refreshId); // Hủy bỏ việc kiểm tra coi có đang typing hay không
var switchID = 1;
// Declare a proxy to reference the hub.
var chatHub = $.connection.chatHub; // Tạo Hub
$.connection.hub.logging = true; // Bắt buộc đăng nhập


registerClientMethods(chatHub); // đăng ký các sự kiện đăng nhập đăng xuất  cho Hub

// Start Hub ( chạy Hub ) => đợi đến khi nào xong rồi chạy function
$.connection.hub.start().done(function () {
    registerEvents(chatHub) // Đăng ký sự kiện đăng nhập, gửi mail cho Hub
});

// ------------------------------------------------------------------Variable (Thuộc Tính Cần) ----------------------------------------------------------------------//
var loadMesgCount = 10; // dùng để load số lượng message cần
var topPosition = 0; // từ vị trí nào
var refreshId = null; // sự kiện kiểm tra xem người dùng có đang typing hay không

// ------------------------------------------------------------------Start All Chat ----------------------------------------------------------------------//
// sự kiện đăng ký bắt đầu chat
function registerEvents(chatHub) {
    // sự kiện đăng ký và đăng nhập
    $("#btnStartChat").click(function () {
        // lấy tên
        var name = $("#txtNickName").val();
        // lấy Email
        var email = $('#txtEmailId').val();
        // validate 
        if (name.length > 0 && email.length > 0) {
            // gán và giữ email để quản lý 
            $('#hdEmailID').val(email);
            // bắt đầu kết nối với Server để tạo liên kết => để chat (chatHub.server.tenMethod là gọi các hàm ở Server )
            chatHub.server.connect(name, email);
        }
        else {
            alert("Please enter your details");
        }
    });

    //$("#txtNickName").keypress(function (e) {
    //    if (e.which == 13) {
    //        $("#btnStartChat").click();
    //    }
    //});

    //$("#txtMessage").keypress(function (e) {
    //    if (e.which == 13) {
    //        $('#btnSendMsg').click();
    //    }
    //});

    //$('#btnSendMsg').click(function () {
    //    var msg = $("#txtMessage").val();
    //    if (msg.length > 0) {

    //        var userName = $('#hdUserName').val();
    //        chatHub.server.sendMessageToAll(userName, msg);
    //        $("#txtMessage").val('');
    //    }
    //});
}



// sự kiện ở Client phát sinh
function registerClientMethods(chatHub) {
    // Calls when user successfully logged in
    // Sự kiện đc chạy khi đăng nhập ở server thành công
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {
        // gán các giá trị để quản lý
        $('#hdId').val(id);
        $chatbox.removeClass('chatbox--empty');
        //$('#hdUserName').val(userName);
        //$('#spanUser').html(userName);

        // Add All Users ( quản lý danh sách tất cả những người đang online )
        //for (i = 0; i < allUsers.length; i++) {
        //    AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].EmailID);
        //}

        // Add Existing Messages ( hiện những tin đã được nhắn và lưu )
        //for (i = 0; i < messages.length; i++) {
        //    AddMessage(messages[i].UserName, messages[i].Message);
        //}

        // ẩn đăng nhập
        //$('.login').css('display', 'none');
    }

    // On New User Connected ( nếu có một user mới tham gia thì sẽ chạy sự kiện này )
    //chatHub.client.onNewUserConnected = function (id, name, email) {
    //    AddUser(chatHub, id, name, email);
    //}

    // On User Disconnected ( sự kiện xảy ra khi một user mất kết nối )
    //chatHub.client.onUserDisconnected = function (id, userName) {
    //    $('#' + id).remove();

    //    var ctrId = 'private_' + id;
    //    $('#' + ctrId).remove();

    //    var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');

    //    $(disc).hide();
    //    $('#divusers').prepend(disc);
    //    $(disc).fadeIn(200).delay(2000).fadeOut(200);
    //}

    // On User Disconnected Existing ( sự kiện xảy ra khi một user đang chat disconect )
    //chatHub.client.onUserDisconnectedExisting = function (id, userName) {
    //    $('#' + id).remove();
    //    var ctrId = 'private_' + id;
    //    $('#' + ctrId).remove();
    //}

    // hiện tin nhắn lên group chung
    //chatHub.client.messageReceived = function (userName, message) {
    //    AddMessage(userName, message);
    //}


    // khi client gửi Tin nhắn đến một tài khoản ( windowId dùng để biết đc là mình là key nào để còn giao tiếp )
    chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, userEmail, email, status, fromUserId, thisid) {
        var ctrId = 'chatbox__body';
        if (status == 'Click') {
            if ($('.chatbox__body').length == 0) {
                //createPrivateChatWindow(chatHub, windowId, ctrId, fromUserName, userEmail, email);
                //chatHub.server.getPrivateMessage(userEmail, email, loadMesgCount).done(function (msg) {
                //    for (i = 0; i < msg.length; i++) {
                //        $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + msg[i].userName + '</span>: ' + msg[i].message + '</div>');
                //        // set scrollbar
                //        scrollTop(ctrId);
                //    }
                //});
            }
            else {
                console.log("windowId:" + windowId);
                console.log("fromUserId:" + fromUserId);
                switchID = thisid;
                console.log("switchID:" + switchID);
                var appendHTML = "";
                if (fromUserId != $('#hdId').val()) {
                    
                    appendHTML = '<div class="chatbox__body__message chatbox__body__message--left"><img src="https://s3.amazonaws.com/uifaces/faces/twitter/arashmil/128.jpg" alt="Picture"> <p>' + message + '</p></div>';
                }
                else {
                    appendHTML = '<div class="chatbox__body__message chatbox__body__message--right"><img src="https://s3.amazonaws.com/uifaces/faces/twitter/brad_frost/128.jpg" alt="Picture"> <p>' + message + '</p></div>';

                }

                //$('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + fromUserName + '</span>: ' + message + '</div>');
                $('.chatbox__body').append(appendHTML);
                // set scrollbar
                //scrollTop(ctrId);
            }
        }

        if (status == 'Type') {
            if (fromUserId == windowId)
                $('#' + ctrId).find('#msgTypeingName').text('typing...');
        }
        else { $('#' + ctrId).find('#msgTypeingName').text(''); }
    }
}

// Add User ( khi có thằng nào đăng nhập vào )
//function AddUser(chatHub, id, name, email) {
//    var userId = $('#hdId').val();
//    var userEmail = $('#hdEmailID').val();
//    var code = "";

//    if (userEmail == email && $('.loginUser').length == 0) {
//        code = $('<div class="loginUser">' + name + "</div>");
//    }
//    else {
//        code = $('<a id="' + id + '" class="user" >' + name + '<a>');
//        $(code).click(function () {
//            var id = $(this).attr('id');
//            if (userEmail != email) {
//                OpenPrivateChatWindow(chatHub, id, name, userEmail, email);
//            }
//        });
//    }

//    $("#divusers").append(code);
//}

// Add Message (thêm nội dung tin nhắn )
//function AddMessage(userName, message) {
//    $('#divChatWindow').append('<div class="message"><span class="userName">' + userName + '</span>: ' + message + '</div>');

//    var height = $('#divChatWindow')[0].scrollHeight;
//    $('#divChatWindow').scrollTop(height);
//}

// ------------------------------------------------------------------End All Chat ----------------------------------------------------------------------//





// ------------------------------------------------------------------Start Private Chat ----------------------------------------------------------------------//
// mở cửa sổ chat
function OpenPrivateChatWindow(chatHub, id, userName, userEmail, email) {
    var ctrId = 'private_' + id;
    if ($('#' + ctrId).length > 0) return;

    createPrivateChatWindow(chatHub, id, ctrId, userName, userEmail, email);

    chatHub.server.getPrivateMessage(userEmail, email, loadMesgCount).done(function (msg) {
        for (i = 0; i < msg.length; i++) {
            $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + msg[i].userName + '</span>: ' + msg[i].message + '</div>');
            // set scrollbar
            scrollTop(ctrId);
        }
    });
}
// tạo cửa sổ chat
function createPrivateChatWindow(chatHub, userId, ctrId, userName, userEmail, email) {

    var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" rel="0">' +
                '<div class="header">' +
                    '<div  style="float:right;">' +
                        '<img id="imgDelete"  style="cursor:pointer;" src="/Images/delete.png"/>' +
                    '</div>' +

                    '<span class="selText" rel="0">' + userName + '</span>' +
                    '<span class="selText" id="msgTypeingName" rel="0"></span>' +
                '</div>' +
                '<div id="divMessage" class="messageArea">' +

                '</div>' +
                '<div class="buttonBar">' +
                    '<input id="txtPrivateMessage" class="msgText" type="text"   />' +
                    '<input id="btnSendMessage" class="submitButton button" type="button" value="Send"   />' +
                '</div>' +
                '<div id="scrollLength"></div>' +
            '</div>';

    var $div = $(div);

    // ------------------------------------------------------------------ Scroll Load Data ----------------------------------------------------------------------//
    // lấy dữ liệu chat cũ
    //var scrollLength = 2;
    //$div.find('.messageArea').scroll(function () {
    //    if ($(this).scrollTop() == 0) {
    //        if ($('#' + ctrId).find('#scrollLength').val() != '') {
    //            var c = parseInt($('#' + ctrId).find('#scrollLength').val(), 10);
    //            scrollLength = c + 1;
    //        }
    //        $('#' + ctrId).find('#scrollLength').val(scrollLength);
    //        var count = $('#' + ctrId).find('#scrollLength').val();

    //        chatHub.server.getScrollingChatData(userEmail, email, loadMesgCount, count).done(function (msg) {
    //            for (i = 0; i < msg.length; i++) {
    //                var firstMsg = $('#' + ctrId).find('#divMessage').find('.message:first');

    //                // Where the page is currently:
    //                var curOffset = firstMsg.offset().top - $('#' + ctrId).find('#divMessage').scrollTop();

    //                // Prepend
    //                $('#' + ctrId).find('#divMessage').prepend('<div class="message"><span class="userName">' + msg[i].userName + '</span>: ' + msg[i].message + '</div>');

    //                // Offset to previous first message minus original offset/scroll
    //                $('#' + ctrId).find('#divMessage').scrollTop(firstMsg.offset().top - curOffset);
    //            }
    //        });
    //    }
    //});

    // DELETE BUTTON IMAGE
    //$div.find('#imgDelete').click(function () {
    //    $('#' + ctrId).remove();
    //});

    // Send Button event
    $div.find("#btnSendMessage").click(function () {
        $textBox = $div.find("#txtPrivateMessage");
        var msg = $textBox.val();
        if (msg.length > 0) {
            chatHub.server.sendPrivateMessage(userId, msg, 'Click');
            $textBox.val('');
        }
    });

    //// Text Box event
    //$div.find("#txtPrivateMessage").keyup(function (e) {
    //    if (e.which == 13) {
    //        $div.find("#btnSendMessage").click();
    //    }

    //    // Typing
    //    $textBox = $div.find("#txtPrivateMessage");
    //    var msg = $textBox.val();
    //    if (msg.length > 0) {
    //        chatHub.server.sendPrivateMessage(userId, msg, 'Type');
    //    }
    //    else {
    //        chatHub.server.sendPrivateMessage(userId, msg, 'Empty');
    //    }

    //    clearInterval(refreshId);
    //    checkTyping(chatHub, userId, msg, $div, 5000);
    //});

    // Text Box event
    $div.find(".chatbox__message").keyup(function (e) {
        if (e.which == 13) {
            $textBox = $div.find(".chatbox__message");
            var msg = $textBox.val();
            if (msg.length > 0) {
                chatHub.server.sendPrivateMessage(userId, msg, 'Click');
                $textBox.val('');
            }
        }

        //// Typing
        //$textBox = $div.find("#txtPrivateMessage");
        //var msg = $textBox.val();
        //if (msg.length > 0) {
        //    chatHub.server.sendPrivateMessage(userId, msg, 'Type');
        //}
        //else {
        //    chatHub.server.sendPrivateMessage(userId, msg, 'Empty');
        //}

        //clearInterval(refreshId);
        //checkTyping(chatHub, userId, msg, $div, 5000);
    });

    AddDivToContainer($div);
}

function checkTyping(chatHub, userId, msg, $div, time) {
    refreshId = setInterval(function () {
        // Typing
        $textBox = $div.find("#txtPrivateMessage");
        var msg = $textBox.val();
        if (msg.length == 0) {
            chatHub.server.sendPrivateMessage(userId, msg, 'Empty');
        }
    }, time);
}

function AddDivToContainer($div) {
    $('#divContainer').prepend($div);
    $div.draggable({
        handle: ".header",
        stop: function () {
        }
    });
}
// ------------------------------------------------------------------End Private Chat ----------------------------------------------------------------------//


$(".chatbox__message").keyup(function (e) {
    if (e.which == 13) {
        $textBox = $(".chatbox__message");
        var msg = $textBox.val();
        if (msg.length > 0) {
            chatHub.server.sendPrivateMessage(switchID, msg, 'Click');
            $textBox.val('');
        }
    }

    //// Typing
    //$textBox = $div.find("#txtPrivateMessage");
    //var msg = $textBox.val();
    //if (msg.length > 0) {
    //    chatHub.server.sendPrivateMessage(userId, msg, 'Type');
    //}
    //else {
    //    chatHub.server.sendPrivateMessage(userId, msg, 'Empty');
    //}

    //clearInterval(refreshId);
    //checkTyping(chatHub, userId, msg, $div, 5000);
});







var $chatbox = $('.chatbox'),
                   $chatboxTitle = $('.chatbox__title'),
                   $chatboxTitleClose = $('.chatbox__title__close'),
                   $chatboxCredentials = $('.chatbox__credentials');
$chatboxTitle.on('click', function () {
    $chatbox.toggleClass('chatbox--tray');
});
$chatboxTitleClose.on('click', function (e) {
    e.stopPropagation();
    $chatbox.addClass('chatbox--closed');
});
$chatbox.on('transitionend', function () {
    if ($chatbox.hasClass('chatbox--closed')) $chatbox.remove();
});
$chatboxCredentials.on('submit', function (e) {
    e.preventDefault();
    // lấy Email
    var email = $('#inputEmail').val();
    // gán và giữ email để quản lý 
    $('#hdEmailID').val(email);
    // bắt đầu kết nối với Server để tạo liên kết => để chat (chatHub.server.tenMethod là gọi các hàm ở Server )
    chatHub.server.connect(name, email);

    chatHub.client.onConnected = function (id, userName, allUsers, messages) {
        // gán các giá trị để quản lý
        //$('#hdId').val(id);
        //$('#hdUserName').val(userName);
        //$('#spanUser').html(userName);
        $('#hdId').val(id);
        console.log(id)

        // Add All Users ( quản lý danh sách tất cả những người đang online )
        //for (i = 0; i < allUsers.length; i++) {
        //    AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].EmailID);
        //}

        // Add Existing Messages ( hiện những tin đã được nhắn và lưu )
        //for (i = 0; i < messages.length; i++) {
        //    AddMessage(messages[i].UserName, messages[i].Message);
        //}

        // ẩn đăng nhập
        //$('.login').css('display', 'none');
        $chatbox.removeClass('chatbox--empty');
    }
    //chatHub.client.onConnected();
    //$chatbox.removeClass('chatbox--empty');
});
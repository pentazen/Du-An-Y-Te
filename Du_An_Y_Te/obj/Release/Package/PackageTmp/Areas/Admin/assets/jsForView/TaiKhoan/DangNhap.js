$("#loginform").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    var url = document.location.origin + "/Admin/TaiKhoan/DangNhap";
    $.ajax({
        method: "POST",
        url: url,
        data: {
            __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val(),
            TaiKhoan: $("#user_login").val(),
            MatKhau: $("#user_pass").val()
        },
        success: function (data) {
        },
        error: function (xhr) {
            console.log(xhr);
            return false;
        }
    })
       .done(function (data) {
           if (data == true || data == "True") {
               //sweetAlert("Success", "Complete Create New Parameter Interval", "success");
               swal({
                   title: "Success",
                   text: "Welcome To Admin Page",
                   type: "success",
                   showCancelButton: false,
                   closeOnConfirm: true,
               }, function () {
                   window.location = document.location.origin + "/Admin";
               })

           }
           else {
               sweetAlert("Error", "Login Fail\nPlease check your account or contact with Admin", "error");
           }

       });
    //sweetAlert("Success", "Complete Create New Account", "success");
});
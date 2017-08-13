var table;
var tableThemLoaiXetNghiem;
$(document).ready(function () {
    table = $("#tableLoaiXetNghiem").DataTable({
        responsive: true,
        aLengthMenu: [
            [10, 25, 50, 100, -1],
            [10, 25, 50, 100, "All"]
        ]
    });
    var duongDanTHemLoaiXetNghiem = document.location.origin + "/Admin/MauBenhAn/LoaiXetNghiem";
    tableThemLoaiXetNghiem = $('#tableThemLoaiXetNghiem').DataTable({
        "ajax": {
            "url": duongDanTHemLoaiXetNghiem,
            "type": "GET"
        },
        "columns": [
            { "data": "id" },
            { "data": "TenLoaiXetNghiem" },
            { "data": "TenHienThi" },
            { "data": "DonVi" }
        ],
        "lengthMenu": [[5, 10, 20], [5, 10, 20]]
    });

    $('#tableThemLoaiXetNghiem tbody').on('click', 'tr', function () {
        //$(this).toggleClass('active');
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            tableThemLoaiXetNghiem.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    //$('#tableLoaiXetNghiem').on('click', 'tbody tr', function () {
    //    table.row(this).delete();
    //});
    $('#tableLoaiXetNghiem').on('click', 'tbody tr .buttons-remove', function (event) {
        console.log($(this).parent().parent()[0])
        var rowFind = $(this).parent().parent()[0];
        var tableData = $(rowFind).children("td").map(function () {
            return $(this).text();
        }).get();

        var mauBenhAn_LXN = {
            id_MauBenhAn: parseInt($("#MauBenhAn_Id").val()),
            id_LoaiXetNghiem: parseInt(tableData[0])
        }
        var FormSend = new FormData();
        FormSend.append("mauBenhAn_LXN", JSON.stringify(mauBenhAn_LXN));
        var url = document.location.origin + "/Admin/MauBenhAn/RemoveLoaiXetNghiem";
        $.ajax({
            method: "POST",
            url: url,
            data: FormSend,
            contentType: false,
            processData: false,
            success: function (data) {
            },
            error: function (xhr) {
                console.log(xhr);
                return false;
            }
        })
        .done(function (data) {
            console.log(data)
            if (data == "True") {
                table.row($(rowFind)).remove().draw(false);
            }
        });







    });
    $('#tableLoaiXetNghiem').on('click', 'tbody tr .buttons-edit', function (event) {
        console.log($(this).parent().parent()[0])
        var rowFind = $(this).parent().parent()[0];
        var tableData = $(rowFind).children("td").map(function () {
            return $(this).text();
        }).get();

        console.log(tableData);
        $("#SuaThongTinLoaiXetNghiem_Id").val(tableData[0]);
        $("#SuaThongTinLoaiXetNghiem_Label").html(tableData[1]);
        $("#SuaThongTinLoaiXetNghiem_NameShow").val(tableData[2]);
        $("#SuaThongTinLoaiXetNghiem_Unit").val(tableData[3]);
        $("#SuaThongTinLoaiXetNghiem_Location").val(tableData[4]);
        $("#SuaThongTinLoaiXetNghiem").modal("show")
    });

});
function luuLoaiXetNghiem() {
    var FormSend = new FormData();
    //FormSend.append("ThanhVien", $('form').serialize());
    var url = document.location.origin + "/Admin/MauBenhAn/Edit";
    var mauBenhAn_LXN = {
        id_MauBenhAn: parseInt($("#MauBenhAn_Id").val()),
        id_LoaiXetNghiem: parseInt($("#SuaThongTinLoaiXetNghiem_Id").val()),
        ThuTu: parseInt($("#SuaThongTinLoaiXetNghiem_Location").val())
    }
    console.log(mauBenhAn_LXN)
    FormSend.append("mauBenhAn_LXN", JSON.stringify(mauBenhAn_LXN));
    $.ajax({
        method: "POST",
        url: url,
        data: FormSend,
        contentType: false,
        processData: false,
        success: function (data) {
        },
        error: function (xhr) {
            console.log(xhr);
            return false;
        }
    })
   .done(function (data) {

       if (data) {
           $("#SuaThongTinLoaiXetNghiem").modal("hide");
           var dongLoaiXetNghiem = $("#loaiXetNghiem_" + mauBenhAn_LXN.id_LoaiXetNghiem)[0];
           console.log($(dongLoaiXetNghiem).children("td")[4])
           $($(dongLoaiXetNghiem).children("td")[4]).html(mauBenhAn_LXN.ThuTu)
       }
       else {
           swal("Error", "Update Fail", "error")
       }
   });
}

function themLoaiXetNghiem() {
    var FormSend = new FormData();
    //FormSend.append("ThanhVien", $('form').serialize());
    var url = document.location.origin + "/Admin/MauBenhAn/ThemLoaiXetNghiemVaoMauBenhAn";
    //tableThemLoaiXetNghiem.rows('.selected').remove().draw(false);
    var dataLoaiXetNghiem = tableThemLoaiXetNghiem.rows('.selected').data()[0];
    console.log(dataLoaiXetNghiem)
    var mauBenhAn_LXN = {
        id_MauBenhAn: parseInt($("#MauBenhAn_Id").val()),
        id_LoaiXetNghiem: dataLoaiXetNghiem.id
    }
    // console.log(mauBenhAn_LXN)
    FormSend.append("mauBenhAn_LXN", JSON.stringify(mauBenhAn_LXN));
    $.ajax({
        method: "POST",
        url: url,
        data: FormSend,
        contentType: false,
        processData: false,
        success: function (data) {
        },
        error: function (xhr) {
            console.log(xhr);
            return false;
        }
    })
   .done(function (data) {
       console.log(table.rows())
       if (data == "True") {
           tableThemLoaiXetNghiem.$('tr.selected').removeClass('selected');
           table.row.add([
                   dataLoaiXetNghiem.id,
                   dataLoaiXetNghiem.TenLoaiXetNghiem,
                   dataLoaiXetNghiem.TenHienThi,
                   dataLoaiXetNghiem.DonVi,
                   table.rows().data().length+1,
                   '<div class="buttons-remove"><a class="btn btn-warning">Remove</a></div><div class="buttons-edit"><a class="btn btn-info">Edit</a></div>'
           ]).draw(false);
       }
       else {
           swal("Error", "Update Fail", "error")
       }
   });
}
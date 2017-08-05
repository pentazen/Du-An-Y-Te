$("#TaoTaiKhoan").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    var ThanhVien = {
        "id": $('#idThanhVien').val(),
        "id_LoaiThanhVien": $('#id_LoaiThanhVien').val(),
        "Ho": $('#Ho').val(),
        "Ten": $('#Ten').val(),
        "TaiKhoan": $('#TaiKhoan').val(),
        "MatKhau": $('#MatKhau').val(),
        "Email": $('#Email').val(),
        "CMND": $('#CMND').val(),
        "SoDienThoai": $('#SoDienThoai').val(),
        "DiaChi": $('#DiaChi').val(),
        "ChieuCao": $('#ChieuCao').val(),
        "CanNang": $('#CanNang').val(),
        "MaNhomMau": $('#MaNhomMau').val(),
    }
    var url = document.location.origin + "/Admin/TaiKhoan/Edit";
    var FormSend = new FormData();
    //FormSend.append("ThanhVien", $('form').serialize());


    FormSend.append("ThanhVien", JSON.stringify(ThanhVien));
    FormSend.append("__RequestVerificationToken", $("input[name=__RequestVerificationToken]").val());

    jQuery.each(jQuery('#Picture')[0].files, function (i, file) {
        FormSend.append('file-' + i, file);
    });
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

           var result = JSON.parse(data);
           console.log(result)
           if (result.KetQua == true || result.KetQua == "True") {
               sweetAlert("Success", "Complete Update This Account", "success");
           }
           else {
               sweetAlert("Error", result.Message, "error");
           }

       });

});



function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#PrevImg').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#Picture").change(function () {
    readURL(this);
});
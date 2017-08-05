$("#SuaBacSi").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    var ThanhVien = {
        "id_LoaiBacSi": $('#id_LoaiBacSi').val(),
        "id": $('#id').val(),
        "HoTen": $('#HoTen').val(),
        "SoDienThoaiLienLac": $('#SoDienThoaiLienLac').val(),
        "id_BenhVienLamViec": $('#id_BenhVienLamViec').val(),
        "DiaChiPhongKhamTu": $('#DiaChiPhongKhamTu').val(),
    }
    var url = document.location.origin + "/Admin/BacSi/Edit";
    var FormSend = new FormData();
    //FormSend.append("ThanhVien", $('form').serialize());


    FormSend.append("BacSi", JSON.stringify(ThanhVien));
    FormSend.append("TaiKhoan", $('#TaiKhoan').val());
    FormSend.append("__RequestVerificationToken", $("input[name=__RequestVerificationToken]").val());

    //jQuery.each(jQuery('#Picture')[0].files, function (i, file) {
    //    FormSend.append('file-' + i, file);
    //});
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
               sweetAlert("Success", "Update complete", "success");
           }
           else {
               sweetAlert("Error", result.Message, "error");
               return false;
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
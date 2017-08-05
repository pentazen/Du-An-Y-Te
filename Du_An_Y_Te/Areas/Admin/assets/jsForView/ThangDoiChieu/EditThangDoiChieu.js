$("#TaoThangDoiChieu").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();
    var LoaiXetNghiem = {
        "TenLoaiXetNghiem": $('#TenLoaiXetNghiem').val(),
        "TenKhac": $('#TenKhac').val(),
        "GhiChu": $('#GhiChu').val(),
    }
    var ThangDoiChieu = {
        "id": $('#id').val(),
        "CanTren": $('#CanTren').val(),
        "CanDuoi": $('#CanDuoi').val(),
        "DonVi": $('#DonVi').val(),
        "LoiKhuyenVuotQuaCanTren": $('#LoiKhuyenVuotQuaCanTren').val(),
        "ChuanDoanBenhKhiVuotQuaCanTren": $('#ChuanDoanBenhKhiVuotQuaCanTren').val(),
        "LoiKhuyenVuotQuaCanDuoi": $('#LoiKhuyenVuotQuaCanDuoi').val(),
        "ChuanDoanBenhKhiVuotQuaCanDuoi": $('#ChuanDoanBenhKhiVuotQuaCanDuoi').val(),
    }
    var url = document.location.origin + "/Admin/ThangDoiChieu/Edit";
    var FormSend = new FormData();
    //FormSend.append("ThanhVien", $('form').serialize());


    FormSend.append("LoaiXetNghiem", JSON.stringify(LoaiXetNghiem));
    FormSend.append("ThangDoiChieu", JSON.stringify(ThangDoiChieu));
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
               //sweetAlert("Success", "Complete Create New Parameter Interval", "success");
               swal({
                   title: "Success",
                   text: "Parameter Interval Has Been Save",
                   type: "success",
                   showCancelButton: false,
                   closeOnConfirm: true,
               }, function () {
                   window.location = document.location.origin + "/Admin/ThangDoiChieu";
               })

           }
           else {
               sweetAlert("Error", "The Name Has Been Duplication", "error");
           }

       });
    //sweetAlert("Success", "Complete Create New Account", "success");
});
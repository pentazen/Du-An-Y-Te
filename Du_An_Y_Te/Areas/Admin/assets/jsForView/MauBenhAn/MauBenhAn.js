var table = $("#example-1").DataTable({
    responsive: true,
    aLengthMenu: [
        [10, 25, 50, 100, -1],
        [10, 25, 50, 100, "All"]
    ]
});
$(document).ready(function () {
    $(".TrangThai").click(function (event) {
        //console.log(event);
        //console.log($("#" + event.target.id).val());
        //console.log($("#" + event.target.id).is(':checked'));
        var trangThai = $("#" + event.target.id).is(':checked');
        var idMauBenhAn = event.target.id.split("_");
        idMauBenhAn = idMauBenhAn[idMauBenhAn.length - 1];
        console.log(idMauBenhAn);
        var url = document.location.origin + "/Admin/MauBenhAn/CapNhatTrangThai";

        $.ajax({
            method: "POST",
            url: url,
            data: { id: idMauBenhAn, TrangThai: trangThai, __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val() },
            success: function (data) {
            },
            error: function (xhr) {
                console.log(xhr);
                return false;
            }
        })
      .done(function (data) {
          console.log(data)
      });
    });
});
function TaoMauBenhAn() {
    swal({
        title: "Name",
        text: "",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Huyết Học"
    },
        function (inputValue) {
            if (inputValue === false) return false;

            if (inputValue === "") {
                swal.showInputError("You need to write something!");
                return false
            }
            var url = document.location.origin + "/Admin/MauBenhAn/Create";
            $.ajax({
                method: "POST",
                url: url,
                data: { TenMauBenhAn: inputValue, __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val() },
                success: function (data) {
                },
                error: function (xhr) {
                    console.log(xhr);
                    return false;
                }
            })
        .done(function (data) {
            console.log(data)
        });
            swal("Nice!", "You wrote: " + inputValue, "success");
        });
}
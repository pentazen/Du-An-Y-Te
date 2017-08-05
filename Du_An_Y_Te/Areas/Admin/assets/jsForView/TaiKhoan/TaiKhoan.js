var table = $("#example-1").DataTable({
    responsive: true,
    aLengthMenu: [
        [10, 25, 50, 100, -1],
        [10, 25, 50, 100, "All"]
    ]
});
$('#example-1 tbody').on('click', 'tr', function () {
    //if ($(this).hasClass('selected')) {
    //    $(this).removeClass('selected');
    //}
    //else {
    //    table.$('tr.selected').removeClass('selected');
    //    $(this).addClass('selected');
    //}
    //var thisrow = $(this);
    //thisrow.addClass('selected');
    //swal({
    //    title: "Are you sure?",
    //    text: "This Account ( " + "" + " ) Will Delete",
    //    type: "warning",
    //    showCancelButton: true,
    //    confirmButtonColor: "#DD6B55",
    //    confirmButtonText: "Yes",
    //    cancelButtonText: "No",
    //    closeOnConfirm: false,
    //    closeOnCancel: false
    //},
    // function (isConfirm) {
    //     if (isConfirm) {
    //         //$('tr[data-id-thanhvien=' + id + ']').className += 'selected';
    //         table.row('.selected').remove().draw(true);
    //         swal("Deleted!", "Account " + "" + " has been remove!", "success");
    //     } else {
    //         thisrow.removeClass('selected');
    //         swal("Cancelled", "Your imaginary file is safe :)", "error");
    //     }
    // });
});
function DeleteTaiKhoan(taikhoan, id, them) {
    var thisrow = $(this);
    console.log($(them));
    $(them).parent().parent().parent().addClass('selected');
    swal({
        title: "Are you sure?",
        text: "This Account ( " + taikhoan + " ) Will Delete",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: true
    },
     function (isConfirm) {
         if (isConfirm) {
             var url = document.location.origin + "/Admin/TaiKhoan/XoaThanhVien";
             $.ajax({
                 method: "POST",
                 url: url,
                 data: { id: id, __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val() },
                 success: function (data) {
                 },
                 error: function (xhr) {
                     console.log(xhr);
                     return false;
                 }
             })
           .done(function (data) {
               console.log(data)
               if (data == true || data == "True") {
                   swal("Deleted!", "Account " + taikhoan + " has been remove!", "success");
                   table.row('.selected').remove().draw(true);
               }
               else {
                   return false;
               }

           });
             
             
         } else {
             $(them).parent().parent().parent().removeClass('selected');
         }
     });
}
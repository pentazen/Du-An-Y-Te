var table = $("#example-1").DataTable({
    responsive: true,
    aLengthMenu: [
        [10, 25, 50, 100, -1],
        [10, 25, 50, 100, "All"]
    ],
    "order": [[4, "desc"]]
});
function DeleteBaiViet(taikhoan, id, them) {
    var thisrow = $(this);
    console.log($(them));
    $(them).parent().parent().parent().addClass('selected');
    swal({
        title: "Are you sure?",
        text: "This Message of this Member ( " + taikhoan + " ) Will Delete",
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
             var url = document.location.origin + "/Admin/TinNhanPhongChat/DeleteById";
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
                   swal("Deleted!", "Message of member " + taikhoan + " has been remove!", "success");
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
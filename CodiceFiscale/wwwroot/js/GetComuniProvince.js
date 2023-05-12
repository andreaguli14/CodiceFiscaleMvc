 $(document).ready(function () {
        id = "";
         $("#comuni").attr("disabled", true);
    });


$("#province").change(function () {
    var id = $(this).val();
        loadComuni(id);
    });



  

function loadComuni(id) {
    $("#comuni").empty();
    $("#comuni").attr("disabled", false);

    $.ajax({
        url: '/cf/GetComuni/' + id,
        success: function (response) {
            $("#comuni").append("<option default disabled value=''>Comune</option>");
            $.each(response, function (i, data) {
                $("#comuni").append("<option value='"+data.comune+"' >" + data.comune + "</option>");
            })


        }
    })
}
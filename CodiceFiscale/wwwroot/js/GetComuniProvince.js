 $(document).ready(function () {
        id = "";
         loadProvince()
         $("#comuni").attr("disabled", true);
    });


    $("#province").change(function () {
        id = "";
        id = $(this).val().slice(0, 2);
        loadComuni(id);
    });


    $("#comuni").change(function () {
        id = "#" + $(this).val().slice(0, 2);
        loadProvince(id);
    });


    function loadProvince(id) {
        $("#province").empty();
        $.ajax({
            url: '/cf/GetProvince/',
            success: function (response) {
                $("#province").append("<option  disabled value=''>PR</option>");
                $.each(response, function (i, data) {
                    $("#province").append("<option id='" + data.sigla + "'value='" + data.sigla + "' >" + data.sigla + "</option>");
                });
                $(id).attr("selected", true);
                




            }
        })
    }

function loadComuni(id) {
    $("#comuni").empty();
    $("#comuni").attr("disabled", false);

    $.ajax({
        url: '/cf/GetComuni/' + id,
        success: function (response) {
            $("#comuni").append("<option default disabled value=''>Comune</option>");
            $.each(response, function (i, data) {
                $("#comuni").append("<option value='" + data.sigla + data.comune + "' >" + data.comune + "</option>");
            })


        }
    })
}
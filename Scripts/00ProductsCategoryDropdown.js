$(document).ready(function () {
    $("#IdCategoria").on("change", function () {
        var IdCategoria = $(this).val();
        $.ajax({
            url: "/Products/GetSubCatByCat",
            data: { IdCategoria: IdCategoria },
            type: "GET",
            success: function (data) {
                var IdSubCategoria = $("#IdSubCategoria");
                IdSubCategoria.empty();
                IdSubCategoria.append($("<option></option>")
                    .attr("value", "0")
                    .text("-- Seleziona sottocategoria --"));

                $.each(data, function (index, item) {
                    console.log("data: ", item.id)
                    IdSubCategoria.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.description));
                });
            },
            error: function (xhr, status, error) {
                console.log("Errore: " + xhr + " + " + status + " + " + error)
            }
        });
    });
});
$(document).ready(function () {
    $(".details").click(function () {
        var id = $(this).attr("data-id");
        console.log(id);
        $("#modal").load('/Despesas/Details?id=' + id, function () {
            $("#modal").modal();
        })
    });

    $(".edit").click(function () {
        var id = $(this).attr("data-id");
        console.log(id);
        $("#modal").load('/Despesas/Edit?id=' + id, function () {
            $("#modal").modal();
        })
    });

    $(".delete").click(function () {
        var id = $(this).attr("data-id");
        console.log(id);
        $("#modal").load('/Despesas/Delete?id=' + id, function () {
            $("#modal").modal();
        })
    });

    $(".detailsRendimentos").click(function () {
        var id = $(this).attr("data-id");
        console.log(id);
        $("#modal").load('/Rendimentos/Details?id=' + id, function () {
            $("#modal").modal();
        })
    });

    $(".editRendimentos").click(function () {
        var id = $(this).attr("data-id");
        console.log(id);
        $("#modal").load('/Rendimentos/Edit?id=' + id, function () {
            $("#modal").modal();
        })
    });

    $(".deleteRendimentos").click(function () {
        var id = $(this).attr("data-id");
        console.log(id);
        $("#modal").load('/Rendimentos/Delete?id=' + id, function () {
            $("#modal").modal();
        })
    });


    $(".Register").click(function () {
        $("#modal").load('/Account/Register', function () {
            $("#modal").modal();
        })
    });

    $(".Login").click(function () {
        $("#modal").load('/Account/Login', function () {
            $("#modal").modal();
        })
    });
})
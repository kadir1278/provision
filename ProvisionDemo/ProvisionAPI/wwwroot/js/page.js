$(document).on("click", ("#searchButton"), function () {
    var text = document.getElementById("srcText").value;
    $("#graph").html('');
    $.ajax({
        url: "/api/LastTwoMonthDataDb/list/" + text,
        type: "GET",
        success: function (data) {
            DrawGraphic(data);
        }
    })
});
$(document).on("click", ("#dataUpdate"), function () {
    var text = document.getElementById("srcText").value;
    $("#graph").html('');
    var error = document.getElementById("customMessage");
    error.hidden = false;
    error.innerHTML = "Data is updating ";
    $.ajax({
        url: "/api/LastTwoMonthDataDb/add/" + text,
        type: "POST",
        success: function () {
            error.hidden = true;
            error.innerHTML = "";
            document.getElementById("searchButton").click();
        }
    })
});

$(document).on("click", ("#codeUpdate"), function () {
    var error = document.getElementById("customMessage");
    error.hidden = false;
    error.innerHTML = "Data is updating ";
    $.ajax({
        url: "/api/CurrencyCodeDb/add/",
        type: "POST",
        success: function () {
            location.reload();
        }
    })
});


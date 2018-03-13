$.ajaxSetup({
    async: false
});
$("#delete").click(function (e) {
    var href = $(this).attr("href");
    $.ajax({
        url: href,
        cache: false,
        success: function (html) {
            $("#content").html(html);
        }
    });
});
$("#DeleteProducts").click(function (e) {
    var chk = $(".id").length;
    var asa = 0;
    var text;
    for (var t = 0; t < chk; t++) {
        text = $($(".id")[t]).prop("checked");
        if (text === false) {
            asa++;
        }
    }
    if (asa === chk) {
        alert("Не выбрано ни одного элемента");
    }
    else {
        $("#sendForm").click();
    }
});
$("#sendForm").click(function () {
    $('#form0').submit();
});
var checked = $("#allCheked");
checked.checked = false;
$("#allCheked").click(function (e) {
    if (checked.checked === false) {
        checkedAll = $(".id").prop("checked", true);
        checked.checked = true;
    }
    else {
        checkedAll = $(".id").prop("checked", false);
        checked.checked = false;
    }
});



function submitSearch() {
    $(".search").keyup(function () {
        if (event.keyCode === 13) {
            $.get({
                url: '/product',
                cache: false,
                data: { 'term': $(this).val() },
                success: function (html) {
                    $("#dynamic-content").html(html);
                }
            });
        }
        else {
            $(".search").each(function () {
                var target = $(this);
                target.autocomplete({ source: target.attr("data-autocomplete-source") });
            });
        }

    });
}

$("#LogOff").click(function (e) {
    e.preventDefault();
    $('#Exit').submit();
});

$('#CategoryId option:first-child').remove();
$('#ParentId option:first-child').val('');

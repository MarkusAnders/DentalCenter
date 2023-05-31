// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Выпадающее меню (Личный кабинет)

$(".phone-input").mask("+7(999) 999-99-99");

$('.btn-send').click(function () {
    $('.popup-bg').fadeIn(600);
    $('html').addClass('no-scroll');

});
$('.popup__close').click(function () {
    $('.popup-bg').fadeOut(200);
    $('html').removeClass('no-scroll');
});

$(function () {
    $("#selectAll").click(function () {
        $("input[type=checkbox]").prop('checked', $(this).prop('checked'));
    });
    $("input[type=checkbox]").click(function () {
        if (!$(this).prop("checked")) {
            $("#selectAll").prop("checked", false);
        }
    });
})

$(document).ready(function () {
    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[1].classList.add("active");
});
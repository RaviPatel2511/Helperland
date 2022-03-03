$(document).ready(function () {
    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[0].classList.add("active");
});
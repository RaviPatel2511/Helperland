$(document).ready(function () {
    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[5].classList.add("active");
});
$(document).ready(function () {
    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[6].classList.add("active");
});
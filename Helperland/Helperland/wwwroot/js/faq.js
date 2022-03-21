const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn=document.querySelector('.menubtn');
const nav = document.querySelector("nav");


menubtn.addEventListener('click',()=>{
    Sidenav.classList.add('open');
})
closebtn.addEventListener('click',()=>{
    Sidenav.classList.remove('open')
})

window.onclick = function(event) {
    if (event.target == Sidenav) {
        Sidenav.classList.remove('open')
    }
}

function changeImage(x) {
    var id = document.getElementById(x);
    if($(id).parent().hasClass('collapsed')) {
        $(id).css({
            'transform': 'rotate(0deg)',
            'transition': 'transform 0.4s'
        });
    } else {
        $(id).css({
            'transform': 'rotate(90deg)',
            'transition': 'transform 0.4s'
        });
    }
};

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn=document.querySelector('.menubtn');
const nav = document.querySelector("nav");
const loginbtn = document.querySelector('.loginbtn');

window.addEventListener("scroll", () => {
    if (window.scrollY > 130) {
        nav.classList.add("fixed-top");
        mobileNav.classList.add("fixed-top");
        nav.css("background-color", "yellow");
    } else {
        nav.classList.remove("fixed-top");
        mobileNav.classList.remove("fixed-top");
    }
});

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


loginbtn.addEventListener("click", ()=>{
	document.cookie = "LoginModalOpen=true";
	document.location.href = "index.html";
})
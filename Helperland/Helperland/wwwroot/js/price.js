const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn=document.querySelector('.menubtn');
const nav = document.querySelector("nav");
const up_arr = document.querySelector(".up_arr");


window.addEventListener("scroll", () => {
	if (window.scrollY > 130) {
	
		nav.classList.add("fixed-top");
	
		up_arr.classList.remove("hidden");
		mobileNav.classList.add("fixed-top");
	} else {
	
		nav.classList.remove("fixed-top");
		up_arr.classList.add("hidden");
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

$(document).ready(function () {
	$('[data-toggle="popover"]').popover();
});

// PREVENT FFROM BACK BUTTON AFTER LOGOUT
window.history.forward();
function noBack() {
	window.history.forward();
}
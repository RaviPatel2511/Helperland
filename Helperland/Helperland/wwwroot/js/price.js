const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn=document.querySelector('.menubtn');
const nav = document.querySelector("nav");
const up_arr = document.querySelector(".up_arr");


window.addEventListener("scroll", () => {
	if (window.scrollY > 130) {	
		up_arr.classList.remove("hidden");
	} else {
		up_arr.classList.add("hidden");
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

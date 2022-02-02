
const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn=document.querySelector('.menubtn');
const nav = document.querySelector("nav");

jQuery('.dropdown-menu li a').click(function() {
    var _this_img = jQuery(this).attr('data-img');
    jQuery(this).closest('.btn-group').find(' .dropdown-toggle img').attr('src', _this_img);
})


window.addEventListener("scroll", () => {
	if (window.scrollY > 130) {
		nav.classList.add("smalllogo");
		nav.classList.add("fixed-top");
		mobileNav.classList.add("fixed-top");
	} else {
		nav.classList.remove("fixed-top");
		nav.classList.remove("smalllogo");
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



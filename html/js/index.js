const nav = document.querySelector("nav");
const up_arr = document.querySelector(".up_arr");
const menubtn=document.querySelector('.menubtn');
const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const cookie = document.querySelector('.frow2');

window.addEventListener("scroll", () => {
	if (window.scrollY > 730) {
		nav.classList.add("bg-grey");
		nav.classList.add("fixed-top");
		nav.classList.add("smalllogo");
        up_arr.classList.remove("hidden");
        mobileNav.classList.add("bg-grey");
		mobileNav.classList.add("fixed-top");
		cookie.classList.remove('hidden');

	} else {
        nav.classList.remove("bg-grey");
		up_arr.classList.add("hidden");
		nav.classList.remove("fixed-top");
		nav.classList.remove("smalllogo");
        mobileNav.classList.remove("bg-grey");
		mobileNav.classList.remove("fixed-top");
		cookie.classList.add('hidden');
	}

});

$("#okbtn").click(function() {
	$(".frow2").css("display", "none");
});
jQuery('.dropdown-menu li a').click(function() {
	var _this_img = jQuery(this).attr('data-img');
	jQuery(this).closest('.btn-group').find(' .dropdown-toggle img').attr('src', _this_img);
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



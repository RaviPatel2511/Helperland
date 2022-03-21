const nav = document.querySelector("nav");
const up_arr = document.querySelector(".up_arr");
const menubtn=document.querySelector('.menubtn');
const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const cookierow = document.querySelector('.frow2');
const loginbtn = document.querySelector('.loginbtn');

window.addEventListener("scroll", () => {
	if (window.scrollY > 730) {
		nav.classList.add("bg-grey");
		nav.classList.add("fixed-top");
		nav.classList.add("smalllogo");
        mobileNav.classList.add("bg-grey");
		mobileNav.classList.add("fixed-top");
        up_arr.classList.remove("hidden");
	}
	else if(window.scrollY > 300){
		cookierow.classList.remove('hidden');
	}
	
	else {
        nav.classList.remove("bg-grey");
		up_arr.classList.add("hidden");
		nav.classList.remove("fixed-top");
		nav.classList.remove("smalllogo");
        mobileNav.classList.remove("bg-grey");
		mobileNav.classList.remove("fixed-top");
		cookierow.classList.add('hidden');
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
up_arr.addEventListener('click', () => {
	window.scrollTo(0, 0);
})


window.onclick = function(event) {
	if (event.target == Sidenav) {
		Sidenav.classList.remove('open')
  }
}



$('#forgotPassLink').click(function(){
	$("#userloginModal").modal('hide');
})

AOS.init({
	once : true,
});


const urlSearchParams = new URLSearchParams(window.location.search);
if (urlSearchParams == "loginModal=true") {
	$("#userloginModal").modal('show');
};



$(".loginfield").keyup(function () {
	if ($('#user-email').val() && $('#user-password').val() != null) {
		$('#loginbtn').prop('disabled', false);
		$('#loginbtn').css('cursor', 'pointer');
	} else {
		$('#loginbtn').prop('disabled', true);
		$('#loginbtn').css('cursor', 'not-allowed');
	}
});
$("#passReseat-email").keyup(function () {
	if ($('#passReseat-email').val() != null) {
		$('#pass-mail-button').prop('disabled', false);
		$('#pass-mail-button').css('cursor', 'pointer');
	} else {
		$('#pass-mail-button').prop('disabled', true);
		$('#pass-mail-button').css('cursor', 'not-allowed');
	}
});

$(document).ready(function () {
	$('[data-toggle="popover"]').popover();
});


const urlSearchParamsforlogout = new URLSearchParams(window.location.search);
if (urlSearchParamsforlogout == "logoutModal=true") {
	$('#logoutSuccessfully').modal({
		backdrop: 'static', // to prevent closing with click
		keyboard: false  // to prevent closing with 
	});
	$("#logoutSuccessfullyBtn").click();
};

const urlSearchParamsforpass = new URLSearchParams(window.location.search);
if (urlSearchParamsforpass == "forgotPass=true") {
	$("#forgotPass").modal('show');
};

const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn=document.querySelector('.menubtn');
const nav = document.querySelector("nav");


window.addEventListener("scroll", () => {
	if (window.scrollY > 130) {
	
		nav.classList.add("fixed-top");

		mobileNav.classList.add("fixed-top");
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


$(document).ready(function () {
	$('#upload-file').on("change", function () {
		var filename = $(this).val().split("\\").pop();
		$(this).next('#uploadFilePath').html(filename);
	});
});

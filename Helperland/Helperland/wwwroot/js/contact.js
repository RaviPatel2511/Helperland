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


$(document).ready(function () {
	$('#upload-file').on("change", function () {
		var filename = $(this).val().split("\\").pop();
		$(this).next('#uploadFilePath').html(filename);
	});
});


// PREVENT FFROM BACK BUTTON AFTER LOGOUT
//window.history.forward();
//function noBack() {
//	window.history.forward();
//}
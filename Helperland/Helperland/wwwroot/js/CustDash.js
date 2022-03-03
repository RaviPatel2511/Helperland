
const menubtn=document.querySelector('.menubtn');
const openSidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const nav = document.querySelector("nav");

var $links = $('#sidebar-wrapper a');
$links.click(function(){
   $links.removeClass('active');
   $(this).addClass('active');
});
var $links2 = $('.sideNav a');
$links2.click(function(){
   $links2.removeClass('current');
   $(this).addClass('current');
});


menubtn.addEventListener('click',()=>{
    openSidenav.classList.add('open');
})
closebtn.addEventListener('click',()=>{
    openSidenav.classList.remove('open')
})
window.onclick = function(event) {
	if (event.target == openSidenav) {
		openSidenav.classList.remove('open')
  }
}



$(document).ready(function () {
	$('[data-toggle="popover"]').popover();

});

// PREVENT FFROM BACK BUTTON AFTER LOGOUT
//window.history.forward();
//function noBack() {
//    window.history.forward();
//}


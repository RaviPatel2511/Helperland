
const menubtn=document.querySelector('.menubtn');
const Sidenav=document.querySelector('.sideNav');
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


// PREVENT FFROM BACK BUTTON AFTER LOGOUT
//window.history.forward();
//function noBack() {
//    window.history.forward();
//}
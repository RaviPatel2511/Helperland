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


$("#confirmPass").keyup(function () {
    if ($('#pass').val() != $('#confirmPass').val()) {
        $('#confirmPassError').html('Password doesnot match');
        $('#register').prop('disabled', true);
        $('#register').css('cursor', 'not-allowed');
    } else {
        $('#confirmPassError').html('');
        $('#register').prop('disabled', false);
        $('#register').css('cursor', 'pointer');
    }
});




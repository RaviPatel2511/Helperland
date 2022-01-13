const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn=document.querySelector('.menubtn');
const nav = document.querySelector("nav");
const loginbtn = document.querySelector('.loginbtn');

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

loginbtn.addEventListener("click", ()=>{
	document.cookie = "LoginModalOpen=true";
	document.location.href = "index.html";
})



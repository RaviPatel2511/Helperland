
const menubtn=document.querySelector('.menubtn');
const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const nav = document.querySelector("nav");

$(document).ready( function () {
  $('#upcomingService').DataTable({
      "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
      "pagingType": "full_numbers",
      "searching":false,
      "order":[],
      'columnDefs': [ {
          'targets': [4], 
          'orderable': false, 
       }],
      "language": {
          "paginate": {
            "first": '<img src="./image/upcoming_service/step-backward-solid.svg"></img>',
            "next": '<img src="./image/upcoming_service/angle-right-solid.svg"></img>',
          "previous": '<img src="./image/upcoming_service/angle-left-solid.svg"></img>',
          "last":'<img src="./image/upcoming_service/step-forward-solid.svg"></img>'
          },
          'info': "Total Record: _MAX_",
          
      }
  });
} );

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

$('table').on('click', 'input[type="button"]', function(e){
   $(this).closest('tr').remove()
   alert('data deleted sucessfully;')
})


window.addEventListener("scroll", () => {
  if (window.scrollY > 130) {
  
    nav.classList.add("fixed-top");

  } else {
    
    nav.classList.remove("fixed-top");

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

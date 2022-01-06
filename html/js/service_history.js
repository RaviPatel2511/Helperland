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
$(document).ready( function () {
    $('#service_history_table').DataTable({
        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
        "pagingType": "full_numbers",
        "searching":false,
        'columnDefs': [ {
            'targets': [4], 
            'orderable': false, 
         }],
        "language": {
            "paginate": {
                "first": '<i class="fas fa-step-backward"></i>',
                "next": '<i class="fas fa-angle-right"></i>',
              "previous": '<i class="fas fa-angle-left"></i>',
              "last":'<i class="fas fa-step-forward"></i>'
            },
            'info': "Total Record: _MAX_",
            
        }
    });
} );



const menubtn=document.querySelector('.menubtn');
const openSidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');

menubtn.addEventListener('click',()=>{
    openSidenav.classList.add('open');
})
closebtn.addEventListener('click',()=>{
    openSidenav.classList.remove('open')
})
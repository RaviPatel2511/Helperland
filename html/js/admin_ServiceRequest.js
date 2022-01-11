$(document).ready( function () {
    $('#serviceRequesttable').DataTable({
        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
        
        "pagingType": "full_numbers",
        "searching":false,
        "order": [],
        "info": false,
        'columnDefs': [ {
            'targets': [5], 
            'orderable': false, 
         }],
         
        "language": {
            "paginate": {
                "first" : false,
                "next": '<i class="fas fa-angle-right"></i>',
                "previous": '<i class="fas fa-angle-left"></i>',
                "last" : false,
              
            },
            
            
        }
    });
} );

$(document).ready(function(){
    var date_input=$('input[name="date"]'); 
    var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
    date_input.datepicker({
        format: 'mm/dd/yyyy',
        container: container,
        todayHighlight: true,
        autoclose: true,
    })
})

const mobileNavIcon = document.querySelector('.mobileNavIcon');
const sidebar_wrapper = document.querySelector('#sidebar-wrapper');
const closebtn = document.querySelector('.closebtn');

mobileNavIcon.addEventListener('click',()=>{
    sidebar_wrapper.classList.add('open');
    closebtn.classList.add('show');
});
closebtn.addEventListener('click',()=>{
    sidebar_wrapper.classList.remove('open');
    closebtn.classList.remove('show');
})
window.onclick = function(event) {
	if (event.target == sidebar_wrapper) {
		sidebar_wrapper.classList.remove('open');
        closebtn.classList.remove('show');

  }
}

$(".action").click(function (e) { 
    $(this).closest('.actionbutton').children('.threeDotsubMenu').toggle();
});
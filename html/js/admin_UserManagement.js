$(document).ready( function () {
    $('#userManagementTable').DataTable({
        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
        
        "pagingType": "full_numbers",
        "searching":false,
        "order": [],
        'columnDefs': [ {
            'targets': [1,2,4,7], 
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

var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
    $("#userManagementTable").on('click', 'th', function() {
        $("#userManagementTable thead th").each(function(i, th) {
            $(th).find('.arrow-hack').remove();
            var html = $(th).html();
            if($(th).hasClass("sorting_asc")){
                $(th).html(html+spanAsc);
            }else if($(th).hasClass("sorting_desc")){
                $(th).html(html+spanDesc);
            }else{
                $(th).html(html+spanSorting);
            }        
        });     
        
});   

$("#userManagementTable th").first().click().click();
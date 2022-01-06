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


// const submenu = document.querySelector('.submenu');
// const menu = document.querySelector('.menu');

// menu.addEventListener('click',()=>{
//     if($('.submenu').hasClass('close')){
//             this.classList.remove('close');
//             this.classList.add('open');
//         }else{
//             this.classList.remove('open');
//             this.classList.add('close');
//         }
        
//     }
// )
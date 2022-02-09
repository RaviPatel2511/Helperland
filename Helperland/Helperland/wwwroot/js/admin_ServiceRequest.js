$(document).ready( function () {
  table =  $('#serviceRequesttable').DataTable({
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

var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
    $("#serviceRequesttable").on('click', 'th', function() {
        $("#serviceRequesttable thead th").each(function(i, th) {
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

$("#serviceRequesttable th").first().click().click();

$("td").each(function() {
    var tddata = $(this).html();
    if(tddata == ""){
        $(this).html("No Data");
    }
    else{

    }
});

function sort(col, order) {
	table.order([col, order]).draw();
}


  $('input[type=radio][name=sortOption]').change(function() {
    if (this.value == 'ServiceId:Ascending') {
        sort(0,"asc");
    }
    else if (this.value == 'ServiceId:Descending') {
        sort(0,"desc");
    }
    else if (this.value == 'ServiceDate:Oldest') {
        sort(1,"asc");
    }
    else if (this.value == 'ServiceDate:Latest') {
        sort(1,"desc");
    }
    else if (this.value == 'Customer:AtoZ') {
        sort(2,"asc");
    }
    else if (this.value == 'Customer:ZtoA') {
        sort(2,"desc");
    }
    else if (this.value == 'ServiceProvider:AtoZ') {
        sort(3,"asc");
    }
    else if (this.value == 'ServiceProvider:ZtoA') {
        sort(3,"desc");
    }
    else if (this.value == 'Status:Ascending') {
        sort(4,"asc");
    }
    else if (this.value == 'Status:Descending') {
        sort(4,"desc");
    }
  });

// PREVENT FFROM BACK BUTTON AFTER LOGOUT
window.history.forward();
function noBack() {
    window.history.forward();
}

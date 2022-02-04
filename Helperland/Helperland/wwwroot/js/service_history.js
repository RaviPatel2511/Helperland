
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
$(document).ready( function () {
     table = $('#service_history_table').DataTable({
        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
        "pagingType": "full_numbers",
        "searching":false,
        "order":[],
        'buttons': [{
            extend:'excel',
            text:'Export'
        }],
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

window.addEventListener("scroll", () => {
	if (window.scrollY > 130) {
	    nav.classList.add("fixed-top");
	} else {
    	nav.classList.remove("fixed-top");
	}

});

var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
    $("#service_history_table").on('click', 'th', function() {
        $("#service_history_table thead th").each(function(i, th) {
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

$("#service_history_table th").first().click().click();

function sort(col, order) {
	table.order([col, order]).draw();
}


  $('input[type=radio][name=sortOption]').change(function() {
    if (this.value == 'ServiceDate:Oldest') {
        sort(0,"desc");
    }
    else if (this.value == 'ServiceDate:Latest') {
        sort(0,"asc");
    }
    else if (this.value == 'ServiceProvider:AtoZ') {
        sort(1,"asc");
    }
    else if (this.value == 'ServiceProvider:ZtoA') {
        sort(1,"desc");
    }
    else if (this.value == 'PaymentLowtoHigh') {
        sort(2,"asc");
    }
    else if (this.value == 'PaymentHightoLow') {
        sort(2,"desc");
    }else{
        
    }
  });

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

const menubtn=document.querySelector('.menubtn');
const Sidenav=document.querySelector('.sideNav');
const closebtn=document.querySelector('.closebtn');
const nav = document.querySelector("nav");

$(document).ready( function () {
 table = $('#upcomingService').DataTable({
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
              "first": '<i class="fas fa-step-backward"></i>',
              "next": '<i class="fas fa-angle-right"></i>',
              "previous": '<i class="fas fa-angle-left"></i>',
              "last": '<i class="fas fa-step-forward"></i>'
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

var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
    $("#upcomingService").on('click', 'th', function() {
        $("#upcomingService thead th").each(function(i, th) {
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

$("#upcomingService th").first().click().click();


function sort(col, order) {
	table.order([col, order]).draw();
}


  $('input[type=radio][name=sortOption]').change(function() {
    if (this.value == 'ServiceDate:Oldest') {
        sort(1,"asc");
    }
    else if (this.value == 'ServiceDate:Latest') {
        sort(1,"desc");
    }
    else if (this.value == 'ServiceId:Oldest') {
        sort(0,"asc");
    }
    else if (this.value == 'ServiceId:Latest') {
        sort(0,"desc");
    }
    else if (this.value == 'Customer:AtoZ') {
        sort(2,"asc");
    }
    else if (this.value == 'Customer:ZtoA') {
        sort(2,"desc");
    }
    else if (this.value == 'DistanceLowtoHigh') {
        sort(3,"asc");
    }
    else if (this.value == 'DistanceHightoLow') {
        sort(3,"desc");
    }
  });

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

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
});

var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
$(".dataTable").on('click', 'th', function() {
    $(".dataTable thead th").each(function(i, th) {
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

$(".dataTable th").first().click().click();

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
    }
    else {
        
    }
  });

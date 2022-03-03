
var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
$("#upcomingService").on('click', 'th', function () {
    $("#upcomingService thead th").each(function (i, th) {
        $(th).find('.arrow-hack').remove();
        var html = $(th).html();
        if ($(th).hasClass("sorting_asc")) {
            $(th).html(html + spanAsc);
        } else if ($(th).hasClass("sorting_desc")) {
            $(th).html(html + spanDesc);
        } else {
            $(th).html(html + spanSorting);
        }
    });

});

$("#upcomingService th").first().click().click();


function sort(col, order) {
    table.order([col, order]).draw();
}


$('input[type=radio][name=sortOption]').change(function () {
    if (this.value == 'ServiceDate:Oldest') {
        sort(1, "asc");
    }
    else if (this.value == 'ServiceDate:Latest') {
        sort(1, "desc");
    }
    else if (this.value == 'ServiceId:Oldest') {
        sort(0, "asc");
    }
    else if (this.value == 'ServiceId:Latest') {
        sort(0, "desc");
    }
    else if (this.value == 'Customer:AtoZ') {
        sort(2, "asc");
    }
    else if (this.value == 'Customer:ZtoA') {
        sort(2, "desc");
    }
    else if (this.value == 'DistanceLowtoHigh') {
        sort(3, "asc");
    }
    else if (this.value == 'DistanceHightoLow') {
        sort(3, "desc");
    }
});

$(document).ready(function () {
    table = $('#upcomingService').DataTable({
        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
        "pagingType": "full_numbers",
        "searching": false,
        "autoWidth": false,
        "order": [],
        'buttons': [{
            extend: 'excel',
            text: 'Export'
        }],
        'columnDefs': [{
            'targets': [5],
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

    
    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[2].classList.add("active");
    
});
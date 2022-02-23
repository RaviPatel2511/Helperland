


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


$('input[type=radio][name=sortOption]').change(function () {
    if (this.value == 'ServiceId:Oldest') {
        sort(2, "desc");
    }
    else if (this.value == 'ServiceId:Latest') {
        sort(0, "asc");
    }
    else if (this.value == 'ServiceDate:Oldest') {
        sort(1,"desc");
    }
    else if (this.value == 'ServiceDate:Latest') {
        sort(1,"asc");
    }
    else if (this.value == 'ServiceProvider:AtoZ') {
        sort(2,"asc");
    }
    else if (this.value == 'ServiceProvider:ZtoA') {
        sort(2,"desc");
    }
    else if (this.value == 'PaymentLowtoHigh') {
        sort(3,"asc");
    }
    else if (this.value == 'PaymentHightoLow') {
        sort(3,"desc");
    }
    else {
        
    }
  });

// reschedule date picker
$(document).ready(function () {
    var date_input = $('#rescheduleDate');
    date_input.datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        startDate: '+1d',
    });
});

function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/getDashboardDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                ////var address_picker = $('.address-picker');
                ////address_picker.empty();
                ////for (var i = 0; i < response.length; i++) {
                ////    if (response[i].isdefault == true) {
                ////        address_picker.append(' <li> <label> <input type="radio" name="addressradio" id="addressradio" value="' + response[i].id + '" checked /> <span class="address-block"> <b>address : </b> ' + response[i].addressline1 + ' ' + response[i].addressline2 + ', ' + response[i].city + ' ' + response[i].postalcode + ' </span > <span> <b>phone number :</b>  ' + response[i].mobile + '   </span> <span class="radio-pointer"></span> </label > </li > ')
                ////    } else {
                ////        address_picker.append(' <li> <label> <input type="radio" name="addressradio" id="addressradio" value="' + response[i].id + '" /> <span class="address-block"> <b>address : </b> ' + response[i].addressline1 + ' ' + response[i].addressline2 + ', ' + response[i].city + ' ' + response[i].postalcode + ' </span > <span> <b>phone number :</b>  ' + response[i].mobile + '   </span> <span class="radio-pointer"></span> </label > </li > ')
                ////    }
                ////}
                var dashtbldata = $('#dashTblData');
                dashtbldata.empty();
                for (var i = 0; i < response.length; i++) {
                    dashtbldata.append('<tr> <td data-toggle="modal" data-target="#displaydatamodal" data-dismiss="modal" >'+response[i].serviceId+'</td><td><p class="date"><img src="../image/upcoming_service/calendar.webp">'+response[i].serviceDate+'</p><p><img src="../image/upcoming_service/layer-14.png">12:00-18:00</p></td><td><div class="row"><div class="col sp_icon d-flex align-items-center"><span class="img_circle"><img src="../image/service_history/cap.png" alt="cap"></span></div><div class="col"><p>lyum watson</p><p><div class="stars" style="--rating:4;"></div>&nbsp;4</p></div></div></td><td><p class="payment">'+response[i].payment + " Rs." +'</p></td><td><a href="#" data-toggle="modal" data-target="#reschedulemodal" data-dismiss="modal" class="btn rescheduleBtn">Reschedule</a><a href="#" data-toggle="modal" data-target="#canclemodal" data-dismiss="modal" class="btn cancelbtn">Cancle</a></td></tr>')
                    //dashtbldata.append('<tr> < td data - toggle="modal" data - target="#displaydatamodal" data - dismiss="modal" > "1"</td > <td> <p class="date"><img src="~/image/upcoming_service/calendar.webp"> "2"</p> <p><img src="~/image/upcoming_service/layer-14.png"> 12:00-18:00</p></td> <td><div class="row"><div class="col sp_icon d-flex align-items-center" ><span class="img_circle"><img src="~/image/service_history/cap.png" alt="cap"></span></div><div class="col"><p>lyum watson</p> <p><div class="stars" style="--rating: 4;"></div>&nbsp;4</p></div></div></td> <td><p class="payment">"5"</p></td> <td><a href="#" data-toggle="modal" data-target="#reschedulemodal" data-dismiss="modal" class="btn reschedulebtn">reschedule</a><a href="#" data-toggle="modal" data-target="#canclemodal" data-dismiss="modal" class="btn cancelbtn">cancle</a></td></tr>')
                    //dashtbldata.append('<tr> < td data - toggle="modal" data - target="#displaydatamodal" data - dismiss="modal" > "1"</td > <td> <p class="date"><img src="~/image/upcoming_service/calendar.webp"> "2"</p> <p><img src="~/image/upcoming_service/layer-14.png"> 12:00-18:00</p></td> <td><div class="row"><div class="col sp_icon d-flex align-items-center" ><span class="img_circle"><img src="~/image/service_history/cap.png" alt="cap"></span></div><div class="col"><p>lyum watson</p> <p><div class="stars" style="--rating: 4;"></div>&nbsp;4</p></div></div></td> <td><p class="payment">"5"</p></td> <td><a href="#" data-toggle="modal" data-target="#reschedulemodal" data-dismiss="modal" class="btn reschedulebtn">reschedule</a><a href="#" data-toggle="modal" data-target="#canclemodal" data-dismiss="modal" class="btn cancelbtn">cancle</a></td></tr>');
                //    dashtbldata.append('ok');
                }
                console.log(response);
                //console.log("ok");

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}


getData();










$(document).ready(function () {
    table = $('#Dashboard_table').DataTable({
        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
        "pagingType": "full_numbers",
        "searching": false,
        "order": [],
        'buttons': [{
            extend: 'excel',
            text: 'Export'
        }],
        'columnDefs': [{
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
});
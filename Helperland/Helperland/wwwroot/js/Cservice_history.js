
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
        sort(0, "asc");
    }
    else if (this.value == 'ServiceId:Latest') {
        sort(0, "desc");
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



function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/GetServiceHistoryDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    var ServiceHistoryTblData = $('#ServiceHistoryTblData');
                    ServiceHistoryTblData.empty();
                    for (var i = 0; i < response.length; i++) {
                        if (response[i].status == 1) {
                            ServiceHistoryTblData.append('<tr><td class="pl-4">' + response[i].serviceId + '</td><td><p><span class="mx-1"><img src="../image/upcoming_service/calendar.webp" alt="calendar"></span> <strong>' + response[i].serviceDate + '</strong></p><p><span class="mx-1"><img src="../image/upcoming_service/layer-14.png" alt="calendar" class="clockImg"></span>' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td></td><td class="text-center"><span class="payment">' + response[i].payment + " Rs." +'</span></td><td><span class="status_cancelled">Cancelled</span></td><td class="text-center"><span><button class="rate" disabled="disabled">Rate SP</button></span></td></tr>');
                            //ServiceHistoryTblData.append('<tr><td>' + +'</td><td><p><span><img src="../image/service_history/calendar.png" alt="calendar"></span> <strong>' + +'</strong></p><p>' + +'</p></td><td><div class="row"><div class="col sp_icon d-flex align-items-center"><Span class="img_circle"><img src="../image/service_history/cap.png" alt="cap"></Span></div><div class="col"><p>Lyum Watson</p><p><div class="Stars" id="rate1" style="--rating: 4;"></div>&nbsp;4</p></div></div></td><td><span class="payment">' + +'</span></td><td><span class="status_cancelled">' + +'</span></td><td><span><button class="rate" disabled="disabled">Rate SP</button></span></td></tr>');
                        } else {
                            ServiceHistoryTblData.append('<tr><td class="pl-4">' + +'</td><td><p><span><img src="../image/service_history/calendar.png" alt="calendar"></span> <strong>' + +'</strong></p><p>' + +'</p></td><td></td><td class="text-center"><span class="payment">' + +'<span class="status_complete">Completed</span>' + +'</span></td><td class="text-center"><span><button class="rate">Rate SP</button></span></td></tr>');
                        }
                        

                    }
                    table = $('#service_history_table').DataTable({
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


                    //$(".rescheduleBtn").on('click', function () {
                    //    $('#rescheduleTime').val('08:00 AM');
                    //    $("#rescheduleModalBtn").click();
                    //    var ClickedserviceId = $(this).parent().parent().children(':first-child').text();
                    //    $("#serviceIdForReschedule").val(ClickedserviceId);
                    //    GetRescheduleRequest(ClickedserviceId);
                    //});
                    //$(".cancelbtn").on('click', function () {
                    //    $("#canclecomments").val('');
                    //    $('#cancleServiceMdodelBtn').prop('disabled', true);
                    //    $('#cancleServiceMdodelBtn').css('cursor', 'not-allowed');
                    //    $("#cancleModalBtn").click();
                    //    var ClickserviceId = $(this).parent().parent().children(':first-child').text();
                    //    $("#cancleReqServiceId").val(ClickserviceId);

                    //});

                }, 1000);
            },
        error:
            function (err) {
                console.error(err);

            },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 1000);
        }
    });
}



$(document).ready( function () {
    getData();
});
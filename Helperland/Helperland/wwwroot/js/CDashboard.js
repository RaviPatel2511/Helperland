
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
        url: '/Customer/getDashboardDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                var dashtbldata = $('#dashTblData');
                dashtbldata.empty();
                for (var i = 0; i < response.length; i++) {
                    dashtbldata.append('<tr> <td class="pl-4" data-toggle="modal" data-target="#displaydatamodal" data-dismiss="modal" >' + response[i].serviceId + '</td><td><p class="date"><img class="ServiceDateImg" src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img class="ServiceTimeImg" src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td></td><td class="text-center"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td><a href="#" data-toggle="modal" data-target="#reschedulemodal" data-dismiss="modal" class="btn rescheduleBtn mx-1">Reschedule</a><a href="#" data-toggle="modal" data-target="#canclemodal" data-dismiss="modal" class="btn cancelbtn mx-1">Cancle</a></td></tr>');
                   /* dashtbldata.append('<tr> <td class="pl-4" data-toggle="modal" data-target="#displaydatamodal" data-dismiss="modal" >' + response[i].serviceId + '</td><td><p class="date"><img class="ServiceDateImg" src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img class="ServiceTimeImg" src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td><div class="row"><div class="col sp_icon d-flex align-items-center"><span class="img_circle"><img src="../image/service_history/cap.png" alt="cap"></span></div><div class="col"><p>lyum watson</p><p><div class="stars" style="--rating:4;"></div>&nbsp;4</p></div></div></td><td><p class="payment">' + response[i].payment + " Rs." + '</p></td><td><a href="#" data-toggle="modal" data-target="#reschedulemodal" data-dismiss="modal" class="btn rescheduleBtn">Reschedule</a><a href="#" data-toggle="modal" data-target="#canclemodal" data-dismiss="modal" class="btn cancelbtn">Cancle</a></td></tr>');*/

                }
                table = $('#Dashboard_table').DataTable({
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

                    $('#Dashboard_table tbody').on('click', '.rescheduleBtn', function () {
                        $('#rescheduleTime').val('08:00 AM');
                        $("#rescheduleModalBtn").click();
                        var ClickedserviceId = $(this).parent().parent().children(':first-child').text();
                        $("#serviceIdForReschedule").val(ClickedserviceId);
                        //GetRescheduleRequest(ClickedserviceId);
                    });
                    
                    
                    $('#Dashboard_table tbody').on('click', '.cancelbtn', function () {
                    $("#canclecomments").val('');
                    $('#cancleServiceMdodelBtn').prop('disabled', true);
                    $('#cancleServiceMdodelBtn').css('cursor', 'not-allowed');
                    $("#cancleModalBtn").click();
                    var ClickserviceId = $(this).parent().parent().children(':first-child').text();
                    $("#cancleReqServiceId").val(ClickserviceId);
                    
                });

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

//function GetRescheduleRequest(x) {
//    $.ajax({
//        type: 'GET',
//        cache: false,
//        data: { 'ReqServiceId': x },
//        url: '/Customer/GetRescheduleRequestData',
//        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
//        success:
//            function (response) {
//                console.log(response);
//            },
//        error:
//            function (err) {
//                console.error(err);
//            }
//    });
//}

$("#rescheduleServiceUpdateBtn").click(function () {
    var InputserviceIdVal = $("#serviceIdForReschedule").val();
    var rescheduleServiceTime = $('#rescheduleDate').val() + " " + $('#rescheduleTime').val();
    $.ajax({
        type: "POST",
        url: '/Customer/RescheduleService',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'InputserviceIdVal': InputserviceIdVal, 'rescheduleServiceTime': rescheduleServiceTime },
        cache: false,
        success:
            function (response) {
                if (response == "ok") {
                    window.location.reload();
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
});

$("#cancleServiceMdodelBtn").click(function () {
    var InputCancleServiceId = $("#cancleReqServiceId").val();
    var canclecomments = $('#canclecomments').val();
    $.ajax({
        type: "POST",
        url: '/Customer/CancleRequest',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'InputCancleServiceId': InputCancleServiceId, 'canclecomments': canclecomments },
        cache: false,
        success:
            function (response) {
                if (response == "Successfully") {
                    $("#cancleModal").modal('hide');
                    $("#CancleReferenceId").text(InputCancleServiceId);
                    $("#CancleSuccessfulyDoneBtn").click();
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
});

$("#rescheduleDate").change(function () {
    if ($("#rescheduleDate").val() == '' || $("#rescheduleTime").val() == '') {
        $('#rescheduleServiceUpdateBtn').prop('disabled', true);
        $('#rescheduleServiceUpdateBtn').css('cursor', 'not-allowed');
    } else {
        $('#rescheduleServiceUpdateBtn').prop('disabled', false);
        $('#rescheduleServiceUpdateBtn').css('cursor', 'pointer');
    }
});

$("#canclecomments").keyup(function () {
    if ($("#canclecomments").val() == '') {
        $('#cancleServiceMdodelBtn').prop('disabled', true);
        $('#cancleServiceMdodelBtn').css('cursor', 'not-allowed');
    } else {
        $('#cancleServiceMdodelBtn').prop('disabled', false);
        $('#cancleServiceMdodelBtn').css('cursor', 'pointer');
    }
});




$(document).ready(function () {

    getData();

    var date_input = $('#rescheduleDate');
    date_input.datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        startDate: '+1d',
    });
});



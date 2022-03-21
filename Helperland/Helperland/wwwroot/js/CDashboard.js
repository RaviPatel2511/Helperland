
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
                        if (response[i].providerId == null) {
                                dashtbldata.append('<tr> <td class="pl-4 serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><p class="date"><img class="ServiceDateImg" src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img class="ServiceTimeImg" src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serviceSummary"></td><td class="text-center serviceSummary"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td><a  class="btn rescheduleBtn mx-1">Reschedule</a><a  class="btn cancelbtn mx-1">Cancle</a></td></tr>');
                        } else {
                            if (response[i].avtar == null) {
                                    dashtbldata.append('<tr> <td class="pl-4 serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><p class="date"><img class="ServiceDateImg" src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img class="ServiceTimeImg" src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serviceSummary"><div class="row"><div class="col sp_icon d-flex align-items-center"><span class="img_circle"><img src="../image/upcoming_service/cap.png" alt="avtar"></span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" style="--rating:' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="text-center serviceSummary"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td><a  class="btn rescheduleBtn mx-1">Reschedule</a><a  class="btn cancelbtn mx-1">Cancle</a></td></tr>');
                            } else {
                                    dashtbldata.append('<tr> <td class="pl-4 serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><p class="date"><img class="ServiceDateImg" src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img class="ServiceTimeImg" src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serviceSummary"><div class="row"><div class="col sp_icon d-flex align-items-center"><span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="avtar"></span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" style="--rating:' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="text-center serviceSummary"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td><a  class="btn rescheduleBtn mx-1">Reschedule</a><a  class="btn cancelbtn mx-1">Cancle</a></td></tr>');
                            }
                        }
                    }
                    //console.log(response);     
                table = $('#Dashboard_table').DataTable({
                    "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
                    "pagingType": "full_numbers",
                    "searching": false,
                    "autoWidth": false,
                    "order": [[0, "desc"]],
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
                        $('#rescheduleServiceUpdateBtn').prop('disabled', true);
                        $('#rescheduleServiceUpdateBtn').css('cursor', 'not-allowed');
                        $("#rescheduleModalBtn").click();
                        var ClickedserviceId = $(this).parent().parent().children(':first-child').text();
                        $("#serviceIdForReschedule").val(ClickedserviceId);
                        GetRescheduleRequest(ClickedserviceId);
                    });
                    
                    
                    $('#Dashboard_table tbody').on('click', '.cancelbtn', function () {
                        $("#canclecomments").val('');
                        $('#cancleServiceMdodelBtn').prop('disabled', true);
                        $('#cancleServiceMdodelBtn').css('cursor', 'not-allowed');
                        $("#cancleModalBtn").click();
                        var ClickserviceId = $(this).parent().parent().children(':first-child').text();
                        $("#cancleReqServiceId").val(ClickserviceId);
                    });

                    $('#Dashboard_table tbody').on('click', '.serviceSummary', function () {
                        var clickedRow = $(this).parent().children(':first-child').text();
                        var ServicePro = $(this).parent().children(':nth-child(3)').html();
                        GetServiceSummary(clickedRow, ServicePro);
                    });

                    $('#SerRescheduleBtn').click(function () {
                        $('#rescheduleServiceUpdateBtn').prop('disabled', true);
                        $('#rescheduleServiceUpdateBtn').css('cursor', 'not-allowed');
                        $("#rescheduleModalBtn").click();
                        var ClickedSerIdReschedule = $("#SerId").text();
                        $("#serviceIdForReschedule").val(ClickedSerIdReschedule);
                        $("#displaydataModal").modal('hide');
                        GetRescheduleRequest(ClickedSerIdReschedule);
                    });

                    $('#SerCancleBtn').click(function () {
                        $("#canclecomments").val('');
                        $('#cancleServiceMdodelBtn').prop('disabled', true);
                        $('#cancleServiceMdodelBtn').css('cursor', 'not-allowed');
                        $("#cancleModalBtn").click();
                        var ClickedSerIdCancle = $("#SerId").text();
                        $("#displaydataModal").modal('hide');
                        $("#cancleReqServiceId").val(ClickedSerIdCancle);
                    });

                }, 500);
            },
        error:
            function (err) {
                console.error(err);
            
            },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 500);
        }
    });
}

function GetRescheduleRequest(x) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'ReqServiceId': x },
        url: '/Customer/GetRescheduleRequestData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                var time = response.serviceStartTime;
                var date = response.serviceDate;
                $("#rescheduleDate").val(date);
                $("#rescheduleTime").val(time);
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

function GetServiceSummary(x,y) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'ReqServiceId': x },
        url: '/Customer/GetServiceSummaryData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                //console.log(response);
                $("#SerExtra").empty();
                if (response.cabinet) {
                    $("#SerExtra").append('Inside Cabinet, ')
                }
                if (response.fridge) {
                    $("#SerExtra").append('Inside Fridge, ')
                }
                if (response.laundary) {
                    $("#SerExtra").append('Laundary Wash & Dry, ')
                }
                if (response.oven) {
                    $("#SerExtra").append('Inside Oven, ')
                }
                if (response.window) {
                    $("#SerExtra").append('Inside Window')
                }
                $("#SerDate").text(response.serviceDate);
                $("#SerStartTime").text(response.serviceStartTime);
                $("#SerEndTime").text(response.serviceEndTime);
                $("#SerDuration").text(response.duration);
                $("#SerId").text(response.id);
                $("#SerPayment").html(response.payment + " Rs.");
                $("#SerAddress").html(response.addressLine1 + " " + response.addressLine2 + " , " + response.city + " " + response.postalCode);
                $("#SerMobile").text(response.mobile);
                $("#SerEmail").text(response.email);
                $("#SerComment").text(response.comments);
                $("#SerPets").empty();
                if (response.havePets) {
                    $("#SerPets").html('<img src="../image/service_history/havepet.png" /> I have pets at home');
                } else {
                    $("#SerPets").html('<img src="../image/service_history/notpet.png" /> I do not have pets at home');
                }
                if (y != null) {
                    $("#spDetails").html(y);
                }
                $("#displaydataModal").modal('show');

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

$("#rescheduleServiceUpdateBtn").click(function () {
    var InputserviceIdVal = $("#serviceIdForReschedule").val();
    var rescheduleServiceTime = $('#rescheduleDate').val() + " " + $('#rescheduleTime').val();
    $.ajax({
        type: "POST",
        url: '/Customer/RescheduleService',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'InputserviceIdVal': InputserviceIdVal, 'rescheduleServiceTime': rescheduleServiceTime },
        cache: false,
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                if (response == "AnotherServiceBooked") {
                    $("#rescheduleModal").modal('hide');
                    $("#RescheduleError").modal('show');
                } else {
                    window.location.reload();
                }
            },
        error:
            function (err) {
                console.error(err);
            },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 500);
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
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
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
            },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 500);
        }
    });
});

$("#rescheduleDate,#rescheduleTime").change(function () {
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

    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[0].classList.add("active");
});



﻿
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

function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Provider/GetUpcomingServiceData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    var UpcomingServiceTblData = $('#UpcomingServiceTblData');
                    UpcomingServiceTblData.empty();
                    for (var i = 0; i < response.length; i++) {
                        UpcomingServiceTblData.append('<tr><td class="SerSummary">' + response[i].serviceId + '</td><td class="SerSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</span></td><td class="SerSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ' ,' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="text-center SerSummary">' + response[i].payment + ' Rs.</td><td></td><td class="text-center"><input class="cancelbtn" type="button" value="Cancel"></td></tr>')
                    }

                    console.log(response);
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

                    $('#upcomingService tbody').on('click', '.SerSummary', function () {
                        var clickedRow = $(this).parent().children(':first-child').text();
                        GetServiceSummary(clickedRow);
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

function GetServiceSummary(x) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'ReqServiceId': x },
        url: '/Provider/GetServiceSummaryData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
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

                $("#displaydataModal").modal('show');

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}


$(document).ready(function () {
    
    getData();
    
    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[2].classList.add("active");
    
});
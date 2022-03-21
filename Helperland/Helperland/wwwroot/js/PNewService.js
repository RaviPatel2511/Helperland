var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
$("#NewServiceTbl").on('click', 'th', function () {
    $("#NewServiceTbl thead th").each(function (i, th) {
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

$("#NewServiceTbl th").first().click().click();


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
    else if (this.value == 'CustomerDetails:AtoZ') {
        sort(2, "asc");
    }
    else if (this.value == 'CustomerDetails:AtoZ') {
        sort(2, "desc");
    }
    else if (this.value == 'PaymentLowtoHigh') {
        sort(3, "asc");
    }
    else if (this.value == 'PaymentHightoLow') {
        sort(3, "desc");
    }
});

function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Provider/GetNewServiceData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    var NewServiceTblData = $('#NewServiceTblData');
                    NewServiceTblData.empty();
                    for (var i = 0; i < response.length; i++) {
                        if (response[i].hasPet == true) {
                            if (response[i].conflictServiceId == null) {
                                NewServiceTblData.append('<tr><td class="SerSummary">' + response[i].serviceId + '</td><td class="SerSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</span></td><td class="SerSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ' ,' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="text-center SerSummary">' + response[i].payment + ' Rs.</td><td></td><td class="text-center"><input type="button" class="AcceptBtn" value="Accept" /></td><td></td></tr>');
                            } else {
                                NewServiceTblData.append('<tr><td class="SerSummary">' + response[i].serviceId + '</td><td class="SerSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</span></td><td class="SerSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ' ,' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="text-center SerSummary">' + response[i].payment + ' Rs.</td><td class="text-center"><span class="d-none">' + response[i].conflictServiceId + '</span><input type="button" class="ConfllictBtn" value="Conflict" /></td><td class="text-center"><input type="button" class="AcceptBtn disabled" value="Accept" disabled="disabled"/></td><td></td></tr>');
                            }
                        } else {
                            if (response[i].conflictServiceId == null) {
                                NewServiceTblData.append('<tr><td class="SerSummary">' + response[i].serviceId + '</td><td class="SerSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</span></td><td class="SerSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ' ,' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="text-center SerSummary">' + response[i].payment + ' Rs.</td><td></td><td class="text-center"><input type="button" class="AcceptBtn" value="Accept" /></td><td><div class="d-none">withoutpet</div></td></tr>');
                            } else {
                                NewServiceTblData.append('<tr><td class="SerSummary">' + response[i].serviceId + '</td><td class="SerSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</span></td><td class="SerSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ' ,' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="text-center SerSummary">' + response[i].payment + ' Rs.</td><td class="text-center"><span class="d-none">' + response[i].conflictServiceId + '</span><input type="button" class="ConfllictBtn" value="Conflict" /></td><td class="text-center"><input type="button" class="AcceptBtn disabled" value="Accept" disabled="disabled"/></td><td><div class="d-none">withoutpet</div></td></tr>');
                            }
                        }
                        
                    }
                    

                    //console.log(response);
                    table = $('#NewServiceTbl').DataTable({
                        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
                        "pagingType": "full_numbers",
                        "searching": true,
                        //retrieve: true,
                        "autoWidth": false,
                        "order": [[0, "desc"]],
                        'columnDefs': [{
                            'orderable': false,
                            'target':5,
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

                    $("#havePetFilter").change(() => {
                        console.log("ok");
                        var petCheck = $("#havePetFilter").is(':checked');
                        if (!petCheck) {
                            table.column(6).search('withoutpet').draw();
                        } else {
                            console.log("okkk");
                            table.search('').columns().search('').draw();
                        }
                    });

                    $('#NewServiceTbl tbody').on('click', '.SerSummary', function () {
                        var clickedRow = $(this).parent().children(':first-child').text();
                        var btnHtml = $(this).parent().children(':nth-child(6)').html();
                        GetServiceSummary(clickedRow, btnHtml);
                    });

                    $('#NewServiceTbl tbody').on('click', '.AcceptBtn', function () {
                        var clickRow = $(this).parent().parent().children(':first-child').text();
                        $("#AcceptService").modal('show');
                        $("#acceptSerId").val(clickRow);
                    });

                    $('#NewServiceTbl tbody').on('click', '.ConfllictBtn', function () {
                        var clickkedRow = $(this).parent().children(':first-child').text();
                        GetServiceSummary(clickkedRow);
                    });

                    $('#displaydataModal').on('click', '.AcceptBtn', function () {
                        $("#displaydataModal").modal('hide');
                        var clickmodalBtn = $("#SerId").text();
                        $("#AcceptService").modal('show');
                        $("#acceptSerId").val(clickmodalBtn);
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


function AcceptService() {
    var acceptSerId = $("#acceptSerId").val();
    $.ajax({
        type: "POST",
        url: '/Provider/AcceptServiceRequest',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'acceptSerId': acceptSerId },
        cache: false,
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                if (response == "alreadyBooked") {
                    $('#AcceptServiceError').modal({
                        backdrop: 'static',
                        keyboard: false
                    });
                    $("#AcceptService").modal('hide');
                    $("#AcceptServiceError").modal("show");
                } else {
                    window.location.reload();
                }
            },
        error:
            function (err) {
                console.error(err);
            },
        complete: function () {
                $(".loader-div").addClass('d-none');
        }
    });

}


$("#AcceptServiceErrorBtn").click(function () {
    window.location.reload();
});

function GetServiceSummary(x,y) {
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
                $("#SerCustName").text(response.customerName);
                $("#SerMobile").text(response.mobile);
                $("#SerEmail").text(response.email);
                $("#SerComment").text(response.comments);
                $("#SerPets").empty();
                if (response.havePets) {
                    $("#SerPets").html('<img src="../image/service_history/havepet.png" /> I have pets at home');
                } else {
                    $("#SerPets").html('<img src="../image/service_history/notpet.png" /> I do not have pets at home');
                }
                $(".action-btn").empty();
                $(".action-btn").append(y);
                getlon_len(response.postalCode);
                $("#displaydataModal").modal('show');

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

//var map = L.map('CustMap');
//function GetMap(x) {
//    $.ajax({
//        "async": true,
//        "crossDomain": true,
//        "url": "https://trueway-geocoding.p.rapidapi.com/Geocode?address=" + x,
//        "method": "GET",
//        "headers": {
//            "x-rapidapi-host": "trueway-geocoding.p.rapidapi.com",
//            "x-rapidapi-key": "af7a97fb09msh8757aecf65ca54dp1d68e3jsn9b9058109b2e"
//        },
//        success: (response) => {
//            map.setView([response.results[0].location.lat, response.results[0].location.lng], 14);

//            L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
//                attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
//            }).addTo(map);

//            L.marker([response.results[0].location.lat, response.results[0].location.lng]).addTo(map);
//            console.log(response);
//        },
//        error: (err) => {
//            console.log(err);

//        }
//    });
//}

var count = 0;
var map = L.map("CustMap");

async function getlon_len(zipcode) {
    map.setView([0, 0], 1);
    const attribution = '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors';
    const tileUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    const tiles = L.tileLayer(tileUrl, { attribution });
    tiles.addTo(map);
    const response = await fetch('https://nominatim.openstreetmap.org/search?format=json&limit=1&q=india,' + zipcode);
    const data = await response.json();
    const { lat, lon } = data[0];
    map.setView([lat, lon], 15);
    L.marker([lat, lon]).addTo(map);
}

$(document).ready(function () {
    getData();
    $("#havePetFilter").prop("checked", true);
    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[1].classList.add("active");
});


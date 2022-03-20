var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
$("#ServiceHistoryTbl").on('click', 'th', function () {
    $("#ServiceHistoryTbl thead th").each(function (i, th) {
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

$("#ServiceHistoryTbl th").first().click().click();


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
    else if (this.value == 'CustomerDetails:ZtoA') {
        sort(2, "desc");
    }
});

function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Provider/GetServiceHistoryDetails',
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
                        ServiceHistoryTblData.append('<tr><td>' + response[i].serviceId + '</td><td class="text-center"><p class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</p><p><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + " ," + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td></tr>')
                    }

                    console.log(response);
                    table = $('#ServiceHistoryTbl').DataTable({
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

                    $('#ServiceHistoryTbl tbody').on('click', 'tr', function () {
                        var clickedRow = $(this).children(':first-child').text();
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
                GetMap(response.postalCode);
                $("#displaydataModal").modal('show');

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

var count = 0;
var map = L.map("CustMap");

async function GetMap(zipcode) {
    map.setView([0, 0], 1);
    const attribution = '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors';
    const tileUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    const tiles = L.tileLayer(tileUrl, { attribution });
    tiles.addTo(map);
    const response = await fetch('https://nominatim.openstreetmap.org/search?format=json&limit=1&q=india,' + zipcode);
    const data = await response.json();
    const { lat, lon } = data[0];
    map.flyTo([lat, lon], 15);
    L.marker([lat, lon]).addTo(map);
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

//        },
//        error: (err) => {
//            console.log(err);

//        }
//    });
//}

function ExcelSheetDown() {
    var data = document.getElementById("ServiceHistoryTbl");

    var file = XLSX.utils.table_to_book(data, { sheet: "sheet1" });

    XLSX.write(file, { bookType: 'xlsx', bookSST: true, type: "base64" });

    XLSX.writeFile(file, "ServiceHistory." + 'xlsx');
}

$(document).ready(function () {
   
    getData();

    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[4].classList.add("active");

});


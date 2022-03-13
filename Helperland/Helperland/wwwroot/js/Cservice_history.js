
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
                        if (response[i].status == 1)
                        {
                            if (response[i].providerId != null)
                            {
                                if (response[i].avtar == null) {
                                ServiceHistoryTblData.append('<tr><td class="pl-4 serDetails">' + response[i].serviceId + '</td><td class="serDetails"><p><span class="mx-1"><img src="../image/upcoming_service/calendar.webp" alt="calendar"></span> <strong>' + response[i].serviceDate + '</strong></p><p><span class="mx-1"><img src="../image/upcoming_service/layer-14.png" alt="calendar" class="clockImg"></span>' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serDetails"><div class="row"><div class="col sp_icon d-flex align-items-center"><Span class="img_circle"><img src="../image/upcoming_service/cap.png" alt="avtar"></Span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="text-center serDetails"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td class="serDetails"><span class="status_cancelled">Cancelled</span></td><td class="text-center"><span><button class="rate disable" disabled="disabled">Rate SP</button></span></td></tr>');
                                } else {
                                ServiceHistoryTblData.append('<tr><td class="pl-4 serDetails">' + response[i].serviceId + '</td><td class="serDetails"><p><span class="mx-1"><img src="../image/upcoming_service/calendar.webp" alt="calendar"></span> <strong>' + response[i].serviceDate + '</strong></p><p><span class="mx-1"><img src="../image/upcoming_service/layer-14.png" alt="calendar" class="clockImg"></span>' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serDetails"><div class="row"><div class="col sp_icon d-flex align-items-center"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="avtar"></Span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="text-center serDetails"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td class="serDetails"><span class="status_cancelled">Cancelled</span></td><td class="text-center"><span><button class="rate disable" disabled="disabled">Rate SP</button></span></td></tr>');
                                }
                            }
                            else
                            {
                                ServiceHistoryTblData.append('<tr><td class="pl-4 serDetails">' + response[i].serviceId + '</td><td class="serDetails"><p><span class="mx-1"><img src="../image/upcoming_service/calendar.webp" alt="calendar"></span> <strong>' + response[i].serviceDate + '</strong></p><p><span class="mx-1"><img src="../image/upcoming_service/layer-14.png" alt="calendar" class="clockImg"></span>' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serDetails"></td><td class="text-center serDetails"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td class="serDetails"><span class="status_cancelled">Cancelled</span></td><td class="text-center"><span><button class="rate disable" disabled="disabled">Rate SP</button></span></td></tr>');
                            }                            
                        }
                        else
                        {
                            if (response[i].providerId != null)
                            {
                                ServiceHistoryTblData.append('<tr><td class="pl-4 serDetails">' + response[i].serviceId + '</td><td class="serDetails"><p><span class="mx-1"><img src="../image/upcoming_service/calendar.webp" alt="calendar"></span> <strong>' + response[i].serviceDate + '</strong></p><p><span class="mx-1"><img src="../image/upcoming_service/layer-14.png" alt="calendar" class="clockImg"></span>' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serDetails"><div class="row"><div class="col sp_icon d-flex align-items-center"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="avtar"></Span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="text-center serDetails"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td class="serDetails"><span class="status_complete">Completed</span></td><td class="text-center"><span><button class="rate">Rate SP</button></span></td></tr>');
                            }
                            else
                            {
                                ServiceHistoryTblData.append('<tr><td class="pl-4 serDetails">' + response[i].serviceId + '</td><td class="serDetails"><p><span class="mx-1"><img src="../image/upcoming_service/calendar.webp" alt="calendar"></span> <strong>' + response[i].serviceDate + '</strong></p><p><span class="mx-1"><img src="../image/upcoming_service/layer-14.png" alt="calendar" class="clockImg"></span>' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></td><td class="serDetails"></td><td class="text-center serDetails"><span class="payment">' + response[i].payment + " Rs." + '</span></td><td class="serDetails"><span class="status_complete">Completed</span></td><td class="text-center"><span><button class="rate">Rate SP</button></span></td></tr>');
                            }
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

                    $('#service_history_table tbody').on('click', '.serDetails', function () {
                        var clickedRow = $(this).parent().children(':first-child').text();
                        var AccepetedSp = $(this).parent().children(':nth-child(3)').html();
                        GetServiceSummary(clickedRow, AccepetedSp);
                    });

                    $('#service_history_table tbody').on('click', '.rate', function () {
                        $('input[name="ontime"]').prop('checked', false);
                        $('input[name="friendly"]').prop('checked', false);
                        $('input[name="quality"]').prop('checked', false);
                        $("#feedback").val('');
                        $('#RatingSaveMdodelBtn').prop('disabled', true);
                        $('#RatingSaveMdodelBtn').css('cursor', 'not-allowed');
                        var clickdRow = $(this).parent().parent().parent().children(':first-child').text();
                        var spdetail = $(this).parent().parent().parent().children(':nth-child(3)').html();
                        $("#rateServiceId").val(clickdRow);
                        getRatingData(clickdRow, spdetail);
                        
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

function GetServiceSummary(x,y) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'ReqServiceId': x },
        url: '/Customer/GetServiceSummaryData',
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
                if (y != null) {
                    $("#AccepetedSp").html(y);
                }
                $("#displaydataModal").modal('show');

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

function getRatingData(x,y) 
{
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'ReqServiceId': x },
        url: '/Customer/GetRatingData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                if (response == "NoRatingFound") {
                    $("#RatingSaveMdodelBtn").removeClass('d-none');
                    $("#SpDetails").html(y);
                    $("#ratingModal").modal('show');
                } else {
                    $("#RatingSaveMdodelBtn").addClass('d-none');
                    $("input[name=ontime][value=" + response.ontime + "]").prop('checked', true);
                    $("input[name=friendly][value=" + response.friendly + "]").prop('checked', true);
                    $("input[name=quality][value=" + response.quality + "]").prop('checked', true);
                    $("#feedback").val(response.comment);
                    $("#SpDetails").html(y);
                    $("#ratingModal").modal('show');
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

function saveRating() {
    let OntimeRate = $('input[name="ontime"]:checked').val();
    let FriendlyRate = $('input[name="friendly"]:checked').val();
    let QualityRate = $('input[name="quality"]:checked').val();
    if (OntimeRate == undefined) {
        OntimeRate = 0;
    }
    if (FriendlyRate == undefined) {
        FriendlyRate = 0;
    }
    if (QualityRate == undefined) {
        QualityRate = 0;
    }
    var comment = $("#feedback").val();
    var id = $("#rateServiceId").val();
    var obj = {};
    obj.ServiceRequestId = id;
    obj.Comments = comment;
    obj.OnTimeArrival = OntimeRate;
    obj.Friendly = FriendlyRate;
    obj.QualityOfService = QualityRate;
    $.ajax({
        type: 'Post',
        cache: false,
        data: obj,
        url: '/Customer/SaveSpRating',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                if (response == "Successfully") {
                    window.location.reload();
                } else {
                    alert("fail please try again");
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

$("#feedback").keyup(function () {
    $('#RatingSaveMdodelBtn').prop('disabled', false);
    $('#RatingSaveMdodelBtn').css('cursor', 'pointer');
});

$('input[type=radio][name=ontime],input[type=radio][name=friendly],input[type=radio][name=quality]').change(function () {
    $('#RatingSaveMdodelBtn').prop('disabled', false);
    $('#RatingSaveMdodelBtn').css('cursor', 'pointer');
});

function ExcelSheetDown() {
    var data = document.getElementById("service_history_table");

    var file = XLSX.utils.table_to_book(data, { sheet: "sheet1" });

    XLSX.write(file, { bookType: 'xlsx', bookSST: true, type: "base64" });

    XLSX.writeFile(file, "ServiceHistory." + 'xlsx');
}

$(document).ready( function () {
    getData();

    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[1].classList.add("active");
});
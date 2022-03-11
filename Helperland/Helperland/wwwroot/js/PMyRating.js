
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
    else if (this.value == 'RatingLowtoHigh') {
        sort(2, "asc");
    }
    else if (this.value == 'RatingHightoLow') {
        sort(2, "desc");
    }
});

function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Provider/GetMyRatingData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    var myRatingTblData = $('#myRatingTblData');
                    myRatingTblData.empty();
                    for (var i = 0; i < response.length; i++) {
                        if (response[i].spRatings >= 4) {
                            myRatingTblData.append('<tr><td class="text-center"><p>' + response[i].serviceId + '</p><p class="font-weight-bold">' + response[i].custName + '</p></td><td class="text-center"><div class="dateDiv"><p class="date"><img src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></div></td><td class="text-center"><span class="d-none">' + Math.round(response[i].spRatings) + '</span><div class="rateDiv"><p class="font-weight-bold">ratings</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;Very Good</p></div></td><td><p class="font-weight-bold">Customer Comment</p><p>' + response[i].comment + '</p></td></tr>');
                        } else if (response[i].spRatings >= 3 && response[i].spRatings < 4) {
                            myRatingTblData.append('<tr><td class="text-center"><p>' + response[i].serviceId + '</p><p class="font-weight-bold">' + response[i].custName + '</p></td><td class="text-center"><div class="dateDiv"><p class="date"><img src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></div></td><td class="text-center"><span class="d-none">' + Math.round(response[i].spRatings) + '</span><div class="rateDiv"><p class="font-weight-bold">ratings</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;Best</p></div></td><td><p class="font-weight-bold">Customer Comment</p><p>' + response[i].comment + '</p></td></tr>');
                        }
                        else if (response[i].spRatings >= 1 && response[i].spRatings < 3) {
                            myRatingTblData.append('<tr><td class="text-center"><p>' + response[i].serviceId + '</p><p class="font-weight-bold">' + response[i].custName + '</p></td><td class="text-center"><div class="dateDiv"><p class="date"><img src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></div></td><td class="text-center"><span class="d-none">' + Math.round(response[i].spRatings) + '</span><div class="rateDiv"><p class="font-weight-bold">ratings</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;Average</p></div></td><td><p class="font-weight-bold">Customer Comment</p><p>' + response[i].comment + '</p></td></tr>');
                        } else if (response[i].spRatings >= 0 && response[i].spRatings < 1) {
                            myRatingTblData.append('<tr><td class="text-center"><p>' + response[i].serviceId + '</p><p class="font-weight-bold">' + response[i].custName + '</p></td><td class="text-center"><div class="dateDiv"><p class="date"><img src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></div></td><td class="text-center"><span class="d-none">' + Math.round(response[i].spRatings) + '</span><div class="rateDiv"><p class="font-weight-bold">ratings</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;Poor</p></div></td><td><p class="font-weight-bold">Customer Comment</p><p>' + response[i].comment + '</p></td></tr>');
                        } else {
                            myRatingTblData.append('<tr><td class="text-center"><p>' + response[i].serviceId + '</p><p class="font-weight-bold">' + response[i].custName + '</p></td><td class="text-center"><div class="dateDiv"><p class="date"><img src="../image/upcoming_service/calendar.webp">' + response[i].serviceDate + '</p><p><img src="../image/upcoming_service/layer-14.png">' + response[i].serviceStartTime + "-" + response[i].serviceEndTime + '</p></div></td><td class="text-center"><span class="d-none">' + 0 + '</span><div class="rateDiv"><p class="font-weight-bold">ratings</p><p><div class="Stars" id="rate1" style="--rating: ' + 0 + ';"></div>&nbsp;No Rating</p></div></td><td><p class="font-weight-bold">Customer Comment</p><p>' + response[i].comment + '</p></td></tr>');
                        }
                    }
                    console.log(response);
                    table = $('#myRatingTbl').DataTable({
                        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
                        "pagingType": "full_numbers",
                        "searching": true,
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

                    $("#ratingDropdown select").change(function () {
                        var searchWord = $("#ratingDropdown select").val();
                        if (searchWord != "All") {
                            table.search(searchWord).draw();
                        } else {
                            table.search("Very Good" | "Best" | "Average" | "Poor" | "No Rating").draw();
                        }
                    })


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


$(document).ready(function () {

    getData();

    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[5].classList.add("active");
});
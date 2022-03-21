function GetBlockCust() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/GetFavouritePro',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    var FavProTblData = $("#FavProTblData");
                    FavProTblData.empty();
                        if (response != "noData") {
                            for (var i = 0; i < response.length; i++) {
                                if (response[i].isBlocked == true) {
                                    FavProTblData.append('<tr><td class="d-none">' + response[i].proId + '</td><td><img src="../image/upcoming_service/' + response[i].avtar + '.png" /></td><td><span class="ProName">' + response[i].name + '</span></td><td><div class="Stars" style="--rating: ' + response[i].spRating + ';"></div> ' + response[i].spRating + '</td><td><span class="CleaningCount">' + response[i].totalCleaning + ' cleaning</span></td><td><button class="btn UnBlockBtn">Unblock</button></td></tr>');
                                } else if (response[i].isBlocked == false && response[i].isFavourite == true) {
                                    FavProTblData.append('<tr><td class="d-none">' + response[i].proId + '</td><td><img src="../image/upcoming_service/' + response[i].avtar + '.png" /></td><td><span class="ProName">' + response[i].name + '</span></td><td><div class="Stars" style="--rating: ' + response[i].spRating + ';"></div> ' + response[i].spRating + '</td><td><span class="CleaningCount">' + response[i].totalCleaning + ' cleaning</span></td><td><button class="btn UnFavBtn">Unfavourite</button><button class="btn BlockBtn">Block</button></td></tr>');
                                } else {
                                    FavProTblData.append('<tr><td class="d-none">' + response[i].proId + '</td><td><img src="../image/upcoming_service/' + response[i].avtar + '.png" /></td><td><span class="ProName">' + response[i].name + '</span></td><td><div class="Stars" style="--rating: ' + response[i].spRating + ';"></div> ' + response[i].spRating + '</td><td><span class="CleaningCount">' + response[i].totalCleaning + ' cleaning</span></td><td><button class="btn FavBtn">Favourite</button><button class="btn BlockBtn">Block</button></td></tr>');
                                }
                            }
                        }
                        
                    table = $('#FavProTbl').DataTable({
                        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
                        "pagingType": "full_numbers",
                        "searching": false,
                        "autoWidth": false,
                        "order": [],
                        'buttons': [{
                            extend: 'excel',
                            text: 'Export'
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

                    $('#FavProTbl tbody').on('click', '.BlockBtn', function () {
                        var clickedRow = $(this).parent().parent().children(':first-child').text();
                        console.log(clickedRow);
                        BlockPro(clickedRow);
                    });

                    $('#FavProTbl tbody').on('click', '.UnBlockBtn', function () {
                        var clickRow = $(this).parent().parent().children(':first-child').text();
                        console.log(clickRow);
                        UnblockPro(clickRow);
                    });

                    $('#FavProTbl tbody').on('click', '.FavBtn', function () {
                        var clickFavRow = $(this).parent().parent().children(':first-child').text();
                        console.log(clickFavRow);
                        FavProvider(clickFavRow);
                    });

                    $('#FavProTbl tbody').on('click', '.UnFavBtn', function () {
                        var clickUnFavRow = $(this).parent().parent().children(':first-child').text();
                        console.log(clickUnFavRow);
                        UnfavProvider(clickUnFavRow);
                    });
                    

                }, 500);
            },
        error: (err) => {
            console.log(err);
        },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 500);
        }
    });
}

function BlockPro(x) {
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Customer/PostBlockProvider',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'ProId': x },
        success:
            function (response) {
                if (response == "success") {
                    $("#successText").text("Provider has been blocked successfully !");
                    $('#SuccessModal').modal({
                        backdrop: 'static',
                        keyboard: false
                    });
                    $("#SuccessModal").modal("show");
                } else {
                    alert("Fail");
                }

            },
        error: (err) => {
            console.log(err);
        }
    });
}

function UnblockPro(x) {
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Customer/UnBlockProvider',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'ProId': x },
        success:
            function (response) {
                if (response == "success") {
                    $("#successText").text("Provider has been Unblocked successfully !");
                    $('#SuccessModal').modal({
                        backdrop: 'static',
                        keyboard: false
                    });
                    $("#SuccessModal").modal("show");
                }

            },
        error: (err) => {
            console.log(err);
        }
    });
}

function FavProvider(x) {
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Customer/FavProvider',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'ProId': x },
        success:
            function (response) {
                if (response == "success") {
                    $("#successText").text("Provider has been Marked as Favourite !");
                    $('#SuccessModal').modal({
                        backdrop: 'static',
                        keyboard: false
                    });
                    $("#SuccessModal").modal("show");
                }

            },
        error: (err) => {
            console.log(err);
        }
    });
}


function UnfavProvider(x) {
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Customer/UnfavProvider',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'ProId': x },
        success:
            function (response) {
                if (response == "success") {
                    $("#successText").text("Provider has been Marked as Unfavourite !");
                    $('#SuccessModal').modal({
                        backdrop: 'static',
                        keyboard: false
                    });
                    $("#SuccessModal").modal("show");
                }

            },
        error: (err) => {
            console.log(err);
        }
    });
}


$(document).ready(function () {

    GetBlockCust();

    

    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[3].classList.add("active");
});
function GetBlockCust() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Provider/GetBlockCustData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                        var BlockCustTblData = $("#BlockCustTblData");
                        BlockCustTblData.empty();
                        if (response != "noData") {
                            for (var i = 0; i < response.length; i++) {
                                if (response[i].isBlocked == true) {
                                    BlockCustTblData.append('<tr><td><input type="hidden" value="' + response[i].userId + '"/><img src="../image/upcoming_service/cap.png" /></td><td><span class="BlockCustName">' + response[i].name + '</span></td><td><button class="btn UnblockBtn">Unblock</button></td></tr>');
                                } else {
                                    BlockCustTblData.append('<tr><td><input type="hidden" value="' + response[i].userId + '"/><img src="../image/upcoming_service/cap.png" /></td><td><span class="BlockCustName">' + response[i].name + '</span></td><td><button class="btn BlockBtn">Block</button></td></tr>');
                                }
                            }
                        }
                        
                    table = $('#BlockCustTbl').DataTable({
                        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
                        "pagingType": "full_numbers",
                        "searching": false,
                        "autoWidth": false,
                        "order": [],
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

                    $('#BlockCustTbl tbody').on('click', '.BlockBtn', function () {
                        var clickedRow = $(this).parent().parent().children(':first-child').children(':first-child').val();
                        BlockCustomer(clickedRow);
                    });

                    $('#BlockCustTbl tbody').on('click', '.UnblockBtn', function () {
                        var clickRow = $(this).parent().parent().children(':first-child').children(':first-child').val();
                        UnblockCustomer(clickRow);
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

function BlockCustomer(x) {
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Provider/PostBlockCustomer',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'CustId': x },
        success:
            function (response) {
                if (response == "success") {
                    $("#successText").text("User has been blocked successfully !");
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

function UnblockCustomer(x) {
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Provider/UnBlockCustomer',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'CustId': x },
        success:
            function (response) {
                if (response == "success") {
                    $("#successText").text("User has been Unblocked successfully !");
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
    $(selector)[6].classList.add("active");
});
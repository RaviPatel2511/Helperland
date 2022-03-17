const mobileNavIcon = document.querySelector('.mobileNavIcon');
const sidebar_wrapper = document.querySelector('#sidebar-wrapper');
const closebtn = document.querySelector('.closebtn');

mobileNavIcon.addEventListener('click',()=>{
    sidebar_wrapper.classList.add('open');
    closebtn.classList.add('show');
});
closebtn.addEventListener('click',()=>{
    sidebar_wrapper.classList.remove('open');
    closebtn.classList.remove('show');
})
window.onclick = function(event) {
	if (event.target == sidebar_wrapper) {
		sidebar_wrapper.classList.remove('open');
        closebtn.classList.remove('show');

  }
}

$(".action").click(function (e) { 
    $(this).closest('.actionbutton').children('.threeDotsubMenu').toggle();
});

var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
    $("#serviceRequesttable").on('click', 'th', function() {
        $("#serviceRequesttable thead th").each(function(i, th) {
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

$("#serviceRequesttable th").first().click().click();

function sort(col, order) {
	table.order([col, order]).draw();
}


  $('input[type=radio][name=sortOption]').change(function() {
    if (this.value == 'ServiceId:Ascending') {
        sort(0,"asc");
    }
    else if (this.value == 'ServiceId:Descending') {
        sort(0,"desc");
    }
    else if (this.value == 'Customer:AtoZ') {
        sort(2,"asc");
    }
    else if (this.value == 'Customer:ZtoA') {
        sort(2,"desc");
    }
    else if (this.value == 'ServiceProvider:AtoZ') {
        sort(3,"asc");
    }
    else if (this.value == 'ServiceProvider:ZtoA') {
        sort(3,"desc");
    }
    else if (this.value == 'Status:Ascending') {
        sort(5,"asc");
    }
    else if (this.value == 'Status:Descending') {
        sort(5,"desc");
    }
  });


function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Admin/GetServiceRequestData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    var serviceRequesttable = $('#ServiceRequestTblData');
                    var CustArr = [];
                    var ProArr = [];
                    serviceRequesttable.empty();
                    for (var i = 0; i < response.length; i++) {
                        if (response[i].status == 1)
                        {
                            serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="new"><span>New</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li><a class="EditAndReschedule">Edit & Reschedule</a></li><li class="mb-0"><a class="cancleBtn">Cancle</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail + '</span></td><td class="d-none">' + response[i].pincode + '</td></tr>');
                        }
                        else if (response[i].status == 2)
                        {
                            serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"><div class="row"><div class="col sp_icon"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="cap"></Span></div><div class="col"><p class="mb-0">' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="pending"><span>Pending</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li><a class="EditAndReschedule">Edit & Reschedule</a></li><li class="mb-0"><a class="cancleBtn">Cancle</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail + '</span></td><td class="d-none">' + response[i].pincode + '</td></tr>');
                        }
                        else if (response[i].status == 3)
                        {
                            if (response[i].providerId == null)
                            {
                                serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="Cancel"><span>Cancelled</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="mb-0"><a class="refundbtn">Refund</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail + '</span></td><td class="d-none">' + response[i].pincode + '</td></tr>');
                            }
                            else
                            {
                                serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"><div class="row"><div class="col sp_icon"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="cap"></Span></div><div class="col"><p class="mb-0">' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="Cancel"><span>Cancelled</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="mb-0"><a class="refundbtn">Refund</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail + '</span></td><td class="d-none">' + response[i].pincode + '</td></tr>');
                            }
                        }
                        else if (response[i].status == 4)
                        {
                            serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"><div class="row"><div class="col sp_icon"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="cap"></Span></div><div class="col"><p class="mb-0">' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="Completed"><span>Completed</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="mb-0"><a class="refundbtn">Refund</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail + '</span></td><td class="d-none">' + response[i].pincode + '</td></tr>');
                        }

                        if (response[i].spname != null) {
                            ProArr.push(response[i].spname);
                        }
                        CustArr.push(response[i].custName);
                    }

                    var DistinctCust = [...new Set(CustArr)];
                    var DistinctPro = [...new Set(ProArr)];

                    for (var i in DistinctCust) {
                        $("#Customer").append('<option value="' + DistinctCust[i] + '">' + DistinctCust[i]+'</option>');
                    }
                    for (var i in DistinctPro) {
                        $("#ServiceProvider").append('<option value="' + DistinctPro[i] + '">' + DistinctPro[i] + '</option>');
                    }

                    table = $('#serviceRequesttable').DataTable({
                        "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',

                        "pagingType": "full_numbers",
                        "searching": true,
                        "order": [],
                        "info": false,
                        "autoWidth": false,
                        'columnDefs': [{
                            'targets': [6],
                            'orderable': false,
                        }],

                        "language": {
                            "paginate": {
                                "first": false,
                                "next": '<i class="fas fa-angle-right"></i>',
                                "previous": '<i class="fas fa-angle-left"></i>',
                                "last": false,

                            },

                        }
                    });

                    
                    $('#serviceRequesttable tbody').on('click', '.action', function () {
                            $(".threeDotsubMenu").hide();
                            $(this).closest('.actionbutton').children('.threeDotsubMenu').toggle();
                    });

                    $('#serviceRequesttable tbody').on('click', '.cancleBtn', function () {
                        var CancleClickedRow = $(this).closest('tr').children(':first-child').text();
                        $("#CancleClickedId").val(CancleClickedRow);
                        $("#CancleModal").modal('show');
                    });

                    $('#serviceRequesttable tbody').on('click', '.serviceSummary', function () {
                        var clickedRow = $(this).parent().children(':first-child').text();
                        var ServicePro = $(this).parent().children(':nth-child(4)').html();
                        GetServiceSummary(clickedRow, ServicePro);
                    });

                    $('#serviceRequesttable tbody').on('click', '.EditAndReschedule', function () {
                        $('#EditAndRescheduleBtn').prop('disabled', true);
                        $('#EditAndRescheduleBtn').css('cursor', 'not-allowed');
                        $("#EditRescheduleModel").modal('show');
                        var ClickedserviceId = $(this).closest('tr').children(':first-child').text();
                        $("#serviceIdForReschedule").val(ClickedserviceId);
                        GetRescheduleRequest(ClickedserviceId);
                    });

                    $('#serviceRequesttable tbody').on('click', '.refundbtn', function () {
                        $('#RefundBtn').prop('disabled', true);
                        $('#RefundBtn').css('cursor', 'not-allowed');
                        $("#RefundErrAlert").empty();
                        $("#AmountInput").val('');
                        $("#RefundCalculaterAns").val('');
                        $("#RefundReason").val('');
                        $("#RefundModal").modal('show');
                        var ClickedRefundId = $(this).closest('tr').children(':first-child').text();
                        $("#RefundClickId").val(ClickedRefundId);
                        GetRefundData(ClickedRefundId);
                    });
                   

                    var tbl = $('#serviceRequesttable').DataTable();
                    $('#page-content-wrapper').on('click', '#search', function () {
                        var Customer = $("#Customer").val();
                        var ServiceProvider = $('#ServiceProvider').val();
                        var status = $('#status').val();
                        var serviceID = $("#serviceID").val();
                        var postalCode = $("#postalCode").val();
                        var email = $("#email").val();
                        var fromDate = $("#from-date").val();
                        var toDate = $("#to-date").val();
                        if (Customer != null) {
                            tbl.columns(2).search(Customer);
                        } else {
                            tbl.columns(2).search("");
                        }
                        if (ServiceProvider != null) {
                            tbl.columns(3).search(ServiceProvider);
                        } else {
                            tbl.columns(3).search("");
                        }
                        if (status != null) {
                            tbl.columns(5).search(status);
                        } else {
                            tbl.columns(5).search("");
                        }
                        if (serviceID != '') {
                            tbl.columns(0).search(serviceID);
                        } else {
                            tbl.columns(0).search("");
                        }
                        if (postalCode != '') {
                            tbl.columns(8).search(postalCode);
                        } else {
                            tbl.columns(8).search("");
                        }
                        if (email != '') {
                            tbl.columns(7).search(email);
                        } else {
                            tbl.columns(7).search("");
                        }
                        if (fromDate != '' && toDate != '') {
                            $.fn.dataTable.ext.search.push(
                                function (settings, data, dataIndex) {
                                    var min = new Date($("#from-date").val().split("-")[1] + "-" + $("#from-date").val().split("-")[0] + "-" + $("#from-date").val().split("-")[2]);
                                    var max = new Date($("#to-date").val().split("-")[1] + "-" + $("#to-date").val().split("-")[0] + "-" + $("#to-date").val().split("-")[2]);
                                    var date = new Date(data[1].split("-")[1] + "-" + data[1].split("-")[0] + "-" + data[1].split("-")[2]);
                                    console.log(min);
                                    console.log(max);
                                    console.log(date);
                                    if (
                                        (min === null && max === null) ||
                                        (min === null && date <= max) ||
                                        (min <= date && max === null) ||
                                        (min <= date && date <= max)
                                    ) {
                                        return true;
                                        console.log("ok");
                                    }
                                    return false;
                                        console.log("no");
                                }
                            );
                        } else {
                            tbl.columns(1).search("");
                        }
                        tbl.draw();
                    });

                    
                    $('#page-content-wrapper').on('click', '#clear', function () {
                        window.location.reload();
                    });
                    
                }, 300);
            },
        error:
            function (err) {
                console.error(err);
            },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 300);
        }
    });
}


function GetRescheduleRequest(x) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'RescheduleServiceId': x },
        url: '/Admin/GetRescheduleRequestData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                //console.log(response);
                $("#RescheduleDate").val(response.serviceDate);
                $("#rescheduleTime").val(response.serviceStartTime);
                $("#EditStreetName").val(response.addLine2);
                $("#EditHouseNumber").val(response.addLine1);
                $("#EditPincode").val(response.postalCode);
                $("#EditCity").val(response.city);
                $('#EditState').val(response.state);
                if (response.spId == null) {
                    $("#EditPincode").prop('disabled', false);
                    $("#EditCity").prop('disabled', true);
                } else {
                    $("#EditPincode").prop('disabled', true);
                    $("#EditCity").prop('disabled', true);
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}


function GetRefundData(x) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'RefundServiceId': x },
        url: '/Admin/GetRefundData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {

                $("#TotalAmount").text(response.totalAmount + " Rs.");
                $("#RefundAmount").text(response.refundAmount + " Rs.");
                $("#BalanceAmount").text(response.balanceAmount + " Rs.");
                if (response.refundAmount == 0) {
                    $('#RefundBtn').removeClass('d-none');
                } else {
                    $("#RefundErrAlert").empty();
                    $("#RefundErrAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">User has been already Refunded !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                    $('#RefundBtn').addClass('d-none');
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

$("#RefundCalculater").on('click', () => {
    if ($("#RefundMode").val() == "Percentage") {
        var RefundAmount = ($("#AmountInput").val() * $("#TotalAmount").text().split(' ')[0]) / 100;
        $("#RefundCalculaterAns").val(RefundAmount);
    } else {
        $("#RefundCalculaterAns").val($("#AmountInput").val());
    }
});


$("#RefundReason").keyup(() => {
    if ($("#RefundReason").val() == '' || !$("#refundError").hasClass('d-none')) {
        $('#RefundBtn').prop('disabled', true);
        $('#RefundBtn').css('cursor', 'not-allowed');
    } else {
        $('#RefundBtn').prop('disabled', false);
        $('#RefundBtn').css('cursor', 'pointer');
    }
});

$("#RefundBtn").on('click', () => {
    if ($("#RefundReason").val() == '' || !$("#refundError").hasClass('d-none')) {
        $("#RefundErrAlert").empty();
        $("#RefundErrAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Please Fill All Detail Properly !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
    } else {
        var obj = {};
        obj.id = $("#RefundClickId").val();
        obj.refundAmount = $("#RefundCalculaterAns").val();
        obj.comment = $("#RefundReason").val();
        $.ajax({
            type: 'Post',
            cache: false,
            data: obj,
            url: '/Admin/RefundPayment',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            success:
                function (response) {
                    if (response == "Refund is Greater than Actual Ammount") {
                        $("#RefundErrAlert").empty();
                        $("#RefundErrAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Refund Amount must be less than paid amount !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                    }
                    else if (response == "Already Refunded")
                    {
                        $("#RefundErrAlert").empty();
                        $("#RefundErrAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">User has been already Refunded !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                    }
                    else {
                        $("#RefundModal").modal("hide");
                        $('#SuccessActionModal').modal({
                            backdrop: 'static', // to prevent closing with click
                            keyboard: false  // to prevent closing with 
                        });
                        $("#SuccessActionModal").modal("show");
                    }
                },
            error:
                function (err) {
                    console.error(err);
                }
        });

    }
});

$("#EditAndRescheduleBtn").on('click', () => {
    var obj = {};
    obj.id = $("#serviceIdForReschedule").val();
        obj.serviceTime = $("#RescheduleDate").val() + " " + $("#rescheduleTime").val();
        obj.addressLine1 = $("#EditHouseNumber").val();
        obj.addressLine2 = $("#EditStreetName").val();
        obj.city = $("#EditCity").val();
        obj.state = $("#EditState").val();
        obj.postalCode = $("#EditPincode").val();
        obj.comments = $("#RescheduleReason").val();

    if ($("#RescheduleDate").val() == '' || $("#rescheduleTime").val() == '' || $("#EditHouseNumber").val() == '' || $("#EditStreetName").val() == '' || $("#EditCity").val() == '' || $("#EditState").val() == '' || $("#EditPincode").val() == '') {
        $("#RescheduleErrAlert").empty();
        $("#RescheduleErrAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Please Fill All Detail Properly !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
    } else {
        $.ajax({
            type: 'Post',
            cache: false,
            data: obj,
            url: '/Admin/PostRescheduleRequestData',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            beforeSend: function () {
                $(".loader-div").removeClass('d-none');
            },
            success:
                function (response) {
                    if (response == "UpdateSuccessfully") {
                        $("#EditRescheduleModel").modal('hide');
                        $('#SuccessActionModal').modal({
                            backdrop: 'static', // to prevent closing with click
                            keyboard: false  // to prevent closing with 
                        });
                        $("#SuccessActionModal").modal("show");
                    } else {
                        alert("please try again");
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
});

$("#EditPincode").keyup(function () {
    $("#pinErr").empty();
    $("#pinErr").text("Enter valid pincode");
    $("#EditCity").val('');
    $("#EditState").val('');
    var pinVal = $("#EditPincode").val();
    if (pinVal.length == 6) {
        $("#pinErr").empty();
        $.ajax({
            type: 'GET',
            cache: false,
            data: { 'pinVal': pinVal },
            url: '/Admin/CheckAvailableZip',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            success:
                function (response) {
                    if (response == "NotAvailable") {
                        $("#pinErr").text("Service not available in this pincode");
                    } else {
                        $("#EditCity").val(response.city);
                        $("#EditState").val(response.state);
                    }
                
            },
            error: (err) => {
                console.log(err);
            }

        });
    }
});


function CancleService() {
    var CancleClickedId = $("#CancleClickedId").val();
    $.ajax({
        type: "POST",
        url: '/Admin/CancleService',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'CancleClickedId': CancleClickedId },
        cache: false,
        success:
            function (response) {
                if (response == "Sucessfully") {
                    $("#CancleModal").modal("hide");
                    $('#SuccessActionModal').modal({
                        backdrop: 'static', // to prevent closing with click
                        keyboard: false  // to prevent closing with 
                    });
                    $("#SuccessActionModal").modal("show");
                } else {
                    alert("please try again");
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}


function GetServiceSummary(x, y) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'ReqServiceId': x },
        url: '/Admin/GetServiceSummaryData',
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

$("#SuccessActionModalBtn").on('click', () => {
    window.location.reload();
});


$("#rescheduleDate,#rescheduleTime").on('change',function () {
    $('#EditAndRescheduleBtn').prop('disabled', false);
    $('#EditAndRescheduleBtn').css('cursor', 'pointer');
});

$("#EditHouseNumber,#EditStreetName,#EditCity,#EditPincode,#RescheduleReason").keyup(function () {
    $('#EditAndRescheduleBtn').prop('disabled', false);
    $('#EditAndRescheduleBtn').css('cursor', 'pointer');
});



$(document).ready(function () {


    getData();

    $('#Customer,#ServiceProvider').select2();
    var date_input = $('#from-date, #to-date');
    date_input.datepicker({
        format: 'dd-mm-yyyy',
        autoclose: true,
    });

    var EditDate = $("#RescheduleDate");
    EditDate.datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        startDate: '+1d',
    });

})
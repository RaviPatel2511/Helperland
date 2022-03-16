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

//$("td").each(function() {
//    var tddata = $(this).html();
//    if(tddata == ""){
//        $(this).html("No Data");
//    }
//    else{

//    }
//});

//function sort(col, order) {
//	table.order([col, order]).draw();
//}


//  $('input[type=radio][name=sortOption]').change(function() {
//    if (this.value == 'ServiceId:Ascending') {
//        sort(0,"asc");
//    }
//    else if (this.value == 'ServiceId:Descending') {
//        sort(0,"desc");
//    }
//    else if (this.value == 'ServiceDate:Oldest') {
//        sort(1,"asc");
//    }
//    else if (this.value == 'ServiceDate:Latest') {
//        sort(1,"desc");
//    }
//    else if (this.value == 'Customer:AtoZ') {
//        sort(2,"asc");
//    }
//    else if (this.value == 'Customer:ZtoA') {
//        sort(2,"desc");
//    }
//    else if (this.value == 'ServiceProvider:AtoZ') {
//        sort(3,"asc");
//    }
//    else if (this.value == 'ServiceProvider:ZtoA') {
//        sort(3,"desc");
//    }
//    else if (this.value == 'Status:Ascending') {
//        sort(4,"asc");
//    }
//    else if (this.value == 'Status:Descending') {
//        sort(4,"desc");
//    }
//  });

////// PREVENT FFROM BACK BUTTON AFTER LOGOUT
////window.history.forward();
////function noBack() {
////    window.history.forward();
////}



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
                    var ServiceProvider = $("#ServiceProvider");
                    var Customer = $("#Customer");
                    serviceRequesttable.empty();
                    for (var i = 0; i < response.length; i++) {
                        ServiceProvider.append('<option value="' + response[i].spname + '">' + response[i].spname + '</option>');
                        Customer.append('<option value="' + response[i].custName + '">' + response[i].custName + '</option>');
                        if (response[i].status == 1)
                        {
                            serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="new"><span>New</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li><a>Edit & Reschedule</a></li><li class="mb-0"><a>Cancle</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail +'</span></td></tr>');
                        }
                        else if (response[i].status == 2)
                        {
                            serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"><div class="row"><div class="col sp_icon"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="cap"></Span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="pending"><span>Pending</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li><a>Edit & Reschedule</a></li><li class="mb-0"><a>Cancle</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail +'</span></td></tr>');
                        }
                        else if (response[i].status == 3)
                        {
                            if (response[i].providerId == null)
                            {
                                serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="Cancel"><span>Cancelled</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="mb-0"><a>Refund</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail +'</span></td></tr>');
                            }
                            else
                            {
                                serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"><div class="row"><div class="col sp_icon"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="cap"></Span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="Cancel"><span>Cancelled</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="mb-0"><a>Refund</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail +'</span></td></tr>');
                            }
                        }
                        else if (response[i].status == 4)
                        {
                            serviceRequesttable.append('<tr><td class="serviceSummary">' + response[i].serviceId + '</td><td class="serviceSummary"><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].serviceDate + '</span><span><img src="../image/upcoming_service/layer-14.png"> ' + response[i].serviceStartTime + '-' + response[i].serviceEndTime + '</span></td><td class="serviceSummary"><div class="custDetails"><div><img src="../image/upcoming_service/home.png" /></div><div class="custInfo"><span>' + response[i].custName + '</span><span>' + response[i].add1 + ' ' + response[i].add2 + ',' + '</span><span>' + response[i].city + ' ' + response[i].pincode + '</span></div></div></td><td class="serviceSummary"><div class="row"><div class="col sp_icon"><Span class="img_circle"><img src="../image/upcoming_service/' + response[i].avtar + '.png" alt="cap"></Span></div><div class="col"><p>' + response[i].spname + '</p><p><div class="Stars" id="rate1" style="--rating: ' + response[i].spRatings + ';"></div>&nbsp;' + response[i].spRatings + '</p></div></div></td><td class="serviceSummary">' + response[i].payment + ' Rs.</td><td class="Completed"><span>Completed</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="mb-0"><a>Refund</a></li></ul></div></td><td><span class="d-none">' + response[i].custEmail + ',' + response[i].proEmail +'</span></td></tr>');
                        }
                    }
                    console.log(response);     
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

                        $(this).closest('.actionbutton').children('.threeDotsubMenu').toggle();
                    });

                    $('#serviceRequesttable tbody').on('click', '.serviceSummary', function () {
                        var clickedRow = $(this).parent().children(':first-child').text();
                        var ServicePro = $(this).parent().children(':nth-child(4)').html();
                        GetServiceSummary(clickedRow, ServicePro);
                    });
                   

                    var tbl = $('#serviceRequesttable').DataTable();
                    $('#page-content-wrapper').on('click', '#search', function () {
                        var Customer = $("#Customer").val();
                        var ServiceProvider = $('#ServiceProvider').val();
                        var status = $('#status').val();
                        var serviceID = $("#serviceID").val();
                        var postalCode = $("#postalCode").val();
                        var email = $("#email").val();
                        if (Customer != null) {
                            tbl.columns(2).search(Customer);
                            console.log(Customer);
                        }
                        if (ServiceProvider != null) {
                            tbl.columns(3).search(ServiceProvider);
                        }
                        if (status != null) {
                            tbl.columns(5).search(status);
                        }
                        if (serviceID != '') {
                            tbl.columns(0).search(serviceID);
                        }
                        if (postalCode != '') {
                            tbl.columns(2).search(postalCode);
                        }
                        if (email != '') {
                            tbl.columns(7).search(email);
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


$(document).ready(function () {

    getData();

    var date_input = $('input[name="date"]');
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    date_input.datepicker({
        format: 'mm/dd/yyyy',
        container: container,
        autoclose: true,
    });

})


function GetData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/GetServiceScheduleData',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    //console.log(response);
                    var Service = [];
                    var a = 0;
                    for (var i = 0; i < response.length; i++) {
                        var ServiceInfo = new Object();
                        ServiceInfo.id = response[i].serviceId,
                            ServiceInfo.title = response[i].serviceStartTime + " " + response[i].serviceEndTime,
                            ServiceInfo.start = response[i].serviceDate.split("-")[2] + "-" + response[i].serviceDate.split("-")[1] + "-" + response[i].serviceDate.split("-")[0],
                            ServiceInfo.color = response[i].color,
                            Service[a] = ServiceInfo;
                        a++;
                    }
                    $('#calendar').fullCalendar({
                        header: {
                            left: 'prev,next title',
                            right: ''
                        },
                        viewRender: function (view) {

                            $('#calendar').fullCalendar('removeEvents');
                            $('#calendar').fullCalendar('addEventSource', Service);
                        },
                        eventClick: function (info) {
                            GetServiceSummary(info.id);
                        }
                        
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
                $("#ModalOpenBtn").click();

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}




$(document).ready(function () {
    GetData();

    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[2].classList.add("active");
});
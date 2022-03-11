

function GetData(){
        $.ajax({
            type: 'GET',
            cache: false,
            url: '/Provider/GetDashboardData',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            beforeSend: function () {
                $(".loader-div").removeClass('d-none');
            },
            success:
                function (response) {
                    setTimeout(function () {
                        $("#NewServiceCount").text(response.totalNewService);
                        $("#UpcomingServiceCount").text(response.totalUpcomingService);
                        $("#CompleteServiceCount").text(response.totalCompleteService);
                        console.log(response);

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
};



$(document).ready(function () {

    GetData();

    var selector = '#sidebar-wrapper a';
    $(selector).removeClass('active');
    $(selector)[0].classList.add("active");
});
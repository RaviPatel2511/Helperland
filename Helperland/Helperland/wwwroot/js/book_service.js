const Sidenav = document.querySelector('.sideNav');
const closebtn = document.querySelector('.closebtn');
const mobileNav = document.querySelector('.mobileNav');
const menubtn = document.querySelector('.menubtn');
const nav = document.querySelector("nav");
const loginbtn = document.querySelector('.loginbtn');
const AddNewAddressbtn = document.querySelector('#AddNewAddress');
const newAddressForm = document.querySelector('.newAddressForm');
const cancelAdressform = document.querySelector('#cancelAdressform');

menubtn.addEventListener('click', () => {
	Sidenav.classList.add('open');
})
closebtn.addEventListener('click', () => {
	Sidenav.classList.remove('open')
})

window.onclick = function (event) {
	if (event.target == Sidenav) {
		Sidenav.classList.remove('open')
	}
}

// ADDRESS PICKER
$(document).ready(function () {
    var date_input = $('#scheduleDate');
    date_input.datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        startDate: '+1d',
    });
});

//FOR ADD NEW ADDRESS BTN
AddNewAddressbtn.addEventListener('click', () => {
	newAddressForm.classList.remove('d-none');
	AddNewAddressbtn.classList.add('d-none');
})

cancelAdressform.addEventListener('click', () => {
	AddNewAddressbtn.classList.remove('d-none');
	newAddressForm.classList.add('d-none');
})

$('#bookingSuccessfulyDoneBtn').click(function () {
$('#bookingSuccessfulyDone').modal({
    backdrop: 'static', // to prevent closing with click
    keyboard: false  // to prevent closing with 
});
});

//FOR PAYMENT SUMMARY
function PaymentSummaryTable() {

	var ExtraHr,basicHr,cabinet, fridge, oven, laundary, window = 0;
	$(".psDatetime").text($('#scheduleDate').val() + "   " + $("#scheduleTime").val());
	$('.psBasicHrs').text($('#StayHour').val() + " " + "Hrs");

	if ($('#cabinet').is(':checked')) {
		$(".psInsideCabinet").removeClass('d-none');
		cabinet = 0.50;
	} else {
		$(".psInsideCabinet").addClass('d-none');
		cabinet = 0;
	}

	if ($('#fridge').is(':checked')) {
		$(".psInsideFridge").removeClass('d-none');
		fridge = 0.50;
	} else {
		$(".psInsideFridge").addClass('d-none');
		fridge = 0;
	}

	if ($('#oven').is(':checked')) {
		$(".psInsideOven").removeClass('d-none');
		oven = 0.50;
	} else {
		$(".psInsideOven").addClass('d-none');
		oven = 0;
	}

	if ($('#laundary').is(':checked')) {
		$(".psLaundary").removeClass('d-none');
		laundary = 0.50;
	} else {
		$(".psLaundary").addClass('d-none');
		laundary = 0;
	}

	if ($('#window').is(':checked')) {
		$(".psWindows").removeClass('d-none');
		window = 0.50;
	} else {
		$(".psWindows").addClass('d-none');
		window = 0;
	}
	ExtraHr = cabinet + fridge + oven + laundary + window;
	basicHr = parseFloat($('#StayHour').val());
	var totalHr = basicHr + ExtraHr;
	$('.psTotalHour').text(totalHr+ " " + "Hrs");

	$(".psTotalPayment").text(parseFloat(totalHr * 10)+ " " + "Rs.");
}

// FIRST TAB POST REQUEST
function checkZip() {
    var Zipdata = $('#setupService').serialize();
    $('#NewPostalCode').val($('#postalCode').val());
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Customer/IsAvailableZip',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: Zipdata,
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    if (response.value == "false") {
                        $('#errormsg').text("We are not providing service in this area. We will notify you if any helper would start working near your area.");
                        $('#errorAlert').removeClass('d-none');
                    }
                    else if (response.value == "Invalid") {
                        $('#errormsg').text("Please Enter Valid ZipCode !");
                        $('#errorAlert').removeClass('d-none');
                    }
                    else {
                        secondTab();
                        $('#NewCity').val(response.value);
                        if (!$('#errorAlert').hasClass('d-none')) {
                            $('#errorAlert').addClass('d-none');
                        }
                    }
                }, 1000);
            },
        error:
            function (err) {
                console.error(err);
            },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 1000);
        }
    });
}

// SECOND TAB POST REQUEST
function ScheduleService() {
    var ScheduleData = $('#scheduleData').serialize();
    $.ajax({
        type: 'Post',
        cache: false,
        url: '/Customer/ScheduleService',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: ScheduleData,
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    if (response.value == "true") {
                        getAddressOfUser();
                        getFavPro();
                        thirdTab();
                        if (!$('#DateErrorAllert').hasClass('d-none')) {
                            $('#DateErrorAllert').addClass('d-none');
                        }
                    } else {

                        $('#DateErrorAllert').removeClass("d-none");
                    }
                }, 1000);
            },
        error:
            function (err) {
                console.error(err);
            },
        complete: function () {
            setTimeout(function () {
                $(".loader-div").addClass('d-none');
            }, 1000);
        }
    });
}

// THIRD TAB POST REQUEST
function getAddressOfUser() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/getAddressOfUser',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                var address_picker = $('.address-picker');
                address_picker.empty();
                for (var i = 0; i < response.length; i++) {
                    if (response[i].isDefault == true) {
                        address_picker.append(' <li> <label> <input type="radio" name="addressRadio" id="addressRadio" value="' + response[i].id + '" checked /> <span class="address-block"> <b>Address : </b> ' + response[i].addressline1 + ' ' + response[i].addressline2 + ', ' + response[i].city + ' ' + response[i].postalcode + ' </span > <span> <b>Phone Number :</b>  ' + response[i].mobile + '   </span> <span class="radio-pointer"></span> </label > </li > ')
                    } else {
                        address_picker.append(' <li> <label> <input type="radio" name="addressRadio" id="addressRadio" value="' + response[i].id + '" /> <span class="address-block"> <b>Address : </b> ' + response[i].addressline1 + ' ' + response[i].addressline2 + ', ' + response[i].city + ' ' + response[i].postalcode + ' </span > <span> <b>Phone Number :</b>  ' + response[i].mobile + '   </span> <span class="radio-pointer"></span> </label > </li > ')
                    }
                }

            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

function getFavPro() {
    console.log("ok");
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/GetFavPro',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                var favProvider = $('.favProvider');
                favProvider.empty();
                console.log(response);
                if (response.length > 0) {
                    $("#ChooseFavProvider").removeClass('d-none');
                    for (var i = 0; i < response.length; i++) {
                            favProvider.append('<div><label><input class="d-none" type="radio" name="FavProRadio" value="' + response[i].proId + '"/><img src="../image/Upcoming_Service/' + response[i].avtar + '.png" /><p class="mb-2">' + response[i].name + '</p><span class="btn btn-outline-dark" type="button">Select</span></label></div>')
                            console.log("v");
                    }
                } else {
                    $("#ChooseFavProvider").addClass('d-none');
                }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

function ClearAddressForm() {
    $('#NewHouseNo').val("");
    $('#NewStreetName').val("");
    $('#NewMobile').val("");
    if (!$('#AddAddressErrorAllert').hasClass('d-none')) {
        $('#AddAddressErrorAllert').addClass('d-none');
    }
}

function AddNewUserAddress() {
    var IsValidMobile = /^([ 0-9]){10}$/.test($('#NewMobile').val());
    if ($('#NewHouseNo').val() == "" || $('#NewStreetName').val() == "" || $('#NewMobile').val() == "") {
        $("#AddAddressErrorAllert").removeClass('d-none').text("Please Fill All Field !");
    }
    else if (!IsValidMobile) {
        $("#AddAddressErrorAllert").removeClass('d-none').text("Please Enter Valid Mobile !");
    }
    else {
        if (!$('#AddAddressErrorAllert').hasClass('d-none')) {
            $('#AddAddressErrorAllert').addClass('d-none');
        }
        var NewAddress = {};
        NewAddress.AddLine1 = $('#NewHouseNo').val();
        NewAddress.AddLine2 = $('#NewStreetName').val();
        NewAddress.NewCity = $('#NewCity').val();
        NewAddress.NewMobile = $('#NewMobile').val();
        NewAddress.NewPostal = $('#NewPostalCode').val();

        $.ajax({
            type: "POST",
            url: '/Customer/AddNewAddressToDB',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: { 'AddLine1': NewAddress.AddLine1, 'AddLine2': NewAddress.AddLine2, 'NewCity': NewAddress.NewCity, 'NewMobile': NewAddress.NewMobile, 'NewPostal': NewAddress.NewPostal },
            cache: false,
            success:
                function (response) {
                    if (response.value == "true") {
                        getAddressOfUser();
                        ClearAddressForm();
                        $('.newAddressForm').addClass('d-none');
                        $('#AddNewAddress').removeClass('d-none');
                    } else {
                        alert("fail");
                    }
                },
            error:
                function (err) {
                    console.error(err);
                }
        });
    }
}

$("#step3btn").on("click", function () {
    var isValid = $("input[name=addressRadio]").is(":checked");
    if (isValid) {
        if (!$('#AddressErrorAlert').hasClass('d-none')) {
            $('#AddressErrorAlert').addClass('d-none');
        }
        fourthTab();
    }
    else {
        $("#AddressErrorAlert").removeClass("d-none");
    }
});


// FINAL TAB POST REQUEST
function CompleteBooking() {

    var NewBookingRequest = {};
    NewBookingRequest.ServiceZipCode = $('#postalCode').val();
    NewBookingRequest.ServiceDateTime = $('#scheduleDate').val() + " " + $("#scheduleTime").val();
    NewBookingRequest.ServiceHours = $('#StayHour').val();
    NewBookingRequest.Comments = $('#comments').val();
    NewBookingRequest.HavePets = $('#pets').is(":checked");
    NewBookingRequest.ExtraServiceHours = 0;
    NewBookingRequest.cabinet = $('#cabinet').is(":checked");
    NewBookingRequest.fridge = $('#fridge').is(":checked");
    NewBookingRequest.oven = $('#oven').is(":checked");
    NewBookingRequest.laundary = $('#laundary').is(":checked");
    NewBookingRequest.window = $('#window').is(":checked");
    NewBookingRequest.SelectedAddressId = $(".address-picker input[name='addressRadio']:checked").val();
    NewBookingRequest.FavProId = $(".favProvider input[name='FavProRadio']:checked").val();

    if (NewBookingRequest.cabinet) {
        NewBookingRequest.ExtraServiceHours += 0.5;
    }
    if (NewBookingRequest.fridge) {
        NewBookingRequest.ExtraServiceHours += 0.5;
    }
    if (NewBookingRequest.oven) {
        NewBookingRequest.ExtraServiceHours += 0.5;
    }
    if (NewBookingRequest.laundary) {
        NewBookingRequest.ExtraServiceHours += 0.5;
    }
    if (NewBookingRequest.window) {
        NewBookingRequest.ExtraServiceHours += 0.5;
    }
    $.ajax({
        type: 'POST',
        url: '/Customer/CompleteBooking',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: NewBookingRequest,
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                if (response == "false")
                {
                    alert("booking not done Please try again !");
                }
                else if (response == "AnotherServiceBooked")
                {
                    $('#RescheduleError').modal({
                        backdrop: 'static', // to prevent closing with click
                        keyboard: false  // to prevent closing with 
                    });
                    $("#RescheduleError").modal('show');
                }
                else
                {
                        $('#CoonfirmBookReferenceId').text(response);
                        $('#bookingSuccessfulyDoneBtn').click();
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

function firstTab() {
    $("#pills-service-tab").removeClass("visited-tab").addClass("active");
    $("#pills-schedule-tab").removeClass("visited-tab").removeClass("active");
    $("#pills-details-tab").removeClass("visited-tab").removeClass("active");
    $("#pills-payment-tab").removeClass("visited-tab").removeClass("active");

    $("#pills-service").addClass("show active");
    $("#pills-schedule").removeClass("show active");
    $("#pills-details").removeClass("show active");
    $("#pills-payment").removeClass("show active");

    $("#pills-schedule-tab").css("pointer-events", "none");
    $("#pills-details-tab").css("pointer-events", "none");
    $("#pills-payment-tab").css("pointer-events", "none");

}
function secondTab() {
    $("#pills-service-tab").removeClass("active").addClass("visited-tab");
    $("#pills-schedule-tab").removeClass("visited-tab").addClass("active");
    $("#pills-details-tab").removeClass("visited-tab").removeClass("active");
    $("#pills-payment-tab").removeClass("visited-tab").removeClass("active");

    $("#pills-service").removeClass("show active");
    $("#pills-schedule").addClass("show active");
    $("#pills-details").removeClass("show active");
    $("#pills-payment").removeClass("show active");


    $("#pills-details-tab").css("pointer-events", "none");
    $("#pills-payment-tab").css("pointer-events", "none");
}
function thirdTab() {
    $("#pills-service-tab").removeClass("active").addClass("visited-tab");
    $("#pills-schedule-tab").removeClass("active").addClass("visited-tab");
    $("#pills-details-tab").removeClass("visited-tab").addClass("active");
    $("#pills-payment-tab").removeClass("visited-tab").removeClass("active");

    $("#pills-service").removeClass("show active");
    $("#pills-schedule").removeClass("show active");
    $("#pills-details").addClass("show active");
    $("#pills-payment").removeClass("show active");

    $("#pills-service-tab").css("pointer-events", "auto");
    $("#pills-schedule-tab").css("pointer-events", "auto");
    $("#pills-details-tab").css("pointer-events", "auto");
    $("#pills-payment-tab").css("pointer-events", "none");
}
function fourthTab() {
    $("#pills-service-tab").removeClass("active").addClass("visited-tab");
    $("#pills-schedule-tab").removeClass("active").addClass("visited-tab");
    $("#pills-details-tab").removeClass("active").addClass("visited-tab");
    $("#pills-payment-tab").removeClass("visited-tab").addClass("active");

    $("#pills-service").removeClass("show active");
    $("#pills-schedule").removeClass("show active");
    $("#pills-details").removeClass("show active");
    $("#pills-payment").addClass("show active");
}
firstTab();

$("#postalCode").keyup(function () {
    if ($('#postalCode').val().length > 5) {
        $('#PostalCodeBtn').prop('disabled', false);
        $('#PostalCodeBtn').css('cursor', 'pointer');
    } else {
        $('#PostalCodeBtn').prop('disabled', true);
        $('#PostalCodeBtn').css('cursor', 'not-allowed');
    }
});



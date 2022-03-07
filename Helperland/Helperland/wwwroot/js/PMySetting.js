function getUserDetails() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Provider/getUserDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                $("#fname").val(response.fname);
                $("#lname").val(response.lname);
                $("#email").val(response.email);
                $("#mobile").val(response.mobile);
                $("#nationnalityId").val(response.nationnalityId);
                $("#EditStreetName").val(response.addressline2);
                $("#EditHouseNo").val(response.addressline1);
                $("#EditPostalCode").val(response.postalcode);
                $("#EditCity").val(response.city);
                $("#EditState").val(response.city)
                if (response.avtar != null) {
                    $("#avtarPreviewImg").attr("src", "../image/upcoming_service/" + response.avtar + ".png");
                }
                if (response.isActive == true) {
                    $("#status").text("Active");
                    $("#status").css('color', '#67b644');
                } else {
                    $("#status").text("Not Active");
                    $("#status").css('color', 'red');
                }
                var $radios1 = $('input:radio[name=gender]');
                if ($radios1.is(':checked') === false) {
                    $radios1.filter('[value=' + response.gender + ']').prop('checked', true);
                }
                var $radios2 = $('input:radio[name=avtar]');
                if ($radios2.is(':checked') === false) {
                    $radios2.filter('[value=' + response.avtar + ']').prop('checked', true);
                }
                var dob = response.dob;
                if (dob != null) {
                    var dobfulldatearr = dob.split("T");
                    var dobdatearr = dobfulldatearr[0].split("-");
                    $(".date-dropdowns .day").val(dobdatearr[2]);
                    $(".date-dropdowns .month").val(dobdatearr[1]);
                    $(".date-dropdowns .year").val(dobdatearr[0]);
                    $("#userDob").val(dobfulldatearr[0]);
                }

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
}

$('input[type=radio][name=avtar]').change(function () {
    if (this.value == 'cap') {
        $("#avtarPreviewImg").attr("src", "../image/upcoming_service/cap.png");
    }
    else if (this.value == 'woman') {
        $("#avtarPreviewImg").attr("src", "../image/upcoming_service/woman.png");
    }
    else if (this.value == 'car') {
        $("#avtarPreviewImg").attr("src", "../image/upcoming_service/car.png");
    }
    else if (this.value == 'iron') {
        $("#avtarPreviewImg").attr("src", "../image/upcoming_service/iron.png");
    }
    else if (this.value == 'man') {
        $("#avtarPreviewImg").attr("src", "../image/upcoming_service/man.png");
    }
    else if (this.value == 'ship') {
        $("#avtarPreviewImg").attr("src", "../image/upcoming_service/ship.png");
    }
});

$("#EditPostalCode").keyup(function () {
    $("#EditCity").val('');
    $("#EditState").val('');
    $("#EditinvalidZipError").removeClass("d-none");
    var pinVal = $("#EditPostalCode").val();
    if (pinVal.length == 6) {
        $.ajax({
            url: "https://api.postalpincode.in/pincode/" + pinVal,
            method: "GET",
            dataType: "json",
            cache: false,
            success: (data) => {
                console.log(data);
                if (data[0].Status == "Error") {
                    $("#EditinvalidZipError").removeClass("d-none");
                } else if (data[0].Status == "Success") {
                    $("#EditCity").val(data[0].PostOffice[0].District);
                    $("#EditState").val(data[0].PostOffice[0].State);
                    $("#EditinvalidZipError").addClass("d-none");
                }
            },
            error: (err) => {
                console.log(err);
            }

        });
    }
});

function updateMyDetails() {
    var UpdateDetails = {};
    UpdateDetails.fname = $("#fname").val();
    UpdateDetails.lname = $("#lname").val();
    UpdateDetails.mobile = $("#mobile").val();
    UpdateDetails.dob = $("#userDob").val();
    UpdateDetails.nationnalityId = $("#nationnalityId").val();
    UpdateDetails.gender = $("input[name='gender']:checked").val();
    UpdateDetails.avtar = $("input[name='avtar']:checked").val();
    UpdateDetails.addressline1 = $("#EditHouseNo").val();
    UpdateDetails.addressline2 = $("#EditStreetName").val();
    UpdateDetails.postalcode = $("#EditPostalCode").val();
    UpdateDetails.city = $("#EditCity").val();
    UpdateDetails.state = $("#EditState").val();
    UpdateDetails.email = $("#email").val();

    if ($("#fname").val() == '' || $("#lname").val() == '' || $("#mobile").val() == '' || $("input[name='avtar']:checked").val() == '' || $("#EditHouseNo").val() == '' || $("#EditStreetName").val() == '' || $("#EditPostalCode").val() == '' || $("#EditCity").val() == '' || $("#mobile").val().length > 10 || $("#mobile").val().length < 10 ) {
        $("#updateDetailsAllert").empty();
        $("#updateDetailsAllert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Please Fill All Detail Properly !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Provider/updateMyDetails',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: UpdateDetails,
            success:
                function (response) {
                    if (response == "updateSuccessfully") {
                        $("#updateDetailsAllert").empty();
                        $("#updateDetailsAllert").append('<div class="alert alert-success alert-dismissible fade show" role="alert">Your details were Updated Successfully !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                        
                        window.location.reload();
                    }
                    else {
                        $("#updateDetailsAllert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Some error occur Please Try Again !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>')
                    }
                    console.log("OK");
                },
            error:
                function (err) {
                    console.error(err);
                }
        });
    }
}


function updatePassword() {
    var UpdatePass = {};
    UpdatePass.oldPassword = $("#oldPass").val();
    UpdatePass.password = $("#newPass").val();
    UpdatePass.confirmPassword = $("#confirmPass").val();
    if ($("#oldPass").val() == '' || $("#newPass").val() == '' || $("#confirmPass").val() == '') {
        $("#passUpdateAlert").empty();
        $("#passUpdateAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Please Fill All Details !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Provider/UpdatePassword',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: UpdatePass,
            beforeSend: function () {
                $(".loader-div").removeClass('d-none');
            },
            success:
                function (response) {
                    setTimeout(function () {
                        if (response == "PasswordUpdate") {
                            $("#passUpdateAlert").empty();
                            $("#passUpdateAlert").append('<div class="alert alert-success alert-dismissible fade show" role="alert">Password update Successfully !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                            $("#oldPass").val("");
                            $("#newPass").val("");
                            $("#confirmPass").val("");
                            $('#updatePassBtn').prop('disabled', true);
                            $('#updatePassBtn').css('cursor', 'not-allowed');
                        } else {
                            $("#passUpdateAlert").empty();
                            $("#passUpdateAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Old password not match Please try again !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                        }
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
}

$("#newPass").keyup(function () {
    if ($(this).val() == $("#oldPass").val()) {
        $("#passValidationError").html('New Password should be different from current password');
        $('#updatePassBtn').prop('disabled', true);
        $('#updatePassBtn').css('cursor', 'not-allowed');
    } else {
        var data = $(this).val();
        var regx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,14}$/;
        if (data.match(regx)) {
            $("#passValidationError").html('');
            $('#updatePassBtn').prop('disabled', false);
            $('#updatePassBtn').css('cursor', 'pointer');
        } else {
            $("#passValidationError").html('Password must contain at least 1 capital letter, 1 small letter, 1 number and one special character. Password length must be in between 6 to 14 characters');
            $('#updatePassBtn').prop('disabled', true);
            $('#updatePassBtn').css('cursor', 'not-allowed');
        }
    }

});
$("#confirmPass,#newPass").keyup(function () {
    if ($('#newPass').val() != $('#confirmPass').val()) {
        $('#confirmPassError').html('Password doesnot match');
        $('#updatePassBtn').prop('disabled', true);
        $('#updatePassBtn').css('cursor', 'not-allowed');
    } else {
        $('#confirmPassError').html('');
        if ($("#newPass").val() == $("#oldPass").val()) {
            $('#updatePassBtn').prop('disabled', true);
            $('#updatePassBtn').css('cursor', 'not-allowed');
        } else {
            $('#updatePassBtn').prop('disabled', false);
            $('#updatePassBtn').css('cursor', 'pointer');
        }
    }
});



$(document).ready(function () {
    $("#userDob").dateDropdowns({
        submitFieldName: 'userDob',
        minAge: 18,
        submitFormat: "dd/mm/yyyy"
    });

    getUserDetails();
});
function getUserDetails() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/getUserDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                $("#fname").val(response.fname);
                $("#lname").val(response.lname);
                $("#email").val(response.email);
                $("#mobile").val(response.mobile);
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
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

function updateMyDetails() {
    var UpdateDetails = {};
    UpdateDetails.fname = $("#fname").val();
    UpdateDetails.lname = $("#lname").val();
    UpdateDetails.email = $("#email").val();
    UpdateDetails.mobile = $("#mobile").val();
    UpdateDetails.dob = $("#userDob").val();
    UpdateDetails.languageid = $("#languageId").val();
    if ($("#fname").val() == '' || $("#lname").val() == '' || $("#email").val() == '' || $("#mobile").val() == '')
    {
        $("#updateDetailsAllert").empty();
        $("#updateDetailsAllert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Please Fill All Detail !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
    }
    else
    {
    $.ajax({
        type: 'POST',
        url: '/Customer/updateMyDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: UpdateDetails,
        success:
            function (response) {
                    if (response == "updateSuccessfully") {
                        $("#updateDetailsAllert").empty();
                        $("#updateDetailsAllert").append('<div class="alert alert-success alert-dismissible fade show" role="alert">Your details were Updated Successfully !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                        $('#updateDetailsBtn').prop('disabled', true);
                        $('#updateDetailsBtn').css('cursor', 'not-allowed');
                        window.location.reload();
                    }
                    else {
                        $("#updateDetailsAllert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Some error occur Please Try Again !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>')
                    }
            },
        error:
            function (err) {
                console.error(err);
            }
    });
    }
}


function getUserAddress() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Customer/getUserAddress',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                var userAddressDetailsTbl = $("#userAddressDetailsTbl");
                userAddressDetailsTbl.empty();
                for (var i = 0; i < response.length; i++) {
                    userAddressDetailsTbl.append('<tr><td class="d-none">' + response[i].id+'</td><td class="userAddressDetails"><span><b>Address : </b><span class="userAddress">' + response[i].addressline1 + ' ' + response[i].addressline2 + ' ' + response[i].city + ' , ' + response[i].postalcode + '</span></span><span><b>Phone Number : </b><span class="userMobile">' + response[i].mobile + '</span></span></td><td class="text-center action-buttons"><a title="Edit" class="EditAdd"><img src="../image/service_history/edit-icon.png" /></a> <a title="Delete" class="DeleteAdd"><img src="../image/service_history/delete-icon.png" /></a></td></tr>');
                }

                    $(".EditAdd").click(function () {
                     $("#EditinvalidZipError").addClass("d-none");
                    $("#EditAddressModel").modal("show");
                    var ClickedserviceId = $(this).parent().parent().children(':first-child').text();
                    $("#editAddServiceId").val(ClickedserviceId);
                    getAddIntoModel(ClickedserviceId);

                });

                $(".DeleteAdd").click(function () {
                    $("#DeleteAddressModel").modal("show");
                    var ClickserviceId = $(this).parent().parent().children(':first-child').text();
                    $("#DeleteAddServiceId").val(ClickserviceId);
                });

                    ShowAddNum();


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

function getAddIntoModel(x) {
    $.ajax({
        type: 'GET',
        cache: false,
        data: { 'ReqAddId': x },
        url: '/Customer/EditAddGetReq',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                $("#EditStreetName").val(response.addline2);
                $("#EditHouseNo").val(response.addline1);
                $("#EditPostalCode").val(response.postalcode);
                $("#EditCity").val(response.city);
                $("#EditMobile").val(response.mobile);
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}

var NumRow = 0;

function ShowAddNum() {

    NumRow = 5;

    var $table = $('#myAddressTbl').find('tbody');
    var TotalAddRow = $table.find('tr').length;

    $table.find('tr:gt(' + (NumRow - 1) + ')').hide();

    if (NumRow > TotalAddRow) {
        $("#shownRow").text(TotalAddRow);
    } else {
        $("#shownRow").text(NumRow);
    }
    $("#totalRow").text(TotalAddRow);
}

$("#loadMoreAddBtn").click(() => {

    NumRow += 5;

    var $table = $('#myAddressTbl').find('tbody');
    var TotalAddRow = $table.find('tr').length;


    if (NumRow > TotalAddRow) {
        NumRow = TotalAddRow;
    }

    $("#shownRow").text(NumRow);
    $table.find('tr:lt(' + NumRow + ')').show();
});

function EditAdd() {
    if ($("#EditStreetName").val() == '' || $("#EditHouseNo").val() == '' || $("#EditPostalCode").val() == '' || $("#EditCity").val() == '' || $("#EditMobile").val() == '') {
    $("#EditAddAlert").removeClass('d-none');
    } else {
    $("#EditAddAlert").addClass('d-none');
    var UpdateAdd = {};
    UpdateAdd.id = $("#editAddServiceId").val();
    UpdateAdd.addressline2 = $("#EditStreetName").val();
    UpdateAdd.addressline1 = $("#EditHouseNo").val();
    UpdateAdd.postalcode = $("#EditPostalCode").val();
    UpdateAdd.city = $("#EditCity").val();
    UpdateAdd.mobile = $("#EditMobile").val();

    $.ajax({
        type: 'Post',
        cache: false,
        data: UpdateAdd,
        url: '/Customer/EditAddPostReq',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                $('#EditAddBtn').prop('disabled', true);
                $('#EditAddBtn').css('cursor', 'not-allowed');
                $('#EditAddressModel').modal('hide');
                $('#EditAddSuccessAlert').removeClass('d-none');
                getUserAddress();
                setTimeout(function () {
                    $('#EditAddSuccessAlert').addClass('d-none');
                }, 3000);
            },
        error:
            function (err) {
                console.error(err);
            }
    });
    }
}

$("#EditPostalCode").keyup(function () {
    $("#EditCity").val('');
    $("#EditState").val('');
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

function DeleteAdd() {
    var deletAddId = $("#DeleteAddServiceId").val();
    $.ajax({
        type: 'Post',
        cache: false,
        data: {'deletAddId' :deletAddId },
        url: '/Customer/DeleteAddPostReq',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success:
            function (response) {
                $('#DeleteAddressModel').modal('hide');
                $('#DeleteAddAlert').removeClass('d-none');
                getUserAddress();
                setTimeout(function () {
                    $('#DeleteAddAlert').addClass('d-none');
                }, 3000);
            },
        error:
            function (err) {
                console.error(err);
            }
    });
}
function AddNewAdd() {
    if ($("#NewHouseNo").val() == '' || $("#NewStreetName").val() == '' || $("#NewCity").val() == '' || $("#NewPostalCode").val() == '' || $("#NewMobile").val() == '') {
        $("#AddNewAddAlert").removeClass('d-none');
    } else {
    $("#AddNewAddAlert").addClass('d-none');
    var newAdd = {};
    newAdd.addressline1 = $("#NewHouseNo").val();
    newAdd.addressline2 = $("#NewStreetName").val();
    newAdd.city = $("#NewCity").val();
    newAdd.postalcode = $("#NewPostalCode").val();
    newAdd.mobile = $("#NewMobile").val();
    newAdd.state = $("#NewState").val();
    $.ajax({
        type: 'POST',
        url: '/Customer/AddNewAdd',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: newAdd,
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                    if (response == "AddSuccessfully") {
                        getUserAddress();
                           $("#AddNewAddressModel").modal('hide');
                        $("#updateAddAllert").empty();
                        $("#updateAddAllert").append('<div class="alert alert-success alert-dismissible fade show" role="alert">Address added Successfully !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
                        $('#addNewAddBtn').prop('disabled', true);
                        $('#addNewAddBtn').css('cursor', 'not-allowed');
                        getUserDetails();
                        $("#NewHouseNo").val('');
                        $("#NewStreetName").val('');
                        $("#NewCity").val('');
                        $("#NewPostalCode").val('');
                        $("#NewMobile").val('');
                        $("#NewState").val('')
                    }
                    else {
                        $("#updateAddAllert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Some error occur Please Try Again !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>')
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
}

$("#NewPostalCode").keyup(function () {
    $("#NewCity").val('');
    $("#NewState").val('');
    var pinVal = $("#NewPostalCode").val();
    if (pinVal.length == 6) {
        $.ajax({
            url: "https://api.postalpincode.in/pincode/" + pinVal,
            method: "GET",
            dataType: "json",
            cache: false,
            success: (data) => {
                console.log(data);
                if (data[0].Status == "Error") {
                    $("#AddinvalidZipError").removeClass("d-none");
                } else if (data[0].Status == "Success") {
                    $("#NewCity").val(data[0].PostOffice[0].District);
                    $("#NewState").val(data[0].PostOffice[0].State);
                    $("#AddinvalidZipError").addClass("d-none");
                }
            },
            error: (err) => {
                console.log(err);
            }

        });
    }
});

function updatePassword() {
    var UpdatePass = {};
    UpdatePass.oldPassword = $("#oldPass").val();
    UpdatePass.password = $("#newPass").val();
    UpdatePass.confirmPassword = $("#confirmPass").val();
    if ($("#oldPass").val() == '' || $("#newPass").val() == '' || $("#confirmPass").val() == '') {
        $("#passUpdateAlert").empty();
        $("#passUpdateAlert").append('<div class="alert alert-danger alert-dismissible fade show" role="alert">Please Fill All Details !<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
    }
    else
    {
    $.ajax({
        type: 'POST',
        url: '/Customer/UpdatePassword',
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
$("#fname,#lname,#userDob,#languageId,#mobile").keyup(function () {
    if ($("#fname").val() == '' || $("#lname").val() == '' || $("#mobile").val() == '' || $("#mobile").val().length > 10 || $("#mobile").val().length < 10) {
        $('#updateDetailsBtn').prop('disabled', true);
        $('#updateDetailsBtn').css('cursor', 'not-allowed');
    } else {
    $('#updateDetailsBtn').prop('disabled', false);
    $('#updateDetailsBtn').css('cursor', 'pointer');
    }
});
$("#NewHouseNo,#NewStreetName,#NewPostalCode,#NewCity,#NewMobile").keyup(function () {
    if ($("#NewHouseNo").val() == '' || $("#NewStreetName").val() == '' || $("#NewPostalCode").val() == '' || $("#NewMobile").val().length > 10 || $("#NewMobile").val().length < 10 || $("#NewPostalCode").val().length > 6 || $("#NewPostalCode").val().length < 6) {
        $('#addNewAddBtn').prop('disabled', true);
        $('#addNewAddBtn').css('cursor', 'not-allowed');
    } else {
        $('#addNewAddBtn').prop('disabled', false);
        $('#addNewAddBtn').css('cursor', 'pointer');
    }
});
$("#EditStreetName,#EditHouseNo,#EditPostalCode,#EditCity,#EditMobile").keyup(function () {
    if ($("#EditHouseNo").val() == '' || $("#EditStreetName").val() == '' || $("#EditPostalCode").val() == '' || $("#EditMobile").val().length > 10 || $("#EditMobile").val().length < 10 || $("#EditPostalCode").val().length > 6 || $("#EditPostalCode").val().length < 6) {
        $('#EditAddBtn').prop('disabled', true);
        $('#EditAddBtn').css('cursor', 'not-allowed');
    } else {
        $('#EditAddBtn').prop('disabled', false);
        $('#EditAddBtn').css('cursor', 'pointer');
    }
});

$(document).ready(function () {
    $("#userDob").dateDropdowns({
        submitFieldName: 'userDob',
        minAge: 18,
        submitFormat: "dd/mm/yyyy"
    });

    getUserDetails();

    getUserAddress();


});
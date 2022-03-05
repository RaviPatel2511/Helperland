$(document).ready(function () {
    $("#userDob").dateDropdowns({
        submitFieldName: 'userDob',
        minAge: 18,
        submitFormat: "dd/mm/yyyy"
    });

    getUserDetails();
});
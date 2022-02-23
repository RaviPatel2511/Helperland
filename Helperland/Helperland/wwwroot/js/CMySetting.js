function updatePassword() {
    var UpdatePass = {};
    UpdatePass.oldPassword = $("#oldPass").val();
    UpdatePass.password = $("#newPass").val();
    UpdatePass.confirmPassword = $("#confirmPass").val();
    $.ajax({
        type: 'POST',
        url: '/Customer/UpdatePassword',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: UpdatePass,
        //beforeSend: function () {
        //    $(".loader-div").removeClass('d-none');
        //},
        success:
            function (response) {
               /* setTimeout(function () {*/
                if (response == "PasswordUpdate") {
                        alert("Password update");
                } else {
                    alert("PAssword not match");
                    }
                //}, 1000);
                console.log(response);
            },
        error:
            function (err) {
                console.error(err);
            }
            //},
        //complete: function () {
        //    setTimeout(function () {
        //        $(".loader-div").addClass('d-none');
        //    }, 1000);
        //}
    });
    //console.log(UpdatePass);
}
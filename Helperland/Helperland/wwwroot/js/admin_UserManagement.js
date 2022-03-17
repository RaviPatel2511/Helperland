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

var spanSorting = '<span class="arrow-hack sort">&nbsp;&nbsp;&nbsp;</span>',
    spanAsc = '<span class="arrow-hack asc">&nbsp;&nbsp;&nbsp;</span>',
    spanDesc = '<span class="arrow-hack desc">&nbsp;&nbsp;&nbsp;</span>';
    $("#userManagementTable").on('click', 'th', function() {
        $("#userManagementTable thead th").each(function(i, th) {
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

$("#userManagementTable th").first().click().click();


function getData() {
    $.ajax({
        type: 'GET',
        cache: false,
        url: '/Admin/getUserManagementDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        beforeSend: function () {
            $(".loader-div").removeClass('d-none');
        },
        success:
            function (response) {
                setTimeout(function () {
                var UserManagementTblData = $('#UserManagementTblData');
                var userName = $("#userName");
                UserManagementTblData.empty();
                for (var i = 0; i < response.length; i++) {
                    userName.append('<option value="' + response[i].username+'">' + response[i].username + '</option>');
                    if (response[i].status == 1) {
                        UserManagementTblData.append('<tr><td>' + response[i].username + '</td><td></td><td><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].createdDate + '</span></td><td>' + response[i].userType + '</td><td>' + response[i].mobile + '</td><td class="text-center">' + response[i].postalCode + '</td><td class="active"><span>Active</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="actionBtnDeactivate"><span class="d-none">' + response[i].userId + '</span><a>Deactivate</a></li></ul></div></td><td class="d-none">' + response[i].email + '</td></tr>');
                    } else {
                        UserManagementTblData.append('<tr><td>' + response[i].username + '</td><td></td><td><span class="date"><img src="../image/upcoming_service/calendar.webp"> ' + response[i].createdDate + '</span></td><td>' + response[i].userType + '</td><td>' + response[i].mobile + '</td><td class="text-center">' + response[i].postalCode + '</td><td class="inactive"><span>Inactive</span></td><td class="actionbutton"><div class="action"> <span></span> <span></span> <span></span> </div><div class="threeDotsubMenu"><ul><li class="actionBtnActivate"><span class="d-none">' + response[i].userId + '</span><a>Activate</a></li></ul></div></td><td class="d-none">' + response[i].email + '</td></tr>');
                    }
                    }
                    //console.log(response);     
                table = $('#userManagementTable').DataTable({
                    "dom": 'Bt<"table-bottom d-flex justify-content-between"<"table-bottom-inner d-flex"li>p>',
                    "pagingType": "full_numbers",
                    "searching": true,
                    "order": [],
                    'columnDefs': [{
                        'targets': [7],
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

                var tbl = $('#userManagementTable').DataTable();

                $('#page-content-wrapper').on('click', '#search', function () {
                    var SearchByName = $("#userName").val();
                    var SearchuserRole = $('#userRole').val();
                    var Searchphone = $("#phone").val();
                    var Searchzip = $("#zipcode").val();
                    var Searchemail = $("#email").val();
                    var fromDate = $("#from-date").val();
                    var toDate = $("#to-date").val();
                    if (SearchByName != null) {
                        tbl.columns(0).search(SearchByName);
                    } else {
                        tbl.columns(0).search("");
                    }
                    if (SearchuserRole != null) {
                        tbl.columns(3).search(SearchuserRole);
                    } else {
                        tbl.columns(3).search("");
                    }
                    if (Searchphone != '') {
                        tbl.columns(4).search(Searchphone);
                    } else {
                        tbl.columns(4).search("");
                    }
                    if (Searchzip != '') {
                        tbl.columns(5).search(Searchzip);
                    } else {
                        tbl.columns(5).search("");
                    }
                    if (Searchemail != '') {
                        tbl.columns(8).search(Searchemail);
                    } else {
                        tbl.columns(8).search("");
                    }
                    if (fromDate != '' && toDate != '') {
                        $.fn.dataTable.ext.search.push(
                            function (settings, data, dataIndex) {
                                var min = new Date($("#from-date").val().split("-")[1] + "-" + $("#from-date").val().split("-")[0] + "-" + $("#from-date").val().split("-")[2]);
                                var max = new Date($("#to-date").val().split("-")[1] + "-" + $("#to-date").val().split("-")[0] + "-" + $("#to-date").val().split("-")[2]);
                                var date = new Date(data[2].split("-")[1] + "-" + data[2].split("-")[0] + "-" + data[2].split("-")[2]);
                                if (
                                    (min === null && max === null) ||
                                    (min === null && date <= max) ||
                                    (min <= date && max === null) ||
                                    (min <= date && date <= max)
                                ) {
                                    return true;
                                }
                                return false;
                            }
                        );
                    } else {
                        tbl.columns(2).search("");
                    }
                    tbl.draw();
                });
                
                    $('#page-content-wrapper').on('click', '#clear', function () {
                        window.location.reload();
                });

                    $('#userManagementTable tbody').on('click', '.action', function () {
                        $(".threeDotsubMenu").hide();
                    $(this).closest('.actionbutton').children('.threeDotsubMenu').toggle();
                });

                $('#userManagementTable tbody').on('click', '.actionBtnDeactivate', function () {
                    var clickedRow = $(this).children(':first-child').text();
                    $('#ClickedUserId').val(clickedRow);
                    $('#DeactivateActionModal').modal({
                        backdrop: 'static', // to prevent closing with click
                        keyboard: false  // to prevent closing with 
                    });
                    $("#DeactivateActionModal").modal("show");
                });

                    $('#userManagementTable tbody').on('click', '.actionBtnActivate', function () {
                    var clickRow = $(this).children(':first-child').text();
                        $('#ClickUserId').val(clickRow);
                        $('#ActivateActionModal').modal({
                            backdrop: 'static', // to prevent closing with click
                            keyboard: false  // to prevent closing with 
                        });
                        $("#ActivateActionModal").modal("show");
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

function DeactivateUser() {
    var InputDeactivateUserId = $("#ClickedUserId").val();
    $.ajax({
        type: "POST",
        url: '/Admin/DeactivateUser',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'InputDeactivateUserId': InputDeactivateUserId },
        cache: false,
        success:
            function (response) {
                if (response == "Successfully") {
                    $("#DeactivateActionModal").modal("hide");
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


function ActivateUser() {
    var InputActivateUserId = $("#ClickUserId").val();
    $.ajax({
        type: "POST",
        url: '/Admin/ActivateUser',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'InputActivateUserId': InputActivateUserId },
        cache: false,
        success:
            function (response) {
                if (response == "Successfully") {
                    $("#ActivateActionModal").modal("hide");
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

$("#SuccessActionModalBtn").on('click', () => {
    window.location.reload();
});

function ExcelSheetDown() {
    var data = document.getElementById("userManagementTable");

    var file = XLSX.utils.table_to_book(data, { sheet: "sheet1" });

    XLSX.write(file, { bookType: 'xlsx', bookSST: true, type: "base64" });

    XLSX.writeFile(file, "UserManagement." + 'xlsx');
}


function sort(col, order) {
	table.order([col, order]).draw();
}


  $('input[type=radio][name=sortOption]').change(function() {
    if (this.value == 'UserNameAtoZ') {
        sort(0,"asc");
    }
    else if (this.value == 'UserNameZtoA') {
        sort(0,"desc");
    }
    else if (this.value == 'PostalCode:Ascending') {
        sort(5,"asc");
    }
    else if (this.value == 'PostalCode:Descending') {
        sort(5,"desc");
    }
    else if (this.value == 'UserStatus:Ascending') {
        sort(6,"asc");
    }
    else if (this.value == 'UserStatus:Descending') {
        sort(6,"desc");
    }
  });

//// PREVENT FFROM BACK BUTTON AFTER LOGOUT
//window.history.forward();
//function noBack() {
//    window.history.forward();
//}



$(document).ready(function () {

    getData();

    $('#userName').select2();

    var date_input = $('input[name="date"]');
    date_input.datepicker({
        format: 'dd-mm-yyyy',
        todayHighlight: true,
        autoclose: true,
    });

    
});

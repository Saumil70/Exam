$(document).ready(function () {
    showStudent();
    $('#country').attr('disable', true);
    $('#state').attr('disable', true);
    $('#city').attr('disable', true);

    LoadCountries();

    $('#searchInput').on('input', function () {
        var searchTerm = $(this).val().trim();
        if (searchTerm.length > 0) {
            // Perform search when search term length is at least 3 characters
            searchStudent(searchTerm);
        }
        else {
            // If search term is empty, show all data
            showStudent();
        }
    });


})

function showStudent() {
    $.ajax({
        url: 'StudentManager/StudentList',
        type: 'GET',
        dataType: 'html', // Change the dataType to HTML
        success: function (result) {
            $('#tblData').html(result); // Inject the partial view into the container div
        },
        error: function () {
            alert("Data can't be retrieved.");
        }
    });
};




function LoadCountries() {
    $('#country').empty();

    $.ajax({
        url: '/StudentManager/CountryList',
        success: function (response) {

            if (response != null && response != undefined && response.length > 0) {
                $('#country').attr('disable', true);
                $('#country').append('<option disabled selected>--- Select Country ---</option>');

                $.each(response, function (i, data) {

                    $('#country').append('<option value= ' + data.CountryId + '>' + data.CountryName + '</option>');
                });
            }
            else {
                $('#country').attr('disable', true);
                $('#country').append('<option>---  Countries not Available ---</option>');
            }
        }
    }).done(function () {
        $('#country').on('change', function () {
            var selectedCountryId = $(this).val();
            LoadStates(selectedCountryId);
        });
    });
}

function LoadStates(CountryId, StateId) {
    $('#state').empty();

    $.ajax({
        url: '/StudentManager/StateList?countryId=' + CountryId,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#state').prop('disabled', false);
                $('#state').empty().append('<option disabled selected>--- Select State ---</option>');

                $.each(response, function (i, data) {
                    var option = $('<option value="' + data.StateId + '">' + data.StateName + '</option>');
                    if (data.StateId === StateId) { // Corrected comparison here
                        option.prop('selected', true);
                    }
                    $('#state').append(option);
                });
            }
            else {
                $('#state').prop('disabled', true);
                $('#state').empty().append('<option>--- States not Available ---</option>');
            }
        }
    }).done(function () {
        $('#state').on('change', function () {
            var StateId = $(this).val();
            LoadCities(StateId);
        });
    });
}

function LoadCities(StateId, CityId) {
    $('#city').empty();

    $.ajax({
        url: '/StudentManager/CityList?stateId=' + StateId,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#city').prop('disabled', false);
                $('#city').empty().append('<option disabled selected>--- Select City ---</option>');

                $.each(response, function (i, data) {
                    var option = $('<option value="' + data.CityId + '">' + data.CityName + '</option>');
                    if (data.CityId === CityId) { // Corrected comparison here
                        option.prop('selected', true);
                    }
                    $('#city').append(option);
                });
            }
            else {
                $('#city').prop('disabled', true);
                $('#city').empty().append('<option>--- Cities not Available ---</option>');
            }
        }
    });
}


$('#btnAdd').click(function () {
    ClearTextBox();
    /*$('userImage').empty();*/
    /*$('span').remove();*/
    $('#studentModal').modal('show');
    $('#cId').hide();
    $('#AddData').show();
    $('#UpdateData').hide();
    $('#addStudent').text('Add User');
});

/*function giveErrorUser() {

    var FirstName = $('#FirstName').val();
    if (!FirstName) {
        if ($('#FirstNameError').length === 0) {
            $('#FirstName').keyup(function () {
                if ($(this).val()) {
                    $('#FirstNameError').remove();
                }
            });
            $('#FirstName').after('<span id="FirstNameError" class="text-danger">First Name is required.</span>');
        }
       
    }
    var LastName = $('#LastName').val();
    if (!LastName) {
        if ($('#LastNameError').length === 0) {
            $('#LastName').keyup(function () {
                if ($(this).val()) {
                    $('#LastNameError').remove();
                }
            });
            $('#LastName').after('<span id="LastNameError" class="text-danger">last Name is required.</span>');
        }

    }

    var Email = $('#Email').val();
    if (!Email) {
        if ($('#EmailError').length === 0) {
            $('#Email').keyup(function () {
                if ($(this).val()) {
                    $('#EmailError').remove();
                }
            });
            $('#Email').after('<span id="EmailError" class="text-danger">Email is required.</span>');
        }
    } else { // Add email format validation here
        var isValidFormat = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(Email);
        if (!isValidFormat) {
            if ($('#EmailFormatError').length === 0) {
                $('#Email').keyup(function () {
                    if ($(this).val().match(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)) {
                        $('#EmailFormatError').remove();
                    }
                });
                $('#Email').after('<span id="EmailFormatError" class="text-danger">Invalid email format.</span>');
            }
        }
    }



    var PhoneNumber = $('#PhoneNumber').val();
    if (!PhoneNumber) {
        if ($('#PhoneNumberError').length === 0) {
            $('#PhoneNumber').keyup(function () {
                if ($(this).val()) {
                    $('#PhoneNumberError').remove();
                }
            });
            $('#PhoneNumber').after('<span id="PhoneNumberError" class="text-danger">PhoneNumber is required.</span>');
        }
    } else {
        var isValidPhoneNumber = /^\d{10}$/.test(PhoneNumber);
        if (!isValidPhoneNumber) {
            if ($('#PhoneNumberFormatError').length === 0) {
                $('#PhoneNumber').keyup(function () {
                    if ($(this).val().match(/^\d{10}$/)) {
                        $('#PhoneNumberFormatError').remove();
                    }
                });
                $('#PhoneNumber').after('<span id="PhoneNumberFormatError" class="text-danger">Invalid phone number format (should be 10 digits).</span>');
            }
        }
    }

    var DOB = $('#DOB').val();
    if (!DOB) {
        if ($('#DOBError').length === 0) {
            $('#DOB').keyup(function () {
                if ($(this).val()) {
                    $('#DOBError').remove();
                }
            });
            $('#DOB').after('<span id="DOBError" class="text-danger">DOB is required.</span>');
        }
    } else {
        var isValidDOBFormat = /^(\d{2})-(\d{2})-(\d{4})$/.test(DOB);
        if (!isValidDOBFormat) {
            if ($('#DOBFormatError').length === 0) {
                $('#DOB').keyup(function () {
                    if ($(this).val().match(/^(\d{2})-(\d{2})-(\d{4})$/)) {
                        $('#DOBFormatError').remove();
                    }
                });
                $('#DOB').after('<span id="DOBFormatError" class="text-danger">Invalid DOB format (should be dd-mm-yyyy).</span>');
            }
        }
    }




    var Salary = $('#Salary').val();
    if (!Salary) {
        if ($('#SalaryError').length === 0) {
            $('#Salary').change(function () {
                if ($(this).val()) {
                    $('#SalaryError').remove();
                }
            });
            $('#Salary').after('<span id="SalaryError" class="text-danger">Salary is required.</span>');
        }
        
    }
    var StartingDate = $('#StartingDate').val();
    if (!StartingDate) {
        if ($('#StartingDateError').length === 0) {
            $('#StartingDate').change(function () {
                if ($(this).val()) {
                    $('#StartingDateError').remove();
                }
            });
            $('#StartingDate').after('<span id="StartingDateError" class="text-danger">StartingDate is required.</span>');
        }

    }
    var Position = $('#Position').val();
    if (!Position) {
        if ($('#PositionError').length === 0) {
            $('#Position').change(function () {
                if ($(this).val()) {
                    $('#PositionError').remove();
                }
            });
            $('#Position').after('<span id="PositionError" class="text-danger">Position is required.</span>');
        }

    }

    var Country = $('#country').val();
    if (!Country) {
        if ($('#CountryError').length === 0) {
            $('#country').change(function () {
                if ($(this).val()) {
                    $('#CountryError').remove();
                }
            });
            $('#country').after('<span id="CountryError" class="text-danger">Country is required.</span>');
        }
        
    }

    var State = $('#state').val();
    if (!State) {
        if ($('#StateError').length === 0) {
            $('#state').change(function () {
                if ($(this).val()) {
                    $('#StateError').remove();
                }
            });
            $('#state').after('<span id="StateError" class="text-danger">State is required.</span>');
        }
       
    }

    var City = $('#city').val();
    if (!City) {
        if ($('#CityError').length === 0) {
            $('#city').change(function () {
                if ($(this).val()) {
                    $('#CityError').remove();
                }
            });
            $('#city').after('<span id="CityError" class="text-danger">City is required.</span>');
        }
        
    }
    return;
}*/





function AddStudent() {
    /*giveErrorUser();*/
    var isValid = $('#studentForm').valid();
    if (!isValid) {
        return;
    }

    
    var formData = new FormData();

    // Append form data to the formData object
    formData.append('FirstName', $('#FirstName').val());
    formData.append('LastName', $('#LastName').val());
    formData.append('Email', $('#Email').val());
    formData.append('PhoneNumber', $('#PhoneNumber').val());
    formData.append('DOB', $('#DOB').val());
    formData.append('Salary', $('#Salary').val());
    formData.append('StartingDate', $('#StartingDate').val());
    formData.append('Position', $('#Position').val());
    formData.append('CountryId', $('#country').val());
    formData.append('StateId', $('#state').val());
    formData.append('CityId', $('#city').val());
    

    $.ajax({
        url: '/StudentManager/AddStudent',
        type: 'POST',
        data: formData,
        processData: false,  
        contentType: false, 
        dataType: 'json',
        success: function (respone) {
            if (respone.success) {
                var fullName = $('#FirstName').val().trim() + ' ' + $('#LastName').val().trim();
                sessionStorage.setItem('fullName', fullName);
                console.log('User\'s Full Name:', fullName);
                $('#lastUser').text(fullName);
                setTimeout(function () {
                    $('#lastUser').text('');
                    sessionStorage.removeItem('fullName');
                },  10000);


                LoadCountries();
                LoadStates();
                LoadCities();
                alert('Student Created Successfully  Welcome:' + respone.firstName + respone.lastName);
                ClearTextBox();
                showStudent();
                HidePop();
            }
            else {
                alert('Student not created');
            }
            
        },
        error: function () {
            alert("Student not Created Successfully");
        }
    });
};

function HidePop() {
    $('#studentModal').modal('hide');
}

function ClearTextBox() {
    $('#FirstName').val('');
    $('#LastName').val('');
    $('#Email').val('');
    $('#PhoneNumber').val('');
    $('#DOB').val('');
    $('#Salary').val('');
    $('#StartingDate').val('');
    $('#Position').val('');
    


}

/*function Delete(userId) {
    if (confirm('Are You Sure, You want to delete the User? ')) {
        $.ajax({
            url: '/UserManager/Delete?id=' + userId,
            success: function () {
                alert('User Deleted SuccessFully!');
                showUser();
            },
            error: function () {
                alert("User not Deleted");
            }
        });
    }
}*/

function Edit(StudentId) {
    $('span').remove();
    $.ajax({
        url: '/StudentManager/Edit?id=' + StudentId,
        type: 'GET',
        contentType: 'application/x-www-form-urlencoded;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            
            $('#studentModal').modal('show');
            $('#StudentId').val(response.StudentId);
            $('#FirstName').val(response.FirstName);
            $('#LastName').val(response.LastName);
            $('#Email').val(response.Email);
            $('#PhoneNumber').val(response.PhoneNumber);
            $('#DOB').val(response.DOB);
            $('#Salary').val(response.Salary);
            $('#StartingDate').val(response.StartingDate);
            $('#Position').val(response.Position);
            $('#country').val(response.CountryId);
            LoadStates(response.CountryId,response.StateId);
            $('#state').val(response.StateId);
            LoadCities(response.StateId,response.CityId);
            $('#cities').val(response.CityId);
            $('#IsActive').val(response.IsActive);
            $('#ContactInfoId').val(response.ContactInfoId);



            
          

            $('#AddData').hide();
            $('#UpdateData').show();
        },
        error: function () {
            alert("Student not Found");
        }
    });
}

function UpdateStudent() {
    /*giveErrorUser();*/
    var isValid = $('#studentForm').valid();
    if (!isValid) {
        return;
    }


    var formData = new FormData();

    // Append form data to the formData object
    formData.append('StudentId', $('#StudentId').val());
    formData.append('FirstName', $('#FirstName').val());
    formData.append('LastName', $('#LastName').val());
    formData.append('Email', $('#Email').val());
    formData.append('PhoneNumber', $('#PhoneNumber').val());
    formData.append('DOB', $('#DOB').val());
    formData.append('Salary', $('#Salary').val());
    formData.append('StartingDate', $('#StartingDate').val());
    formData.append('Position', $('#Position').val());
    formData.append('CountryId', $('#country').val());
    formData.append('StateId', $('#state').val());
    formData.append('CityId', $('#city').val());
    formData.append('IsActive', $('#IsActive').val());
    formData.append('ContactInfoId', $('#ContactInfoId').val());



    $.ajax({
        url: '/StudentManager/EditStudent',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false, 
        dataType: 'json',
        success: function (response) {
            formData.append('StudentId', $('#StudentId').val());
            if (response.success) {
                alert('Student Updated Successfully');
                ClearTextBox();
                showStudent();
                HidePop();
            }
            else {
                alert('Student not updated');
            }
            
        },
        error: function () {
            alert("Student can't Updated");
        }
    });

    function HidePop() {
        $('#studentModal').modal('hide');
    }

    function ClearTextBox() {
        $('#FirstName').val('');
        $('#LastName').val('');
        $('#Email').val('');
        $('#PhoneNumber').val('');
        $('#DOB').val('');
        $('#Salary').val('');
        $('#StartingDate').val('');
        $('#Position').val('');

    }

}

$('#btnDelete').click(function () {
    var checkedIds = [];
    $('.checkbox:checked').each(function () {
        checkedIds.push($(this).val());
    });
    if (checkedIds.length > 0) {
        if (confirm('Are you sure you want to delete the selected users?')) {
            $.ajax({
                url: '/StudentManager/Delete',
                type: 'POST',
                data: { ids: checkedIds },
                success: function () {
                    alert('Selected student deleted successfully!');
                    showStudent();
                    // Refresh the table or perform any other action after deletion
                },
                error: function () {
                    alert('Error deleting student.');
                }
            });
        }
    } else {
        alert('No student selected for deletion.');
    }
});


function searchStudent(searchTerm) {
    $.ajax({
        url: 'StudentManager/Search', // Change the URL to your search endpoint
        type: 'GET',
        dataType: 'json',
        data: { searchTerm: searchTerm },
        success: function (result) {
            var obj = "";
            $.each(result, function (index, item) {
                obj += '<tr>';
                obj += '<td><input type="checkbox" class="checkbox" value="' + item.StudentId + '"></td>';
                obj += '<td>' + item.FirstName + ' ' + item.LastName + '</td>';
                obj += '<td>' + item.Position + '</td>';
                obj += '<td>' + item.CityName + '</td>';
                obj += '<td>' + item.Age + '</td>';
                obj += '<td>' + item.StartingDate + '</td>';
                obj += '<td>' + item.Salary + '</td>';
                obj += '<td><a href="#" class="btn btn-primary" onclick="Edit(' + item.StudentId + ')">Edit</a></td >';
                obj += '</tr>';
            })
            $('#tblData').html(obj);
        },
        error: function () {
            alert("Error occurred while searching.");
        }
    });
}

function displaySession() {
    var fullName = sessionStorage.getItem('fullName');
    if (fullName) {
        $('#lastUser').text(fullName);
        setTimeout(function () {
            $('#userNameDisplay').text('');
            sessionStorage.removeItem('fullName');
        }, 10000);
    }
}

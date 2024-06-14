$(document).ready(function () {
    // When the document is ready, call the function to show the employee list
    ShowEmployeeList();
});


// Function to initialize the DataTable and fetch employee data
function ShowEmployeeList() {
    $('#employeeTable').DataTable({
        // DataTable configuration
        processing: true,
        filter: true,
        autoWidth: false,
        responsive: true,
        searching: true,
        pagingType: 'simple_numbers',
        ajax: {
            type: "GET",
            url: '/Employee/GetAllEmployees', // Endpoint to fetch employee data
            dataSrc: ''
        },
        columns: [
            { data: 'id' },
            {
                data: function (row) {
                    return row.firstName + ' ' + row.lastName;
                }
            },
            { data: 'email' },
            { data: 'phone' },
            {
                data: function (row) {

                    return row.department.name;
                }
            }, {
                data: function (row) {
                    var hireDate = new Date(row.hireDate);
                    var day = hireDate.getDate();
                    var month = hireDate.getMonth() + 1;
                    var year = hireDate.getFullYear();

                    return month + '-' + day + '-' + year;
                }
            },
            {
                // Render edit and delete buttons for each employee
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning btn-sm mr-3 me-2" onclick="UpdateEmployee(\'' + row.id + '\')"> Edit</button><button class="btn btn-danger btn-sm" onclick="DeleteEmployee(\'' + row.id + '\')"> Delete</button>';
                }
            },
        ],
        columnDefs: [
            { orderable: false, targets: 6 } // Disable sorting for action column
        ]
    });
}

function OpenAddEmployeeModal() {
    $('#add-employee-modal').modal('show');
    $("#addEmployeeForm").trigger("reset");
    $('#addEmployeeForm').removeClass('was-validated');
}

function CloseAddEmployeeModal() {
    $('#add-employee-modal').modal('hide');
    $('#employeeTable').DataTable().ajax.reload(); // Reload the employee table

    $('#successToastDiv').addClass('show');
    $('#successToastMessage').text("Success, Employee Created ");
     ;
    setTimeout(function () {
        $('#successToastDiv').toast('hide');
         
    }, 3000);
}

$('#addEmployeeForm').on('submit', function (event) {
    event.preventDefault();
    var form = this;

    if (form.checkValidity()) {
        var employeeViewModel = {
            FirstName: $('#EmployeeFirstNameAdd').val(),
            LastName: $('#EmployeeLastNameAdd').val(),
            Email: $('#EmployeeEmailAdd').val(),
            Phone: $('#EmployeePhoneAdd').val(),
            HireDate: $('#EmployeeHireDateAdd').val(),
            DepartmentId: $('#EmployeeDepartmentAdd option:selected').val(),
        };
        $.ajax({
            // AJAX call to add a new employee
            url: "/Employee/AddEmployee",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(employeeViewModel),
            success: function (response) {
                if (response.isSuccess) {
                    // If successful, close the modal
                    CloseAddEmployeeModal();
                }
                else {
                    $('#add-employee-modal').modal('hide');
                    $('#errorToastDiv').addClass('show');
                    $('#errorToastMessage').text(response.message);
                     ;
                    setTimeout(function () {
                        $('#errorToastDiv').toast('hide');
                         
                    }, 3000);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    } else {
        event.stopPropagation();
        $(form).addClass('was-validated');
    }
});

function OpenUpdateEmployeeModal(response) {
    $("#updateEmployeeForm").trigger("reset");
    $('#updateEmployeeForm').removeClass('was-validated');
    $('#update-employee-modal').modal('show');

    $('#EmployeeUpdateId').val(response.id);
    $('#EmployeeFirstNameUpdate').val(response.firstName);
    $('#EmployeeLastNameUpdate').val(response.lastName);
    $('#EmployeeEmailUpdate').val(response.email);
    $('#EmployeePhoneUpdate').val(response.phone);
    $('#EmployeeDepartmentUpdate').val(response.departmentId);
    var hireDate = new Date(response.hireDate);
    var year = hireDate.getFullYear();
    var month = ('0' + (hireDate.getMonth() + 1)).slice(-2);
    var day = ('0' + hireDate.getDate()).slice(-2);

    var formattedHireDate = `${year}-${month}-${day}`;

    $('#EmployeeHireDateUpdate').val(formattedHireDate);

}
function CloseUpdateEmployeeModal() {
    $('#update-employee-modal').modal('hide');
    $('#employeeTable').DataTable().ajax.reload();

    $('#successToastDiv').addClass('show');
    $('#successToastMessage').text("Success, Employee Update ");
     ;
    setTimeout(function () {
        $('#successToastDiv').toast('hide');
         
    }, 3000);
}
function UpdateEmployee(employeeId) {
    $.ajax({
        url: '/Employee/GetEmployeeById/' + employeeId,
        type: 'GET',
        dataSrc: '',
        success: function (response) {
            OpenUpdateEmployeeModal(response);
        }


    });
}

$('#updateEmployeeForm').on('submit', function (event) {
    event.preventDefault();
    var form = this;

    if (form.checkValidity()) {
        var employeeViewModel = {
            FirstName: $('#EmployeeFirstNameUpdate').val(),
            LastName: $('#EmployeeLastNameUpdate').val(),
            Email: $('#EmployeeEmailUpdate').val(),
            Phone: $('#EmployeePhoneUpdate').val(),
            HireDate: $('#EmployeeHireDateUpdate').val(),
            DepartmentId: $('#EmployeeDepartmentUpdate option:selected').val(),
            Id: $('#EmployeeUpdateId').val(),
        };

        $.ajax({
            // AJAX call to update a employee
            url: "/Employee/UpdateEmployee",
            type: "POST",
            dataSrc: '',
            contentType: 'application/json',
            data: JSON.stringify(employeeViewModel),
            success: function (response) {

                if (response.isSuccess) {
                    // If successful, close the modal
                    CloseUpdateEmployeeModal();
                }
                else {
                    $('#update-employee-modal').modal('hide');
                    $('#errorToastDiv').addClass('show');
                    $('#errorToastMessage').text(response.message);
                     ;
                    setTimeout(function () {
                        $('#errorToastDiv').toast('hide');
                         
                    }, 3000);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    } else {
        event.stopPropagation();
        $(form).addClass('was-validated');
    }
});


function DeleteEmployee(employeeId) {
    $('#delete-employee-modal').modal('show');
    $('#deleteEmployeeMessage').text("Are you sure you want to delete this employee ?");
    $('#EmployeeDeleteId').val(employeeId);
}
function ConfirmDeleteEmployee() {
    var employeeId = $('#EmployeeDeleteId').val();
    $.ajax({
        // AJAX call to delete a employee
        url: '/Employee/DeleteEmployee/' + employeeId,
        type: 'DELETE',
        dataSrc: '',
        success: function (response) {
            if (response.isSuccess) {
                // If successful, close the modal and reload the employee table
                $('#delete-employee-modal').modal('hide');
                $('#employeeTable').DataTable().ajax.reload();
                $('#successToastDiv').addClass('show');
                $('#successToastMessage').text("Success, Employee Deleted ");
                 ;
                setTimeout(function () {
                    $('#successToastDiv').toast('hide');
                     
                }, 3000);
            }
            else {
                $('#delete-employee-modal').modal('hide');
                $('#errorToastDiv').addClass('show');
                $('#errorToastMessage').text(response.message);
                 ;
                setTimeout(function () {
                    $('#errorToastDiv').toast('hide');
                     
                }, 3000);
            }

        }, error: function (err) {
            console.log(err);
        }
    });
}
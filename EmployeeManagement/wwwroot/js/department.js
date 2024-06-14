$(document).ready(function () {
    // When the document is ready, call the function to show the department list
    ShowDepartmentList();
});

// Function to initialize the DataTable and fetch department data
function ShowDepartmentList() {
    $('#departmentTable').DataTable({
        // DataTable configuration
        processing: true,
        filter: true,
        autoWidth: false,
        responsive: true,
        searching: true,
        pagingType: 'simple_numbers',
        ajax: {
            type: "GET",
            url: '/Department/GetAllDepartments', // Endpoint to fetch department data
            dataSrc: ''
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'description' },
            {
                // Render edit and delete buttons for each department
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning btn-sm mr-3 me-2" onclick="UpdateDepartment(\'' + row.id + '\')"> Edit</button><button class="btn btn-danger btn-sm" onclick="DeleteDepartment(\'' + row.id + '\')"> Delete</button>';
                }
            },
        ],
        columnDefs: [
            { orderable: false, targets: 3 } // Disable sorting for action column
        ]
    });
}

function OpenAddDepartmentModal() {
    $('#add-department-modal').modal('show');
    $("#addDepartmentForm").trigger("reset");
    $('#addDepartmentForm').removeClass('was-validated');
}

function CloseAddDepartmentModal() {
    $('#add-department-modal').modal('hide');
    $('#departmentTable').DataTable().ajax.reload(); // Reload the department table

    $('#successToastDiv').addClass('show');
    $('#successToastMessage').text("Success, Department Updated ");
    setTimeout(function () {
        $('#successToastDiv').toast('hide');
        console.log("Toast")
    }, 3000);

}

$('#addDepartmentForm').on('submit', function (event) {
    event.preventDefault();
    var form = this;
    if (form.checkValidity()) {

        var departmentViewModel = {
            Name: $('#DepartmentNameAdd').val(),
            Description: $('#DepartmentDescriptionAdd').val(),
        };

        $.ajax({
            // AJAX call to add a new department
            url: "/Department/AddDepartment",
            type: "POST",
            dataSrc: '',
            contentType: 'application/json',
            data: JSON.stringify(departmentViewModel),
            success: function (response) {
                if (response.isSuccess) {
                   
                    // If successful, close the modal
                    CloseAddDepartmentModal();
                }
                else {
                    $('#add-department-modal').modal('hide');
                    $('#errorToastDiv').addClass('show');
                    $('#errorToastMessage').text(response.message);
                    console.log("Toast");
                    setTimeout(function () {
                        $('#errorToastDiv').toast('hide');
                        console.log("Toast")
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

function OpenUpdateDepartmentModal(response) {
    $("#updateDepartmentForm").trigger("reset");
    $('#updateDepartmentForm').removeClass('was-validated');
    $('#update-department-modal').modal('show');
    $('#DepartmentUpdateId').val(response.id);
    $('#DepartmentNameUpdate').val(response.name);
    $('#DepartmentDescriptionUpdate').val(response.description);
}

function CloseUpdateDepartmentModal() {
    $('#update-department-modal').modal('hide');
    $('#departmentTable').DataTable().ajax.reload();

    $('#successToastDiv').addClass('show');
    $('#successToastMessage').text("Success, Department Updated ");
    setTimeout(function () {
        $('#successToastDiv').toast('hide');
        console.log("Toast")
    }, 3000);

}

function UpdateDepartment(departmentId) {
    $.ajax({
        url: '/Department/GetDepartmentById/' + departmentId,
        type: 'GET',
        dataSrc: '',
        success: function (response) {
            OpenUpdateDepartmentModal(response);
        }
    });
}

$('#updateDepartmentForm').on('submit', function (event) {
    event.preventDefault();
    var form = this;

    if (form.checkValidity()) {
        var departmentViewModel = {
            Name: $('#DepartmentNameUpdate').val(),
            Description: $('#DepartmentDescriptionUpdate').val(),
            Id: $('#DepartmentUpdateId').val(),
        };

        $.ajax({
            // AJAX call to update a department
            url: "/Department/UpdateDepartment",
            type: "POST",
            dataSrc: '',
            contentType: 'application/json',
            data: JSON.stringify(departmentViewModel),
            success: function (response) {
                if (response.isSuccess) {
                    // If successful, close the modal
                    CloseUpdateDepartmentModal();
                }
                else {

                    $('#update-department-modal').modal('hide');
                    $('#errorToastDiv').addClass('show');
                    $('#errorToastMessage').text(response.message);
                    setTimeout(function () {
                        $('#errorToastDiv').toast('hide');
                        console.log("Toast")
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

function DeleteDepartment(departmentId) {
    $('#delete-department-modal').modal('show');
    $('#deleteDepartmentMessage').text("Are you sure you want to delete this department ?")
    $('#DepartmentDeleteId').val(departmentId);
}

function ConfirmDeleteDepartment() {
    var departmentId = $('#DepartmentDeleteId').val();
    $.ajax({
        // AJAX call to delete a department
        url: '/Department/DeleteDepartment/' + departmentId,
        type: 'DELETE',
        dataSrc: '',
        success: function (response) {
            if (response.isSuccess) {
                // If successful, close the modal and reload the department table
                $('#delete-department-modal').modal('hide');
                $('#departmentTable').DataTable().ajax.reload();

                $('#successToastDiv').addClass('show');
                $('#successToastMessage').text("Success, Department Deleted ");
                console.log("Toast");
                setTimeout(function () {
                    $('#successToastDiv').toast('hide');
                    console.log("Toast")
                }, 3000);
            }
            else {
                $('#delete-department-modal').modal('hide');
                $('#errorToastDiv').addClass('show');
                $('#errorToastMessage').text(response.message);
                console.log("Toast");
                setTimeout(function () {
                    $('#errorToastDiv').toast('hide');
                    console.log("Toast")
                }, 3000);
            }

        },
        error: function (err) {
            console.log(err);
        }
    });
}
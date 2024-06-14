$(document).ready(function () {
    // When the document is ready, call the function to show the task list
    ShowTaskList();
});


// Function to initialize the DataTable and fetch task data
function ShowTaskList() {
    $('#taskTable').DataTable({
        // DataTable configuration
        processing: true,
        filter: true,
        autoWidth: false,
        responsive: true,
        searching: true,
        pagingType: 'simple_numbers',
        ajax: {
            type: "GET",
            url: '/Task/GetAllTasks', // Endpoint to fetch task data
            dataSrc: ''
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'description' },
            { data: 'title' },
            {
                data: function (row) {
                    var dueDate = new Date(row.dueDate);
                    var day = dueDate.getDate();
                    var month = dueDate.getMonth() + 1;
                    var year = dueDate.getFullYear();

                    return month + '-' + day + '-' + year;
                }
            },
            {
                data: function (row) {

                    return row.employee.firstName + " " + row.employee.lastName;
                }
            },
            {
                // Render edit and delete buttons for each task
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning btn-sm mr-3 me-2" onclick="UpdateTask(\'' + row.id + '\')"> Edit</button><button class="btn btn-danger btn-sm" onclick="DeleteTask(\'' + row.id + '\')"> Delete</button>';
                }
            },
        ],
        columnDefs: [
            { orderable: false, targets: 6 } // Disable sorting for action column
        ]
    });
}

function OpenAddTasksModal() {
    $('#add-task-modal').modal('show');
    $("#addTaskForm").trigger("reset");
    $('#addTaskForm').removeClass('was-validated');
}

function CloseAddTaskModal() {
    $('#add-task-modal').modal('hide');
    $('#taskTable').DataTable().ajax.reload(); // Reload the task table
}

$('#addTaskForm').on('submit', function (event) {
    event.preventDefault();
    var form = this;

    if (form.checkValidity()) {
        var taskViewModel = {
            Name: $('#TaskNameAdd').val(),
            Description: $('#TaskDescriptionAdd').val(),
            Title: $('#TaskTitleAdd').val(),
            DueDate: $('#TaskDueDateAdd').val(),
            EmployeeId: $('#TaskEmployeeIdAdd').val(),
        };

        $.ajax({
            // AJAX call to add a new task
            url: "/Task/AddTask",
            type: "POST",
            dataSrc: '',
            contentType: 'application/json',
            data: JSON.stringify(taskViewModel),
            success: function (response) {
                if (response.isSuccess) {
                    // If successful, close the modal
                    CloseAddTaskModal();

                    $('#successToastDiv').addClass('show');
                    $('#successToastMessage').text("Success, Task Created ");
                     ;
                    setTimeout(function () {
                        $('#successToastDiv').toast('hide');
                         
                    }, 3000);
                }
                else {
                    $('#add-task-modal').modal('hide');
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

function OpenUpdateTaskModal(response) {
    $("#updateTaskForm").trigger("reset");
    $('#updateTaskForm').removeClass('was-validated');
    $('#update-task-modal').modal('show');
    $('#TaskUpdateId').val(response.id);
    $('#TaskNameUpdate').val(response.name);
    $('#TaskDescriptionUpdate').val(response.description);
    $('#TaskTitleUpdate').val(response.title);
    var dueDate = new Date(response.dueDate);
    var year = dueDate.getFullYear();
    var month = ('0' + (dueDate.getMonth() + 1)).slice(-2);
    var day = ('0' + dueDate.getDate()).slice(-2);

    var formattedDueDate = `${year}-${month}-${day}`;
    $('#TaskDueDateUpdate').val(formattedDueDate);
    $('#TaskEmployeeIdUpdate').val(response.employeeId);

}
function CloseUpdateTaskModal() {
    $('#update-task-modal').modal('hide');
    $('#taskTable').DataTable().ajax.reload();

    $('#successToastDiv').addClass('show');
    $('#successToastMessage').text("Success, Asset Created ");
    setTimeout(function () {
        $('#successToastDiv').toast('hide');
    }, 3000);
}
function UpdateTask(taskId) {
    $.ajax({
        url: '/Task/GetTaskById/' + taskId,
        type: 'GET',
        dataSrc: '',
        success: function (response) {
            OpenUpdateTaskModal(response);
        }
    });
}

$('#updateTaskForm').on('submit', function (event) {
    event.preventDefault();
    var form = this;

    if (form.checkValidity()) {
        var taskViewModel = {
            Name: $('#TaskNameUpdate').val(),
            Description: $('#TaskDescriptionUpdate').val(),
            Title: $('#TaskTitleUpdate').val(),
            DueDate: $('#TaskDueDateUpdate').val(),
            EmployeeId: $('#TaskEmployeeIdUpdate').val(),
            Id: $('#TaskUpdateId').val(),
        };

        $.ajax({
            // AJAX call to update a task
            url: "/Task/UpdateTask",
            type: "POST",
            dataSrc: '',
            contentType: 'application/json',
            data: JSON.stringify(taskViewModel),
            success: function (response) {

                if (response.isSuccess) {
                    // If successful, close the modal
                    CloseUpdateTaskModal();

                    $('#successToastDiv').addClass('show');
                    $('#successToastMessage').text("Success, Task Update ");
                     ;
                    setTimeout(function () {
                        $('#successToastDiv').toast('hide');
                         
                    }, 3000);
                }
                else {
                    $('#update-task-modal').modal('hide');
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


function DeleteTask(taskId) {
    $('#delete-task-modal').modal('show');
    $('#deleteTaskMessage').text("Are you sure you want to delete this task ?")
    $('#TaskDeleteId').val(taskId);
}
function ConfirmDeleteTask() {
    var taskId = $('#TaskDeleteId').val();
    $.ajax({
        // AJAX call to delete a task
        url: '/Task/DeleteTask/' + taskId,
        type: 'DELETE',
        dataSrc: '',
        success: function (response) {
            if (response.isSuccess) {
                // If successful, close the modal and reload the task table
                $('#delete-task-modal').modal('hide');
                $('#taskTable').DataTable().ajax.reload();

                $('#successToastDiv').addClass('show');
                $('#successToastMessage').text("Success, Task Delete ");
                 ;
                setTimeout(function () {
                    $('#successToastDiv').toast('hide');
                     
                }, 3000);
            }
            else {
                $('#delete-task-modal').modal('hide');
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

var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "postalCode", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {

                    return `
                        <div class="btn-group w-100" width="20%" role="group">
                            <a href="/Admin/Company/Upsert?id=${data}" class="btn btn-info m-0 w-50"> <i class="bi bi-pencil-square"></i> &nbsp; Edit</a>
                            <a onClick=Delete('/Admin/Company/Delete/${data}') class="btn btn-danger m-0 w-50"><i class="bi bi-trash"></i> &nbsp; Delete</a>
                        </div>
                    `
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            /*$.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            })*/
            fetch(url, {
                method: 'DELETE',
                headers: {
                    'User-Agent': 'Delete Product Request'
                }
            })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                })
        }
    })
}
/*
 Certainly! The $.ajax() method in this code is an AJAX call to the server, which is used to send an HTTP request to the server and receive a response asynchronously without reloading the entire web page.

Here's a breakdown of the attributes that can be used in a typical $.ajax() call:

url: specifies the URL to which the AJAX request is sent.
type: specifies the HTTP request method. Possible values are "GET", "POST", "PUT", "DELETE", and others.
data: specifies the data to be sent to the server. This can be in the form of a query string, an object, or a JSON string.
dataType: specifies the expected data type of the response from the server, such as "json", "html", or "text".
success: a callback function that is called when the request completes successfully. It receives the response data as an argument.
error: a callback function that is called when the request fails. It receives the error message as an argument.
beforeSend: a callback function that is called before the request is sent. This can be used to modify the request headers or add additional data to the request.
complete: a callback function that is called when the request completes, whether it succeeds or fails.
In addition to these attributes, there are many other options that can be used in a $.ajax() call, such as headers, cache, timeout, and crossDomain.

It's important to note that the exact attributes available and their specific options may depend on the version of jQuery being used, so it's always a good idea to refer to the official jQuery documentation for more information.
*/
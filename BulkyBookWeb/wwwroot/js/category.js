function handleDelete(url) {
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
            fetch(url, {
                method: 'DELETE'
            }).then(res => res.json()).then(data => {
                if (data.success) {
                    toastr.success(data.message);
                    reload();

                } else {
                    toastr.error(data.message);
                }
            })
        }
    })

}

function reload() {

    // this is jquery get request to the getAll function which return the JSON object and the call-back function will handle the data
    $.get("/Admin/Category/GetAll", function (data) {

        // once data is here, empty the t-body
        $('#table-body').empty();

        //now refil the data into tbody
        $.each(data, function (index, category) {

            var row = '<tr>' +
                '<td width="50%">' + category.name + '</td>' +
                '<td width="30%">' + category.displayOrder + '</td>' +
                '<td class="btn-group w-100" width="20%" role="group">' +
                '<a class="btn btn-info m-0 w-50" asp-controller="Category" asp-action="Edit" asp-route-id="' + category.Id + '"> <i class="bi bi-pencil-square"></i> &nbsp; Edit</a>' +
                '<button class="btn btn-danger m-0 w-50" onClick="handleDelete(\'/Admin/Category/Delete/' + category.Id + '\')"><i class="bi bi-trash"></i> &nbsp; Delete</button>' +
                '</td>' +
                '</tr>';
            $('#table-body').append(row);
        })
    })
}
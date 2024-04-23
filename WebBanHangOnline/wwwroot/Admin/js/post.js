$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/post/getall' },
        "columns": [
            { data: 'ImageUrl', "width":"15%" },
            { data: 'Title', "width": "15%" },
            { data: 'CreatedDate', "width": "15%" },
            { data: 'office', "width": "15%" }
        ]
    });
}

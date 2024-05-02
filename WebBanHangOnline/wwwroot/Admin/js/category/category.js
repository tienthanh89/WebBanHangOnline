$(document).ready(() => {
    LoadData();
});

// Hàm thêm danh mục khi ấn nút "Thêm danh mục jquery"
// Lấy các giá trị trong input và gửi tới controller thông qua ajax
// Thêm đối tượng trả về vào table
$("#add-category").click(() => {
    var id = $("#idCategory").val();
    if (id == "") {
        $.ajax({
            type: 'POST',
            url: '/add-jquery',
            data: {
                Title: $("#Title").val(),
                Description: $("#Description").val(),
                Position: $("#Position").val(),
                SeoTitle: $("#SeoTitle").val(),
                SeoDescription: $("#SeoDescription").val(),
                SeoKeyWords: $("#SeoKeyWords").val()
            },
            success: function (result) {
                if (result != null) {
                    // đóng modal
                    $("#category-info").modal('hide');

                    let lastRow = parseInt($("#list-category tr:last-child td:first-child").text());
                    if (isNaN(lastRow)) lastRow = 0;
                    lastRow++;
                    // chạy hàm load
                    let r =
                        `<tr id="trow_${result.id}">
                            <td class="stt">${lastRow}</td>
                            <td>${result.title}</td>
                            <td>${result.position}</td>
                            <td>
                                <div class="dropdown">
                                    <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                        <i class="bx bx-dots-vertical-rounded"></i>
                                    </button>
                                    <div class="dropdown-menu">
                                        <a class="dropdown-item" href="/Admin/category/edit/${result.id}">
                                            <i class="bx bx-edit-alt me-1"></i>
                                            Sửa MVC
                                        </a>
                                        <a class="dropdown-item" onclick = "editCategoryAjax('${result.Id}')">
                                                <i class="bx bx-edit-alt me-1" ></i>
                                                Sửa Ajax
                                        </a >
                                        <a class="dropdown-item"  href="/Admin/category/delete/${result.id}">
                                            <i class="bx bx-trash me-1"></i>
                                            Xóa
                                        </a>
                                        <a data-id="${result.id}" class="dropdown-item" onclick="deleteCategory('${result.id}')">
                                            <i class="bx bx-trash me-1"></i>
                                            Xóa Ajax
                                        </a>
                                    </div>
                                </div>
                            </td>
                        </tr>`;
                    $("#list-category").append(r);
                }
            },
            error: function () { },
        });
    }
    else {
                
        $.ajax({
            type: 'POST',
            url: '/editcategoryajax',
            data: {
                Id: $("#idCategory").val(),
                Title: $("#Title").val(),
                Description: $("#Description").val(),
                Position: $("#Position").val(),
                SeoTitle: $("#SeoTitle").val(),
                SeoDescription: $("#SeoDescription").val(),
                SeoKeyWords: $("#SeoKeyWords").val()
            },
            success: function (result) {
                $("#category-info").modal('hide');
                LoadData();
                //alert($("#trow_" + id + "> td:nth-child(2)").text());

                //$("#trow_"+ result.id+">td:nth-child(2)").text(result.title);
                //$("#trow_" +result.id+">td:nth-child(3)").text(result.position);
                //$("#trow_${"result.id"} .dropdown-menu .dropdown-item:n-child(1)").attr("href", "/Admin/category/edit/${result.id}");
                //$("#trow_${"result.id"} .dropdown-menu .dropdown-item:n-child(2)").attr("href", "/Admin/category/edit/${result.id}");
                $("#idCategory").val("") ;
            },
            error: function (erro) { },
        })
    }
});

// Hàm xóa danh mục
// Sau khi xóa danh mục thì cập nhật lại cột STT
function deleteCategory(id) {
    $.ajax({
        type: 'post',
        url: '/test-jquery',
        data: { id: id },
        success: function (result) {
            if (result != null) {
                $('#trow_' + id).remove();
                $(".stt").each(function (index) {
                    $(this).text(index + 1);
                });
            }
        },
        error: function () { },

    });
}

// hàm Load lại toàn bộ thông tin trong database
function LoadData() {
    // xóa danh sách nhiệm vụ cũ
    $("#list-category").empty();
    // gửi request
    $.ajax({
        type: 'POST',
        url: '/loaddata',
        success: function (result) {
            if (result.length > 0) {
                let i = 1;
                result.map((item) => {
                    let r =
                        `<tr id="trow_${item.id}">
                            <td class="stt">${i}</td>
                            <td>${item.title}</td>
                            <td>${item.position}</td>
                            <td>
                                <div class="dropdown">
                                    <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                        <i class="bx bx-dots-vertical-rounded"></i>
                                    </button>
                                    <div class="dropdown-menu">
                                        <a class="dropdown-item" href="/Admin/category/edit/${item.id}">
                                            <i class="bx bx-edit-alt me-1"></i>
                                            Sửa MVC
                                        </a>
                                        <a class="dropdown-item" onclick = "editCategoryAjax('${item.id}')" >
                                            <i class="bx bx-edit-alt me-1" ></i>
                                            Sửa Ajax
                                        </a >
                                        <a class="dropdown-item" href="/Admin/category/delete/${item.id}">
                                            <i class="bx bx-trash me-1"></i>
                                            Xóa MVC
                                        </a>
                                        <a data-id="${item.id}" class="dropdown-item" onclick="deleteCategory('${item.id}')">
                                            <i class="bx bx-trash me-1"></i>
                                            Xóa Ajax
                                        </a>
                                    </div>  
                                </div>
                            </td>
                        </tr>`;
                    $("#list-category").append(r);
                    i++;
                });
            }

        },
        error: function (error) {
            let r = `<h4>Không có nhiệm vụ</h4>`
            $("#list-category").append(r);

        }
    });
}

// Hàm xử lý sự kiện khi ấn nút Edit Ajax
function editCategoryAjax(id) {
    $.ajax({
        type: 'get',
        url: '/editcategoryajax',
        data: { id: id },
        success: function (result) {
            RenderModalUpdate(result);
        },
    });
}

function RenderModalUpdate(result) {
    $("#idCategory").val(result.id);
    $("#Title").val(result.title);
    $("#Description").val(result.description);
    $("#Position").val(result.position);
    $("#SeoTitle").val(result.seoTitle);
    $("#SeoDescription").val(result.seoDescription);
    $("#SeoKeyWords").val(result.seoKeyWords);
    $("#category-info").modal('show');
}

// Hàm xử lý sự kiện hide modal
// Sau khi hide modal thì xóa thông tin trong form
$("#category-info").on("hide.bs.modal", () => {
    ClearForm();
});

// Hàm xóa thông tin form
function ClearForm() {
    $("#Title").val("");
    $("#Description").val("");
    $("#Position").val("");
    $("#SeoTitle").val("");
    $("#SeoDescription").val("");
    $("#SeoKeyWords").val("");
}


$(document).ready(() => {
    loadData();
});

$("#open-modal").click(() => {
    $.ajax({
        type: 'post',
        url: '/admin/news/open_Modal',
        success: function (result) {
            renderModalEdit(result);
            $("#news-info").modal('show');
        },
        errorr: function (errorr) { }
    });
});

// Hàm sử lý sự kiện ấn nút add-new
$("#add-news").click(() => {
    tinyMCE.triggerSave();
    $.ajax({
        type: 'post',
        url: '/admin/news/add_Ajax',
        data: {
            Title: $("#Title").val(),
            Description: $("#Description").val() ,   
            Detail: $("#Detail").val(),
            ImageUrl: $("#ImageUrl").val(),
            IsActive: $("#IsActive").val() ,
            CategoryId: $("#CategoryId").val() ,
            SeoTitle: $("#SeoTitle").val() ,
            SeoDescription: $("#SeoDescription").val() ,
            SeoKeyWords: $("#SeoKeyWords").val() 
        },
        success: function (result) {
            $("#news-info").modal('hide');
            loadData();
        },
        errorr: function (error) {console.log(error)},
    });
});

// Hàm xử lý sự kiện ấn nút Sửa Ajax
function editNewsAjax(id){
    $.ajax({
        type: 'get',
        url: '/admin/news/edit_Ajax',
        data: { Id: id },
        success: function (result) {
            renderModalEdit(result);
        },
        errorr: function (errorr) {
            alert(errorr);
        },
    });
}

// Hàm xử lý sự kiện hide modal
// Sau khi hide modal thì xóa thông tin trong form
// hiện input text 
// ẩn select
$("#news-info").on("hide.bs.modal", () => {
    ClearForm();
});

// hàm xóa data trong form
function ClearForm() {
    $("#Title").val('');
    $("#avatar").removeAttr('src');
    $("#Description").val('');
    tinymce.activeEditor.setContent('');
    $("#IsActive").prop('checked', true);
    $("#CategoryId").empty();
    $("#SeoTitle").val('');
    $("#SeoDescription").val("");
    $("#SeoKeyWords").val("");
}

// hàm render modal
function renderModalEdit(item) {
    if (item.tbNews == null) {
        item.categoryList.forEach((i) => {
            $("#CategoryId").append(`<option value=${i.value.toString()}>${i.text.toString()}</option>`);
        });
    }
    else {
        $("#Title").val(item.tbNews.title);
        $("#avatar").prop('src', item.tbNews.imageUrl);
        $("#Description").val(item.tbNews.description);
        tinymce.activeEditor.setContent(item.tbNews.detail);
        $("#IsActive").prop('checked', item.tbNews.isActive ? true : false);
        $("#CategoryId").append(`<option value=${item.tbNews.categoryId}>${item.tbNews.tbCategory.title}</option>`);
        item.categoryList.forEach((i) => {
            $("#CategoryId").append(`<option value=${i.value.toString()}>${i.text.toString()}</option>`);
        });
        $("#SeoTitle").val(item.tbNews.seoTitle);
        $("#SeoDescription").val(item.tbNews.seoDescription);
        $("#SeoKeyWords").val(item.tbNews.seoKeyWords);
        $("#news-info").modal('show');
    }
    
}

// Hàm chọn tất cả ô checkbox
function selectAll() {
    var isCheck = $("#select-all").is(":checked");
    if (isCheck) {
        $(".check-all").prop("checked", true);
    }
    else {
        $(".check-all").prop("checked", false);
    }
}

function deleteChecked() {
    $(".check-all").each(function (index) {
        if ($(this).is(":checked")) {
            deleteNews($(this).val());
        }
    });
}

// Hàm xóa tin tức
// Sau khi xóa tin tức thì cập nhật lại cột STT
function deleteNews(id) {
    $.ajax({
        type: 'post',
        url: '/Admin/News/Delete',
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

// Hàm thay đổi trạng thái IsActive
function changeStatus(id) {
    $.ajax({
        type: 'post',
        url: '/Admin/News/changeStatus',
        data: {
            Id: id
        },
        success: function (result) {
            if (result.isActive == true) {
                $("#isActive_" + result.id).removeClass("text-danger");
                $("#isActive_" + result.id).removeClass("bx-x-circle");
                $("#isActive_" + result.id).addClass("bx-check-circle");
                $("#isActive_" + result.id).addClass("text-success");
            }
            else if (result.isActive == false) {
                $("#isActive_" + result.id).removeClass("text-success");
                $("#isActive_" + result.id).removeClass("bx-check-circle");
                $("#isActive_" + result.id).addClass("text-danger");
                $("#isActive_" + result.id).addClass("bx-x-circle");
            }
        }
    })
}

// Hàm load data từ db
function loadData() {
    $.ajax({
        type: 'post',
        url: '/admin/news/loadData',
        success: function (data) {
            $("#list-news").empty();
            if (data.length > 0) {
                data.forEach((item, index) => {
                    $("#list-news").append(`
                        <tr id="trow_${item.id}">
                            <td>
                                <input type="checkbox" class="check-all" style="transform: scale(1.5)" value="${item.id}" id="check_${item.id}"/>
                            </td>jj
                            <td class="stt">${index + 1}</td>
                            <td><img src="${item.imageUrl}" style="width:50px" /></td>
                            <td>${item.title}</td>
                            <td>${item.createdDate.toString("dd/MM/yyyy")}</td>
                            <td class="">
                                ${item.isActive ?
                            `<a href="javascript:void(0)" onclick="changeStatus(${item.id})">
                                        <i id="isActive_${item.id}" class='bx bx-check-circle text-success' style="transform: scale(1.5)"></i>
                                    </a>` :
                            `<a href="javascript:void(0)" onclick="changeStatus(${item.id})">
                                        <i id="isActive_${item.id}" class='bx bx-x-circle text-danger' style="transform: scale(1.5)"></i>
                                    </a>`
                        }
                            </td>
                            <td>
                                ${item.tbCategory ?
                            `<span>${item.tbCategory.title}</span>` :
                            `""`
                        }
                            </td>
                            <td>
                                <div class="dropdown">
                                    <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                        <i class="bx bx-dots-vertical-rounded"></i>
                                    </button>
                                    <div class="dropdown-menu">
                                        <a class="dropdown-item" href="/admin/news/edit_mvc/${item.id}"><i class="bx bx-edit-alt me-1"></i>Sửa MVC</a>
                                        <a class="dropdown-item" onclick="editNewsAjax(${item.id})"><i class="bx bx-edit-alt me-1"></i>Sửa Ajax</a>
                                        <a class="dropdown-item" href="/admin/news/delete/${item.id}"><i class="bx bx-trash me-1"></i> Xóa MVC</a>
                                        <a data-id="@item.News.Id" class="dropdown-item" onclick="deleteNews('${item.id}')"><i class="bx bx-trash me-1"></i> Xóa Ajax</a>
                                    </div>
                                </div>
                            </td>
                        </tr>    
                    `);
                });
            }
            else {
                $("#lis").append(`
                    <tr>
                        <td style="border:0" colspan="4">Không có tin tức</td>
                    </tr>
                `)
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}






//// var controllerName = '@ViewBag.ControllerName';
//// $(".menu-item").each(() => {
////     $(this).removeClass("active");
//// });
//// $("#" + controllerName).addClass("active");



//// Hàm thêm danh mục khi ấn nút "Thêm danh mục jquery"
//// Lấy các giá trị trong input và gửi tới controller thông qua ajax
//// Thêm đối tượng trả về vào table
//$("#add-news").click(() => {
//    var id = $("#idNews").val();
//if (id = "") {
//    $.ajax({
//        type: 'POST',
//        url: '/add-jquery',
//        data: {
//            Title: $("#Title").val(),
//            Description: $("#Description").val(),
//            Position: $("#Position").val(),
//            SeoTitle: $("#SeoTitle").val(),
//            SeoDescription: $("#SeoDescription").val(),
//            SeoKeyWords: $("#SeoKeyWords").val()
//        },
//        success: function (result) {
//            if (result != null) {
//                // đóng modal
//                $("#news-info").modal('hide');

//                let lastRow = parseInt($("#list-news tr:last-child td:first-child").text());
//                if (isNaN(lastRow)) lastRow = 0;
//                lastRow++;
//                // chạy hàm load
//                let r =
//                    `<tr id="trow_${result.id}">
//                                        <td class="stt">${lastRow}</td>
//                                        <td>${result.title}</td>
//                                        <td>${result.position}</td>
//                                        <td>
//                                            <div class="dropdown">
//                                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
//                                                    <i class="bx bx-dots-vertical-rounded"></i>
//                                                </button>
//                                                <div class="dropdown-menu">
//                                                            <a class="dropdown-item" asp-controller="category" asp-action="edit" asp-route-Id="${result.id}">
//                                                        <i class="bx bx-edit-alt me-1"></i>
//                                                        Sửa
//                                                    </a>
//                                                            <a class="dropdown-item" asp-controller="category" asp-action="delete" asp-route-Id="${result.id}">
//                                                        <i class="bx bx-trash me-1"></i>
//                                                        Xóa
//                                                    </a>
//                                                        <a data-id="${result.id}" class="dropdown-item" onclick="deleted('${result.id}')">
//                                                        <i class="bx bx-trash me-1"></i>
//                                                        Xóa Jquery
//                                                    </a>
//                                                </div>
//                                            </div>
//                                        </td>
//                                    </tr>`;
//                $("#list-category").append(r);
//            }
//        },
//        error: function () { },
//    });
//    }
//else {
//    $.ajax({
//        type: 'POST',
//        url: '/editcategoryajax',
//        data: {
//            Id: $("#idCategory").val(),
//            Title: $("#Title").val(),
//            Description: $("#Description").val(),
//            Position: $("#Position").val(),
//            SeoTitle: $("#SeoTitle").val(),
//            SeoDescription: $("#SeoDescription").val(),
//            SeoKeyWords: $("#SeoKeyWords").val()
//        },
//        success: function (result) {
//            $("#category-info").modal('hide');
//            LoadData();
//        },
//        error: function (erro) { },
//    })
//}

//});

//// Hàm xóa danh mục
//// Sau khi xóa danh mục thì cập nhật lại cột STT
//function deleteCategory(id) {
//    $.ajax({
//        type: 'post',
//        url: '/test-jquery',
//        data: { id: id },
//        success: function (result) {
//            if (result != null) {
//                $('#trow_' + id).remove();
//                $(".stt").each(function (index) {
//                    $(this).text(index + 1);
//                });
//            }
//        },
//        error: function () { },

//    });
//}

//// hàm Load lại toàn bộ thông tin trong database
//function LoadData() {
//    // xóa danh sách nhiệm vụ cũ
//    $("#list-category").empty();
//// gửi request
//$.ajax({
//    type: 'POST',
//url: '/loaddata',
//success: function (result) {
//            if (result.length > 0) {
//    let i = 1;
//                result.map((item) => {
//    let r =
//`<tr id="trow_${item.id}">
//    <td class="stt">${i}</td>
//    <td>${item.title}</td>
//    <td>${item.position}</td>
//    <td>
//        <div class="dropdown">
//            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
//                <i class="bx bx-dots-vertical-rounded"></i>
//            </button>
//            <div class="dropdown-menu">
//                <a class="dropdown-item" asp-controller="category" asp-action="edit" asp-route-Id="${item.id}">
//                    <i class="bx bx-edit-alt me-1"></i>
//                    Sửa
//                </a>
//                <a class="dropdown-item" asp-controller="category" asp-action="delete" asp-route-Id="${item.id}">
//                    <i class="bx bx-trash me-1"></i>
//                    Xóa
//                </a>
//                <a data-id="${item.id}" class="dropdown-item" onclick="deleted('${item.id}')">
//                    <i class="bx bx-trash me-1"></i>
//                    Xóa Jquery
//                </a>
//            </div>
//        </div>
//    </td>
//</tr>`;
//$("#list-category").append(r);
//i++;
//                });
//            }

//        },
//error: function (error) {
//    let r = `<h4>Không có nhiệm vụ</h4>`
//$("#list-category").append(r);

//        }
//    });
//}

//// Hàm xử lý sự kiện khi ấn nút Edit Ajax
//function editCategoryAjax(id) {
//    $.ajax({
//        type: 'get',
//        url: '/editcategoryajax',
//        data: { id: id },
//        success: function (result) {
//            alert(result.title);
//            RenderModalUpdate(result);
//        },
//    });

//}

//// Hàm xử lý sự kiện hide modal
//// Sau khi hide modal thì xóa thông tin trong form
//$("#category-info").on("hide.bs.modal", () => {
//    ClearForm();
//});

//// Hàm xóa thông tin form
//function ClearForm() {
//    $("#Title").val("");
//$("#Description").val("");
//$("#Position").val("");
//$("#SeoTitle").val("");
//$("#SeoDescription").val("");
//$("#SeoKeyWords").val("");
//}

//function RenderModalUpdate(result) {
//    $("#idCategory").val(result.id);
//$("#Title").val(result.title);
//$("#Description").val(result.description);
//$("#Position").val(result.position);
//$("#SeoTitle").val(result.seoTitle);
//$("#SeoDescription").val(result.seoDescription);
//$("#SeoKeyWords").val(result.seoKeyWords);
//$("#category-info").modal('show');
//}



$(document).ready(() => {

});

// Hàm load toàn bộ sản phẩm
// Vào db lấy thông tin sản phẩm theo View Model
// chạy vòng lặp trong danh sách trả về rồi hiển thị theo mẫu
function loadData() {
    $("#product_list").empty();
    $('.fruite-categorie > li').removeClass("bg-warning");
    $(`.fruite-categorie > li:last-child`).addClass("bg-warning");
    alert("gg");
    $.ajax({
        type: 'post',
        url: '/shop/loadData',
        success: function (result) {
            
            if (result.length > 0) {
                result.forEach((item) => {
                    $("#product_list").append(`
                        <div class="col-md-6 col-lg-6 col-xl-4">
                            <div class="rounded position-relative fruite-item">
                                <a href="javascript:void(0)" onclick="shopDetail(${item.id})" class="fruite-img">
                                    <img src=${item.imageUrl} class="img-fluid w-100 rounded-top" alt="">
                                </a>
                                <div class="text-white bg-secondary px-3 py-1 rounded position-absolute" style="top: 10px; left: 10px;">${item.productCategory}</div>
                                <div class="p-4 border border-secondary border-top-0 rounded-bottom">
                                    <a href="javascript:void(0)" onclick="shopDetail(${item.id})">
                                        <h4>${item.title}</h4>
                                    </a>
                                    <p>${item.description}</p>
                                    <div class="d-flex justify-content-between flex-lg-wrap">
                                        <p class="text-dark fs-5 fw-bold mb-0">${item.price} / kg</p>
                                        <a href="#" class="btn border border-secondary rounded-pill px-3 text-primary"><i class="fa fa-shopping-bag me-2 text-primary"></i> Add to cart</a>
                                    </div>
                                </div>
                            </div>
                        </div
                    `);
                });

            }
        },
        errorr: function (errorr) {
            console.log(errorr);
        },
    });
}

// Hàm load sản phẩm theo danh mục
// Vào db lấy thông tin sản phẩm theo danh mục
// chạy vòng lặp trong danh sách trả về rồi hiển thị theo mẫu
function loadProductCategory(id, stt) {
    $("#product_list").empty();
    $('.fruite-categorie > li').removeClass("bg-warning");
    $(`.fruite-categorie > li:nth-child(${stt})`).addClass("bg-warning");

    $.ajax({
        type: 'post',
        url: '/shop/loadData',
        data: { id: id },
        success: function (result) {
            if (result.length > 0) {
                result.forEach((item) => {
                    $("#product_list").append(`
                        <div class="col-md-6 col-lg-6 col-xl-4">
                            <div class="rounded position-relative fruite-item">
                                <a href="javascript:void(0)" onclick="shopDetail(${item.id})" class="fruite-img">
                                    <img src=${item.imageUrl} class="img-fluid w-100 rounded-top" alt="">
                                </a>
                                <div class="text-white bg-secondary px-3 py-1 rounded position-absolute" style="top: 10px; left: 10px;">${item.productCategory}</div>
                                <div class="p-4 border border-secondary border-top-0 rounded-bottom">
                                    <a href="javascript:void(0)" onclick="shopDetail(${item.id})">
                                        <h4>${item.title}</h4>
                                    </a>
                                    <p>${item.description} </p>
                                    <div class="d-flex justify-content-between flex-lg-wrap">
                                        <p class="text-dark fs-5 fw-bold mb-0">${item.price} / kg</p>
                                        <a href="#" class="btn border border-secondary rounded-pill px-3 text-primary"><i class="fa fa-shopping-bag me-2 text-primary"></i> Add to cart</a>
                                    </div>
                                </div>
                            </div>
                        </div
                    `);
                });

            }
        },
        errorr: function (errorr) {
            console.log(errorr);
        },
    });
}
//href="/shopdetail/index/${item.id}"
function shopDetail(id) {
    $("#product_list").empty();

    $.ajax({
        type: 'post',
        url: '/shop/loadShopDetail',
        data: { id: id },
        success: function (result) {
            $("#product_list").append(`
                <div class="col-lg-6">
                    <div class="border rounded">
                        <a href="#">
                            <img src=${result.imageUrl} class="img-fluid rounded" alt="Image">
                        </a>
                    </div>
                </div>

                <div class="col-lg-6">
                    <h4 class="fw-bold mb-3">${result.title}</h4>
                    <p class="mb-3">Category: ${result.category}</p>
                    <h5 class="fw-bold mb-3">${result.price} $</h5>
                    <div class="d-flex mb-4">
                        <i class="fa fa-star text-secondary"></i>
                        <i class="fa fa-star text-secondary"></i>
                        <i class="fa fa-star text-secondary"></i>
                        <i class="fa fa-star text-secondary"></i>
                        <i class="fa fa-star"></i>
                    </div>
                    <p class="mb-4">${result.description}</p>
                    <div class="input-group quantity mb-5" style="width: 100px;">
                        <div class="input-group-btn">
                            <button class="btn btn-sm btn-minus rounded-circle bg-light border">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                        <input type="text" class="form-control form-control-sm text-center border-0" value="1">
                        <div class="input-group-btn">
                            <button class="btn btn-sm btn-plus rounded-circle bg-light border">
                                <i class="fa fa-plus"></i>
                            </button>
                        </div>
                    </div>
                    <a href="#" class="btn border border-secondary rounded-pill px-4 py-2 mb-4 text-primary"><i class="fa fa-shopping-bag me-2 text-primary"></i> Add to cart</a>
                </div>

                <div class="col-lg-12">
                    <nav>
                        <div class="nav nav-tabs mb-3">
                            <button class="nav-link active border-white border-bottom-0" type="button" role="tab"
                                    id="nav-about-tab" data-bs-toggle="tab" data-bs-target="#nav-about"
                                    aria-controls="nav-about" aria-selected="true">
                                Description
                            </button>
                            <button class="nav-link border-white border-bottom-0" type="button" role="tab"
                                    id="nav-mission-tab" data-bs-toggle="tab" data-bs-target="#nav-mission"
                                    aria-controls="nav-mission" aria-selected="false">
                                Reviews
                            </button>
                        </div>
                    </nav>
                    <div class="tab-content mb-5">
                        <div class="tab-pane active" id="nav-about" role="tabpanel" aria-labelledby="nav-about-tab">
                            <div>${result.detail}</div>
                            <div class="px-2">
                                <div class="row g-4">
                                    <div class="col-6">
                                        <div class="row bg-light align-items-center text-center justify-content-center py-2">
                                            <div class="col-6">
                                                <p class="mb-0">Weight</p>
                                            </div>
                                            <div class="col-6">
                                                <p class="mb-0">1 kg</p>
                                            </div>
                                        </div>
                                        <div class="row text-center align-items-center justify-content-center py-2">
                                            <div class="col-6">
                                                <p class="mb-0">Country of Origin</p>
                                            </div>
                                            <div class="col-6">
                                                <p class="mb-0">Agro Farm</p>
                                            </div>
                                        </div>
                                        <div class="row bg-light text-center align-items-center justify-content-center py-2">
                                            <div class="col-6">
                                                <p class="mb-0">Quality</p>
                                            </div>
                                            <div class="col-6">
                                                <p class="mb-0">Organic</p>
                                            </div>
                                        </div>
                                        <div class="row text-center align-items-center justify-content-center py-2">
                                            <div class="col-6">
                                                <p class="mb-0">Сheck</p>
                                            </div>
                                            <div class="col-6">
                                                <p class="mb-0">Healthy</p>
                                            </div>
                                        </div>
                                        <div class="row bg-light text-center align-items-center justify-content-center py-2">
                                            <div class="col-6">
                                                <p class="mb-0">Min Weight</p>
                                            </div>
                                            <div class="col-6">
                                                <p class="mb-0">250 Kg</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="nav-mission" role="tabpanel" aria-labelledby="nav-mission-tab">
                            <div class="d-flex">
                                <div class="">
                                    <p class="mb-2" style="font-size: 14px;">April 12, 2024</p>
                                    <div class="d-flex justify-content-between">
                                        <h5>Jason Smith</h5>
                                        <div class="d-flex mb-3">
                                            <i class="fa fa-star text-secondary"></i>
                                            <i class="fa fa-star text-secondary"></i>
                                            <i class="fa fa-star text-secondary"></i>
                                            <i class="fa fa-star text-secondary"></i>
                                            <i class="fa fa-star"></i>
                                        </div>
                                    </div>
                                    <p>
                                        The generated Lorem Ipsum is therefore always free from repetition injected humour, or non-characteristic
                                        words etc. Susp endisse ultricies nisi vel quam suscipit
                                    </p>
                                </div>
                            </div>
                            <div class="d-flex">
                                <div class="">
                                    <p class="mb-2" style="font-size: 14px;">April 12, 2024</p>
                                    <div class="d-flex justify-content-between">
                                        <h5>Sam Peters</h5>
                                        <div class="d-flex mb-3">
                                            <i class="fa fa-star text-secondary"></i>
                                            <i class="fa fa-star text-secondary"></i>
                                            <i class="fa fa-star text-secondary"></i>
                                            <i class="fa fa-star"></i>
                                            <i class="fa fa-star"></i>
                                        </div>
                                    </div>
                                    <p class="text-dark">
                                        The generated Lorem Ipsum is therefore always free from repetition injected humour, or non-characteristic
                                        words etc. Susp endisse ultricies nisi vel quam suscipit
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="nav-vision" role="tabpanel">
                            <p class="text-dark">
                                Tempor erat elitr rebum at clita. Diam dolor diam ipsum et tempor sit. Aliqu diam
                                amet diam et eos labore. 3
                            </p>
                            <p class="mb-0">
                                Diam dolor diam ipsum et tempor sit. Aliqu diam amet diam et eos labore.
                                Clita erat ipsum et lorem et sit
                            </p>
                        </div>
                    </div>
                </div>
            `);
        },
        errorr: function (errorr) {

        },
    });
}
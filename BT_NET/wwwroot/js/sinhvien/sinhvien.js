let currentPage = 1;
let currPageSize = 10;

$(document).ready(function(){
    loadSinhVien(currentPage);
});

function loadSinhVien(page){
    $('#sinhVienTableContainer').html(`
        <div class="text-center p-5">
            <div class="spinner-border text-primary" role="status"></div>
            <div class="mt-2 text-muted">Đang tải danh sách sinh viên...</div>
        </div>
    `);

    $.ajax({
        url:'/SinhVien/GetSinhVien',
        type:'GET',
        data:{
            page: page,
            pageSize: currPageSize
        },
        success: function(response){
            $('#sinhVienTableContainer').html(response)

            currentPage = page;
        },
        error : function(){
            alert("Lỗi: Không thể tải dữ liệu sinh viên lúc này")
        }
    });
}

$(document).on('click','.pagination-link',function(e){
    e.preventDefault();

    let page =$(this).data('page');

    if($(this).parent().hasClass('disabled') || $(this).parent().hasClass('active')){
        return;
    }
    loadSinhVien(page)
})

$(document).on('change','#pageSizeSelect', function(){
    currPageSize = $(this).val();
    currentPage = 1;
    loadSinhVien(currentPage);
})g
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
})

$(document).on('click','#btnOpenCreateModal',function(){
    $.get('/SinhVien/Create',function(response){
        $('#modalContent').html(response);

        $('#sinhVienModal').modal('show')
    })

})

$(document).on('click','#btnSaveCreate',function(){
    let formData = $('#createSinhVienForm').serialize();

    $.ajax({
        url:'/SinhVien/Create',
        type:'POST',
        data: formData,
        success: function(response){
            if(response.success){
                $('#sinhVienModal').modal('hide');
                
                loadSinhVien(currentPage);

                alert(response.message)
            }else{
                $('modalContent').html(response);
            }
        },
        error: function(){
            alert("Co loi xay ra trong qua trinh luu du lieu")
        }
    });
});

$(document).on('click','.btn-edit', function(){
    let sinhVienId = $(this).data("id");
    alert("Đã bấm nút sửa, ID của sinh viên là: " + sinhVienId);

    $.get('/SinhVien/Edit',{id : sinhVienId}, function(response){
        $('#modalContent').html(response);
        $('#sinhVienModal').modal('show');
    }).fail(function(){
        alert("khong the tai thong tin")
    });
});

$(document).on('click','#btnSaveEdit', function(){
    let formData = $('#editSinhVienForm').serialize();

    $.ajax({
        url:'/SinhVien/Edit',
        type:'POST',
        data:formData,
        success:function(response){
            if(response.success){
                $('#sinhVienModal').modal('hide');

                loadSinhVien(currentPage);
                alert(response.message);
            }else{
                $('modalContent').html(response);
            }
        },
        error:function(){
            alert("co loi xay ra khi cap nhat du lieu");
        }
    });
})

$(document).on('click','.btn-delete',function(){
    let sinhVienId = $(this).data('id');

    if(confirm("ban co chac chan muon xoa")){
        $.ajax({
            url:'/SinhVien/Delete',
            type:'POST',
            data:{id:sinhVienId},
            success: function(response){
                if(response.success){
                    loadSinhVien(currentPage);

                    alert(response.message);
                }else{
                    alert(response.message);
                }
            },
            error: function(){
                alert("co loi xay ra");
            }
        })
    }
})
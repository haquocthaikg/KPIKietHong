$(document).ready(function () {
    var t = $('#editable-datatable').DataTable({

        "language": {
            "lengthMenu": "Hiển thị _MENU_ dòng trên mỗi trang",
            "zeroRecords": "Không tìm thấy",
            "info": "Hiện trang _PAGE_ của _PAGES_ ",
            "infoEmpty": "Không có dữ liệu",
            "infoFiltered": "(Được lọc từ _MAX_ tổng số dòng)",
            "thousands": ",",
            "lengthMenu": "Hiện _MENU_ dòng",
            "loadingRecords": "Đang tải...",
            "processing": "Xử lý...",
            "search": "Tìm:",
            "zeroRecords": "Không tìm thấy",
            "paginate": {
                "first": "Đầu",
                "last": "Cuối",
                "next": "Kế",
                "previous": "Trước"
            },
            "aria": {
                "sortAscending": ": Kích hoạt để sắp xếp cột tăng dần",
                "sortDescending": ": Kích hoạt để sắp xếp cột giảm dần"
            }
        },//Thêm số thứ tự vào
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']]
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();


});


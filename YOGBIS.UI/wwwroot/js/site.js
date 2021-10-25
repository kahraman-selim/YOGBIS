// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function myFunction() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("mySearch");
    filter = input.value.toUpperCase();
    ul = document.getElementById("myMenu");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

$(document).ready(function () {
    var selected = [];
    $('#myTable').DataTable({
        "fixedHeader": true,
        "fixedColumns": true,
        "responsive": true,
        "dom": '<"html5buttons"B>lTfgitp',
        "scrollY": "350px",
        "scrollCollapse": true,
        "paging": true,
        "scrollX": true,
        "autoFill": true,
        //"processing": true,
        //"serverSide": true,
        //"ajax": "scripts/ids-arrays.php",
        //"rowCallback": function (row, data) {
        //    if ($.inArray(data.DT_RowId, selected) !== -1) {
        //        $(row).addClass('selected');
        //    }
        //},
        "language": {
            "emptyTable": "Gösterilecek veri yok.",
            "processing": "Veriler yükleniyor",
            "sDecimal": ".",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        },
        //columnDefs: [{
        //    targets: 0,
        //    data: null,
        //    defaultContent: '',
        //    orderable: false,
        //    className: 'select-checkbox'
        //}],
        //select: {
        //    style: 'os',
        //    selector: 'td:first-child'
        //},
        //order: [[1, 'asc']],
        /*dom: 'Bfrtip',*/
        buttons: [
            {
                text: 'Kopyala',
                extend: 'copy',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2'
            },
            {
                text: 'Excel',
                extend: 'excel',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2'
            },
            {
                text: 'Pdf',
                extend: 'pdf',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2'
            },
            {
                text: 'PdfYatay',
                extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2',
                download:'open'
            },
            {
                text: 'Yazdır',
                extend: 'print',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2',
                exportOptions: {
                    columns: ':visible',
                    modifier: {
                        selected: null
                    }
                }
            },
            {
                text: 'Kolonlar',
                extend: 'colvis',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2'
            },
        ],
        columnDefs: [{
            targets: -1,
            visible: false
        }],
        select: true

    });
        //table.buttons().container()
        //.appendTo('#example_wrapper .small-6.columns:eq(0)');

    //$('#myTable tbody').on('click', 'tr', function () {
    //    var id = this.id;
    //    var index = $.inArray(id, selected);

    //    if (index === -1) {
    //        selected.push(id);
    //    } else {
    //        selected.splice(index, 1);
    //    }

    //    $(this).toggleClass('selected');
    //});
});
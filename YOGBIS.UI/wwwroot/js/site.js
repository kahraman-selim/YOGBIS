
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
    var table=$('#myTable').DataTable({
        fixedHeader: {
            header: true,
            headerOffset: $('#navbar').height()
        },  
        stateSave: true,
        responsive: true,
        dom: '<"html5buttons"B>lTfgitp',  
        "paging": true,
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
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2',
                download: 'open'
            },
            {
                text: 'PdfYatay',
                extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2',
                download: 'open'
            },
            {
                text: 'Yazdır',
                extend: 'print',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2',
                exportOptions: {
                    columns: ':visible',
                }
            },
            {
                text: 'Kolonlar',
                extend: 'colvis',
                className: 'btn btn-info btn-sm mt-1 mb-2 ml-2'
            },
        ],
        columnDefs: [{            
            visible: false
        }],
        select: true
    });

    new $.fn.dataTable.FixedHeader(table);
});
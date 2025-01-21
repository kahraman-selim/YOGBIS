$(document).ready(function () {
    //var selected = [];
    var table = $('#myTableNoButtons').DataTable({
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

        },
        buttons: [

        ],
        columnDefs: [{
            visible: false
        }],
        //select: true

    });

    new $.fn.dataTable.FixedHeader(table);
});
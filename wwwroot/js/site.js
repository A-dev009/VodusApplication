$(document).ready(function () {
    $('#sidebarcollapse').on('click', function () {
        $('#sidebar').toggleclass('active');
        if ($('#sidebar').hasclass('active')) {
            $('#contentwrapper').css('margin-left', 0);
            $('.main-header').css('margin-left', 0);
            $('#sidebarcollapse').css('margin-left', 250);
        } else {
            $('#contentwrapper').css('margin-left', 0);
            $('.main-header').css('margin-left', 0);
            $('#sidebarcollapse').css('margin-left', 0);
        }
    });
});

var startDate, endDate;

$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var start = startDate.val();
        var end = endDate.val();
        var date = new Date(data[4]);

        if (
            (start === null && end === null) ||
            (start === null && date <= end) ||
            (start <= date && end === null) ||
            (start <= date && date <= end)
        ) {
            return true;
        }
        return false;
    }
);

$(document).ready(function () {
    // Create date inputs
    startDate = new DateTime($('#startDate'), {
        format: 'MMMM Do YYYY'
    });
    endDate = new DateTime($('#endDate'), {
        format: 'MMMM Do YYYY'
    });

    // DataTables initialisation
    var table = $('#vodusTable').DataTable();

    // Refilter the table
    $('#startDate, #endDate').on('change', function () {

        let sdate = $('#startDate').val();
        let edate = $('#endDate').val();
        if (sdate == '' && edate == '') {
          
            window.location.reload();
        }
     
        table.draw();
    });

    $("#vodusTable").sortable({
        items: 'tr:not(tr:first-child)',
        cursor: 'pointer',
        axis: 'y',
        dropOnEmpty: false,
        start: function (e, ui) {
            ui.item.addClass("selected");
        },
        stop: function (e, ui) {
            ui.item.removeClass("selected");
    
        }
    });
});

$("#vodusExcelFile").change(() => {
    $("#vodusExcelFileUploadForm").submit();
});


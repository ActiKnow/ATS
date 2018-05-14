function GetDimenDtl(data) {
    var selectedTransporterTable = $('.selectedColVal_Table');
    var selectedColumnList = $('#selectedColumnValueIndex');
    if (data === undefined) {

        showDimensionDetails();
        $('.selectedColumnValueIndex').val(0);

        Pagination(selectedColumnValueIndex, selectedColumnValuepazeSize, selectedColumnData.length, selectedTransporterTable, 'selected-col-valu-table', true, true, function () { selectedColumnList.val(parseInt($(this).attr('pagenum'))); GetDimenDtl(); });

        if (selectedColumnData && selectedColumnData.length > 0) {
            for (counter = selectedColumnValueIndex; counter < (selectedColumnValueIndex + selectedColumnValuepazeSize); counter++) {
                if (counter >= selectedColumnData.length)
                    break;
                var row = selectedColumnData[counter];

                var displayRow = $('<tr class="gradeA"></tr>')
                    .append($("<td/>").attr('data-colname', 'Code').append(row[code]).attr('data-id', row[code]))
                    .append($("<td/>").attr('data-colname', 'Description').append(row[description]).attr('data-id', row[code]))
                    .dblclick(function () {
                        $(this).parent().find('tr').removeClass('active');
                        $(this).addClass('active');

                        var selectedColVal = $(this).find('td[data-colname="Description"]');

                        if (selectedColVal) {
                            if (loadByDataId) {
                                $(valuecontainer).val(selectedColVal.data('id'));
                            }
                            else
                                $(valuecontainer).val(selectedColVal.text());
                            (displaycontainer).val(selectedColVal.text());
                            (displaycontainer).attr('title', selectedColVal.text());
                            modalPopup.modal("hide");
                        }
                    });
                selectedTransporterTable.find('tbody').append(displayRow);
            }
        }
    }
    selectedColumnData = data;
    var selectedColumnValueIndex = parseInt(selectedColumnList.val());
    var selectedColumnValuepazeSize = 10;

    selectedTransporterTable.find('tbody').html('');
    if (selectedColumnData) {
        if (selectedColumnData.length > 0) {
            selectedColumnList.val(0);
            Pagination(selectedColumnValueIndex, selectedColumnValuepazeSize, selectedColumnData.length, selectedTransporterTable, 'selected-col-valu-table', true, true, function () { selectedColumnList.val(parseInt($(this).attr('pagenum'))); GetDimenDtl(); });

            if (selectedColumnData.length > 0) {
                for (counter = selectedColumnValueIndex; counter < (selectedColumnValueIndex + selectedColumnValuepazeSize); counter++) {
                    if (counter >= selectedColumnData.length)
                        break;
                    var row = selectedColumnData[counter];

                    var displayRow = $('<tr class="gradeA"></tr>')
                        .append($("<td/>").attr('data-colname', 'Code').append(row[code]).attr('data-id', row[code]))
                        .append($("<td/>").attr('data-colname', 'Description').append(row[description]).attr('data-id', row[code]))
                        .dblclick(function () {
                            $(this).parent().find('tr').removeClass('active');
                            $(this).addClass('active');

                            var selectedColVal = $(this).find('td[data-colname="Description"]');

                            if (selectedColVal) {
                                if (loadByDataId) {
                                    $(valuecontainer).val(selectedColVal.data('id'));
                                }
                                else
                                    $(valuecontainer).val(selectedColVal.text());
                                (displaycontainer).val(selectedColVal.text());
                                (displaycontainer).attr('title', selectedColVal.text());
                                modalPopup.modal("hide");
                            }
                        });
                    selectedTransporterTable.find('tbody').append(displayRow);
                }
            }
        }
        else {
            selectedColumnList.val(0);
            Pagination(selectedColumnValueIndex, selectedColumnValuepazeSize, selectedColumnData.length, selectedTransporterTable, 'selected-col-valu-table', true, true, function () { selectedColumnList.val(parseInt($(this).attr('pagenum'))); GetDimenDtl(); });
            selectedTransporterTable.find('tbody').html($('<tr ></tr>').append($('<td colspan="2"></td>').append('Data Not found')));
        }
    }
}

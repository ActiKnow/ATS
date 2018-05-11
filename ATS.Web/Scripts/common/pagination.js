function Pagination(recordStartIndex, pageSize, totalResultFound, tableElement, elementName, showontop, showonbot, callback) {
    if (!showontop && !showonbot)
        return;

    $(tableElement).parent().parent().find('.pagination-main').remove();
    if (showontop) {
        $(tableElement).parent().before(createDiv('row pagination-main').attr('name', elementName)
            .append(createDiv('col-sm-5').append(createDiv('record-details')))
            .append(createDiv('col-sm-7').append($('<ul/>').addClass('pagination pagination-sm justify-content-end'))));
    }
    if (showonbot) {
        $(tableElement).parent().after(createDiv('row pagination-main').attr('name', elementName)
            .append(createDiv('col-sm-5').append(createDiv('record-details')))
            .append(createDiv('col-sm-7').append($('<ul/>').addClass('pagination pagination-sm justify-content-end'))));
    }
    var paginationcontainer = $('[name="' + elementName + '"]').find('.pagination');
    var recordcountcontainer = $('[name="' + elementName + '"]').find('.record-details');
    var startIndex = recordStartIndex;
    toRecord = (pageSize + recordStartIndex);
    if ((pageSize + recordStartIndex) > totalResultFound)
        toRecord = totalResultFound;
    $(recordcountcontainer).html('Showing ' + (recordStartIndex + totalResultFound > 0 ? (recordStartIndex > 1 ? recordStartIndex : 1) : 0) + ' to ' + toRecord + ' of ' + totalResultFound + '');
    var currentPage = (startIndex / pageSize) + 1;
    var lastPageNum = parseInt(totalResultFound / pageSize);
    if (parseInt(totalResultFound % pageSize) > 0)
        lastPageNum += 1;

    if (currentPage != 1) {
        $(paginationcontainer).append($('<li></li>').addClass('page-item pageNumber').append($('<a></a>').addClass('page-link').attr('pagenum', (0)).append('First')));
        $(paginationcontainer).append($('<li></li>').addClass('page-item pageNumber').append($('<a></a>').addClass('page-link').attr('pagenum', (recordStartIndex - pageSize)).append('Previous')));
    }
    var pageCount = 5;

    for (backCounter = 2; backCounter > 0; backCounter--) {
        thisPageNum = currentPage - backCounter;
        if (thisPageNum >= 1) {
            $(paginationcontainer).append($('<li></li>').addClass('page-item pageNumber').append($('<a></a>').addClass('page-link').attr('pagenum', ((thisPageNum * pageSize) - pageSize)).append(thisPageNum)));
            pageCount--;
        }
    }

    $(paginationcontainer).append($('<li></li>').addClass('page-item active').append($('<a></a>').addClass('page-link').attr('pagenum', (recordStartIndex)).append(currentPage)));
    pageCount--;

    for (backCounter = 1; backCounter <= pageCount; backCounter++) {
        thisPageNum = currentPage + backCounter;
        if (thisPageNum <= lastPageNum) {
            $(paginationcontainer).append($('<li></li>').addClass('page-item').append($('<a></a>').addClass('page-link').attr('pagenum', ((thisPageNum * pageSize) - pageSize)).append(thisPageNum)));
        }
    }

    if (currentPage < lastPageNum) {
        $(paginationcontainer).append($('<li></li>').addClass('page-item').append($('<a></a>').addClass('page-link').attr('pagenum', (recordStartIndex + pageSize)).append('Next')));
        $(paginationcontainer).append($('<li></li>').addClass('page-item').append($('<a></a>').addClass('page-link').attr('pagenum', ((lastPageNum - 1) * pageSize)).append('Last')));
    }

    $(paginationcontainer).find('a').not('.disabled').click(callback);
}
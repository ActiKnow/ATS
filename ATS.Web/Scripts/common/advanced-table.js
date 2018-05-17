///Use this as following
//$("#test").advancedTable({
//  tabIndex:20,
//rowActiveClass:'active',
//cellFocus : true,
//    tableClick: function (row) {
//        console.log('table Click');
//    },
//    tableFocus: function (row) {
//        console.log('TABLE focus');
//    },
//    tableRowClick: function (row) {
//        console.log('Row Click');
//        console.log(row);
//    },
//    tableRowdblClick: function (row) {
//        console.log('Row dbl Click');
//        //console.log(row);
//    },
//    tableDownPress: function (row) {
//        console.log('down key');
//        //console.log(row);
//    },
//    tableEnterPress: function (row) {
//        console.log('down key');
//        console.log('enter is down.......i repeat enter is down')
//        //console.log(row);
//    },
//    containerClick: function (row) {
//        console.log('container click');
//    }
//  tableRowChanged: function(row) {
//  });

(function () {
    var keys = { RETURN: 13, LEFT: 37, UP: 38, RIGHT: 39, DOWN: 40, TAB: 9 };
    var _allOption;
    var _$container, _$table, _$tBody;
    var _allContainer = [];
    var _containerIndex = 0;
    var containerID = 'containerID';
    var defaultContainerClass = 'table-container',
        defualtTableClass = 'advanced-table',
        defualtTableBodyClass = 'advanced-table-body',
        defaultTableActiveClass = 'active-advanced-table',
        defaultRowActiveClass = 'active';
    var _isTableActive = false;
    var _activeRow;
    var _tabIndexContainer = '-1', _tabIndexTable = '0';
    var _offsetScroll = 0;

    //This is start function 
    var init = function (container, options) {
        _allOption = options;
        if (_allOption.tabIndex !== undefined) {
            _tabIndexTable = _allOption.tabIndex;
        }
        if (_allOption.rowActiveClass !== undefined) {
            defaultRowActiveClass = _allOption.rowActiveClass;
        }
        _$container = $(container).addClass(defaultContainerClass).attr('tabindex', _tabIndexContainer).data(containerID, _containerIndex);
        _allContainer.push({ container: _$container, option: _allOption });
        _containerIndex++;
        load.loadControls();
        load.loadEvents();
    };
    var load = {
        //Load Table Controls
        loadControls: function () {
            _$table = _$container.find('table').addClass(defualtTableClass).attr('tabindex', _tabIndexTable);
        },
        //Load All Events
        loadEvents: function () {
            unload.unloadEvents();

            for (let event in bindEvent) {
                bindEvent[event]();
            }
        }

    };
    var unload = {
        //Unload all events
        unloadEvents: function () {

            for (let event in unbindEvent) {
                unbindEvent[event]();
            }
        }
    };
    var action = {
        //set active table
        setActiveTable: function (e) {
            var target = $(e.target);
            _$container = target.closest('div.' + defaultContainerClass);
            _activeRow = target.closest('tr');
            load.loadControls();
            _$table = target.closest('table');
            load.loadEvents();
            action.setActiveRow(_$table, _activeRow);
            _$table.focus();
            if (_allContainer[_$container.data(containerID)].option.cellFocus !== undefined) {
                if (_allContainer[_$container.data(containerID)].option.cellFocus) {
                    target.focus();
                }
            }
        },
        //event for focus on containing table
        containerFocus: function (e) {
            var target = $(e.target);
            _$container = target.closest('div.' + defaultContainerClass);
            load.loadControls();
            load.loadEvents();
        },
        //container click event 
        containerClick: function (e) {
            action.setActiveTable(e);
            action.callBack(_allContainer[_$container.data(containerID)].option.containerClick, _activeRow);
        },
        //container table click
        containerTableClick: function (e) {
            action.setActiveTable(e);
            action.callBack(_allContainer[_$container.data(containerID)].option.containerTableClick, _activeRow);
        },
        //container row click
        containerRowClick: function (e) {
            action.setActiveTable(e);
            action.callBack(_allContainer[_$container.data(containerID)].option.containerRowClick, _activeRow);
        },
        //set active row
        setActiveRow: function (tableFound, selectedRow) {
            _activeRow = undefined;
            tableFound.find('tbody').addClass(defualtTableBodyClass).each(function () {
                $(this).find('tr.' + defaultRowActiveClass).removeClass(defaultRowActiveClass);
                if (selectedRow === undefined) {
                    _activeRow = $(this).find('tr:visible:first').addClass(defaultRowActiveClass);
                }
                else {
                    //$(this).find('tr.active').removeClass('active');
                    _activeRow = $(this).find(selectedRow).addClass(defaultRowActiveClass);
                }
            });
        },
        //set scrolling
        setScrolling: function (tbody) {
            var totalRow = tbody.find('tr');
            var activeRow = tbody.find("tr." + defaultRowActiveClass);
            var rowIndex = totalRow.index(activeRow);
            var allRowLength = totalRow.length;
            var offset = - _offsetScroll;
            if (activeRow !== undefined && activeRow.offset() !== undefined) {
                //console.log(activeRow.offset().top + ' + ' + tbody.offset().top + ' + ' + offset);
                var scrollVal = (activeRow.offset().top + tbody.scrollTop()) - tbody.offset().top - $(window).scrollTop() + offset;
                scrollVal = (scrollVal);
                tbody.scrollTop(scrollVal);
            }
        },
        //keys event 
        keyEventHandler: function (e) {
            var current = $(e.target);
            _$container = current.closest('div.' + defaultContainerClass);
            var table = current.closest('table.' + defualtTableClass);
            //var tbody = table.find('tbody.' + defualtTableBodyClass);
            var tbody = table.find('tbody');
            //console.log(tbody);
            switch (e.keyCode) {
                case keys.TAB: //tab
                    _isTableActive = false;
                    var ntabindex = parseFloat(table.attr('tabIndex'));
                    $('input[tabindex=' + (ntabindex + 1) + ']').focus();
                    break;
                case keys.UP: // up

                    if (table.find("tbody tr." + defaultRowActiveClass).prevAll(':visible:first').length > 0) {
                        _activeRow = table.find("tbody tr." + defaultRowActiveClass).removeClass(defaultRowActiveClass).prevAll(':visible:first').addClass(defaultRowActiveClass);
                        action.setScrolling(tbody);

                        action.callBack(_allContainer[_$container.data(containerID)].option.tableUpPress, _activeRow);

                    }
                    else if (table.find("tbody tr:visible." + defaultRowActiveClass).length == 0) {
                        _activeRow = table.find("tbody tr:visible:first").addClass(defaultRowActiveClass);
                    }
                    break;
                case keys.DOWN: // down

                    if (table.find("tbody tr." + defaultRowActiveClass).nextAll(':visible:first').length > 0) {
                        _activeRow = table.find("tbody tr." + defaultRowActiveClass).removeClass(defaultRowActiveClass).nextAll(':visible:first').addClass(defaultRowActiveClass);
                        action.setScrolling(tbody);

                        action.callBack(_allContainer[_$container.data(containerID)].option.tableDownPress, _activeRow);

                    }
                    else if (table.find("tbody tr:visible." + defaultRowActiveClass).length == 0) {
                        _activeRow = table.find("tbody tr:visible:first").addClass(defaultRowActiveClass);
                    }
                    break;
                case keys.RETURN: // enter
                    _activeRow = table.find("tbody tr." + defaultRowActiveClass);

                    action.callBack(_allContainer[_$container.data(containerID)].option.tableEnterPress, _activeRow);

                    return false;
            }


        },
        // table click event
        tableClick: function (e) {
            _isTableActive = $.contains(_$table, e.target);
            load.loadEvents();
            action.callBack(_allContainer[_$container.data(containerID)].option.tableClick, _activeRow);
        },
        //table focus event
        tableFocus: function (e) {
            var table = $(e.target);
            _$container = table.closest('div.' + defaultContainerClass);
            _$container.find('.' + defualtTableClass).removeClass(defaultTableActiveClass);
            table.addClass(defualtTableClass).attr('tabindex', _tabIndexTable).addClass(defaultTableActiveClass);
            if (!_isTableActive) {
                _isTableActive = true;
            }
            _activeRow = table.find('tbody tr:visible.' + defaultRowActiveClass);
            if (_activeRow.length == 0) {
                action.setActiveRow(table);
            }
            action.callBack(_allContainer[_$container.data(containerID)].option.tableFocus, _activeRow);
        },
        //row click event
        rowCLick: function (e) {
            var current = $(e.target);
            _$container = current.closest('div.' + defaultContainerClass);
            var row = current.closest('tr');
            var tbody = current.closest('tbody');
            tbody.find('tr.' + defaultRowActiveClass).removeClass(defaultRowActiveClass);
            _activeRow = row.addClass(defaultRowActiveClass);
            action.setScrollOffset(tbody);
            action.setScrolling(tbody);
            action.callBack(_allContainer[_$container.data(containerID)].option.tableRowClick, _activeRow);
        },
        //row double click
        rowdblClick: function (e) {
            var current = $(e.target);
            _$container = current.closest('div.' + defaultContainerClass);
            var row = current.closest('tr');
            var tbody = current.closest('tbody');
            tbody.find('tr.' + defaultRowActiveClass).removeClass(defaultRowActiveClass);
            _activeRow = row.addClass(defaultRowActiveClass);
            action.setScrollOffset(tbody);
            action.setScrolling(tbody);
            action.callBack(_allContainer[_$container.data(containerID)].option.tableRowdblClick, _activeRow);
        },
        //table scroll event
        tableScroll: function (e) {
            var tbody = $(e.target).closest('tbody.' + defualtTableBodyClass);
            action.setActiveRow(tbody);
        },
        //set scroll offset
        setScrollOffset: function (tbody) {
            var activeRow = tbody.find("tr." + defaultRowActiveClass);
            _offsetScroll = (activeRow.offset().top - tbody.offset().top);
            if (_offsetScroll <= 0) {
                _offsetScroll = activeRow.height();
            }
            if (_offsetScroll > tbody.height()) {
                _offsetScroll = tbody.height();
            }
        },
        //call back function calling
        callBack: function (actionEvent, activeRow) {
            if (actionEvent !== undefined && activeRow !== undefined) {
                actionEvent(activeRow);
            }
            var tableRowChanged = _allContainer[_$container.data(containerID)].option.tableRowChanged;
            if (tableRowChanged !== undefined && activeRow !== undefined) {
                tableRowChanged(activeRow);
            }
        },

    };

    //binding events
    var bindEvent = {
        containerFocus: function () {
            _$container.on("focusin", action.containerFocus);
        },
        containerClick: function () {
            _$container.on("click", "div", action.containerClick);
        },
        containerTableClick: function () {
            _$container.on("click", "table", action.containerTableClick);
        },
        containerRowClick: function () {
            _$container.on("click", "table tbody tr", action.containerRowClick);
        },
        tableClick: function () {
            _$table.on("click", action.tableClick);
        },
        tableFocus: function () {
            _$table.on("focus", action.tableFocus);
        },
        rowClick: function () {
            _$table.find("tbody tr").on("click", action.rowCLick);
        },
        rowdblClick: function () {
            _$table.find("tbody tr").on("dblclick", action.rowdblClick);
        },
        tableKeyDown: function () {
            _$table.on("keydown", action.keyEventHandler);
        },
        tableScroll: function () {
            _$table.on("scroll", action.tableScroll);
        },
    };
    //unbind events
    var unbindEvent = {
        containerFocus: function () {
            _$container.off("focusin", action.containerFocus);
        },
        containerClick: function () {
            _$container.off("click", "div", action.containerClick);
        },
        containerTableClick: function () {
            _$container.off("click", "table", action.containerTableClick);
        },
        containerRowClick: function () {
            _$container.off("click", "table tbody tr", action.containerRowClick);
        },
        tableClick: function () {
            _$table.off("click", action.tableClick);
        },
        tableFocus: function () {
            _$table.off("focus", action.tableFocus);
        },
        rowClick: function () {
            _$table.find("tbody tr").off("click", action.rowCLick);
        },
        rowdblClick: function () {
            _$table.find("tbody tr").off("dblclick", action.rowdblClick);
        },
        tableKeyDown: function () {
            _$table.off("keydown", action.keyEventHandler);
        },
        tableScroll: function () {
            _$table.off("scroll", action.tableScroll);
        },
    };
    $.fn.extend({
        advancedTable: function (options) {
            var defaults = {
                defaultContainerClass: defaultContainerClass,
                defualtTableClass: defualtTableClass,
            },
                options = $.extend(defaults, options);
            return this.each(function () {
                init(this, options)
            })
        }
    });

})(jQuery);
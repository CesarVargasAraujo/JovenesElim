/*
* Ding.DataTable.js V 1.0
* Abatta Systems 2012
* Author: Diana Mallida
*/

(function ($) {
  $.widget("controls.DingTable", {
    options: {},
    _create: function () {
      var Control = this;

      var $Rows = $('tbody tr', this.element);
      var $RowFilter = $('thead tr.DataFilter', this.element);
      var $RowSortable = $('thead tr.DataSortable ', this.element);
      var $Pages = $('tfoot tr td .Pages', this.element);

      var $OrderBy = $('thead input.OrderBy', this.element);
      var $OrderByDirection = $('thead input.OrderByDirection', this.element);
      var $PagesPerPage = $('thead input.PagesPerPage', this.element);
      var $PaginationPages = $('thead input.PaginationPages', this.element);
      var $CurrentPage = $('thead input.CurrentPage', this.element);
      var $NoRowLimit = $('thead input.NoRowLimit', this.element);

      this.formatPages();

      // FilterOnKeyPress
      $('.TextDataFilter, .IdDataFilter, .DateDataFilter, .DateTimeDataFilter', $RowFilter).each(function (index, td) {
        $('input', $(td)).keydown(function (event) {
          if (event.keyCode == 13)
            Control.changeDataTable();
        });
      });

      $('.ListDataFilter', $RowFilter).each(function (index, td) {
        $('select', $(td)).change(function () {
          Control.changeDataTable();
        });
      });

      // ChangeOrderBy
      $('.Sortable', $RowSortable).each(function (index, td) {
        $(td).click(function () {
          $OrderBy.val($(this).attr('OrderBy'));
          $OrderByDirection.val($(this).attr('OrderByDirection'));
          Control.changeDataTable(1);
        });
      });
    },
    formatPages: function () {
      var $Pages = $('tfoot tr td .Pages', this.element);

      $Pages.css("width", $(".Home:visible", $Pages).width() + $(".Back:visible", $Pages).width() +
			 ($(".Page:visible", $Pages).length * ($(".Page:first", $Pages).width())) +
			$(".Next:visible", $Pages).width() + $(".End:visible", $Pages).width() +
			($("div:visible", $Pages).length * 4) + "px");
    },
    resetFilters: function () {
      var $RowFilter = $('thead tr.DataFilter', this.element);

      // Table 
      $('.TextDataFilter, .IdDataFilter, .DateDataFilter, .DateTimeDataFilter', $RowFilter).each(function (index, td) {
        $('input', $(td)).val("");
      });

      $('.ListDataFilter', $RowFilter).each(function (index, td) {
        $('select', $(td)).val("");
      });

      //Extra
      $('.extraParamsTextDataFilter, .extraParamsIdDataFilter, .extraParamsDateDataFilter, .extraParamsDateTimeDataFilter, .extraParamsListDataFilter').each(function (index, td) {
        $(this).val("");
      });
    },
    getFilters: function () {
      var $RowFilter = $('thead tr.DataFilter', this.element);
      var Params = {
        orderBy: null,
        orderByDirection: null,
        pagesPerPage: null,
        paginationPages: null,
        currentPage: null,
        noRowLimit: 0,
        filters: []
      };

      //Table
      $('.TextDataFilter, .IdDataFilter', $RowFilter).each(function (index, td) {
        var $Input = $('input', $(td));
        Params.filters.push({ key: $Input.attr('id'), value: encodeURIComponent($Input.val()) });
      });

      $('.DateDataFilter', $RowFilter).each(function (index, td) {
        var $Input = $('input', $(td));
        Params.filters.push({ key: $Input.attr('id'), value: encodeURIComponent($Input.val()) });
      });

      $('.DateTimeDataFilter', $RowFilter).each(function (index, td) {
        var $Input = $('input', $(td));
        Params.filters.push({ key: $Input.attr('id'), value: encodeURIComponent($Input.val()) });
      });

      $('.ListDataFilter', $RowFilter).each(function (index, td) {
        var $Select = $('select', $(td));
        Params.filters.push({ key: $Select.attr('id'), value: encodeURIComponent($Select.val()) });
      });

      //Extra
      $('.extraParamsHidenDataFilter, .extraParamsTextDataFilter, .extraParamsIdDataFilter').each(function (index, td) {
        var $Input = $(this);
        Params.filters.push({ key: $Input.attr('id'), value: encodeURIComponent($Input.val()) });
      });

      $('.extraParamsDateDataFilter').each(function (index, td) {
        var $Input = $(this);
        Params.filters.push({ key: $Input.attr('id'), value: encodeURIComponent($Input.val()) });
      });

      $('.extraParamsDateTimeDataFilter').each(function (index, td) {
        var $Input = $(this);
        Params.filters.push({ key: $Input.attr('id'), value: encodeURIComponent($Input.val()) });
      });

      $('.extraParamsListDataFilter').each(function (index, td) {
        var $Select = $(this);
        Params.filters.push({ key: $Select.attr('id'), value: encodeURIComponent($Select.val()) });
      });

      $('.extraParamsCheckDataFilter').each(function (index, td) {
        var $Check = $(this);
        Params.filters.push({ key: $Check.attr('id'), value: encodeURIComponent($Check.is(':checked')) });
      });

      Params.orderBy = encodeURIComponent($('thead input.OrderBy', this.element).val());
      Params.orderByDirection = encodeURIComponent($('thead input.OrderByDirection', this.element).val());
      Params.pagesPerPage = encodeURIComponent($('thead input.PagesPerPage', this.element).val());
      Params.paginationPages = encodeURIComponent($('thead input.PaginationPages', this.element).val());
      Params.currentPage = encodeURIComponent($('thead input.CurrentPage', this.element).val());
      Params.noRowLimit = $('thead input.NoRowLimit', this.element).val() == "True" ? true : false;

      return Params;
    },
    changePage: function (page) {
      this.changeDataTable(page);
    },
    seeAll: function () {
      this.ResetFilters();

      var $NoRowLimit = $('thead input.NoRowLimit', this.element);
      $NoRowLimit.val("True");

      this.changeDataTable();
    },
    changeDataTable: function (page) {
      var params = this.getFilters();
      params.currentPage = page == undefined ? 1 : page;

      this._trigger("getTable", null, { Params: params });
    }
  });
})(jQuery);
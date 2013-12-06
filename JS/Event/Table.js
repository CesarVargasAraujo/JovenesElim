/// <reference path="../_JQuery/JQuery.Vsdoc.js" />

EventRegisterTableLoad = function () {
  $EventRegisterTable.Load();
}

$EventRegisterTable = {
  Html: {
    Table: {
      Title: null,
      Container: null,
      Table:null,
      Cover: null
    },
    Filter: {
    },
    Get: function () {
      var $ERTH = $EventRegisterTable.Html;

      $ERTH.Table.Title = $('#ERTTitle');
      $ERTH.Table.Container = $('#ERTContainer');
      //$ERTH.Table.Table = $('#ERTTable');
      $ERTH.Table.Cover = $('#ERTCover');
    }
  },
  Initialize: function () {
    var $ERT = $EventRegisterTable;

    $ERT.Html.Get();
    $ERT.Events();
    $ERT.Reset();
    $ERT.Search();
  },
  Events: function () {
    var $ERT = $EventRegisterTable;
  },
  Reset: function () {
    var $ERTD = $EventRegisterTable.Html.Filter.Data;
  },
  Load: function () {
    var $ERT = $EventRegisterTable;

    $ERT.Reset();
    $ERT.Search();
  },
  Search: function (Params) {
    var $ERT = $EventRegisterTable;
    var $ERTH = $EventRegisterTable.Html;
    var $ERTD = $EventRegisterTable.Html.Filter.Data;

    $ERTH.Table.Title.addClass('general-table-loading');

    if ($ERTH.Table.Table != null)
      Effect.Cover($ERTH.Table.Cover, $($ERTH.Table.Table[$ERTH.Table.Table.length - 1]), 0, false);

    var JSONParams = JSON.stringify({ TableData: (Params == undefined ? null : Params) });
    WebMethod.Call("Event", "GetTable", JSONParams, function (Table) {
      var $ERT = $EventRegisterTable;
      var $ERTH = $EventRegisterTable.Html;
      var $ERTD = $EventRegisterTable.Html.Filter.Data;

      $ERTH.Table.Table = $(Table.d);
      $ERTH.Table.Container.html($ERTH.Table.Table);

      $ERTH.Table.Cover.hide();
      $ERTH.Table.Title.removeClass('general-table-loading');

      $ERTH.Table.Table.DingTable({}, { getTable: function (event, data) { $EventRegisterTable.Search(data.Params); } });
    });
  }
}

$(document).ready(function () {
	$EventRegisterTable.Initialize();
});
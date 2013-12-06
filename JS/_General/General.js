$(document).ready(function () {
  //AutoNumeric
  $(".money").autoNumeric('init', { mDec: 2, aPad: true, aSign: '$' });
});

/* Url Params */
$.urlParam = function (name) {
  var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(window.location.href);
  return results[1] || 0;
};

$.urlHasParam = function (name) {
  return window.location.href.indexOf(name) != -1;
};

// ToNumber & FormatNumber
String.prototype.ToNumber = function () {
  return Number(this.replace(/(\$|,|%)/g, ''));
};

Number.prototype.FormatNumber = function () {
  var NumberParts = this.toFixed(2).toString().split('.');
  if (Number(NumberParts[1]) == 0)
    return NumberParts[0].replace(/(\d)(?=(\d{3})+(?!\d))/g, "1,");
  else
    return NumberParts[0].replace(/(\d)(?=(\d{3})+(?!\d))/g, "1,") + "." + NumberParts[1];
};

String.prototype.FormatNumber = function () {
  return Number(this).FormatNumber();
};

// FormatCurrency
Number.prototype.FormatCurrency = function () {
  var NumberParts = this.toFixed(2).toString().split('.');
  return "$" + NumberParts[0].replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") + "." + NumberParts[1];
}
String.prototype.FormatCurrency = function () {
  return Number(this).FormatCurrency();
}

// JSONDate
function FormatJSONDate(jsonDate) {
  if (jsonDate == null) return null;

  var newDate = new Date(parseInt(jsonDate.substr(6)));
  var dd = newDate.getDate();
  var mm = newDate.getMonth() + 1; //January is 0! 
  var yyyy = newDate.getFullYear();

  if (dd < 10) { dd = '0' + dd }
  if (mm < 10) { mm = '0' + mm }

  return dd + '/' + mm + '/' + yyyy;
}

// ParameterizedFormat
String.prototype.ParameterizedFormat = function (collection) {
    var Text = this;
    $.each(collection, function (key, value) {
        Text = Text.replace(key, value);
    });

    return Text;
}
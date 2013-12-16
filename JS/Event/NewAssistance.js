/// <reference path="Actions.js" />
var $EventNewAssistance = {
  Html: {
    Messages : null,
    SaveButton: null,
    Get: function () {
      var $ENA = $EventNewAssistance;
      var $ENAH = $ENA.Html;
      var $ENAV = $ENA.Values;

      $ENAH.Messages = $("#NAMessages");
      $ENAH.SaveButton = $("#NASave");

      $ENAV.Name = $("#NAName");
      $ENAV.Surname = $("#NASurname");
      $ENAV.Email = $("#NAEmail");
      $ENAV.Facebook = $("#NAFacebook");
      $ENAV.BirthDay = $("#NABirthday");
      $ENAV.Cooperation = $("#NACooperation");
      $ENAV.Church = $("#NAChurch");
      $ENAV.EventId = $("#NAEventId");
    }
  },
  Values: {
    Name: null,
    Surname: null,
    Email: null,
    Facebook : null,
    BirthDay: null,
    Cooperation: null,
    Church : null,
    EventId: null
  },
  Initialize: function () {
    var $ENA = $EventNewAssistance;
    $ENA.Html.Get();
    $ENA.Events();
  },
  Events: function () {
    var $ENA = $EventNewAssistance;
    var $ENAH = $ENA.Html;
    var $ENAV = $ENA.Values;

    $ENAH.SaveButton.click(function () {
      $EventActions.RegisterYoungAssistance($ENAV.EventId.val(), $ENAV.Church.val(), $ENAV.Name.val(), $ENAV.Surname.val(), $ENAV.Email.val(),
        $ENAV.Facebook.val(), $ENAV.BirthDay.val(), $ENAV.Cooperation.val(), function (data) {
          if (data.Messages.MessageTypes.SuccessMessages == "True") {
            $ENA.Reset();
            EventRegisterTableLoad();
          }
          $ENAH.Messages.html(data.Messages.TranslateText);
          $ENAH.Messages.show();
          $ENAH.SaveButton.StopLoading();
        });
    });
  },
  Reset : function(){
    var $ENA = $EventNewAssistance;
    var $ENAV = $ENA.Values;
    $ENAV.Name.val('');
    $ENAV.Surname.val('');
    $ENAV.Email.val('');
    $ENAV.Facebook.val('');
    $ENAV.BirthDay.val('');
    $ENAV.Church.combobox("autocomplete", "");
  }
}

$(document).ready(function () {
  $EventNewAssistance.Initialize();
});
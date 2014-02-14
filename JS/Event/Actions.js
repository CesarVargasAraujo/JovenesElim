var $EventActions = {
  RegisterYoungAssistance: function (EventId, YoungId, ChurchId, Name, Surnames, Email, Facebook, BirthDay, Cooperation, CallBackFunction) {
    var Params = JSON.stringify({
      EventId: EventId,
      YoungId: YoungId,
      ChurchId: ChurchId,
      Name: Name,
      Surnames: Surnames,
      Email: Email,
      Facebook: Facebook,
      BirthDay: BirthDay,
      Cooperation: Cooperation.ToNumber()
    });
    WebMethod.Call("Event", "RegisterYoungAssistance", Params, function (data) {
      CallBackFunction({ Messages: jQuery.parseJSON(data.d) });
    });
  }
}
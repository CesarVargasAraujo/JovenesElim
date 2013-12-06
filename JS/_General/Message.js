/*
* Abatta.Styles.Core.js V 1.0
* Abatta Systems 2012
* Author: Diana Mallida
*
* Depends:
*/

/* Methods */
//$("#MeesageExaple").AddMessage("Success","Text {Param1} {Param2}",{"{Param1}":"Value1","{Param2}":"Value2"});
//$("#MeesageExaple").AddMessage("Success", "Text");

// type: Information, Success, Warning, Error, Validation
// messageCollection: key, value, translate
$.fn.AddMessageHtml = function (html) {
  Message.AddMessageHtml(this, html);
}

$.fn.AddMessage = function (type, name, messageCollection) {
    Message.AddMessage(this, type, name, messageCollection);
}

$.fn.ClearMessage = function () {
    Message.Clear(this);
}

function AutomaticClose() {
	$(".AutomaticClose").slideUp();
}

/* Class */
var Message = {
  MessageFormat: "<div class='Message {type}'><div class='Close'></div><ul></ul></div>",
  Initialize: function () {
    $("input:text").keypress(function () {
      $(".AutomaticClose").slideUp();
    });

    $("input:password").keypress(function () {
      $(".AutomaticClose").slideUp();
    });

    $("input:checkbox").click(function () {
      $(".AutomaticClose").slideUp();
    });

    $("input:radio").click(function () {
      $(".AutomaticClose").slideUp();
    });

    $(".Message .Close").click(function () {
      $(this).parent('.Message').slideUp();
    });
  },
  AddMessageHtml: function (container, html) {
    $(container).html(html);

    Message.Close(container);
    $(container).show();
  },
  AddMessage: function (container, type, name, messageCollection) {
    if ($("." + type, $(container)).length == 0)
      Message.New(container, type, name, messageCollection);
    else
      Message.Add(container, type, name, messageCollection);

    Message.Close(container);
    $(container).show();
  },
  New: function (container, type, name, messageCollection) {
    $(container).append($(Message.MessageFormat.ParameterizedFormat({ "{type}": type })));

    Message.Add(container, type, name, messageCollection);
  },
  Add: function (container, type, name, messageCollection) {
    var $List = $("ul", $("." + type, $(container)));

    if (messageCollection != undefined)
      $List.append(("<li>{name}</li>".ParameterizedFormat({ "{name}": name })).ParameterizedFormat(messageCollection));
    else
      $List.append("<li>{name}</li>".ParameterizedFormat({ "{name}": name }));
  },
  Close: function (container) {
    $(".Close", $(container)).click(function () {
      $(this).closest('.Message').slideUp();
    });
  },
  Clear: function (container) {
    $('.Message', $(container)).remove();
  }
}

$(document).ready(function () {
    Message.Initialize();
});
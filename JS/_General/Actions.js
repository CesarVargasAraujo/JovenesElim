$.fn.StopLoading = function () {
	Actions.Events.Functional.RemoveEffect($(this), "Loading");
}

$.fn.Loading = function () {
    Actions.Events.Functional.AddEffect($(this), "Loading");
}

var Actions = {
	Html: {
		Inputs: {
			Button: null			
		},
		Get: function () {
			var $A = Actions.Html;
			$A.Inputs.Button = $("input[type=button]");						
		}
	},
	Initialize: function () {
		var $A = Actions;

		$A.Html.Get();
		$A.Events.Graphics();		
	},
	Events: {
		Graphics: function () {
			var $A = Actions;			

			//Buttons
			$A.Html.Inputs.Button.live("click", function () {			  
				if (!$(this).hasClass('NoLoading')) {
					$A.Events.Functional.AddEffect($(this), "Loading");
				}
			});
			
		},
		Functional: {
			AddEffect: function (button, effectClass) {
				var imgSrc = button.outerHeight() > 35 ? "35" : "25";
				var width = button.outerWidth();
				var height = button.outerHeight();
				var top = button.position().top;
				var left = button.position().left;

				if (button.attr("adjustw") != undefined)
					width = parseInt(button.attr("adjustw")) + parseInt(width);
				if (button.attr("adjusth") != undefined)
					height = parseInt(button.attr("adjusth")) + parseInt(height);
				if (button.attr("adjustt") != undefined)
					top = parseInt(button.attr("adjustt")) + parseInt(top);
				if (button.attr("adjustl") != undefined)
					left = parseInt(button.attr("adjustl")) + parseInt(left);

				var $Object = $("<div class='" + effectClass + " " + effectClass + imgSrc + "'></div>")
											.css({ "width": width, "height": height, "top": top, "left": left });
				button.after($Object);
			},
			RemoveEffect: function (button,effectClass) {
				setTimeout(function () {
					var $ParentDiv = button.parent();
					var $EffectDiv = $ParentDiv.find("." + effectClass);
					$EffectDiv.remove();
				}, 200);
			}
		}
	}
}

$(document).ready(function () {
	Actions.Initialize();
});
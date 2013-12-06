var Effect = {
	MoveObject: function (Element, Destiny, AdjustmentTop, AdjustmentLeft, CompleteFunction) {
		var offset = $(Destiny).offset();
		$(Element).animate({
			'top': offset.top + AdjustmentTop,
			'left': offset.left + AdjustmentLeft
		}, {
			duration: 500,
			complete: CompleteFunction
		});
	},
	Cover: function (ElementWhoCover, ElementToCover, AdjustmentHeight, ShowLoader) {
		$(ElementWhoCover).css({
			"width": $(ElementToCover).width(),
			"height": $(ElementToCover).height() + AdjustmentHeight,
			"top": $(ElementToCover).position().top,
			"left": $(ElementToCover).position().left
		});
		$(ElementWhoCover).show();

		if (ShowLoader == true) {
			$("#LoadCover").css({
				"top": $(ElementToCover).position().top + 50,
				"left": $(ElementToCover).position().left + 430
			});
			$("#LoadCover").show();
		}
		$(ElementWhoCover).show();
	}
}
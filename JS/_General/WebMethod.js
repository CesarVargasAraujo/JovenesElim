var WebMethod = {
  GetOptions: function (page, method, data, onSuccess, async) {
		var WebMethodOptions = {
			async: async == undefined ? true : async,
			type: "POST",
			url: Global.Host + "WebMethods/" + page + ".aspx/" + method + "?NoCache=12",
			data: data,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			cache: false,
			success: onSuccess == undefined ? function () { } : onSuccess,
			error: function (xhr, reason, text) {
			  try{} catch (e) { }
			}
		};
		return WebMethodOptions;
	},
	Call: function (page, method, data, onSuccess, async) {
		$.ajax(this.GetOptions(page, method, data, onSuccess, async));
	}
}
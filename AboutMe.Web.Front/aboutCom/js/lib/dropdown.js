(function($) {
	function hideOptions(speed) {
		if (speed.data) {
			speed = speed.data
		}
		if ($(document).data("nowselectoptions")) {
			$($(document).data("nowselectoptions")).slideUp(speed);
			$($(document).data("nowselectoptions")).prev("a").removeClass("tag_select_open");
			$(document).data("nowselectoptions", null);
			$(document).unbind("click", hideOptions);
			$(document).unbind("keyup", hideOptionsOnEscKey);
		}
	}
	function hideOptionsOnEscKey(e) {
		var myEvent = e || window.event;
		var keyCode = myEvent.keyCode;
		if (keyCode == 27) hideOptions(e.data);
	}
	function showOptions(speed) {
		$(document).bind("click", speed, hideOptions);
		$(document).bind("keyup", speed, hideOptionsOnEscKey);
		$($(document).data("nowselectoptions")).slideDown(speed);
		$($(document).data("nowselectoptions")).prev("a").addClass("tag_select_open");
	}

	$.fn.selectCssA = function(_speed) {
		$(this).each(function() {
			var speed = _speed || "fast";
			if ($(this).data("cssobj")) {
				$($(this).data("cssobj")).remove();
			}
			$(this).hide();
			var divselect = $("<a href='javascript:' title='"+$(this).attr('title')+"'></a>").insertAfter(this).addClass("tag_select");
			if ( $(this).attr('id') ){
				divselect.wrap('<div id="sel_'+$(this).attr('id')+'" class="outSelA"></div>');
			}else{
				divselect.wrap('<div class="outSelA"></div>');
			}
			if ( $(this).attr('disabled') == "disabled" ){
				$(this).next(".outSelA").addClass("dised");
				$(this).next(".outSelA").children(".tag_select").css({"color":"#babec3"});
			}
			$(this).data("cssobj", divselect);
			var divoptions = $("<ul></ul>").insertAfter(divselect).addClass("tag_options").hide();

			divselect.click(function(e) {
				if ( !$(this).parent().hasClass("dised") ){
					if ($($(document).data("nowselectoptions")).get(0) != $(this).next("ul").get(0)) {
						hideOptions(speed);
					}
					if (!$(this).next("ul").is(":visible")) {
						e.stopPropagation();
						$(document).data("nowselectoptions", $(this).next("ul"));
						showOptions(speed);
					}
				}
			});
			divselect.hover(function() {
				$(this).addClass("tag_select_hover");
			},
			function() {
				$(this).removeClass("tag_select_hover");
			});

			$(this).change(function() {
				$(this).nextAll("ul").children("li:eq(" + $(this)[0].selectedIndex + ")").addClass("open_selected").siblings().removeClass("open_selected");
				$(this).next("div").children('a').html($(this).children("option:eq(" + $(this)[0].selectedIndex + ")").text());
			});
			$(this).children("option").each(function(i) {
				var lioption = $("<li></li>").html("<a href='javascript:'>"+$(this).text()+"</a>").attr("title", $(this).attr("title")).appendTo(divoptions);
				if ($(this).attr("selected")) {
					lioption.addClass("open_selected");
					divselect.html($(this).text());
				}
				lioption.data("option", this);
				lioption.click(function() {
					lioption.data("option").selected = true;
					$(this).parent().children("li").removeClass("open_selected");
					$(this).addClass("open_selected");
					$(lioption.data("option")).trigger("change", true);
				});
				lioption.hover(function() {
					$(this).addClass("open_hover");
				},
				function() {
					$(this).removeClass("open_hover");
				});

			});

		});

	}
	$.fn.selectCssB = function(_speed) {
		$(this).each(function() {
			var speed = _speed || "fast";
			if ($(this).data("cssobj")) {
				$($(this).data("cssobj")).remove();
			}
			$(this).hide();
			var divselect = $("<a href='javascript:' title='"+$(this).attr('title')+"'></a>").insertAfter(this).addClass("tag_select");
			if ( $(this).attr('id') ){
				divselect.wrap('<div id="sel_'+$(this).attr('id')+'" class="outSelB"></div>');
			}else{
				divselect.wrap('<div class="outSelB"></div>');
			}
			if ( $(this).attr('disabled') == "disabled" ){
				$(this).next(".outSelB").addClass("dised");
				$(this).next(".outSelB").children(".tag_select").css({"color":"#babec3"});
			}
			$(this).data("cssobj", divselect);
			var divoptions = $("<ul></ul>").insertAfter(divselect).addClass("tag_options").hide();

			divselect.click(function(e) {
				if ( !$(this).parent().hasClass("dised") ){
					if ($($(document).data("nowselectoptions")).get(0) != $(this).next("ul").get(0)) {
						hideOptions(speed);
					}
					if (!$(this).next("ul").is(":visible")) {
						e.stopPropagation();
						$(document).data("nowselectoptions", $(this).next("ul"));
						showOptions(speed);
					}
				}
			});
			divselect.hover(function() {
				$(this).addClass("tag_select_hover");
			},
			function() {
				$(this).removeClass("tag_select_hover");
			});

			$(this).change(function() {
				$(this).nextAll("ul").children("li:eq(" + $(this)[0].selectedIndex + ")").addClass("open_selected").siblings().removeClass("open_selected");
				$(this).next("div").children('a').html($(this).children("option:eq(" + $(this)[0].selectedIndex + ")").text());
			});
			$(this).children("option").each(function(i) {
				var lioption = $("<li></li>").html("<a href='javascript:'>"+$(this).text()+"</a>").attr("title", $(this).attr("title")).appendTo(divoptions);
				if ($(this).attr("selected")) {
					lioption.addClass("open_selected");
					divselect.html($(this).text());
				}
				lioption.data("option", this);
				lioption.click(function() {
					lioption.data("option").selected = true;
					$(this).parent().children("li").removeClass("open_selected");
					$(this).addClass("open_selected");
					$(lioption.data("option")).trigger("change", true);
				});
				lioption.hover(function() {
					$(this).addClass("open_hover");
				},
				function() {
					$(this).removeClass("open_hover");
				});

			});

		});

	}
})(jQuery);
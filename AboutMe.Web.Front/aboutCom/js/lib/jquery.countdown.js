/**
 * @name		jQuery Countdown Plugin
 * @author		Martin Angelov
 * @version 	1.0
 * @url			http://tutorialzine.com/2011/12/countdown-jquery/
 * @license		MIT License
 */

(function($){
	
	// Number of seconds in every time division
	var days	= 24*60*60,
		hours	= 60*60,
		minutes	= 60;
	
	// Creating the plugin
	$.fn.countdown = function(prop){
		
		var options = $.extend({
			callback	: function(){},
			starttimestamp	: 0,
			endtimestamp	: 0,
			dayview		: "F"
		},prop);
		
		var left, d, h, m, s, positions;
		var nt = 0;
		
		(function tick(){
			
			var endtime = "";
			var starttime = "";
			var stimes = (options.starttimestamp).split(",");
			var etimes = (options.endtimestamp).split(",");
			endtime = new Date(etimes[0], (etimes[1]-1), etimes[2], etimes[3], etimes[4], etimes[5]);
			starttime = new Date(stimes[0], (stimes[1]-1), stimes[2], stimes[3], stimes[4], stimes[5]);

			// Time left
			left = Math.floor((endtime - (Date.parse(starttime)+(nt*1000))) / 1000);
			
			if(left < 0){
				left = 0;
			}
			
			// Number of days left
			d = Math.floor(left / days);
			left -= d*days;
			
			// Number of hours left
			h = Math.floor(left / hours);
			left -= h*hours;
			
			// Number of minutes left
			m = Math.floor(left / minutes);
			left -= m*minutes;
			
			// Number of seconds left
			s = left;
			
			var message = "";
			var t, t1, t2 = "";
			if ( options.dayview == "T" ){
				message += "<span class='txt_day'><strong><span>" + d + "</span></strong>DAY</span>";
			}
//			t = ""+(h<10  ? h = "0"+h : h)+"";
			t = (h+(d*24));
			t = ""+(t<10  ? t = "0"+t : t)+"";
			t1 = t.charAt(0);
			t2 = t.charAt(1);
			//20140912
			message += "<span class='txt_hour'><strong><span>" + t1 + "</span><span>" + t2 + "</span></strong><span class='tcolon'>:</span></span>";
			//message += "<span class='txt_hour'><strong><span>" + t1 + "</span><span>" + t2 + "</span></strong></span>";
			t = ""+(m<10  ? m = "0"+m : m)+"";
			t1 = t.charAt(0);
			t2 = t.charAt(1);
			//20140912
			message += "<span class='txt_minute'><strong><span>" + t1 + "</span><span>" + t2 + "</span></strong><span class='tcolon'>:</span></span>";
			//message += "<span class='txt_minute'><strong><span>" + t1 + "</span><span>" + t2 + "</span></strong></span>";
			t = ""+(s<10  ? s = "0"+s : s)+"";
			t1 = t.charAt(0);
			t2 = t.charAt(1);
			//20140912
			message += "<span class='txt_second'><strong><span>" + t1 + "</span><span>" + t2 + "</span></strong></span>";
			//message += "<span class='txt_second'><strong><span>" + t1 + "</span><span>" + t2 + "</span></strong></span>";

			// Calling an optional user supplied callback
			options.callback(message);
			
			// Scheduling another call of this function in 1s
			nt++;
			setTimeout(tick, 1000);
		})();
		
		return this;
	};
})(jQuery);
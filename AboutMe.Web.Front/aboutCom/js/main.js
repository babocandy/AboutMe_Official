$(function(){
	if( $("*").is(".txtdotdot") ){
		$(".txtdotdot").dotdotdot();
	}
	if( $("*").is(".main") ){
		console.log("dd");
		$("body").css({"min-width":"1320px"});
	}
	/*main 슬라이드*/
	var mainbnrIdx =0;
	var mainbnrLength = $(".mainvisual_banner .slider li").length;
	$(".mainvisual_banner .slider li:eq(0)").css({"display":"block"});
	
	$(".mainvisual_banner .slider li").each(function(){
		$(".mainvisual_banner .nav").append("<li><a href='#'><img src='/aboutCom/images/common/nav_ban.png' /></a></li> ");
	});
	$(".mainvisual_banner .nav li").eq(0).addClass("cnt");
	
	if(mainbnrLength==1){
		$(".navwrap").css({"display":"none"});
		$(".navwrap .nav").css({"display":"none"});
	}else{
		$(".navwrap .nav").css({"display":"block"});
		function mainBannerSlidestart(){
			mainBannerSlideTime = setInterval(function(){
					mainbnrIdx++;
					if (mainbnrIdx==mainbnrLength){
						mainbnrIdx=0;
					}
					$(".mainvisual_banner .nav li:eq("+mainbnrIdx+") a").click();
				},5000);
		}
		function mainBannerSlideclearIntvals(){
			mainBannerSlideTime != undefined && clearInterval(mainBannerSlideTime);
		}
		$(".mainvisual_banner .btn_playstop .play").click(function(e){
			e.preventDefault();
			mainBannerSlideclearIntvals();
			$(this).css({"display":"none"});
			$(".mainvisual_banner .btn_playstop .stop").css({"display":"block"});
		});
		$(".mainvisual_banner .btn_playstop .stop").click(function(e){
			e.preventDefault();
			mainBannerSlidestart();
			$(this).css({"display":"none"});
			$(".mainvisual_banner .btn_playstop .play").css({"display":"block"});
		});
		var mainBannerSlideTime;
		 mainBannerSlidestart();
		$(".mainvisual_banner .nav li a").on("click", function(e){
			mainBannerSlideclearIntvals();
			e.preventDefault();
			mainbnrIdx = $(this).parent().index();
			$(".mainvisual_banner .slider li").fadeOut(500);
			$(".mainvisual_banner .slider li:eq("+mainbnrIdx+")").fadeIn(500);
			$(".mainvisual_banner .nav li").removeClass("cnt");
			$(".mainvisual_banner .nav li:eq("+mainbnrIdx+")").addClass("cnt");
			mainBannerSlidestart();
		});
	}
	

		
		


	/*타임세일*/
	$(".timesale .item").click(function(e){
		e.preventDefault();
		$(".lytimesale").show();
	});
	$(".laytime_close").click(function(e){
		e.preventDefault();
		$(".lytimesale").hide();
	});
	$(".beauty").click(function(e){
		e.preventDefault();
		$(".lybeauty").show();
	});

	/*영상 나오기*/
	var mainMvUrl =$(".mvarea .movie iframe").attr("src");
	var mainMv ='<iframe width="810" height="410" src="'+mainMvUrl+'" frameborder="0" allowfullscreen></iframe>';
	$(".mvarea .movie").html("");
	$(".btn_mv").click(function(e){
		e.preventDefault();
		$(".mvarea .movie").html(mainMv);
		$(".mv_area .lymv").show();
		$(".mv_area .lymv").animate({"width":"1080px", "height":"540px"});
		$(".btn_mv").hide();
		$(".txtdotdot").dotdotdot();
	});
	$(".mv_area .lymv .movie_close").click(function(e){
		e.preventDefault();
		$(".btn_mv").show();
		$(".mv_area .lymv").animate({"width":"0", "height":"0"},function(){
			$(".mv_area .lymv").hide();
		});
		$(".mvarea .movie").html("");
	});


	/*시스타기획배너*/
	$(".m_banner .list1").show();
	/*main 슬라이드*/
	var m_bnrIdx =0;
	var m_bnrLength = $(".m_banner_list li").length;
	
	$(".m_banner_thumb li").eq(0).addClass("cnt");
	
	$(".m_banner_thumb li a").on("click", function(e){
		e.preventDefault();
		m_bnrIdx = $(this).parent().index();
		$(".m_banner_list li").fadeOut(500);
		$(".m_banner_list li:eq("+m_bnrIdx+")").fadeIn(500);
		$(".m_banner_thumb li").removeClass("cnt");
		$(".m_banner_thumb li:eq("+m_bnrIdx+")").addClass("cnt");
	});
	//자동슬라이드
	/*var m_bnrSlideTime = setInterval(function(){
		m_bnrIdx++;
		if (m_bnrIdx==m_bnrLength){
			m_bnrIdx=0;
		}
		$(".m_banner_thumb li:eq("+m_bnrIdx+") a").click();
	},5000);*/
	

});
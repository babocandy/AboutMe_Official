$(function(){
	/*main 슬라이드*/
	var mainbnrIdx =0;
	var mainbnrLength = $(".mainvisual_banner .slider li").length;
	$(".mainvisual_banner .slider li:eq(0)").css({"display":"block"});
	
	$(".mainvisual_banner .slider li").each(function(){
		$(".mainvisual_banner .nav").append("<li><a href='#'><img src='/aboutCom/images/common/nav_ban.png' /></a></li> ");
	});
	$(".mainvisual_banner .nav li").eq(0).addClass("cnt");
		
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
	function mainBannerSlidestart(){
		mainBannerSlideTime = setInterval(function(){
				mainbnrIdx++;
				if (mainbnrIdx==mainbnrLength){
					mainbnrIdx=0;
				}
				$(".mainvisual_banner .nav li:eq("+mainbnrIdx+") a").click();
			},10000);
	}
	function mainBannerSlideclearIntvals(){
		mainBannerSlideTime != undefined && clearInterval(mainBannerSlideTime);
	}


	/*타임세일*/
	$(".timesale").click(function(e){
		e.preventDefault();
		$(".lytimesale").show();
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
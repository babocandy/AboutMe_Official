$(function(){

	//main 슬라이드
	$(".mainbnr .slider li").each(function(){
		$(".mainbnr .nav ul").append("<li><a href='#'></a></li> ");
	});
	$(".mainbnr .nav li").eq(0).addClass("cnt");
	var mbnr_navW = $(".mainbnr .nav").width();
	$(".mainbnr .nav").css({"margin-left":-(mbnr_navW/2)});
	
	var mainbnrWidth = $(window).width();
	function mainbnrResizing(){
		 $(".mainbnr").css({"width":mainbnrWidth});
		 $(".mainbnr .slider li").css({"width":mainbnrWidth})
	}mainbnrResizing();
	$(window).resize(function(){
		mainbnrWidth = $(window).width();
		mainbnrResizing();
	});

	var mainbnrIdx= 0;
	var mainbnrWidth = $(".mainbnr .slider li").width(); //li의 넓이
	var mainbnrLength = $(".mainbnr .slider li").length; //li의 갯수
	$(".mainbnr .nav li a").on("click", function(e){
		e.preventDefault();
		mainbnrIdx = $(this).parent().index();
		$(".mainbnr .slider").animate({"margin-left": -(mainbnrWidth*mainbnrIdx)},300,"easeInExpo");
		$(".mainbnr .nav li").removeClass("cnt");
		$(".mainbnr .nav li:eq("+mainbnrIdx+")").addClass("cnt");
		mainbnrSlideTimeclearIntvals();
	});
	function mainbnr_left(){
		mainbnrIdx--;
		if (mainbnrIdx==-1){
			mainbnrIdx=(mainbnrLength-1);
			$(".mainbnr .slider li:eq("+(mainbnrLength-1)+")").clone().insertBefore($(".mainbnr .slider li:eq(0)"));
			$(".mainbnr .slider").css({"margin-left":-mainbnrWidth});
			$(".mainbnr .slider").animate({"margin-left":0},300,"easeInExpo", function(){
				$(".mainbnr .slider").css({"margin-left":-(mainbnrWidth*(mainbnrIdx))});
				$(".mainbnr .slider li:eq(0)").remove();
			});
		}
		else{
			$(".mainbnr .slider").animate({"margin-left": -(mainbnrWidth*mainbnrIdx)},300,"easeInExpo");
		}
		$(".mainbnr .nav li:eq("+mainbnrIdx+") a").click();
		mainbnrSlideTimeclearIntvals();
	}
	function mainbnr_right(){
		mainbnrIdx++;
		if (mainbnrIdx==mainbnrLength){
			mainbnrIdx=0;
			$(".mainbnr .slider li:eq(0)").clone().insertAfter($(".mainbnr .slider li:eq("+(mainbnrLength-1)+")")); //eq는 0부터 시작. length는 1부터 시작해서 5가 나오기때문에 -1을 줘야 4번째 eq나옴
			$(".mainbnr .slider").animate({"margin-left": +(-(mainbnrWidth*mainbnrLength)) },300,"easeInExpo", function(){
				$(".mainbnr .slider").css({"margin-left":0});
				$(".mainbnr .slider li:eq("+(mainbnrLength)+")").remove();
			});
		}else{
			$(".mainbnr .slidern").animate({"margin-left": -(mainbnrWidth*mainbnrIdx)},300,"easeInExpo");
		}
		$(".mainbnr .nav li:eq("+mainbnrIdx+") a").click();
		mainbnrSlideTimeclearIntvals();
	}
	mainBnrSlidestart();
	//손꾸락 //swipeleft, swiperight, swipeup, swipedown
	$(".mainbnr .slider").on('swipeleft', function(e) {  
		mainbnr_right();
	}).on('swiperight', function(e) {
		mainbnr_left();
	}).on('movestart', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	}).on('move', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	});
	//메인자동슬라이드
	function mainBnrSlidestart(){
		mainbnrSlideTime = setInterval(function(){
			//mainbnr_right();
		},3000);
	}
	function mainbnrSlideTimeclearIntvals(){
		mainbnrSlideTime != undefined && clearInterval(mainbnrSlideTime);
	}

	/* eventpro_bnr */
	function eventpro_bnrResizing(){
		 $(".eventpro_bnr").css({"width":(mainbnrWidth-30)})
		 $(".eventpro_bnr .slider li").css({"width":(mainbnrWidth-50)/2})
	}eventpro_bnrResizing();
	
	var eventpro_bnrIdx =1;
	var eventpro_bnrIdx2 =1;
	var eventpro_bnrIdx3 =1;
	var eventpro_bnrWidth = (mainbnrWidth-30)/2
	var eventpro_bnrLength = $(".mainevent .eventpro_bnr:eq(0) .slider li").length;
	var eventpro_bnrLength2 = $(".skintype_recom .eventpro_bnr .slider li").length;
	var eventpro_bnrLength3 = $(".mainevent .eventpro_bnr:eq(1) .slider li").length; //alert(eventpro_bnrLength3);
	var eventpro_show= 2;
	$(".mainevent .eventpro_bnr:eq(0) .btnarea .prev").click(function(e){
		e.preventDefault();
		if(eventpro_bnrIdx <=1){
			return;
		}
		eventpro_bnrIdx-- ;
		$(".mainevent .eventpro_bnr:eq(0) .slider").animate({"margin-left":-((eventpro_bnrIdx-1)*eventpro_bnrWidth)}, 300, "easeInExpo");
	});
	$(".mainevent .eventpro_bnr:eq(0) .btnarea .next").click(function(e){
		e.preventDefault();
		if(eventpro_bnrIdx+eventpro_show-1==eventpro_bnrLength){
			return;
		}
		eventpro_bnrIdx++;
		$(".mainevent .eventpro_bnr:eq(0) .slider").animate({"margin-left":-((eventpro_bnrIdx-1)*eventpro_bnrWidth)}, 300, "easeInExpo");
	});
	$(window).resize(function(){
		mainbnrWidth = $(window).width();
		eventpro_bnrIdx =1;
		eventpro_bnrWidth = (mainbnrWidth-30)/2;
		eventpro_bnrResizing();
	});
	//손꾸락
	$(".mainevent .eventpro_bnr:eq(0) .slider").on('swipeleft', function(e) {  
		$(".mainevent .eventpro_bnr:eq(0) .btnarea .next").click();
	}).on('swiperight', function(e) {
		$(".mainevent .eventpro_bnr:eq(0) .btnarea .prev").click();
	}).on('movestart', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	}).on('move', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	});

	//슬라이드 하나 더 생길때
	$(".mainevent .eventpro_bnr:eq(1) .btnarea .prev").click(function(e){
		e.preventDefault();
		if(eventpro_bnrIdx3 <=1){
			return;
		}
		eventpro_bnrIdx3-- ;
		$(".mainevent .eventpro_bnr:eq(1) .slider").animate({"margin-left":-((eventpro_bnrIdx3-1)*eventpro_bnrWidth)}, 300, "easeInExpo");
	});
	$(".mainevent .eventpro_bnr:eq(1) .btnarea .next").click(function(e){
		e.preventDefault();
		if(eventpro_bnrIdx3+eventpro_show-1==eventpro_bnrLength3){
			return;
		}
		eventpro_bnrIdx3++;
		$(".mainevent .eventpro_bnr:eq(1) .slider").animate({"margin-left":-((eventpro_bnrIdx3-1)*eventpro_bnrWidth)}, 300, "easeInExpo");
	});
	$(window).resize(function(){
		mainbnrWidth = $(window).width();
		eventpro_bnrIdx3 =1;
		eventpro_bnrWidth = (mainbnrWidth-30)/2;
		eventpro_bnrResizing();
	});
	//손꾸락
	$(".mainevent .eventpro_bnr:eq(1) .slider").on('swipeleft', function(e) {  
		$(".mainevent .eventpro_bnr:eq(1) .btnarea .next").click();
	}).on('swiperight', function(e) {
		$(".mainevent .eventpro_bnr:eq(1) .btnarea .prev").click();
	}).on('movestart', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	}).on('move', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	});
	
	//피부타입
	$(".skintype_recom .eventpro_bnr .btnarea .prev").click(function(e){
		e.preventDefault();
		if(eventpro_bnrIdx2 <=1){
			return;
		}
		eventpro_bnrIdx2-- ;
		$(".skintype_recom .eventpro_bnr .slider").animate({"margin-left":-((eventpro_bnrIdx2-1)*eventpro_bnrWidth)}, 300, "easeInExpo");
	});
	$(".skintype_recom .eventpro_bnr .btnarea .next").click(function(e){
		e.preventDefault();
		if(eventpro_bnrIdx2+eventpro_show-1==eventpro_bnrLength2){
			return;
		}
		eventpro_bnrIdx2++;
		$(".skintype_recom .eventpro_bnr .slider").animate({"margin-left":-((eventpro_bnrIdx2-1)*eventpro_bnrWidth)}, 300, "easeInExpo");
	});
	$(window).resize(function(){
		mainbnrWidth = $(window).width();
		eventpro_bnrIdx2 =1;
		eventpro_bnrWidth = (mainbnrWidth-30)/2;
		eventpro_bnrResizing();
	});
	//손꾸락
	$(".skintype_recom .eventpro_bnr .slider").on('swipeleft', function(e) {  
		$(".skintype_recom .eventpro_bnr .btnarea .next").click();
	}).on('swiperight', function(e) {
		$(".skintype_recom .eventpro_bnr .btnarea .prev").click();
	}).on('movestart', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	}).on('move', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	});


	
	/*리뷰*/
	$(".review_wrap .top").toggle(function(){
		$(this).parent().find(".review").slideDown(300);
	}, function(){
		$(this).parent().find(".review").slideUp(100);
	});

	/*hotnow*/
	$(".hotnow .lagre_img li:eq(0)").show();
	var m_bnrIdx =0;
	var m_bnrLength = $(".hotnow .lagre_img li").length;
	$(".hotnow .thum_img li").eq(0).addClass("cnt");
	$(".hotnow .thum_img li a").on("click", function(e){
		e.preventDefault();
		m_bnrIdx = $(this).parent().index();
		if(m_bnrIdx<=0){
			$(".hotnow .btn_area a.prev").addClass("off");
		}else if(m_bnrIdx==m_bnrLength-1){
			$(".hotnow .btn_area a.next").addClass("off");
		}else{
			$(".hotnow .btn_area a.prev").removeClass("off");
			$(".hotnow .btn_area a.next").removeClass("off");
		}

		$(".hotnow .lagre_img li").fadeOut(500);
		$(".hotnow .lagre_img li:eq("+m_bnrIdx+")").fadeIn(500);
		$(".hotnow .thum_img li").removeClass("cnt");
		$(".hotnow .thum_img li:eq("+m_bnrIdx+")").addClass("cnt");
	});
	$(".hotnow .thum_img li a:eq(0)").on("click", function(e){
		e.preventDefault();
		$(".hotnow .btn_area a.prev").addClass("off");
		$(".hotnow .btn_area a.next").removeClass("off");
	});
	$(".hotnow .thum_img li a:eq(3)").on("click", function(e){
		e.preventDefault();
		$(".hotnow .btn_area a.prev").removeClass("off");
		$(".hotnow .btn_area a.next").addClass("off");
	});
	$(".hotnow .btn_area a.prev").click(function(){
		if (m_bnrIdx<=0){
			return;
		}
		m_bnrIdx--;
		$(".hotnow .thum_img li:eq("+m_bnrIdx+") a").click();
		if (m_bnrIdx<=0){
			$(".hotnow .btn_area a.prev").addClass("off");
		}
		$(".hotnow .btn_area a.next").removeClass("off");
	});
	$(".hotnow .btn_area a.next").click(function(){
		if (m_bnrIdx==m_bnrLength){
			return;
		}
		m_bnrIdx++;
		$(".hotnow .thum_img li:eq("+m_bnrIdx+") a").click();
		if (m_bnrIdx==m_bnrLength-1){
			$(".hotnow .btn_area a.next").addClass("off");
		}
		$(".hotnow .btn_area a.prev").removeClass("off");
	});
	//손꾸락
	$(".hotnow .lagre_img").on('swipeleft', function(e) {  
		$(".hotnow .btn_area a.next").click();
	}).on('swiperight', function(e) {
		$(".hotnow .btn_area a.prev").click();
	}).on('movestart', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	}).on('move', function(e) {
		if ( (e.distX > e.distY && e.distX < -e.distY) || (e.distX < e.distY && e.distX > -e.distY) ){
			e.preventDefault();
			return;
		}
	});

});
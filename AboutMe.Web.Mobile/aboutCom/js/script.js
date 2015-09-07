function link(url){
	location.href = url;
}
function historyback(){
	history.back();
}
/*새창팝업*/
function PopupCenter(popNM, parms, width, height){
	var sw  = screen.availWidth ;
	var sh  = screen.availHeight ;
	px=(sw - width)/2 ;
	py=(sh - height)/2 ;
	var set  = 'top=' + py + ',left=' + px ;
	set += ',width=' + width + ',height=' + height + ',menubar = no, toolbar = no, location = no, status = no, scrollbars = yes, resizable = yes' ;
	window.name="pop";
	window.open (popNM + parms , '' , set) ;
}

//Layer lyPopupShow
function lyPopupShow(obj){
	$(obj).fadeIn(400);
}
//Layer lyPopupHide
function lyPopupHide(obj){
	$(obj).fadeOut(400);
}

//쿠키 설정
function setCookiePopup(name, value, expiredays){
	var todayDate = new Date(); 
	todayDate.setDate( todayDate.getDate() + expiredays ); 
	document.cookie = name + "=" + escape( value ) + ";path=/;"
}
//쿠키 체크
function getCookiePopup(name){
	var Found = false 
	var start, end 
	var i = 0 
	while(i <= document.cookie.length) { 
		start = i 
		end = start + name.length 
		if(document.cookie.substring(start, end) == name) { 
			Found = true 
			break 
		}
		i++
	} 
	if(Found == true) { 
		start = end + 1 
		end = document.cookie.indexOf(";", start) 
		if(end < start)
			end = document.cookie.length 
		return document.cookie.substring(start, end) 
	}
	return "" 
}

$(function(){
	//layer 팝업
	if ( $("*").is("#ly_wrap") ){
		//이번만 닫기
		$("#ly_wrap .ly_button .btn_once").click(function(){
			var cookieid = $(this).parent().parent().get(0).id;
			lyPopupHide("#"+cookieid);
		});
		//24시간 닫기
		$("#ly_wrap .ly_button .btn_oneday").click(function(){
			var cookieid = $(this).parent().parent().get(0).id;
			setCookiePopup(cookieid, "no", 1);
			lyPopupHide("#"+cookieid);
		});
	}

	/*뒤로가기*/
	$(".historyback").click(function(){
		historyback();
	});

	/*nav*/
	var winWidth = $(window).width();
	$(".lnb_area").css({"left":-winWidth});
	$(".openlnb").click(function(e){
		e.preventDefault();
		$(".lnb_area").stop().animate({"left":"0"});
		$(".lnb").stop().animate({"left":"0"});
		$(".lnb_area .dim").fadeIn(300);
		$(".f_bottom ").hide();
		//$(".container").bind('touchmove', function(e){e.preventDefault()});
		$(".container").css({"position":"fixed"});
		$(".footer").hide();
		$(".header .lnb_area .lnbclose").show();
	});
	$(".lnbclose, .lnb_area .dim").click(function(e){
		e.preventDefault();
		$(".lnb_area").css({"left":-winWidth});
		$(".lnb").stop().animate({"left":"-280px"});
		$(".lnb_area .dim").hide(0);
		$(".f_bottom ").show();
		//$(".container").unbind('touchmove');
		$(".container").css({"position":"relative"});
		$(".footer").show();
		$(".header .lnb_area .lnbclose").hide();
	});

	$(".nav li a").on("click", function(){
		if($(this).parent(".slidetype").hasClass("v2")){
			return false;
		}
		if($(this).parent(".slidetype").hasClass("on")){
			$(this).parent().find(".depth2").slideUp(300, function(){
				$(this).parent(".slidetype").removeClass("on");
			});
		}else{
			$(this).parent().find(".depth2").slideDown(300, function(){
				$(this).parent(".slidetype").addClass("on");
			});
		}
		
	});
	/*상단검색
	$(".h_top .btn_search").toggle(function(){
		$(".h_search_wrap").slideDown(300);
		$("body").bind('touchmove', function(e){e.preventDefault()});
	}, function(){
		$(".h_search_wrap").slideUp(300);
		$("body").unbind('touchmove');
	});*/
	var searchVal = false;
	$(".h_top .btn_search").click(function(){
		if(!searchVal){
			$(".h_search_wrap").slideDown(300);
			$("body").bind('touchmove', function(e){e.preventDefault()});
			searchVal = true;
		}else{
			$(".h_search_wrap").slideUp(300);
			$("body").unbind('touchmove');
			searchVal = false
		}
	});
	$(".h_search_wrap .dimd").click(function(){
		$(".h_search_wrap").slideUp(300);
		$("body").unbind('touchmove');
		searchVal = false
	});

	/*footer*/
	$(".footer .btn_term").toggle(function(e){
		e.preventDefault();
		$(".footer .term_area").css({"display":"block"});
		$(".footer .btn_term a").addClass("on");
	}, function(e){
		e.preventDefault();
		$(".footer .term_area").css({"display":"none"});
		$(".footer .btn_term a").removeClass("on");
	});

	/*스크롤시 나오는 하단버튼
	var scrollTop;
	var scrollBottom;
	$(window).scroll(function(){
		scrollTop=$(window).scrollTop();
		scrollBottom = $(document).height()-$(window).height()-$(window).scrollTop();
		if (scrollTop >=44 && scrollBottom > 244){
			$(".f_bottom .historyback, .f_bottom .documtop").css({"position":"fixed"});
		}else if(scrollTop <10 ){
			$(".f_bottom .historyback, .f_bottom .documtop").css({"position":"absolute"});
		}else if(scrollBottom < 244){
			$(".f_bottom .historyback, .f_bottom .documtop").css({"position":"absolute"});
		}
	});*/
	$(window).scroll(function(){
		scrollTop=$(window).scrollTop();
		if(scrollTop > 10){
			$(".f_bottom").fadeIn()
		}else {
			$(".f_bottom").fadeOut()
		}
	});
	$(".documtop").on("click", function(){
		$('html, body').stop().animate({ scrollTop : 0 },300 ,"easeInExpo");
	});

	/*전시*/

	//$('input, textarea').placeholder({customClass:'my-placeholder'});

	/*숫자up&down*/
	$(".counttypea").on("click",".btn_updown .up",function(e){
		e.preventDefault();
		var objCnt = $(this).parents(".counttypea").find("input[name='count']");
		var cntNum = Number(objCnt.val());
		objCnt.val(cntNum+1);
	});
	$(".counttypea").on("click",".btn_updown .down",function(e){
		e.preventDefault();
		var objCnt = $(this).parents(".counttypea").find("input[name='count']");
		var cntNum = Number(objCnt.val());
		if(cntNum === 1) return;
		objCnt.val(cntNum-1);
	});
	$(".counttypeb").on("click",".btn_updown .up",function(e){
		e.preventDefault();
		var objCnt = $(this).parents(".counttypeb").find("input[name='count']");
		var cntNum = Number(objCnt.val());
		objCnt.val(cntNum+1);
	});
	$(".counttypeb").on("click",".btn_updown .down",function(e){
		e.preventDefault();
		var objCnt = $(this).parents(".counttypeb").find("input[name='count']");
		var cntNum = Number(objCnt.val());
		if(cntNum === 1) return;
		objCnt.val(cntNum-1);
	});

	/*닫기*/
	$("#btnClose").live("click", function(e){
		e.preventDefault();
		$(this).parent().hide();
	});

	/*tabs*/
	$('ul.tabs li').click(function() {
		if (!$('ul.tabs').length) { return; }
		tabIdx = $(this).index();
		$(this).parent().find("li").removeClass('cnt');
		$(this).addClass('cnt');
		$($(this).children().attr('href')).show().siblings('div.tab_content').hide();
		return false;
	});

	/*img_over*/
	function getOverImgURL(arg) {
		var imgPath = getOutImgURL(arg);
		imgPath = imgPath.replace(".gif", "_on.gif");
		imgPath = imgPath.replace(".jpg", "_on.jpg");
		imgPath = imgPath.replace(".png", "_on.png");
		return imgPath;
	}
	function getOutImgURL(arg) {
		var imgPath = arg;
		imgPath = imgPath.replace("_on.gif", ".gif");
		imgPath = imgPath.replace("_on.jpg", ".jpg");
		imgPath = imgPath.replace("_on.png", ".png");
		return imgPath;
	}
	$(".imgInteractive").mouseenter(function(){
		$(this).attr("src", getOverImgURL($(this).attr("src")));
	}).mouseleave(function(){
		$(this).attr("src", getOutImgURL($(this).attr("src")));
	});

	/*센터정렬layerpop*/
	var layerpopWidth = $(".layerwrap .layerpop").width();
	var layerpopheight = $(".layerwrap .layerpop").height();
	$(".layerwrap").hide();
	$(".layerpopshow").click(function(){
		$("html").css({"overflow-y":"hidden"});
		$(".layerwrap").show();
		$(".layerwrap .layerpop").css({"display":"block", "margin-left":-(layerpopWidth/2), "margin-top":-(layerpopheight/2)});
	});
	$(".layerpophide").click(function(){
		$(".layerwrap").hide();
		$("html").css({"overflow-y":"auto"});
	});

	/* radio checkbox */
	$(".chkcon").each(function(){
		$(this).append("<span class='bl'></span>");
		if($(this).children(":checked").length != 0){
			$(this).addClass("chkcon_on");
		}
	});
	$(".chkcon").click(function(){
		if($(this).children(":checked").length == 0){
			$(this).removeClass("chkcon_on");
		}else{
			$(this).addClass("chkcon_on");
		}
	});
	/* form - radio */
	$(".racon").each(function(){
		$(this).append("<span class='bl'></span>");
		if($(this).children(":checked").length != 0){
			$(this).addClass("racon_on");
		}
	});
	$(".racon").click(function(){
		$(this).parent().find(".racon").each(function(){
			if($(this).children(":checked").length == 0){
				$(this).removeClass("racon_on");
			}else{
				$(this).addClass("racon_on");
			}
		});
	});

	//select : init
	$(".selbox").each(function(){
		var txt = $(this).children("select").children("option").eq($(this).children("select")[0].selectedIndex).text();
		if ( $(this).hasClass("cnt") ){
			$(this).append("<span class='txt cnt'>"+txt+"</span>");
			$(this).append("<span class='bl cnt'></span>");
		}else{
			$(this).append("<span class='txt'>"+txt+"</span>");
			$(this).append("<span class='bl'></span>");
		}
	});
	//select : change event
	$(".selbox select").change(function(){
		var txt = $(this).children("option").eq($(this)[0].selectedIndex).text();
		$(this).siblings(".txt").html(txt);
		$(this).parent().removeClass("disable");
	});

	/*아코디언타입*/
	$(".accordion .accordcont .accordtxt").hide();
	$(".accordion .accordcont:eq(0) .accordtxt").show();
	$(".accordion .accordcont .accordsubject").click(function(){
		if ( 'none' == $(this).parent().find(".accordtxt").css('display') ) {
			$(".accordion .accordcont .accordtxt").slideUp();
			$(this).parent().find(".accordtxt").slideDown();
			$(".accordcont").removeClass("cnt");
			$(this).parent().addClass("cnt");
		}
	});

	/*쇼핑백*/
	$(".btn_change_count").click(function(){
		$(this).parent().find(".change_count").slideDown(300);
	});
	$(".close_count").click(function(){
		$(this).parent(".change_count").slideUp(100);
	});

	/*같이구매하면 좋은 상품 슬라이드*/
	var relativesliderIdx = 1;
	var showIdx = 2;
	var relativesliderLength= $(".relative_product .list li").length;
	var relativesliderWidth = $(".relative_product").width();
	$(".relative_product .list li").css({"width":relativesliderWidth/2});
	$(window).resize(function(){
		relativesliderWidth = $(".relative_product").width();
		$(".relative_product .list li").css({"width":relativesliderWidth/2});
	});
	$(".relative_product .btnarea .prev").click(function(e){
		e.preventDefault();
		console.log(relativesliderIdx)
		if(relativesliderIdx <=1){
			$(".relative_product .btnarea .prev").addClass("off");
			return;
		}
		relativesliderIdx-- ;
		if(relativesliderIdx ==1){
			$(".relative_product .btnarea .prev").addClass("off");
		}
		$(".relative_product .list").animate({"margin-left":-((relativesliderIdx-1)*(relativesliderWidth/2))}, 300, "easeInExpo");
		$(".relative_product .btnarea .next").removeClass("off");
	});
	$(".relative_product .btnarea .next").click(function(e){
		e.preventDefault();
		if(relativesliderIdx+showIdx-1==relativesliderLength){
			return;
		}
		relativesliderIdx ++;
		$(".relative_product .list").animate({"margin-left":-((relativesliderIdx-1)*(relativesliderWidth/2))}, 300, "easeInExpo");
		if(relativesliderIdx+showIdx-1==relativesliderLength){
			$(".relative_product .btnarea .next").addClass("off");
		}
		$(".relative_product .btnarea .prev").removeClass("off");
	});

	//손꾸락 //swipeleft, swiperight, swipeup, swipedown
	$(".relative_list_wrap").on('swipeleft', function(e) {
		$(this).parent().find(".next").click();
	}).on('swiperight', function(e) {
		$(this).parent().find(".prev").click();
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

	//쇼핑백 textarea 글자수제한
	$("#edu_thesis").on("keyup", function() {
		// 변수초기화
		var ls_str = $(this).val(); // 이벤트가 일어난 컨트롤의 value 값
		var li_max = 100; // 제한할 글자수 크기
		var li_byte = 0;  // 한글일경우는 2 그밗에는 1을 더함
		var li_len = 0;  // substring하기 위해서 사용
		var ls_one_char = ""; // 한글자씩 검사한다

		for(i=0; i< $(this).val().length; i++) {
			// 한글자추출
				ls_one_char = ls_str.charAt(i);
			// 한글이면 2를 더한다.
			if (escape(ls_one_char).length > 4) {
				li_byte += 2;
			}else{   // 그밗의 경우는 1을 더한다.
				li_byte++;
			}
			// 전체 크기가 li_max를 넘지않으면
			if(li_byte <= li_max){
				li_len = i + 1;
			}
		}

		// 전체길이를 초과하면
		if(li_byte > li_max){
			alert(li_max+"byte를 초과 입력할수 없습니다. \n 초과된 내용은 자동으로 삭제 됩니다. "+li_len);
			li_byte = 0
			$(this).val(ls_str.substr(0, li_len));

			for(i=0; i<  $(this).val().length; i++) {
				ls_one_char = ls_str.charAt(i);
				if (escape(ls_one_char).length > 4) {
					li_byte += 2;
				}else{
					li_byte++;
				}
			}
		}
		$("#areatxt").html((li_max-li_byte)+"")
		$(this).focus();
	});
	$("#edu_thesis").on("keypress", function() {
		if(event.keyCode == 13)
		event.returnValue=false;
	});

	// toggle
	if ( $("*").is(".toggle_box") ){
		$(".toggle_box .toggle_ti").click(function(){
			if ( $(this).hasClass("cnt") ){
				$(this).parent().find(".toggle_con").slideUp();
				$(this).removeClass("cnt");
			}else{
				$(this).parent().find(".toggle_con").slideDown();
				$(this).addClass("cnt");
			}
		});
	}
	if ( $("*").is(".review_info .btn_more") ){
		$(".review_info .btn_more").click(function(){
			$(this).parents(".review_info").toggleClass("on");
		});
	}

	/*상세*/
	$(".detail .produt_photo .slider li").each(function(){
		$(".detail .produt_photo .nav ul").append("<li><a href='#'></a></li> ");
	});
	$(".detail .produt_photo .nav li").eq(0).addClass("cnt");
	var detailPhoto_navW = $(".detail .produt_photo .nav").width();
	var detailPhoto_itemW=$(".produt_photo .slider ").width();
	var detail_length = $(".detail .produt_photo .nav li").length;
	$(".produt_photo .slider li").css({"width":detailPhoto_itemW});
	$(".detail .produt_photo .nav").css({"margin-left":-(detailPhoto_navW/2)});

	var detailprd_idx =0;
	$(".detail .produt_photo .nav ul li a").on("click", function(e){
		e.preventDefault();
		detailprd_idx = $(this).parent().index();
		$(".produt_photo .slider ul").animate({"margin-left": -(detailPhoto_itemW*detailprd_idx)},300,"easeInExpo");
		$(".detail .produt_photo .nav li").removeClass("cnt");
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+")").addClass("cnt");
	});
	
	function detailprd_left(){
		detailprd_idx--;
		if (detailprd_idx==-1){
			detailprd_idx=(detail_length-1);
			$(".produt_photo .slider ul li:eq("+(detail_length-1)+")").clone().insertBefore($(".produt_photo .slider ul li:eq(0)"));
			$(".produt_photo .slider ul").css({"margin-left":-detailPhoto_itemW});
			$(".produt_photo .slider ul").animate({"margin-left":0},300,"easeInExpo", function(){
				$(".produt_photo .slider ul").css({"margin-left":-(detailPhoto_itemW*(detailprd_idx))});
				$(".produt_photo .slider ul li:eq(0)").remove();
			});
		}
		else{
			$(".produt_photo .slider ul").animate({"margin-left": -(detailPhoto_itemW*detailprd_idx)},300,"easeInExpo");
		}
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+") a").click();
	}
	function detailprd_right(){ 
		detailprd_idx++;
		if (detailprd_idx==detail_length){
			detailprd_idx=0;
			$(".produt_photo .slider ul li:eq(0)").clone().insertAfter($(".produt_photo .slider ul li:eq("+(detail_length-1)+")")); 
			$(".produt_photo .slider ul").animate({"margin-left": +(-(detailPhoto_itemW*detail_length)) },300,"easeInExpo", function(){
				$(".produt_photo .slider ul").css({"margin-left":0});
				$(".produt_photo .slider ul li:eq("+(detail_length)+")").remove();
			});
		}else{
			$(".produt_photo .slider ul").animate({"margin-left": -(detailPhoto_itemW*detailprd_idx)},300,"easeInExpo");
		}
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+") a").click();
	}
	//손꾸락 //swipeleft, swiperight, swipeup, swipedown
	$(".detail .produt_photo .slider").on('swipeleft', function(e) {
		detailprd_idx++;
		if (detailprd_idx==detail_length){
			detailprd_idx=0;
			$(".produt_photo .slider ul li:eq(0)").clone().insertAfter($(".produt_photo .slider ul li:eq("+(detail_length-1)+")")); 
			$(".produt_photo .slider ul").animate({"margin-left": +(-(detailPhoto_itemW*detail_length)) },300,"easeInExpo", function(){
				$(".produt_photo .slider ul").css({"margin-left":0});
				$(".produt_photo .slider ul li:eq("+(detail_length)+")").remove();
			});
		}else{
			$(".produt_photo .slider ul").animate({"margin-left": -(detailPhoto_itemW*detailprd_idx)},300,"easeInExpo");
		}
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+") a").click();
	}).on('swiperight', function(e) {
		detailprd_idx--;
		if (detailprd_idx==-1){
			detailprd_idx=(detail_length-1);
			$(".produt_photo .slider ul li:eq("+(detail_length-1)+")").clone().insertBefore($(".produt_photo .slider ul li:eq(0)"));
			$(".produt_photo .slider ul").css({"margin-left":-detailPhoto_itemW});
			$(".produt_photo .slider ul").animate({"margin-left":0},300,"easeInExpo", function(){
				$(".produt_photo .slider ul").css({"margin-left":-(detailPhoto_itemW*(detailprd_idx))});
				$(".produt_photo .slider ul li:eq(0)").remove();
			});
		}
		else{
			$(".produt_photo .slider ul").animate({"margin-left": -(detailPhoto_itemW*detailprd_idx)},300,"easeInExpo");
		}
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+") a").click();
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

	/*이벤트 하단 바로구매*/
	$(".buy_product").css({"display":"none"})
	if( $("*").is(".setgood") ){
		$(".buy_product").css({"display":"block"})
	}
	$(".buy_product .slidedown").click(function(e){
		e.preventDefault();
		$(this).hide();
		$(".buy_product .slideup").show();
		$(".buy_product .dim").css({"display":"block"});
		$(".f_bottom").hide();
		$("body").bind('touchmove', function(e){e.preventDefault()});
	});
	$(".buy_product .slideup").click(function(e){
		$(this).hide();
		$(".buy_product .slidedown").show();
		$(".buy_product .dim").css({"display":"none"});
		$(".f_bottom").show();
		$("body").unbind('touchmove');
	});
	$(window).scroll(function(){
		scrollTop=$(window).scrollTop();
		scrollBottom = $(document).height()-$(window).height()-$(window).scrollTop();
		if (scrollTop >=44 && scrollBottom > 244){
			//$(".buy_product").css({"position":"fixed"});
			$(".footer").css({"z-index":"20"});
		}else if(scrollBottom < 244){
			//$(".buy_product").css({"position":"absolute"});
			//$(".footer").css({"z-index":"0"});
		}
	});

	/* 매거진 */
	if( $(".magazin_area").length ){
		//Var
		var curIdx = $("#magazin_list .listwrap ul li.cnt").index(),
			itemWid = $("#magazin_list .listwrap ul li").eq(0).outerWidth(),
			itemCount = $("#magazin_list .listwrap ul li").length,
			itemMargin = 10,
			listWid = (itemWid * itemCount) + (itemMargin * (itemCount-1)),
			halfWid = itemWid/2,
			curPos = 0,
			movePos, maxPos, winWid;

		//List Init
		$("#magazin_list .listwrap ul").css("width",listWid+"px");

		//View Init
		if($("#magazin_list .listwrap ul li.cnt").hasClass("movie")){
			// Movie
			$("#magazin_view .viewarea .bigimg").hide();
			$("#magazin_view .viewarea .moviearea").show();
		} else{
			// Picture
			$("#magazin_view .viewarea .moviearea").hide();
			$("#magazin_view .viewarea .bigimg").show();
		}

		//Windows Resize
		$(window).resize(function(){
			winWid = $(window).outerWidth();
			if(winWid >= (listWid+30) && curPos != 0){
				curPos = 0;
				$("#magazin_list .listwrap").css({"left":0});
			}
		}).trigger("resize");

		//List Swipe
		$("#magazin_list .listwrap").on("movestart", function(e){
			winWid = $(window).outerWidth();
			maxPos = listWid-winWid+30;
			if(winWid >= (listWid+30)){
				console.log(winWid,maxPos);
				e.preventDefault();
				return;
			}
		}).on("move", function(e){
			movePos = curPos + e.distX,
			$(this).stop().animate({"left":movePos+"px"},1);

			if(movePos > halfWid) {
				//Min Over Position
				$(this).stop().animate({"left":halfWid+"px"},1);
			} else if(movePos < -(maxPos+halfWid)) {
				//Max Over Position
				$(this).stop().animate({"left":-(maxPos+halfWid)+"px"},1);
			}
			console.log(movePos);
		}).on("moveend", function(e){
			if(movePos >= halfWid) movePos = 0;
			else if(movePos <= -(maxPos+halfWid)) movePos = -maxPos;
			curPos = movePos;
			$(this).stop().animate({"left":curPos+"px"},300,"easeOutQuad");

			e.preventDefault();
		});

		//Item Touch
		$("#magazin_list").on("click", ".listwrap ul li", function(e){
			e.preventDefault();
			var _item = $(this),
				itemIdx = _item.index(),
				titleTXT = _item.find("a img").attr("data-title"),
				picLOC = _item.find("a img").attr("data-pic");

			$("#magazin_list .listwrap ul li").removeClass("cnt");
			$("#magazin_list .listwrap ul li").eq(itemIdx).addClass("cnt");

			$("#magazin_view .viewarea .magazin_tit").html(titleTXT);

			if( _item.hasClass("movie") ){
				// Movie
				$("#magazin_view .viewarea .moviearea .movie iframe").attr("src",picLOC);

				$("#magazin_view .viewarea .bigimg").hide();
				$("#magazin_view .viewarea .moviearea").show();
			} else{
				// Picture
				$("#magazin_view .viewarea .bigimg img").attr("src",picLOC);

				$("#magazin_view .viewarea .moviearea").hide();
				$("#magazin_view .viewarea .bigimg").show();
			}
		});

		//Go Prev & Next
		$("#magazin_view").on("click", ".btn_area a", function(e){
			e.preventDefault();
			winWid = $(window).outerWidth();
			curIdx = $("#magazin_list .listwrap ul li.cnt").index();

			if($(this).hasClass("prev")){
				//prev item
				if(curIdx <= 0) return;

				/*
				if( (((itemWid*(curIdx+1)) - winWid) - curPos) > 0){
					curPos -= itemWid;
					$("#magazin_list .listwrap").stop().animate({"left":curPos+"px"},300,"easeOutQuad");
				}
				*/

				$("#magazin_list .listwrap ul li").eq(curIdx-1).trigger("click");
			} else{
				//next item
				if(curIdx >= ($("#magazin_list .listwrap ul li").length-1)) return;

				var nextIdx = curIdx+1;

				console.log("nextIdx",nextIdx);

				console.log( itemWid * nextIdx + itemWid );

				var lastItemBoundEnd = itemWid * nextIdx + itemWid;
				if (winWid < lastItemBoundEnd){


				}
				/*
				if( (((itemWid*(curIdx+2)) - winWid) + curPos) >= 0){
					curPos -= itemWid;
					$("#magazin_list .listwrap").stop().animate({"left":curPos+"px"},300,"easeOutQuad");
				}
				*/

				$("#magazin_list .listwrap ul li").eq(nextIdx).trigger("click");
			}
		});

	}

	/*매장안내*/
	$(".store_list ul li .branch_info .btn_mapview").toggle(function(e){
		e.preventDefault();
		$(this).parent().parent().find(".map").slideDown(300);
	}, function(e){
		e.preventDefault();
		$(this).parent().parent().find(".map").slideUp(300);
	});

	/*전시 메인*/
	imagesLoaded( '.display_bnr .slider', function() {
		display_bnrHeight = $(".display_bnr .slider img").height();
		$(".display .display_bnr").css({"height":display_bnrHeight});
	});
	$(".display_bnr .slider li").each(function(){
		$(".display_bnr .nav ul").append("<li><a href='#'></a></li> ");
	});
	$(".display_bnr .nav li").eq(0).addClass("cnt");
	$(".display_bnr .slider li").eq(0).show();

	var display_bnr_navW = $(".display_bnr .nav").width(); 
	$(".display_bnr .nav").css({"margin-left":-(display_bnr_navW/2)});
	
	var displayBnr_idx =0; 
	$(".display_bnr .nav ul li a").on("click", function(e){
		e.preventDefault();
		displayBnr_idx = $(this).parent().index();
		$(".display_bnr .slider li").fadeOut(500);
		$(".display_bnr .slider li:eq("+displayBnr_idx+")").fadeIn(500);
		$(".display_bnr .nav ul li").removeClass("cnt");
		$(".display_bnr .nav ul li:eq("+displayBnr_idx+")").addClass("cnt");
	});
	
	var displayBnr_length = $(".display_bnr .nav li").length;
	function displayBnr_left(){
		if(displayBnr_idx==0){
			displayBnr_idx =displayBnr_length;
		}
		displayBnr_idx--;
		$(".display_bnr .nav li:eq("+displayBnr_idx+") a").click();
	}
	function displayBnr_right(){
		displayBnr_idx++;
		if(displayBnr_idx==displayBnr_length){
			displayBnr_idx = 0;
		}
		$(".display_bnr .nav li:eq("+displayBnr_idx+") a").click();
	}

	//손꾸락 //swipeleft, swiperight, swipeup, swipedown
	$(".display_bnr .slider").on('swipeleft', function(e) {
		displayBnr_right();
	}).on('swiperight', function(e) {
		displayBnr_left();
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

	/*cart*/
	$(".sliderh3_tit").each(function(){
		if( $(this).hasClass("on") ){
			$(this).parent().find(".sliderh3_con").css({"display":"block"});
		}
	});
	
	$(".sliderh3_tit").click(function(){ 
		if( $(this).hasClass("on") ){
			$(this).parent().find(".sliderh3_con").slideUp();
			$(this).removeClass("on")
		}else{
			$(this).parent().find(".sliderh3_con").slideDown();
			$(this).addClass("on")
		}
	});

	/*상품상세*/
	/*
		var ProInfoHeight = $("#proInfo").height();
		$("#proInfo").css({"height":"610px", "overflow":"hidden"});
		$(".btnmore").click(function(e){
			e.preventDefault();
			$("#proInfo").css({"height":ProInfoHeight});
			$(".btnmore").hide();
		});
	*/
	imagesLoaded( '#proInfo', function() {
		ProInfoHeight = $("#proInfo").height();
		//alert(ProInfoHeight);
		$("#proInfo").css({"height":"610px", "overflow":"hidden"});
	});
	$(".btnmore").click(function(e){
		e.preventDefault();
		$("#proInfo").css({"height":ProInfoHeight});
		$(".btnmore").hide();
	});
	
	
	/*브랜드소개*/
	/*var scrollYval = $(".visual").height();
	$(".visual img").load(function(){
		scrollYval = $(".visual").height()+130;
		//alert(scrollYval);
		$(".txt_brandstroy,.brand_tab li").on("click", function(){
			$('html, body').stop().animate({ scrollTop : scrollYval },500 ,"easeInExpo");
		});
	})*/
	
	
	var brandWidth = $(window).width()-20; // 아이템 width
	var winHeight ;
	$(".tab_content_wrap .bg").load(function(){
		$(".tab_content_wrap .bg").height();
		winHeight = $(".tab_content_wrap .bg").height();
		$(".tab_content_wrap").css({"width":brandWidth+20, "height":winHeight});
	});
	$(".tab_content_wrap .tab_content").css({"width":brandWidth});
	$(".tab_content_wrap .tab_content li").css({"width":brandWidth});
	$(window).resize(function(){
		brandWidth = $(window).width()-20;
		winHeight = $(".tab_content_wrap .bg").height();
		$(".tab_content_wrap").css({"width":brandWidth+20, "height":winHeight});
		$(".tab_content_wrap .tab_content").css({"width":brandWidth});
		$(".tab_content_wrap .tab_content li").css({"width":brandWidth});
	});
	
	var brand_idx=0; //idx값
	var brandTotal_length = $(".tab_content_wrap .slider li").length; //총 li갯수
	$(".tab_content_wrap .subject_slider li img").load(function(){
		$(".tab_content_wrap .btn_prevnext").css({"height":$(".tab_content_wrap .subject_slider li img").height()})
	});
	
	function brandSlide(){
		$(".tab_content_wrap .slider").animate({"margin-left": -(brandWidth*brand_idx)},300,"easeInExpo");
	}
	$(".tab_content_wrap .btn_prevnext a.prev ").click(function(e){
		e.preventDefault();
		brand_idx--;
		//console.log(brand_idx);
		if(brand_idx==-1) {
			brand_idx = brandTotal_length-1;
			$(".brand_tab ul li").removeClass("cnt")
			$(".brand_tab ul li:eq(2)").addClass("cnt");
			$(".subject_slider li").hide();
			$(".subject_slider li:eq(2)").fadeIn();
			$(".tab_content_wrap .slider li:eq("+(brandTotal_length-1)+")").clone().insertBefore($(".tab_content_wrap .slider li:eq(0)"));
			$(".tab_content_wrap .slider").css({"margin-left":-brandWidth});
			$(".tab_content_wrap .slider").animate({"margin-left":0},300,"easeInExpo", function(){
				$(".tab_content_wrap .slider").css({"margin-left":-(brandWidth*(brand_idx))});
				$(".tab_content_wrap .slider li:eq(0)").remove();
			});
			$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_1.jpg') 0 0 no-repeat", "background-size":"cover" });
		}else if(brand_idx==4){
			$(".brand_tab ul li").removeClass("cnt")
			$(".brand_tab ul li:eq(0)").addClass("cnt");
			$(".subject_slider li").hide();
			$(".subject_slider li:eq(0)").fadeIn();
			brandSlide();
			$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_2.jpg') 0 0 no-repeat", "background-size":"cover" });
		}else if(brand_idx==8){
			$(".brand_tab ul li").removeClass("cnt")
			$(".brand_tab ul li:eq(1)").addClass("cnt");
			$(".subject_slider li").hide();
			$(".subject_slider li:eq(1)").fadeIn();
			brandSlide();
			$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_2.jpg') 0 0 no-repeat", "background-size":"cover" });
		}else{
			brandSlide();
		}
	});
	$(".tab_content_wrap .btn_prevnext a.next").click(function(e){
		e.preventDefault();
		brand_idx++;
		//console.log(brand_idx);
		if (brand_idx==5){
			brandSlide();
			$(".brand_tab ul li").removeClass("cnt")
			$(".brand_tab ul li:eq(1)").addClass("cnt");
			$(".subject_slider li").hide();
			$(".subject_slider li:eq(1)").fadeIn();
			$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_2.jpg') 0 0 no-repeat", "background-size":"cover" });
		}else if(brand_idx==9){
			$(".brand_tab ul li").removeClass("cnt")
			$(".brand_tab ul li:eq(2)").addClass("cnt");
			$(".subject_slider li").hide();
			$(".subject_slider li:eq(2)").fadeIn();
			brandSlide();
			$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_3.jpg') 0 0 no-repeat", "background-size":"cover" });
		}
		else if(brand_idx==brandTotal_length){
			$(".brand_tab ul li").removeClass("cnt")
			$(".brand_tab ul li:eq(0)").addClass("cnt");
			$(".subject_slider li").hide();
			$(".subject_slider li:eq(0)").fadeIn();
			brand_idx=0;
			$(".tab_content_wrap .slider li:eq(0)").clone().insertAfter($(".tab_content_wrap .slider li:eq("+(brandTotal_length-1)+")")); 
			$(".tab_content_wrap .slider").animate({"margin-left": +(-(brandWidth*brandTotal_length)) },300,"easeInExpo", function(){
				$(".tab_content_wrap .slider").css({"margin-left":0});
				$(".tab_content_wrap .slider li:eq("+(brandTotal_length)+")").remove();
			});
			$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_1.jpg') 0 0 no-repeat", "background-size":"cover" });
		}else{
			brandSlide();
		}
	});
	//손꾸락 //swipeleft, swiperight, swipeup, swipedown
	$(".tab_content_wrap .slider").on('swipeleft', function(e) {
		$(".tab_content_wrap .btn_prevnext a.next").click();
	}).on('swiperight', function(e) {
		$(".tab_content_wrap .btn_prevnext a.prev ").click();
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
	$(".brand_tab ul li:eq(0)").click(function(e){
		e.preventDefault();
		brand_idx =0;
		$(".brand_tab ul li").removeClass("cnt");
		$(".brand_tab ul li:eq(0)").addClass("cnt");
		$(".subject_slider li").hide();
		$(".subject_slider li:eq(0)").fadeIn();
		$(".tab_content_wrap .slider").animate({"margin-left": -(brandWidth*brand_idx)},300,"easeInExpo");
		$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_1.jpg') 0 0 no-repeat", "background-size":"cover" });
	});
	$(".brand_tab ul li:eq(1)").click(function(e){
		e.preventDefault();
		brand_idx =5;
		$(".brand_tab ul li").removeClass("cnt");
		$(".brand_tab ul li:eq(1)").addClass("cnt");
		$(".subject_slider li").hide();
		$(".subject_slider li:eq(1)").fadeIn();
		$(".tab_content_wrap .slider").animate({"margin-left": -(brandWidth*brand_idx)},300,"easeInExpo");
		$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_2.jpg') 0 0 no-repeat", "background-size":"cover" });
	});
	$(".brand_tab ul li:eq(2)").click(function(e){
		e.preventDefault();
		brand_idx =9;
		$(".brand_tab ul li").removeClass("cnt");
		$(".brand_tab ul li:eq(2)").addClass("cnt");
		$(".subject_slider li").hide();
		$(".subject_slider li:eq(2)").fadeIn();
		$(".tab_content_wrap .slider").animate({"margin-left": -(brandWidth*brand_idx)},300,"easeInExpo");
		$(".tab_content_wrap").css({"background":"url('/aboutCom/images/aboutme/img_brand2_3.jpg') 0 0 no-repeat", "background-size":"cover" });
	});
	
	/*타임세일 이벤트 슬라이드*/
	var timebnrWidth=$(window).width();
	$(".eventtime_bnr").css({"width":timebnrWidth});
	$(".eventtime_bnr li").css({"width":timebnrWidth});
	$(window).resize(function(){
		timebnrWidth=$(window).width();
		$(".eventtime_bnr").css({"width":timebnrWidth});
		$(".eventtime_bnr li").css({"width":timebnrWidth});
	});
	var timebnr_idx=0; //idx값
	var timebnrTotal_length = $(".eventtime_bnr .slider li").length; //총 li갯수
	function timebnrSlide(){
		$(".eventtime_bnr .slider").animate({"margin-left": -((timebnrWidth)*timebnr_idx)},300,"easeInExpo");
	}
	if(timebnrTotal_length==1){
		$(".eventtime_bnr .btn_prevnext").css({"display":"none"});
	}else{
		$(".eventtime_bnr .btn_prevnext .btn_prev").click(function(e){
			e.preventDefault();
			timebnr_idx--;
			if(timebnr_idx=-1){
				timebnr_idx = timebnrTotal_length-1;
				$(".eventtime_bnr .slider li:eq("+(timebnrTotal_length-1)+")").clone().insertBefore($(".eventtime_bnr .slider li:eq(0)"));
				$(".eventtime_bnr .slider").css({"margin-left":-(timebnrWidth)});
				$(".eventtime_bnr .slider").animate({"margin-left":0},300,"easeInExpo", function(){
					$(".eventtime_bnr .slider").css({"margin-left":-((timebnrWidth)*(timebnr_idx))});
					$(".eventtime_bnr .slider li:eq(0)").remove();
				});
			}else{
				timebnrSlide();
			}
		});
		$(".eventtime_bnr .btn_prevnext .btn_next").click(function(e){
			e.preventDefault();
			timebnr_idx++;
			if(timebnr_idx==timebnrTotal_length){
				timebnr_idx=0;
				$(".eventtime_bnr .slider li:eq(0)").clone().insertAfter($(".eventtime_bnr .slider li:eq("+(timebnrTotal_length-1)+")")); 
				$(".eventtime_bnr .slider").animate({"margin-left": +(-((timebnrWidth)*timebnrTotal_length)) },300,"easeInExpo", function(){
					$(".eventtime_bnr .slider").css({"margin-left":0});
					$(".eventtime_bnr .slider li:eq("+(timebnrTotal_length)+")").remove();
				});
			}else{
				timebnrSlide();
			}
		});
		//손꾸락 //swipeleft, swiperight, swipeup, swipedown
		$(".eventtime_bnr .slider").on('swipeleft', function(e) {
			$(".eventtime_bnr .btn_prevnext .btn_next").click();
		}).on('swiperight', function(e) {
			$(".eventtime_bnr .btn_prevnext .btn_prev").click();
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
	}

	//메인 배너 슬라이드
	if($(".slide_banner").length){
		$(".slide_banner .slider ul li").each(function(){
			$(".slide_banner .nav ul").append("<li><a href='#'></a></li> ");
		});
		$(".slide_banner .nav ul li").eq(0).addClass("cnt");
		var photo_itemW = $(".slide_banner .slider").width();
		var item_length = $(".slide_banner .nav li").length;
		$(".slide_banner .slider li").css({"width":photo_itemW});

		var prd_idx =0;
		$(".slide_banner .nav ul li a").on("click", function(e){
			e.preventDefault();
			prd_idx = $(this).parent().index();
			$(".slide_banner .slider ul").animate({"margin-left": -(photo_itemW*prd_idx)},300,"easeInExpo");
			$(".slide_banner .nav li").removeClass("cnt");
			$(".slide_banner .nav li:eq("+prd_idx+")").addClass("cnt");
		});
		
		function detailprd_left(){
			prd_idx--;
			if (prd_idx==-1){
				prd_idx=(item_length-1);
				$(".slide_banner .slider ul li:eq("+(item_length-1)+")").clone().insertBefore($(".slide_banner .slider ul li:eq(0)"));
				$(".slide_banner .slider ul").css({"margin-left":-photo_itemW});
				$(".slide_banner .slider ul").animate({"margin-left":0},300,"easeInExpo", function(){
					$(".slide_banner .slider ul").css({"margin-left":-(photo_itemW*(prd_idx))});
					$(".slide_banner .slider ul li:eq(0)").remove();
				});
			}
			else{
				$(".slide_banner .slider ul").animate({"margin-left": -(photo_itemW*prd_idx)},300,"easeInExpo");
			}
			$(".slide_banner .nav li:eq("+prd_idx+") a").click();
		}
		function detailprd_right(){
			prd_idx++;
			if (prd_idx==item_length){
				prd_idx=0;
				$(".slide_banner .slider ul li:eq(0)").clone().insertAfter($(".slide_banner .slider ul li:eq("+(item_length-1)+")")); 
				$(".slide_banner .slider ul").animate({"margin-left": +(-(photo_itemW*item_length)) },300,"easeInExpo", function(){
					$(".slide_banner .slider ul").css({"margin-left":0});
					$(".slide_banner .slider ul li:eq("+(item_length)+")").remove();
				});
			}else{
				$(".slide_banner .slider ul").animate({"margin-left": -(photo_itemW*prd_idx)},300,"easeInExpo");
			}
			$(".slide_banner .nav li:eq("+prd_idx+") a").click();
		}

		$(window).resize(function(){
			photo_itemW = $(".slide_banner .slider").width();
			$(".slide_banner .slider li").css({"width":photo_itemW});
			$(".slide_banner .slider ul").css({"margin-left": -(photo_itemW*prd_idx)});
		});

		//손꾸락 //swipeleft, swiperight, swipeup, swipedown
		$(".slide_banner .slider").on('swipeleft', function(e) {
			detailprd_right();
		}).on('swiperight', function(e) {
			detailprd_left();
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
	}
});
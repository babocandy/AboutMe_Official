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
$(function(){
	/*뒤로가기*/
	$(".historyback").click(function(){
		historyback();
	});
	/*nav*/
	$(".openlnb").click(function(e){
		e.preventDefault();
		$(".lnb_area").show();
		$(".lnb").animate({"left":"0"},"fast" );
		$(".container").css({"position":"fixed"});
		$(".f_bottom ").hide();
	});
	$(".lnbclose").click(function(e){
		e.preventDefault();
		$(".lnb").stop().animate({"left":"-280px"},"100", function(){$(".lnb_area").hide()});
		$(".container").css({"position":"relative"});
		$(".f_bottom ").show();
	});
	$(".nav li a").on("click", function(e){
		e.preventDefault();
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

	/*장바구니*/
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

	//장바구니 textarea 글자수제한
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
	$(".detail .produt_photo .slider li").eq(0).show();
	var detailPhoto_navW = $(".detail .produt_photo .nav").width();
	$(".detail .produt_photo .nav").css({"margin-left":-(detailPhoto_navW/2)});

	var detailprd_idx =0;
	$(".detail .produt_photo .nav ul li a").on("click", function(e){
		e.preventDefault();
		detailprd_idx = $(this).parent().index();
		$(".detail .produt_photo .slider li").fadeOut(500);
		$(".detail .produt_photo .slider li:eq("+detailprd_idx+")").fadeIn(500);
		$(".detail .produt_photo .nav li").removeClass("cnt");
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+")").addClass("cnt");
	});
	var detail_length = $(".detail .produt_photo .nav li").length;
	function detailprd_left(){
		if(detailprd_idx==0){
			detailprd_idx =detail_length;
		}
		detailprd_idx--;
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+") a").click();
	}
	function detailprd_right(){
		detailprd_idx++;
		if(detailprd_idx==detail_length){
			detailprd_idx = 0;
		}
		$(".detail .produt_photo .nav li:eq("+detailprd_idx+") a").click();
	}
	//손꾸락 //swipeleft, swiperight, swipeup, swipedown
	$(".detail .produt_photo .slider").on('swipeleft', function(e) {
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
		$("body").bind('touchmove', function(e){e.preventDefault()});
	});
	$(".buy_product .slideup").click(function(e){
		$(this).hide();
		$(".buy_product .slidedown").show();
		$(".buy_product .dim").css({"display":"none"});
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

});
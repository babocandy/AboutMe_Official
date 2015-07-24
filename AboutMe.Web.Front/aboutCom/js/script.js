function link(url){
	location.href = url;
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
/*새창팝업닫기*/
function PopupClose(){
	self.close();
}
$(function(){
	if( $("*").is(".txtdotdot") ){
		$(".txtdotdot").dotdotdot();
	}
	//$('input, textarea').placeholder({customClass:'my-placeholder'});

	/*gnb 드롭다운*/
	$(".gnb li a.menu").mouseenter(function(){
		if ( $(this).hasClass("dropmenu2") ){
			$(".depth2").fadeIn("fast", function(){
				$(".depth2 .list").slideDown("fast");
			});
			$(".depth1_2").fadeOut("fast");
		}else if( $(this).hasClass("dropmenu1") ){
			$(".depth2 .list").slideUp("fast", function(){
				$(".depth2").slideUp("fast")
			});
			$(".depth1_2").fadeIn("fast");
		}else{
			$(".depth2 .list").slideUp("fast", function(){
				$(".depth2").slideUp("fast")
			});
			$(".depth1_2").fadeOut("fast");
		}
	});
	$(document).mousemove(function(e){
		if(e.pageY < 50 || e.pageY >430){
			$(".depth2 .list").slideUp("fast", function(){
				$(".depth2").slideUp("fast")
			});
		}else if(e.pageY < 50 || e.pageY >330){
			$(".depth1_2").fadeOut("fast");
		}
	})

	/*닫기*/
	if( $("#btnClose").length ){
		$("#btnClose").live("click", function(e){
			e.preventDefault();
			$(this).parent().hide();
		});	
	}

	/*tabs*/
	$(".flight .tab_content").eq(0).show();
	$('ul.tabs li').click(function() {
		if (!$('ul.tabs').length) { return; }
		tabIdx = $(this).index();
		$(this).parent().find("li").removeClass('cnt');
		$(this).addClass('cnt');
		$($(this).children().attr('href')).show().siblings('div.tab_content').hide();
		$(".txtdotdot").dotdotdot();
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

	/* select radio checkbox */
	if ( $("*").is(".selectstylea") ){
		$("select.selectstylea").selectCssA();
	}
	if ( $("*").is(".selectstyleb") ){
		$("select.selectstyleb").selectCssB();
	}
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

	/*아코디언타입*/
	$(".accordion .accordcont .accordtxt").hide();
	$(".accordion .accordcont.cnt .accordtxt").show();
	$(".accordion .accordcont .accordsubject").click(function(){
		if ( 'none' == $(this).parent().find(".accordtxt").css('display') ) {
			$(".accordion .accordcont .accordtxt").slideUp();
			$(this).parent().find(".accordtxt").slideDown();
			$(".accordcont").removeClass("cnt");
			$(this).parent().addClass("cnt");
		}
		else {
			$(".accordion .accordcont .accordtxt").slideUp();
			$(this).parent().removeClass("cnt");
		}
	});

	/*fadeslider*/
	$(".fadeslider .fadeslidercon li:eq(0)").css({"display":"block"});
	var fadesliderIdx = 0;
	var fadeLength = $(".fadeslidercon li").length;
	$(".fadeslidernav li a").on("click", function(){
		fadesliderIdx = $(this).parent().index();
		$(".fadeslider .fadeslidercon li").fadeOut(500);
		$(".fadeslider .fadeslidercon li:eq("+fadesliderIdx+")").fadeIn(500);
		$(".fadeslider .fadeslidernav li").removeClass("cnt");
		$(".fadeslider .fadeslidernav li:eq("+fadesliderIdx+")").addClass("cnt");
	});
	$(".fadeslider .fadesliderbtn .prev").on("click", function(){
		fadesliderIdx--;
		if (fadesliderIdx==-1){
			fadesliderIdx = fadeLength-1;
		}
		$(".fadeslider .fadeslidercon li").fadeOut(500);
		$(".fadeslider .fadeslidercon li:eq("+fadesliderIdx+")").fadeIn(500);
		$(".fadeslider .fadeslidernav li").removeClass("cnt");
		$(".fadeslider .fadeslidernav li:eq("+fadesliderIdx+")").addClass("cnt");
	});
	$(".fadeslider .fadesliderbtn .next").on("click", function(){
		fadesliderIdx++;
		if (fadesliderIdx==fadeLength){
			fadesliderIdx = 0;
		}
		$(".fadeslider .fadeslidercon li").fadeOut(500);
		$(".fadeslider .fadeslidercon li:eq("+fadesliderIdx+")").fadeIn(500);
		$(".fadeslider .fadeslidernav li").removeClass("cnt");
		$(".fadeslider .fadeslidernav li:eq("+fadesliderIdx+")").addClass("cnt");
	});

	/*moveslider*/
	var movesliderIdx= 0;
	var movesliderWidth = $(".moveslider .moveslidercon li").width(); //li의 넓이
	var movesliderLength = $(".moveslider .moveslidercon li").length; //li의 갯수
	$(".gnb_banner .slide li").each(function(){
		$(".moveslidernav").append("<li><a href='#'><img src='/aboutCom/images/common/nav_banner.gif' alt='' /></a></li> ");
	});
	$(".moveslidernav li:eq(0)").addClass("cnt");
	$(".moveslider .moveslidernav a").on("click", function(){
		movesliderIdx = $(this).parent().index();
		$(".moveslider .moveslidercon").animate({"margin-left": -(movesliderWidth*movesliderIdx)},300,"easeInExpo");
		$(".moveslider .moveslidernav li").removeClass("cnt");
		$(".moveslider .moveslidernav li:eq("+movesliderIdx+")").addClass("cnt");
	});
	$(".moveslider .movesliderbtn .prev").on("click", function(){
		movesliderIdx--;
		if (movesliderIdx==-1){
			movesliderIdx=(movesliderLength-1);
			$(".moveslider .moveslidercon li:eq("+(movesliderLength-1)+")").clone().insertBefore($(".moveslidercon li:eq(0)"));
			$(".moveslider .moveslidercon").css({"margin-left":-movesliderWidth});
			$(".moveslider .moveslidercon").animate({"margin-left":0},300,"easeInExpo", function(){
				$(".moveslider .moveslidercon").css({"margin-left":-(movesliderWidth*(movesliderIdx))});
				$(".moveslider .moveslidercon li:eq(0)").remove();
			});
		}
		else{
			//console.log(movesliderIdx);
			$(".moveslider .moveslidercon").animate({"margin-left": -(movesliderWidth*movesliderIdx)},300,"easeInExpo");
		}
		$(".moveslider .moveslidernav li:eq("+movesliderIdx+") a").click();
	});
	$(".moveslider .movesliderbtn .next").on("click", function(){
		movesliderIdx++;
		if (movesliderIdx==movesliderLength){
			movesliderIdx=0;
			$(".moveslider .moveslidercon li:eq(0)").clone().insertAfter($(".moveslidercon li:eq("+(movesliderLength-1)+")")); //eq는 0부터 시작. length는 1부터 시작해서 5가 나오기때문에 -1을 줘야 4번째 eq나옴
			$(".moveslider .moveslidercon").animate({"margin-left": +(-(movesliderWidth*movesliderLength)) },300,"easeInExpo", function(){
				$(".moveslider .moveslidercon").css({"margin-left":0});
				$(".moveslider .moveslidercon li:eq("+(movesliderLength)+")").remove();
			});
		}
		else{
			$(".moveslider .moveslidercon").animate({"margin-left": -(movesliderWidth*movesliderIdx)},300,"easeInExpo");
		}
		$(".moveslider .moveslidernav li:eq("+movesliderIdx+") a").click();
	});
	//메인gnb자동슬라이드
	var gnbBannerSlideTime = setInterval(function(){
		movesliderIdx++;
		if (movesliderIdx==movesliderLength){
			movesliderIdx=0;
			$(".moveslider .moveslidercon li:eq(0)").clone().insertAfter($(".moveslidercon li:eq("+(movesliderLength-1)+")")); //eq는 0부터 시작. length는 1부터 시작해서 5가 나오기때문에 -1을 줘야 4번째 eq나옴
			$(".moveslider .moveslidercon").animate({"margin-left": +(-(movesliderWidth*movesliderLength)) },300,"easeInExpo", function(){
				$(".moveslider .moveslidercon").css({"margin-left":0});
				$(".moveslider .moveslidercon li:eq("+(movesliderLength)+")").remove();
			});
		}
		//$(".moveslider .moveslidernav li:eq("+movesliderIdx+") a").click();
		$(".moveslider .moveslidercon").animate({"margin-left": -(movesliderWidth*movesliderIdx)},300,"easeInExpo");
	},3000);


	/*listslider*/
	var listsliderIdx = 0;
	var listsliderLength= $(".listslidercon li").length;
	var listsliderWidth = $(".listslidercon li").width();
	$(".listslider .listsliderbtn .prev").click(function(e){
		e.preventDefault();
		listsliderIdx-- ;
		$(".listslidercon li:eq("+(listsliderLength-1)+")").clone().insertBefore($(".listslidercon li:eq(0)"));
		$(".listslidercon").css({"margin-left":-listsliderWidth});
		$(".listslidercon").animate({"margin-left":0}, 300, "easeInExpo", function(){
			$(".listslidercon").css({"margin-left":0});
			$(".listslidercon li:eq("+(listsliderLength)+")").remove();
		});
		if(listsliderIdx < 0){
			listsliderIdx = listsliderLength-1;
		}
	});
	$(".listslider .listsliderbtn .next").click(function(e){
		e.preventDefault();
		listsliderIdx ++;
		$(".listslidercon li:eq(0)").clone().insertAfter($(".listslidercon li:eq("+(listsliderLength-1)+")"));
		$(".listslidercon").animate({"margin-left":-(listsliderWidth)}, 300, "easeInExpo", function(){
				$(".listslidercon").css({"margin-left":0});
				$(".listslider .listslidercon li:eq(0)").remove();
		});
		if(listsliderIdx==listsliderLength-1){
			listsliderIdx =0;
		}
	});

	/*rolling*/
	if( $("*").is(".rolling") ){
		var rollingControl = false;
		var list = $(".rolling ul");
		var itemWidth = list.children(":first").outerWidth();
		list.css("width",(itemWidth*list.find("li").length)+"px");
		$(".rolling").each(function(){
			list.append(list.html());
		});

		$(".rolling").on("click",".btn_right",function(e){
			e.preventDefault();
			if(rollingControl) return;
			rollingControl = true;

			if( $("*").is(".event") ){
				list.stop().animate({"opacity":"0"},600);
				setTimeout(function(){
					list.css("margin-left",-itemWidth).children(":last").after(list.children(":first"));
					list.css("margin-left",0);
					list.stop().animate({"opacity":"1"},400);
					rollingControl = false;
				},350);
			} else if( $("*").is(".timesale") ){
				list.stop().animate({
					marginLeft:-itemWidth
				}, 1000, "easeOutExpo", function(){
					$(this).children(":last").after($(this).children(":first"));
					$(this).css("margin-left","0");
					rollingControl = false;
				});
			}
		});

		$(".rolling").on("click",".btn_left",function(e){
			e.preventDefault();
			if(rollingControl) return;
			rollingControl = true;
			list.children(":first").before(list.children(":last"));
			list.css("margin-left",-itemWidth);
			if( $("*").is(".event") ){
				list.stop().animate({"opacity":"0"},600);
				setTimeout(function(){
					list.css("margin-left",0);
					list.stop().animate({"opacity":"1"},400);
					rollingControl = false;
				},350);
			} else if( $("*").is(".timesale") ){
				list.stop().animate({
					marginLeft:0
				}, 1000, "easeOutExpo", function(){
					rollingControl = false;
				});
			}
		});
	}

	/*상세 이벤트 배너롤링*/
	var eventbnrIdx =0;
	var eventbnrWidth = $(".event_banner .slide li").width();
	var eventbnrLength = $(".event_banner .slide li").length;
	$(".event_banner .slide li").each(function(){
		$(".event_banner .nav").append("<li><a href='#'><img src='/aboutCom/images/common/nav_ban.png' /></a></li> ");
	});
	$(".event_banner .nav li").eq(0).addClass("cnt");

	$(".event_banner .nav a").on("click", function(e){
		e.preventDefault();
		eventbnrIdx = $(this).parent().index();
		$(".event_banner .slide").animate({"margin-left": -(eventbnrWidth*eventbnrIdx)},300,"easeInExpo");
		$(".event_banner .nav li").removeClass("cnt");
		$(".event_banner .nav li:eq("+eventbnrIdx+")").addClass("cnt");
	});
	//자동슬라이드
	var eventbnrBannerSlideTime = setInterval(function(){
		eventbnrIdx++;
		if (eventbnrIdx==eventbnrLength){
			eventbnrIdx=0;
			$(".event_banner .slide li:eq(0)").clone().insertAfter($(".event_banner .slide li:eq("+(eventbnrLength-1)+")")); //eq는 0부터 시작. length는 1부터 시작해서 5가 나오기때문에 -1을 줘야 4번째 eq나옴
			$(".event_banner .slide").animate({"margin-left": +(-(eventbnrWidth*eventbnrLength)) },300,"easeInExpo", function(){
				$(".event_banner .slide").css({"margin-left":0});
				$(".event_banner .slide li:eq("+(eventbnrLength)+")").remove();
			});
		}
		$(".event_banner .nav li:eq("+eventbnrIdx+") a").click();
	},4000);

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

	/*상품상세 적립금안내 툴팁*/
	$(".user_have .savemony_txt").hide();
	$(".user_have .savemony").mouseenter(function(){
		$(".user_have .savemony_txt").show();
	});
	$(".user_have .savemony_txt").mouseleave(function(){
		$(".user_have .savemony_txt").hide();
	});


	/*상품상세 썸네일 클릭 시 큰 이미지 변환*/
	$(".thumb_area li").click(function(){
		$(".thumb_area li").removeClass("cnt");
		$(this).addClass("cnt");
//		var thumbImgUrl = $(this).find("img").attr("data-pic");
		var thumbImgUrl = $(this).find("img").data("pic");
		$(".large_area img").attr("src", thumbImgUrl);
	});

	/*상품상세 영상*/
	var lymvUrl =$(".lymv .mv iframe").attr("src");
	var lymv ='<iframe width="640" height="390" src="'+lymvUrl+'" frameborder="0" allowfullscreen></iframe>';
	$(".lymv .mv").html("");
	$(".movie_thumb i").click(function(){
		$("html,body").css("overflow-y","hidden");
		$(".movie_thumb .lydim").show();
		$(".movie_thumb .lymv").show();
		$(".movie_thumb .lymv").animate({"width":"640px", "height":"390px"});
		$(".lymv .mv").html(lymv);
	});
	$(".lymv .mv_close").click(function(){
		$("html,body").css("overflow-y","auto");
		$(".movie_thumb .lymv").hide();
		$(".movie_thumb .lydim").hide();
		$(".lymv .mv").html("");
		$(".movie_thumb .lymv").css({"width":0, "height":0});
	});

	/*상품상세 리뷰class추가
	$(".pur_review .cont:first").addClass("first");
	$(".pur_review .cont:last").addClass("last")*/

	/*전시*/
	$(".product_top .sort li").click(function(e){
		e.preventDefault();
		$(".product_top .sort li").removeClass("cnt");
		$(this).addClass("cnt");
	});

	$(".product_area").each(function(){$(this).find(".product_list li:eq(0),.product_list li:eq(1)").css({"border-top":" 1px solid #bbb5b4"});});

	//2015-07-01 추가
	var productImgUrl;
	var productOverImgUrl ;
	$(".product_list li").mouseenter(function(){
		productImgUrl =$(this).find("img.item").attr("src");
		productOverImgUrl=$(this).find("img.item").data("pic");

		$(this).find("img.item").attr("src",productOverImgUrl)
	}).mouseleave(function(){
		$(this).find("img.item").attr("src",productImgUrl)
	});

	//
	$(".selecttypeC .sort .tit").on("click",function(e){
		e.preventDefault();
		$(".selecttypeC .sort").find("ul").stop().slideUp(300);
		$(".selecttypeC .sort").removeClass("on");

		$(this).parent().find("ul li").each(function(){
			if($(this).hasClass("cnt")){ 
				$(this).parent().parent().addClass("on");
			} 
		})
		if(!$(this).parent().hasClass("open")){ 
			$(this).parent().find("ul").stop().slideDown(300, function(){ 
				$(".selecttypeC .sort").removeClass("open"); 
				$(this).parent().addClass("on");   
				$(this).parent().addClass("open");   
			}); 
		}
		else{
			$(".selecttypeC .sort").removeClass("open"); 
		}  
	});

	$(".selecttypeC .sort ul li").click(function(e){
		e.preventDefault();
		$(".selecttypeC .sort ul li").removeClass("cnt");
		$(this).addClass("cnt");
		$(".selecttypeC .sort ul").delay(40).stop().slideUp(200); 
		$(".selecttypeC .sort").removeClass("open"); 
 
	})

	if( $("*").is(".wrap.order") ){
		/*라디오 버튼별 툴팁 뷰*/
		$(".paycon .racon input[name='radio3']").change(function(e){
			e.preventDefault();
			$(".paycon .paytip").css("display","none");
			if($(this).parents("#rdoCredit").length){
				$("#credit").css("display","block");
			} else if($(this).parents("#rdoRealtime").length){
				$("#realtime").css("display","block");
			} else if($(this).parents("#rdoVirtual").length){
				$("#virtual").css("display","block");
			}
		});
	}

	/*timesale TIP 둥둥이 <-> 윙배너*/
	if( $("*").is(".tip_detail,.tip") ){
		//페이지 로드 시 자동 닫기
		setTimeout(function(){
			$(".tip_detail").trigger("click");
		}, 2000);

		$(".tip_detail").on("click",function(e){
			e.preventDefault();
			$(this).animate({"width":0,"height":0,"font-size":0},300,function(){
				$(this).css("display","none")
				$(".tip").css("display","block").animate({"height":55+"px"},100);
			});
		});
		$(".tip").on("click",function(e){
			e.preventDefault();
			$(this).animate({"height":0},100,function(){
				$(this).css("display","none")
				$(".tip_detail").css("display","block").animate({"width":143+"px","height":143+"px","font-size":14+"px"},300);
			});
		});
	}

	$("#header .viewcart").click(function(){
		$("#header .lycart").show();
	});
	$(".lycart .lycart_close").click(function(){
		$("#header .lycart").hide();
	});

	/*레이어 닫기*/
	if( $(".ly").length ){
		$(".ly .ly_close").on("click",function(e){
			e.preventDefault();
			$(this).parents(".ly").css("display","none");
		});
	}

	/*리뷰*/
	$(".reviewConts_area .sort li").click(function(e){
		e.preventDefault();
		$(".reviewConts_area .sort li").removeClass("cnt")	;
		$(this).addClass("cnt");
	});

	/*aboutme 매장안내*/
	$(".store_area .region li").click(function(e){
		e.preventDefault();
		$(".store_area .region li").removeClass("cnt")	;
		$(this).addClass("cnt");
	});
	var storeidx = 1;
	var storeHeight = $(".store .map_area .store .list li").height()+5;
	var storesliderLength = $(".store .map_area .store .list li").length;
	var showNum = 3 ;

	if(showNum >= storesliderLength){
		$(".map_area .store .btn_updown .down").addClass("disable");
	}
	$(".store .btn_updown .up").on("click", function(e){
		e.preventDefault();
		if(storeidx<=1){
			$(".map_area .store .btn_updown .up").addClass("disable");
			return;
		}
		storeidx--;
		$(".map_area .store .btn_updown .down").removeClass("disable")
		$(".store .map_area .store .list").animate({"margin-top": -(storeHeight*(storeidx-1))},300,"easeInExpo");
	});
	$(".store .btn_updown .down").on("click", function(e){
		e.preventDefault();
		if((storeidx + showNum -1) == storesliderLength){
			return;
		}
		storeidx++;
		$(".store .map_area .store .list").animate({"margin-top": -(storeHeight*(storeidx-1))},300,"easeInExpo");
		if((storeidx + showNum -1) == storesliderLength){
			$(".map_area .store .btn_updown .down").addClass("disable");
		}
		$(".map_area .store .btn_updown .up").removeClass("disable")
	});
	/*매거진 썸네일 클릭 시 큰 이미지 변환*/
	var magazinIdx =1;
	var magazinLength = $(".magazin_list .listwrap li").length;
	var pageSize = 7;
	var magazinWidth = 142;
	var maxIndex = pageSize;
	var magazinImgTitle ="";
	var magazinImgUrl ="";
	var magazinMvUrl ="";
	$(".magazin_view .moviearea .movie iframe").attr("src", "");
	$(".magazin_list .listwrap li i").click(function(e){
		$(".magazin_view .moviearea .movie iframe").attr("src", "");
		if( $(this).parent("li").hasClass("movie") ){
			$(".magazin_view .bigimg ").hide();
			$(".magazin_view .moviearea ").show();
			$(".magazin_list .listwrap li").removeClass("cnt");
			$(this).parents("li").addClass("cnt");
			magazinMvUrl = $(this).parent().find("img").data("pic");
			$(".viewarea .moviearea iframe").attr("src", magazinMvUrl);
		}else{
			$(".magazin_view .bigimg ").show();
			$(".magazin_view .moviearea ").hide();
			$(".magazin_list .listwrap li").removeClass("cnt");
			$(this).parents("li").addClass("cnt");
			magazinImgUrl = $(this).parent().find("img").data("pic");
			$(".viewarea .bigimg img").attr("src", magazinImgUrl);
		}
		magazinImgTitle = $(this).parent().find("img").data("title");
		$(".viewarea .magazin_tit").html(magazinImgTitle);
		magazinIdx = $(this).parents("li").index()+1;
	});


	$(".magazin_view .btn_area .prev").click(function(e){
		e.preventDefault();
		if(magazinIdx==1){
			return;
		}
		magazinIdx--;
		$(".magazin_list .listwrap li:eq('"+(magazinIdx-1)+"') i").click();
		if((maxIndex - pageSize) == magazinIdx ){
			maxIndex --;
			$(".magazin_list .listwrap ul").animate({"margin-left":-((maxIndex-pageSize)*magazinWidth)});
		}
	});
	$(".magazin_view .btn_area .next").click(function(e){
		e.preventDefault();
		if(magazinIdx==magazinLength){
			return;
		}
		magazinIdx++;
		$(".magazin_list .listwrap li:eq('"+(magazinIdx-1)+"') i").click();
		if(magazinIdx > pageSize &&  magazinIdx == (maxIndex + 1)){
			$(".magazin_list .listwrap ul").animate({"margin-left":-((magazinIdx-pageSize)*magazinWidth)});
			maxIndex ++;
		}
	});

	if( $("*").is(".product_list li .chkcon") ){
		$(".product_list li .chkcon.chkcon_on").each(function(){
				if( !($(this).parents("li").hasClass("on")) )
					$(this).parents("li").addClass("on");
		});
		$(".product_list li .chkcon input[type='checkbox']").on("change", function(e){
			e.preventDefault();
			$(this).parents("li").toggleClass("on");
		});
	}

	/*h2_area menu on/off*/
	$(".h2_area h2").toggle(function(){
		$(this).addClass("on");
		$(".h2_area .mypage_menu, .h2_area .smenu").show();
	}, function(){
		$(this).removeClass("on");
		$(".h2_area .mypage_menu, .h2_area .smenu").hide();
	});

	$(".skinsetting_pop li").click(function(e){
		e.preventDefault();
		$(".skinsetting_pop li").removeClass("cnt");
		$(this).addClass("cnt")
	});

	/*소팅cnt*/
	$(".termcheck .termlist li").click(function(e){
		e.preventDefault();
		$(".termcheck .termlist li").removeClass("cnt");
		$(this).addClass("cnt");
	});
	/*마이페이지 1:1문의*/
	$(".myquestion_list tr.qna_subj td").click(function(e){
		e.preventDefault();
		if ( 'none' == $(this).parent().next().find(".qna_area").css('display') ) {
			$(".myquestion_list tr.qna_detail td .qna_area").slideUp(300);
			$(this).parent().next().find(".qna_area").slideDown(300);
			$(".myquestion_list tr.qna_subj td.subj").removeClass("cnt");
			$(this).parent().find(".subj").addClass("cnt")
		}
		else {
			$(".myquestion_list tr.qna_detail td .qna_area").slideUp(300);
			$(".myquestion_list tr.qna_subj td.subj").removeClass("cnt");
		}
	});
	$(".myquestion_list tr.qna_detail td .qna_area .btn_qnaclose").click(function(e){
		e.preventDefault();
		$(this).parent().parent(".qna_area").slideUp(300);
		$(".myquestion_list tr.qna_subj td.subj").removeClass("cnt");
	});

});
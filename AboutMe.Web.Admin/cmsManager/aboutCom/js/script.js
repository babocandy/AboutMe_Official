$(function(){

	/* 왼쪽 사이드바 메뉴 열고 닫기 */
	$(".sidebar .menutoggle .btnopen").click(function(){
		$(".nav-sidebar .depth2").slideDown("fast");
	});
	$(".sidebar .menutoggle .btnclose").click(function(){
		$(".nav-sidebar .depth2").slideUp("fast");
	});
	$(".nav-sidebar.depth1 .menu").on("click" , function(){
		var subMenu = $(this).parent().find(".depth2");
		$(this).toggleClass("menuhide");
		if($(this).hasClass("menuhide")){
			$(subMenu).slideUp("fast");
		}else{
			$(subMenu).slideDown("fast");
		}
	});

	/*조회필드 추가 */
	$(".addField").change(function(){
		if ($(this).is(":checked")){
			var addFiledTxt = $(this).parent().find(".txt").text();
			$(".addtable th:last").after("<th>"+addFiledTxt+"</th>")
			$(".addtable td:last").after("<td></td>")
		}
		else{
			var addFiledTxt = $(this).parent().find(".txt").text(); 
			$(".addtable th").each(function(){
				 if($(this).text() == addFiledTxt){
					var idx = $(this).index();
					$(".addtable th:eq("+idx+")").remove();
					$(".addtable td:eq("+idx+")").remove();
				 } 
			})
		}
	}); 
});
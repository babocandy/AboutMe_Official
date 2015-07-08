// 새창 센터띄우기 scroll no
function openScrollNo(xurl, tar, wid, hei){
   var winl = (screen.width - wid) / 2;
   var wint = (screen.height - hei) / 2;
   set = 'width='+wid+',height='+hei+',top='+wint+',left='+winl+', toolbar=no,location=no,directory=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no';
   win = window.open(xurl,tar,set);
};
// 새창 센터띄우기 scroll yes
function openScrollYes(xurl, tar, wid, hei){
   var winl = (screen.width - wid) / 2;
   var wint = (screen.height - hei) / 2;
   set = 'width='+wid+',height='+hei+',top='+wint+',left='+winl+', toolbar=no,location=no,directory=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no';
   win = window.open(xurl,tar,set);
};

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
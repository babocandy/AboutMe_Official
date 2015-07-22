<%@ CodePage = "65001" %>
<%Response.Charset = "UTF-8"%>
<%
'**************************************************************************
' 파일명 : hs_cnfrm_popup1.aspx
'
' 본인확인서비스 요청 정보 입력 화면
'
'**************************************************************************
%>
<html>
<head>
<title>KCB 본인확인서비스 샘플</title>
	<meta charset="UTF-8">

<script>
<!--
	function jsSubmit(){	
		var form1 = document.form1;
		window.open("", "auth_popup", "width=430,height=590,scrollbar=yes");

		var form1 = document.form1;
		form1.target = "auth_popup";
		form1.submit();
	}
//-->
</script>
</head>
 <body>
 한글 테ㅛㅡ트 -----
	<form name="form1" action="hs_cnfrm_popup2.aspx" method="post">
		<table>
			<tr>
				<td colspan="2" align="center"><input type="button" value="본인확인" onClick="jsSubmit();"></td>
			</tr>
		</table>
	</form>

	<!-- 본인확인 처리결과 정보 -->
	<form name="kcbResultForm" method="post" >
        <input type="hidden" name="mem_id" 					value="" 	/>
        <input type="hidden" name="tx_seqno"			 	value=""	/>
        <input type="hidden" name="result_cd" 				value="" 	/>
        <input type="hidden" name="result_msg" 				value="" 	/>
        <input type="hidden" name="cert_dt_tm" 				value="" 	/>
        <input type="hidden" name="di" 						value="" 	/>
        <input type="hidden" name="ci" 						value="" 	/>
        <input type="hidden" name="name" 					value="" 	/>
        <input type="hidden" name="birthday" 				value="" 	/>
        <input type="hidden" name="gender" 					value="" 	/>
        <input type="hidden" name="nation" 					value="" 	/>
        <input type="hidden" name="tel_com_cd" 				value="" 	/>
        <input type="hidden" name="tel_no" 					value="" 	/>
	</form>  
<a href="http://tsafe.ok-name.co.kr:29080/oknm/sample/cert_rdi.jsp" onclick="window.open(this.href, 'auth_popup', 'width=430,height=590,scrollbar=yes'); return false;">본인확인서비스바로가기</a>
 </body>
</html>

<%@ Page Language="C#" %>
<%@ Import namespace="OkNameComLib" %>
<%
	// KCB 테스트서버를 호출할 경우
	String idpUrl    = "https://tmpin.ok-name.co.kr:5443/tis/ti/POTI90B_SendCertInfo.jsp";
	// KCB 운영서버를 호출할 경우
	//String idpUrl    = "https://ipin.ok-name.co.kr/tis/ti/POTI90B_SendCertInfo.jsp";

	// 아이핀 인증을 마치고 돌아올 페이지 주소. opener(ipin1.aspx)의 도메일과 일치하도록 설정해야 함.
	// (http://www.test.co.kr과 http://test.co.kr는 다른 도메인으로 인식하며, http 및 https도 일치해야 함)
	//String returnUrl = "http://"+Request.Url.Host+"/realname/ipin/ipin3.aspx";  // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<
	String returnUrl = "http://aboutme-dev.cstone.co.kr/realname/ipin/ipin3.aspx";  // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	String idpCode   = "V";//웹사이트 선호본인확인기관(KCB기관)코드
	String memId    = "P19960000000";//회원사코드 (테스트인 경우 'P00000000000'를 사용하며 운영시 발급받은 회원사코드를 설정)

	// 파라미터 정의

	// 암호화키 파일 설정 (절대경로) - 파일은 주어진 파일명으로 자동 생성되며 매월마다 갱신됨. 
	// 웹서버에 해당파일을 생성할 권한 필요.
	// 키파일경로는 웹루트(www 또는 html 등)하위로 설정하면 보안상 위험하므로 웹루트 이외의 경로로 설정!!
    String keyPath = "c:\\WWW\\REALNAME\\module\\key\\okname.key";

	String reserved1 = "0";
	String reserved2 = "0";

	// 웹서비스 EndPointURL
	//String endPointURL = "http://twww.ok-name.co.kr:8888/KcbWebService/OkNameService";	// 테스트서버 <<<<<<<<<<<<<<<<<<<<
	String endPointURL = "http://www.ok-name.co.kr/KcbWebService/OkNameService";// 운영서버 <<<<<<<<<<<<<<<<<<<<

	// 로그 경로 지정 및 권한 부여 (절대경로)
	// 로그파일경로는 웹루트(www 또는 html 등)하위로 설정하면 보안상 위험하므로 웹루트 이외의 경로로 설정!!
    String logPath = "c:\\WWW\\REALNAME\\module\\log";

	// 옵션값에 'L'을 추가하는 경우에만 로그가 생성됨. 예) options="CL"
	String options = "C";

	object [] args = new object[7];
	args[0] = keyPath;
	args[1] = memId;
	args[2] = reserved1;
	args[3] = reserved2;
	args[4] = endPointURL;
	args[5] = logPath;
	args[6] = options;

	int ret = 0;
	object output = new object();

	String retcode = "";
    String pubkey = "";
    String sig = "";
    String curtime = "";

    // 실행
    try {
		OkName okname = new OkName();

		// 모듈을 호출하고 실행결과를 가져옵니다.
		ret = (int)okname.callOkName(args, ref output);
	  
		//**************************************************************************
		//okname 응답 정보
		//**************************************************************************
		Response.Write ("ret="+ret+"<br>");
		Response.Write ("output="+output+"<br>");
		if (ret == 0) {
			string[] result = ((String)output).Split(new string[] {"\n"}, StringSplitOptions.None);
		    pubkey =  result[0];
		    sig  =  result[1];
		    curtime = result[2];
		}
		else {
			if (ret <=200 ) 
				retcode = String.Format("B{0:D3}", ret);
			else
				retcode = String.Format("S{0:D3}", ret);
		}
    }
    catch (Exception e) {
        Response.Write(e.ToString() + "<br>");
    }
%>
<html>
<head>
<script language="JavaScript">
//<!--
function certKCBIpin(){
	document.kcbInForm.target = "_self";

	//KCB 테스트서버를 호출할 경우
	//document.kcbInForm.action = "https://tmpin.ok-name.co.kr:5443/tis/ti/POTI01A_LoginRP.jsp";  //<<<<<<<<<<<<<<<<<<<<

	//KCB 운영서버를 호출할 경우
	document.kcbInForm.action = "https://ipin.ok-name.co.kr/tis/ti/POTI01A_LoginRP.jsp"; //<<<<<<<<<<<<<<<<<<<<

	document.kcbInForm.submit();
	return	;
}
//-->
</script>
</head>
<body onload="javascript:certKCBIpin();">
	<form name="kcbInForm" method="post" >
		<input type="hidden" name="IDPCODE" value="<%=idpCode%>" />
		<input type="hidden" name="IDPURL" value="<%=idpUrl%>" />
		<input type="hidden" name="CPCODE" value="<%=memId%>" />	
		<input type="hidden" name="CPREQUESTNUM" value="<%=curtime%>" />
		<input type="hidden" name="RETURNURL" value="<%=returnUrl%>" />
		<input type="hidden" name="WEBPUBKEY" value="<%=pubkey%>" />
		<input type="hidden" name="WEBSIGNATURE" value="<%=sig%>" />
	</form>	
</body>
</html>

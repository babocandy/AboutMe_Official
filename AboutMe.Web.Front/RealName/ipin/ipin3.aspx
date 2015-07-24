<%@ Page Language="C#" %>
<%@ Import namespace="OkNameComLib" %>

<%@ Import Namespace = "System" %>
<%@ Import Namespace = "System.Configuration" %>
<%
string REAL_NAME_memId = ConfigurationSettings.AppSettings["REAL_NAME_memId"]; //회원사코드
string REAL_NAME_serverIp = ConfigurationSettings.AppSettings["REAL_NAME_serverIp"]; //PrivateIP
string REAL_NAME_siteDomain = ConfigurationSettings.AppSettings["REAL_NAME_siteDomain"]; //회원사 도메인
string REAL_NAME_logPath = ConfigurationSettings.AppSettings["REAL_NAME_logPath"]; //로그파일 생성 경로
string REAL_NAME_keyPath = ConfigurationSettings.AppSettings["REAL_NAME_keyPath"]; //키파일 경로

//string REAL_NAME_returnUrl_HP = ConfigurationSettings.AppSettings["REAL_NAME_returnUrl_HP"]; //HP:본인인증 완료후 리턴될 URL (도메인 포함 full path)
string REAL_NAME_returnUrl_IPIN = ConfigurationSettings.AppSettings["REAL_NAME_returnUrl_IPIN"]; //IPIN:본인인증 완료후 리턴될 URL (도메인 포함 full path)

string REAL_NAME_SubmitLocalURL = ConfigurationSettings.AppSettings["REAL_NAME_SubmitLocalURL"]; //실명인증완료후 submit 경로 : 실제 - HTTPS 적용필요
    
%>


<%
	//아이핀팝업에서 조회한 PERSONALINFO이다.
	String encPsnlInfo = Request.Form.Get("encPsnlInfo");
	
	//KCB서버 공개키
	String WEBPUBKEY = (Request.Form.Get("WEBPUBKEY"));
	//KCB서버 서명값
	String WEBSIGNATURE = (Request.Form.Get("WEBSIGNATURE"));
  
  
	//아이핀 서버와 통신을 위한 키파일 생성
	// 파라미터 정의

    // 암호화키 파일 설정 (ipin2.aspx에서 설정된 값과 동일)
	// 키파일경로는 웹루트(www 또는 html 등)하위로 설정하면 보안상 위험하므로 웹루트 이외의 경로로 설정!!
    String keyPath = REAL_NAME_keyPath; // "c:\\WWW\\REALNAME\\module\\key\\okname.key";

	// 회원사코드 (테스트인 경우 'P00000000000'를 사용하며 운영시 발급받은 회원사코드를 설정)
    String memId = REAL_NAME_memId; // "P19960000000";

	// 웹서비스 EndPointURL
	//String endPointURL = "http://twww.ok-name.co.kr:8888/KcbWebService/OkNameService";	// 테스트서버 <<<<<<<<<<<<<<<<<<<<
	String endPointURL = "http://www.ok-name.co.kr/KcbWebService/OkNameService";	// 운영서버 <<<<<<<<<<<<<<<<<<<<

    // 로그 경로 지정 및 권한 부여 (ipin1.asp에서 설정된 값과 동일)
	// 로그파일경로는 웹루트(www 또는 html 등)하위로 설정하면 보안상 위험하므로 웹루트 이외의 경로로 설정!!
    String logPath = REAL_NAME_logPath; // "c:\\okname\\log";

	// 옵션값에 'L'을 추가하는 경우에만 로그가 생성됨. 예) options="SL"
	String options = "SL";
	
	object [] args = new object[8]; //jsh 7->8
	args[0] = keyPath;
	args[1] = memId;
	args[2] = endPointURL;
	args[3] = WEBPUBKEY;
	args[4] = WEBSIGNATURE;
	args[5] = encPsnlInfo;
	args[6] = logPath;
	args[7] = options; //<<<<<<<<<  jsh 신규생성 why:메뉴얼

	int ret = 0;
	object output = new object();

	String[] result=null;
	//String[] result = new string[20];
	String retcode=null;
    // 실행
    try {
		OkName okname = new OkName();

		// 모듈을 호출하고 실행결과를 가져옵니다.
		ret = (int)okname.callOkName(args, ref output);
	  
		//**************************************************************************
		//okname 응답 정보
		//**************************************************************************
		//Response.Write ("ret="+ret+"<br>");
		//Response.Write ("output="+output+"<br>");
		if (ret == 0) {
			result = ((String)output).Split(new string[] {"\n"}, StringSplitOptions.None);
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
function fncOpenerSubmit() {
    opener.document.kcbOutForm_IPIN.encPsnlInfo.value = "<%=encPsnlInfo %>";
<%
	if (ret == 0) {
%>
    opener.document.kcbOutForm_IPIN.dupinfo.value = "<%=result[0] %>";	// dupInfo
    opener.document.kcbOutForm_IPIN.coinfo1.value = "<%=result[1] %>";	// coinfo1
    opener.document.kcbOutForm_IPIN.coinfo2.value = "<%=result[2] %>";	// coinfo2
    opener.document.kcbOutForm_IPIN.ciupdate.value = "<%=result[3] %>";	// ciupdate
    opener.document.kcbOutForm_IPIN.virtualno.value = "<%=result[4] %>";	// virtualNo
    opener.document.kcbOutForm_IPIN.cpcode.value = "<%=result[5] %>";	// memId
    opener.document.kcbOutForm_IPIN.realname.value = "<%=result[6] %>";	// realName
    opener.document.kcbOutForm_IPIN.cprequestnumber.value = "<%=result[7] %>";	// cpRequestNumber
    opener.document.kcbOutForm_IPIN.age.value = "<%=result[8] %>";	// age
    opener.document.kcbOutForm_IPIN.sex.value = "<%=result[9] %>";	// sex
    opener.document.kcbOutForm_IPIN.nationalinfo.value = "<%=result[10]%>";	// nationalInfo
    opener.document.kcbOutForm_IPIN.birthdate.value = "<%=result[11]%>";	// birthDate
    opener.document.kcbOutForm_IPIN.authinfo.value = "<%=result[12]%>";	// authInfo
<%
	}
%>

    //opener.document.kcbOutForm_IPIN.action = "/MemberShip/RealNameResult";
    //opener.document.kcbOutForm_IPIN.action = "http://aboutme-dev.cstone.co.kr/MemberShip/RealNameResult";  //실제-> https
    opener.document.kcbOutForm_IPIN.action = "<%=REAL_NAME_returnUrl_IPIN%>";  //실제-> https
    opener.document.kcbOutForm_IPIN.submit();
	self.close();
}
</script>
</head>
<body>
</body>
<%
 	if (ret == 0) {
		// 인증결과 복호화 성공
		Response.Write ("<script>alert('Success! 본인인증성공'); fncOpenerSubmit();</script>");
	}
	else {
		// 인증결과 복호화 실패
		Response.Write ("<script>alert('Eror! 인증결과복호화 실패 : "+ret+"'); self.close();</script>");
	}  
%>
</html>

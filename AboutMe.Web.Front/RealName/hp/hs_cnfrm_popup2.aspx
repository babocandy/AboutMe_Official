<%@ Page Language="C#" %>
<%@ Import namespace="OkNameComLib" %>
<%
    //'**************************************************************************
	//파일명 : hs_cnfrm_popup2.aspx
	//'
	//본인확인서비스 개인정보 암호화 및 인증페이지 호출 화면
    //'
    //※주의
    //	실제 운영시에는 
    //	response.write를 사용하여 화면에 보여지는 데이터를 
    //	삭제하여 주시기 바랍니다. 방문자에게 사이트데이터가 노출될 수 있습니다.
    //'**************************************************************************

    //'**************************************************************************
    //'okname 본인확인서비스 파라미터
    //'**************************************************************************
    String inTpBit = "0";										// 입력구분코드(고정값 '0' - KCB 팝업에서 인증정보 입력)외국인, 4:휴대폰정보)
    String name = "x";											// 성명 (고정값 'x' - KCB팝업에서 인증정보 입력)
    String birthday = "x";										// 생년월일 (고정값 'x' - KCB팝업에서 인증정보 입력)
    String gender = "x";										// 성별 (고정값 'x' - KCB팝업에서 인증정보 입력)
    String nation="x";											// 내외국인구분 (고정값 'x' - KCB팝업에서 인증정보 입력)
    String telCmmCd="x";										// 이동통신사코드 (고정값 'x' - KCB팝업에서 인증정보 입력)
    String mbphnNo="x";											// 휴대폰번호 (고정값 'x' - KCB팝업에서 인증정보 입력)


    String svcTxSeqno = DateTime.Now.ToString("yyyyMMddHHmmss");// 거래일련번호 (동일 번호가 두번 생성되지 않도록 함. 0-9,A-Z,a-z 범위 20자 이내 )


    // 예약 항목
    String rsv1 = "0";
    String rsv2 = "0";
    String rsv3 = "0";

    String rqstMsrCd	=	"10";	// 인증수단코드 2byte  (10:핸드폰)
    String rqstCausCd	=	"00";// 인증요청사유코드 2byte  (00:회원가입, 01:성인인증, 02:회원정보수정, 03:비밀번호찾기, 04:상품구매, 99:기타)

    //okname을 호출할 파라미터 정보(내부에서 암호화하여 리턴함. 팝업창에 미리 세팅하는 경우 암호화된 값을 팝업으로 전달할 수 있다.)


	//########################################################################
	//# KCB로부터 부여받은 회원사코드 설정 (12자리)
	//########################################################################
    String memId = "P19960000000";								// *** 회원사코드 ***

	//########################################################################
	//# 회원사 모듈설치서버 IP 및 회원사 도메인 설정
	//########################################################################
    String serverIp =  "172.31.13.229";  	 									// 회원사 IP, InetAddress.getLocalHost().getHostAddress() 로 얻어올 수 있음. 52.68.130.158  , 172.31.13.229
    //String siteDomain = "ec2-52-68-130-158.ap-northeast-1.compute.amazonaws.com";						// *** 회원사 도메인. (휴대폰인증번호 송신시 제휴사명에 노출) <<<<<<<<<<<<<<<<<<<<
    String siteDomain = "52.68.130.158";						// *** 회원사 도메인. (휴대폰인증번호 송신시 제휴사명에 노출) <<<<<<<<<<<<<<<<<<<<


    //'okname을 호출할 파라미터 정보(내부에서 암호화하여 리턴함. 팝업창에 미리 세팅하는 경우 암호화된 값을 팝업으로 전달할 수 있다.)
    String returnMsg = "x";

	//########################################################################
	//# 리턴 URL 설정
	//########################################################################
	//opener(hs_cnfrm_popup1.aspx)의 도메일과 일치하도록 설정해야 함. (http://www.test.co.kr과 http://test.co.kr는 다른 도메인으로 인식하며, http 및 https도 일치해야 함)
    //String returnUrl = "http://"+Request.Url.Host+"/realname/hp/hs_cnfrm_popup3.aspx"; // 본인인증 완료후 리턴될 URL (도메인 포함 full path) <<<<<<<<<<<<<<<<<<<<
    String returnUrl = "http://52.68.130.158/realname/hp/hs_cnfrm_popup3.aspx"; // 본인인증 완료후 리턴될 URL (도메인 포함 full path) <<<<<<<<<<<<<<<<<<<<

	//########################################################################
	//# 운영전환시 변경 필요
	//########################################################################
	//String endPointURL = "http://dsafe.ok-name.co.kr:48088/KcbWebService/OkNameService";	//dev 
	//String endPointURL = "http://tsafe.ok-name.co.kr:29080/KcbWebService/OkNameService";	 //test  <<<<<<<<<<<<<<<<<<<<
	String endPointURL = "http://safe.ok-name.co.kr/KcbWebService/OkNameService";	//real <<<<<<<<<<<<<<<<<<<<

	//########################################################################
	//# 로그 경로 지정 및 권한 부여 (절대경로)
	// # 로그파일경로는 웹루트(www 또는 html 등)하위로 설정하면 보안상 위험하므로 웹루트 이외의 경로로 설정!!
	//########################################################################
    String logPath = "c:\\WWWs\\okname380_aspx_com_win64\\module\\log";							//	' 로그파일 생성 경로

	//########################################################################
	// # 옵션값에 'L'을 추가하는 경우에만 로그가 생성됨. 예) options="QL"
	//########################################################################
    String options = "QL";	//	' Q:인증요청데이터 암호화

	object [] args = new object[21];
	args[0] = svcTxSeqno;
	args[1] = name;
	args[2] = birthday;
	args[3] = gender;
	args[4] = nation;
	args[5] = telCmmCd;
	args[6] = mbphnNo;
	args[7] = rsv1;
	args[8] = rsv2;
	args[9] = rsv3;
	args[10] = returnMsg;
	args[11] = returnUrl;
	args[12] = inTpBit;
	args[13] = rqstMsrCd;
	args[14] = rqstCausCd;
	args[15] = memId;
	args[16] = serverIp;
	args[17] = siteDomain;
	args[18] = endPointURL;
	args[19] = logPath;
	args[20] = options;

	int ret = 0;
	object output = new object();

    String retcode = "";
    String retmsg = "";
    String e_rqstData = "";

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
		    retcode =  result[0];
		    retmsg  =  result[1];
		    e_rqstData = result[2];
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

    //**************************************************************************
    //hscert3.aspx 실행 정보(SOAP호출에서는 사용하지 않고 POPUP시에만 사용함)
    //**************************************************************************

    String targetId = ""; //타겟ID

	//########################################################################
	//# 운영전환시 변경 필요
	//########################################################################
    //String commonSvlUrl = "http://dsafe.ok-name.co.kr:48088/CommonSvl"; //	' 개발 URL
    //String commonSvlUrl = "https://tsafe.ok-name.co.kr:2443/CommonSvl";	// 테스트 URL <<<<<<<<<<<<<<<<<<<<
    String commonSvlUrl = "https://safe.ok-name.co.kr/CommonSvl";		// 운영 URL <<<<<<<<<<<<<<<<<<<<
%>


<html>
	<head>
	<title>KCB 본인인증 샘플</title>
	<script>
		function openPop(){

		window.name = "<%=targetId%>";

		document.form1.action = "<%=commonSvlUrl%>";
		document.form1.method = "post";
		document.form1.submit();
	}
	</script>
	</head>

 <body>
	<form name="form1">
	<!-- POP-UP 요청 정보정보 -->
	<!--// 필수 항목 -->
	<input type="hidden" name="tc" value="kcb.oknm.online.safehscert.popup.cmd.P901_CertChoiceCmd">				<!-- 변경불가-->
	<input type="hidden" name="rqst_data"				value="<%=e_rqstData%>">		<!-- 요청데이터 -->
	<input type="hidden" name="target_id"				value="<%=targetId%>">				<!-- 타겟ID --> 
	<!-- 필수 항목 //-->	
	</form>
 </body>
<%
 	if (retcode == "B000") {
		//팝업요청
		Response.Write ("<script>openPop();</script>");
	}
	else {
		//요청 실패 창 닫음.
		Response.Write ("<script>alert('"+retcode+"'); self.close();</script>");
	}
%>
</html>


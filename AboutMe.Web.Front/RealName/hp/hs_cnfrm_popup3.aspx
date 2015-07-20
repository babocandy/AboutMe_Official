<%@ Page Language="C#" %>
<%@ Import namespace="OkNameComLib" %>
<%
    //**************************************************************************
	// 파일명 : hs_cnfrm_popup3.aspx
	//
	// 본인확인서비스 결과 화면(return url)
	// 암호화된 인증결과정보를 복호화한다.
	//**************************************************************************

	//**************************************************************************
	// 모듈 호출	; 데이터 검증 
	//**************************************************************************/
	//암호화된 인증결과.
	String encInfo = Request.Form.Get("encInfo");
	//KCB서버 공개키
	String WEBPUBKEY = (Request.Form.Get("WEBPUBKEY"));
	//KCB서버 서명값
	String WEBSIGNATURE = (Request.Form.Get("WEBSIGNATURE"));

	//########################################################################
	//# KCB로부터 부여받은 회원사코드 설정 (12자리)
	//########################################################################
    String memId = "P19960000000";								// *** 회원사코드 ***

	//########################################################################
	//# 운영전환시 변경 필요
	//########################################################################
	//String endPointURL = "http://dsafe.ok-name.co.kr:48088/KcbWebService/OkNameService";	// 개발 서버 
	//String endPointURL = "http://tsafe.ok-name.co.kr:29080/KcbWebService/OkNameService";	// 테스트 서버 <<<<<<<<<<<<<<<<<<<<
	String endPointURL = "http://safe.ok-name.co.kr/KcbWebService/OkNameService";	// 운영 서버 <<<<<<<<<<<<<<<<<<<<

	//########################################################################
	//# 암호화키 파일 설정 (절대경로) - 파일은 주어진 파일명으로 자동 생성되며 생성되지 않으면 S211오류가 발생됨
	//# 파일은 매월초에 갱신되며 만일 파일이 갱신되지 않으면 복화화데이터가 깨지는 현상이 발생됨.
	// # 키파일경로는 웹루트(www 또는 html 등)하위로 설정하면 보안상 위험하므로 웹루트 이외의 경로로 설정!!
	//########################################################################
	String keyPath = "c:\\WWW\\REALNAME\\module\\key\\okname.key";

	//########################################################################
	//# 로그 경로 지정 및 권한 부여 (hs_cnfrm_popup2.aspx에서 설정된 값과 동일하게 설정)
	// # 로그파일경로는 웹루트(www 또는 html 등)하위로 설정하면 보안상 위험하므로 웹루트 이외의 경로로 설정!!
	//########################################################################
    String logPath = "c:\\WWW\\REALNAME\\module\\log";

	//########################################################################
	// # 옵션값에 'L'을 추가하는 경우에만 로그가 생성됨. 예) options="SL"
	//########################################################################
	String options = "SL";		// S:인증결과복호화
		
	object [] args = new object[8];
    args[0] = keyPath;
    args[1] = memId;
    args[2] = endPointURL;
    args[3] = WEBPUBKEY;
    args[4] = WEBSIGNATURE;
    args[5] = encInfo;
    args[6] = logPath;
    args[7] = options;

	int ret = 0;
	object output = new object();

    String retcode = "";
	String [] result = null;
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
			retcode = result[0];
			//**************************************************************************
			// 복호화된 본인확인 결과 데이터
			// 개발시 확인 용도로 사용하며 운영시 주석 또는 삭제 처리 필요
			//**************************************************************************
            /*
			Response.Write("복호화 요청 호출 성공.<br/>");		 
			Response.Write("처리결과코드:"+result[0]+"<br/>");		 
			Response.Write("처리결과메시지:"+result[1]+"<br/>");		 
			Response.Write("거래일련번호:"+result[2]+"<br/>");		 
			Response.Write("인증일시:"+result[3]+"<br/>");		 
			Response.Write("DI:"+result[4]+"<br/>");		 
			Response.Write("CI:"+result[5]+"<br/>");		 
			Response.Write("PK:"+result[6]+"<br/>");		// (안심실명확인 가입회원사인 경우)
			Response.Write("성명:"+result[7]+"<br/>");		 
			Response.Write("생년월일:"+result[8]+"<br/>");		 
			Response.Write("성별:"+result[9]+"<br/>");		 
			Response.Write("내외국인구분:"+result[10]+"<br/>");	 
			Response.Write("통신사코드:"+result[11]+"<br/>");	 
			Response.Write("휴대폰번호:"+result[12]+"<br/>");	 
            */
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

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
	<title>KCB 본인확인</title>
    <script type="text/javascript" >
	function fncOpenerSubmit() {
	    opener.document.kcbResultForm_HP.mem_id.value = "<%=memId%>";
<%
	if (ret == 0) {
%>
	    opener.document.kcbResultForm_HP.result_cd.value = "<%=result[0]%>";
	    opener.document.kcbResultForm_HP.result_msg.value = "<%=result[1]%>";
	    opener.document.kcbResultForm_HP.tx_seqno.value = "<%=result[2]%>";
	    opener.document.kcbResultForm_HP.cert_dt_tm.value = "<%=result[3]%>";
	    opener.document.kcbResultForm_HP.di.value = "<%=result[4]%>";
	    opener.document.kcbResultForm_HP.ci.value = "<%=result[5]%>";
	    opener.document.kcbResultForm_HP.name.value = "<%=result[7]%>";
	    opener.document.kcbResultForm_HP.birthday.value = "<%=result[8]%>";
	    opener.document.kcbResultForm_HP.gender.value = "<%=result[9]%>";
	    opener.document.kcbResultForm_HP.nation.value = "<%=result[10]%>";
	    opener.document.kcbResultForm_HP.tel_com_cd.value = "<%=result[11]%>";
	    opener.document.kcbResultForm_HP.tel_no.value = "<%=result[12]%>";
<%
	}
%>

	    //opener.document.kcbResultForm_HP.action = "hs_cnfrm_popup4.aspx";
	    opener.document.kcbResultForm_HP.action = "/MemberShip/RealNameResult";
	    opener.document.kcbResultForm_HP.submit();
		
		self.close();
	}	
	</script>
	</head>
<body>
</body>
<%
 	if (ret == 0) {
		// 인증결과 복호화 성공
		// 인증결과를 확인하여 페이지분기등의 처리를 수행해야한다.
		if (retcode == "B000") {
			Response.Write ("<script>alert('Success! 본인인증성공'); fncOpenerSubmit();</script>");
		}
		else {
			Response.Write ("<script>alert('Error1! 본인인증실패 : "+retcode+"'); fncOpenerSubmit();</script>");
		}
	}
	else {
		// 인증결과 복호화 실패
		Response.Write ("<script>alert('Error2! 인증결과복호화 실패 : "+ret+"'); self.close();</script>");
	}  
%>
</html>

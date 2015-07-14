<%@ Page Language="C#" %>
<%
    //**************************************************************************
	// 파일명 : hs_cnfrm_popup4.aspx
	//
	// 본인확인서비스 결과 완료 화면
    //
	//**************************************************************************

	String memId = Request.Form.Get("mem_id");	// 고객사코드
	String txSeqno = Request.Form.Get("tx_seqno");	// 거래번호
	String resultCd = Request.Form.Get("result_cd");		// 결과코드
	String resultMsg = Request.Form.Get("result_msg");		// 결과메세지
	String certDtTm = Request.Form.Get("cert_dt_tm");		// 인증일시
	String di = Request.Form.Get("di");						// DI
	String ci = Request.Form.Get("ci");						// CI
	String name = Request.Form.Get("name");					// 성명
	String birthday = Request.Form.Get("birthday");			// 생년월일
	String gender = Request.Form.Get("gender");				// 성별
	String nation = Request.Form.Get("nation");				// 내외국인구분
	String telComCd = Request.Form.Get("tel_com_cd");		// 통신사코드
	String telNo = Request.Form.Get("tel_no");				// 휴대폰번호
%>
<html>
<head>
<title>KCB 본인확인서비스 샘플</title>
</head>
<body>
<h3>인증결과</h3>
<ul>
  <li>고객사코드	: <%=memId%> </li>
  <li>결과코드		: <%=resultCd%></li>
  <li>결과메세지	: <%=resultMsg%></li>
  <li>거래번호		: <%=txSeqno%> </li>
  <li>인증일시		: <%=certDtTm%> </li>
  <li>DI			: <%=di%> </li>
  <li>CI			: <%=ci%> </li>
  <li>성명			: <%=name%> </li>
  <li>생년월일		: <%=birthday%> </li>
  <li>성별			: <%=gender%> </li>
  <li>내외국인구분	: <%=nation%> </li>
  <li>통신사코드	: <%=telComCd%> </li>
  <li>휴대폰번호	: <%=telNo%> </li>
</ul>
</body>
</html>

<%@ Page Language="C#" %>
<html>
<body>
Enc PersonalInfo : <%=Request.Form.Get("encPsnlInfo")%><br>
dupInfo : <%=Request.Form.Get("dupinfo")%><br>
coinfo1 : <%=Request.Form.Get("coinfo1")%><br>
coinfo2 : <%=Request.Form.Get("coinfo2")%><br>
ciupdate : <%=Request.Form.Get("ciupdate")%><br>
virtualno : <%=Request.Form.Get("virtualno")%><br>
cpcode : <%=Request.Form.Get("cpcode")%><br>
realname : <%=Request.Form.Get("realname")%><br>
cpRequestNumber : <%=Request.Form.Get("cprequestnumber")%><br>
age : <%=Request.Form.Get("age")%><br>
sex : <%=Request.Form.Get("sex")%><br>
nationalityInfo : <%=Request.Form.Get("nationalinfo")%><br>
birthdate : <%=Request.Form.Get("birthdate")%><br>
authinfo : <%=Request.Form.Get("authinfo")%>
</body>
</html>

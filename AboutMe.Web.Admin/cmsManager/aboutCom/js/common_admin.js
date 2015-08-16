//Full Screen popup 
function fPopFull(argURL,argWIN_NAME)
{
var win_POP_obj ;	
var x=screen.availWidth; 
var y=screen.availHeight; 
var target = parseFloat(navigator.appVersion.substring(navigator.appVersion.indexOf('.')-1,navigator.appVersion.length));
if((navigator.appVersion.indexOf("Mac")!=-1) &&(navigator.userAgent.indexOf("MSIE")!=-1) &&(parseInt(navigator.appVersion)==4)) 
	{
	win_POP_obj = window.open(argURL,argWIN_NAME,'scrollbars=yes'); 
	if(win_POP_obj==null)
		alert("팝업차단 설정을 해제하신후 사용하십시오.");
	else
		win_POP_obj.focus();
	}
if (target >= 4){ 
        if (navigator.appName=="Netscape"){ 
			win_POP_obj=window.open(argURL,argWIN_NAME,'scrollbars=yes','width='+x+',height='+y+',top=0,left=0');
			if(win_POP_obj==null)
				alert("팝업차단 설정을 해제하신후 사용하십시오.");
			else
			{
				win_POP_obj.focus();
				win_POP_obj.moveTo(0,0); 
				win_POP_obj.resizeTo(x,y);
				win_POP_obj.focus();
			}
		} 
		if (navigator.appName=="Microsoft Internet Explorer") {
			win_POP_obj = window.open(argURL,argWIN_NAME,"fullscreen=yes"); 
			if(win_POP_obj==null)
				alert("팝업차단 설정을 해제하신후 사용하십시오.");
			else
				win_POP_obj.focus();
		}
  } 
 else 
	{
	win_POP_obj = window.open(argURL,argWIN_NAME,'scrollbars=yes'); 
	if(win_POP_obj==null)
		alert("팝업차단 설정을 해제하신후 사용하십시오.");
	else
		win_POP_obj.focus();
	}

	
 
} 


function fOpenerFocusnClose() 
{
 if(opener)
	{
    //alert("opener OK2222..");
	opener.focus();
	}
 else
	{
	 //alert("opener ERROR 2222..");
	}
 self.close();
}



// 새창
function ow_no(xurl, tar, wid, hei){ 
	var winl = (screen.width - wid) / 2;
	var wint = (screen.height - hei) / 2;
	set = 'width='+wid+',height='+hei+',top='+wint+',left='+winl+', toolbar=no,location=no,directory=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no'
	win = window.open(xurl,tar,set);
	win.focus()
}

function ow_yes(xurl, tar, wid, hei){
	var winl = (screen.width - wid) / 2;
	var wint = (screen.height - hei) / 2;
	var win_name ;
	set = 'width='+wid+',height='+hei+',top='+wint+',left='+winl+', toolbar=no,location=no,directory=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no'
	win_name = window.open(xurl,tar,set);
	win_name.focus()
}


//날짜 형식 체크 YYYY-MM-DD
function fISDate(argDATE)
{
	var t,m,d;
	var arrD = new Array();
	var limit_day;

	//길이: 10
	if(argDATE.length != 10)
	 return false;

	arrD =argDATE.split("-")

	if(arrD.length != 3)
	 return false;

	y=arrD[0];
	m=arrD[1];
	d=arrD[2];


	if(fISNumber(y) && fISNumber(m) && fISNumber(d))
	 ;
	else
	{
	 return false ; //숫자가 아님
	}

	switch(eval(m))
		{
		case 1: case 3: case 5: case 7: case 8: case 10: case 12: limit_day = 31; break;
		case 2:
			if((y-2008)%4 == 0) 
				limit_day = 29; 
			else 
				limit_day = 28;
			break;
		case 4: case 6: case 9: case 11 : limit_day = 30; break;
		default: return false; break;
		}
	if(eval(d) > limit_day)
		{return false ;}
	if(eval(d)<1)
		{return false ;}
	return true; //정상 날짜
}


//숫자(정수) 인지 판단 
function fISNumber(argNum)
{
	var num =parseInt(argNum) ; //정수 변환
	if(isNaN(num))  //숫자가 아님
	 {
	  return false;
	 }
	 return true ; //숫자임
}


function fISPattern_InValid(obj,argMODE,argNAME)
{
	var bRET = false;
	var deny_pattern_aA9 = /[^(a-zA-Z0-9)]/;
	var deny_pattern_a9 = /[^(a-z0-9)]/;
	var deny_pattern_9 = /[^(0-9)]/;
	var deny_pattern_aA = /[^(a-zA-Z)]/;

	var strVAL ="";
	var strMSG ="허용 문자열 지정 오류.";

	if(obj)
	{
	 strVAL = obj.value ;
	}
	else
	{
	 alert("존재하지 않는 Object입니다.");
	 return true ;
	}

	if(argMODE=="aA9")
		{
		bRET = deny_pattern_aA9.test(strVAL);
		strMSG ="영문 소문자,영문 대문자,숫자";
		}
	else if(argMODE=="a9")
		{
		bRET = deny_pattern_a9.test(strVAL);
		strMSG ="영문 소문자,숫자";
		}
	else if(argMODE=="9")
		{
		bRET = deny_pattern_9.test(strVAL);
		strMSG ="숫자";
		}
	else if(argMODE=="aA")
		{
		bRET = deny_pattern_aA.test(strVAL);
		strMSG ="영문 소문자,영문 대문자";
		}

	if(bRET)
	{
	  alert("[" + argNAME + "] 입력형식이 일치하지 않습니다.\n\n 사용가능한 문자열:"+ strMSG);
	  obj.value="";
	  obj.focus();
	}
	return bRET ; 
}

//문자열 안의 공백 지우기
function deleteSpace(str) { 
	var out = "";
	var common_i ;

	for (common_i = 0; common_i < str.length; common_i++) { 
		if (str.charAt(common_i) == " " ) { 
			continue;
		}
		out += str.charAt(common_i); 
	} 
	return out.length; 
}


/*
 * 메일확인 함수
 * @param sVal 메일주소 값
 */
function validMail(sVal) {
  var regExp = /^[0-9a-zA-Z]([-_\.0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2,3}$/i;
  try {
// 정규식 지원하는 경우
	return regExp.test(sVal);
  } catch (e) {
// 정규식 지원하지 않는 경우
	var tmpArray = new Array();
	var lCma, lStr, tmpStr;
	tmpArray = sVal.split("@");
	if (tmpArray.length != 2) return false;
// 이메일 처음, 끝 문자 제한
	for (var i = 0; i < tmpArray.length; i++) {
	  for (var j = 0; j < tmpArray[i].length; j++) {
		tmpStr = tmpArray[i].charCodeAt(j);
		if (tmpStr == 45 || tmpStr == 46 || tmpStr == 95 || (tmpStr >= 48 && tmpStr <= 57) || (tmpStr >= 65 && tmpStr <= 90) || (tmpStr >= 97 && tmpStr <= 122)) {
		  if (j == 0 && (tmpStr == 45 || tmpStr == 46 || tmpStr == 95)) return false;
		  if (j == tmpArray[i].length-1 && (tmpStr == 45 || tmpStr == 46 || tmpStr == 95)) return false;
		  continue;
		} else {
		  return false;
		}
	  }
	}
// 이메일 뒷자리수 제한
	lCma = tmpArray[1].lastIndexOf(".");
	if (lCma == -1 || lCma == 0) return false;
	lStr = tmpArray[1].substring(lCma+1).length;
	if (!(lStr > 0 && lStr <= 3)) return false;
	return true;
  }
}
 

//숫자만 입력 받게끔 하는 함수.
//<input type="text" name="TEL2_1" value="" maxlength="4" onKeydown="javascript:fOnlyNumber(this);"  id="htel" class="inputtype1" style="width:30px;" />  
function fOnlyNumber(obj)
{
	e = window.event; //윈도우의 event를 잡는것입니다.

	//입력 허용 키
	if( ( e.keyCode >=  48 && e.keyCode <=  57 ) ||   //숫자열 0 ~ 9 : 48 ~ 57
		( e.keyCode >=  96 && e.keyCode <= 105 ) ||   //키패드 0 ~ 9 : 96 ~ 105
		e.keyCode ==   8 ||    //BackSpace
		e.keyCode ==  46 ||    //Delete
			//e.keyCode == 110 ||    //소수점(.) : 문자키배열
			//e.keyCode == 190 ||    //소수점(.) : 키패드
			e.keyCode ==  37 ||  //좌 화살표
			e.keyCode ==  39 ||  //우 화살표
			e.keyCode ==  35 ||  //End 키
			e.keyCode ==  36 ||  //Home 키
			e.keyCode ==   9 ||  //Tab 키
			e.keyCode ==   17 || //Ctrl 키
			e.keyCode ==   86 || //v 키
			e.keyCode ==   67  //c 키
		) 
		return;
//		{
//		if(e.keyCode == 48 || e.keyCode == 96) 
//			{ //0을 눌렀을경우
//			if ( obj.value == "" || obj.value == '0' ) //아무것도 없거나 현재 값이 0일 경우에서 0을 눌렀을경우
//				e.returnValue=false; //-->입력되지않는다.
//			else //다른숫자뒤에오는 0은
//				return; //-->입력시킨다.
//			}
//		else //0이 아닌숫자
//			return; //-->입력시킨다.
//		}
	else //숫자가 아니면 넣을수 없다.
		{
		alert('숫자만 입력가능합니다');
		obj.value="";
		e.returnValue=false;
		}

}



// 입력글자수 제한
// <textarea name="hMEMO1" cols="70" rows="5" onKeyUp="javascript:return content_length_check(this,500);" ></textarea> 
function content_length_check(obj_Content, argMAX_SIZE) {

    if (obj_Content.value.length > argMAX_SIZE) {
        alert(argMAX_SIZE + "자 이내로 기술해 주세요.");
        obj_Content.value = obj_Content.value.substring(0, argMAX_SIZE);
    }

}


//ID 체계 유효성 검증 첫글자영문, 4~12 자 영문 숫자,
function fIsValidMemberID(argID) {
    var reg_exp = new RegExp("^[a-zA-Z][a-zA-Z0-9]{3,11}$", "g");
    var match = reg_exp.exec(argID);

    if (match == null || argID.length < 4 || argID.length > 12) {
        alert("아이디 형식이 잘못되었습니다. 첫글자가 영문이어야하며, 4~12자의 영문or 숫자로 구성되어야 합니다");
        return false;
    }
    return true;
}

//PW 체계 유효성 검증 : 첫글자영문, 5~12 자 영문 숫자,
function fIsValidMemberPWD(argPWD) {
    var reg_exp = new RegExp("^[a-zA-Z][a-zA-Z0-9]{4,11}$", "g");
    var match = reg_exp.exec(argPWD);

    if (match == null || argPWD.length < 5 || argPWD.length > 12) {
        alert("비밀번호 형식이 잘못되었습니다. 첫글자가 영문이어야하며, 5~12자의 영문or 숫자로 구성되어야 합니다");
        return false;
    }
    return true;
}

//전화번호 중간 및 나머지 부분 체크 3~4자리숫자
function fIsValidMemberTEL_PART(argTEL_PART, argNULL_PERMIT) {
    var reg_exp = new RegExp("^[0-9]{3,4}$", "g");
    var match = reg_exp.exec(argTEL_PART);
    if (argNULL_PERMIT == "Y") // 옵션 
    {
        if (argTEL_PART == "")
            return true;
    }
    //필수 or 값이 있음
    if (match == null || argTEL_PART.length < 3 || argTEL_PART.length > 4) {
        alert("휴대폰/전화번호 형식이 잘못되었습니다. 3~4자의 숫자로 구성되어야 합니다");
        return false;
    }
    return true;
}

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
		alert("�˾����� ������ �����Ͻ��� ����Ͻʽÿ�.");
	else
		win_POP_obj.focus();
	}
if (target >= 4){ 
        if (navigator.appName=="Netscape"){ 
			win_POP_obj=window.open(argURL,argWIN_NAME,'scrollbars=yes','width='+x+',height='+y+',top=0,left=0');
			if(win_POP_obj==null)
				alert("�˾����� ������ �����Ͻ��� ����Ͻʽÿ�.");
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
				alert("�˾����� ������ �����Ͻ��� ����Ͻʽÿ�.");
			else
				win_POP_obj.focus();
		}
  } 
 else 
	{
	win_POP_obj = window.open(argURL,argWIN_NAME,'scrollbars=yes'); 
	if(win_POP_obj==null)
		alert("�˾����� ������ �����Ͻ��� ����Ͻʽÿ�.");
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



// ��â
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


//��¥ ���� üũ YYYY-MM-DD
function fISDate(argDATE)
{
	var t,m,d;
	var arrD = new Array();
	var limit_day;

	//����: 10
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
	 return false ; //���ڰ� �ƴ�
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
	return true; //���� ��¥
}


//����(����) ���� �Ǵ� 
function fISNumber(argNum)
{
	var num =parseInt(argNum) ; //���� ��ȯ
	if(isNaN(num))  //���ڰ� �ƴ�
	 {
	  return false;
	 }
	 return true ; //������
}


function fISPattern_InValid(obj,argMODE,argNAME)
{
	var bRET = false;
	var deny_pattern_aA9 = /[^(a-zA-Z0-9)]/;
	var deny_pattern_a9 = /[^(a-z0-9)]/;
	var deny_pattern_9 = /[^(0-9)]/;
	var deny_pattern_aA = /[^(a-zA-Z)]/;

	var strVAL ="";
	var strMSG ="��� ���ڿ� ���� ����.";

	if(obj)
	{
	 strVAL = obj.value ;
	}
	else
	{
	 alert("�������� �ʴ� Object�Դϴ�.");
	 return true ;
	}

	if(argMODE=="aA9")
		{
		bRET = deny_pattern_aA9.test(strVAL);
		strMSG ="���� �ҹ���,���� �빮��,����";
		}
	else if(argMODE=="a9")
		{
		bRET = deny_pattern_a9.test(strVAL);
		strMSG ="���� �ҹ���,����";
		}
	else if(argMODE=="9")
		{
		bRET = deny_pattern_9.test(strVAL);
		strMSG ="����";
		}
	else if(argMODE=="aA")
		{
		bRET = deny_pattern_aA.test(strVAL);
		strMSG ="���� �ҹ���,���� �빮��";
		}

	if(bRET)
	{
	  alert("[" + argNAME + "] �Է������� ��ġ���� �ʽ��ϴ�.\n\n ��밡���� ���ڿ�:"+ strMSG);
	  obj.value="";
	  obj.focus();
	}
	return bRET ; 
}

//���ڿ� ���� ���� �����
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
 * ����Ȯ�� �Լ�
 * @param sVal �����ּ� ��
 */
function validMail(sVal) {
  var regExp = /^[0-9a-zA-Z]([-_\.0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2,3}$/i;
  try {
// ���Խ� �����ϴ� ���
	return regExp.test(sVal);
  } catch (e) {
// ���Խ� �������� �ʴ� ���
	var tmpArray = new Array();
	var lCma, lStr, tmpStr;
	tmpArray = sVal.split("@");
	if (tmpArray.length != 2) return false;
// �̸��� ó��, �� ���� ����
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
// �̸��� ���ڸ��� ����
	lCma = tmpArray[1].lastIndexOf(".");
	if (lCma == -1 || lCma == 0) return false;
	lStr = tmpArray[1].substring(lCma+1).length;
	if (!(lStr > 0 && lStr <= 3)) return false;
	return true;
  }
}
 

//���ڸ� �Է� �ްԲ� �ϴ� �Լ�.
//<input type="text" name="TEL2_1" value="" maxlength="4" onKeydown="javascript:fOnlyNumber(this);"  id="htel" class="inputtype1" style="width:30px;" />  
function fOnlyNumber(obj)
{
	e = window.event; //�������� event�� ��°��Դϴ�.

	//�Է� ��� Ű
	if( ( e.keyCode >=  48 && e.keyCode <=  57 ) ||   //���ڿ� 0 ~ 9 : 48 ~ 57
		( e.keyCode >=  96 && e.keyCode <= 105 ) ||   //Ű�е� 0 ~ 9 : 96 ~ 105
		e.keyCode ==   8 ||    //BackSpace
		e.keyCode ==  46 ||    //Delete
			//e.keyCode == 110 ||    //�Ҽ���(.) : ����Ű�迭
			//e.keyCode == 190 ||    //�Ҽ���(.) : Ű�е�
			e.keyCode ==  37 ||  //�� ȭ��ǥ
			e.keyCode ==  39 ||  //�� ȭ��ǥ
			e.keyCode ==  35 ||  //End Ű
			e.keyCode ==  36 ||  //Home Ű
			e.keyCode ==   9 ||  //Tab Ű
			e.keyCode ==   17 || //Ctrl Ű
			e.keyCode ==   86 || //v Ű
			e.keyCode ==   67  //c Ű
		) 
		return;
//		{
//		if(e.keyCode == 48 || e.keyCode == 96) 
//			{ //0�� ���������
//			if ( obj.value == "" || obj.value == '0' ) //�ƹ��͵� ���ų� ���� ���� 0�� ��쿡�� 0�� ���������
//				e.returnValue=false; //-->�Էµ����ʴ´�.
//			else //�ٸ����ڵڿ����� 0��
//				return; //-->�Է½�Ų��.
//			}
//		else //0�� �ƴѼ���
//			return; //-->�Է½�Ų��.
//		}
	else //���ڰ� �ƴϸ� ������ ����.
		{
		alert('���ڸ� �Է°����մϴ�');
		obj.value="";
		e.returnValue=false;
		}

}


// �Է±��ڼ� ����
// <textarea name="hMEMO1" cols="70" rows="5" onKeyUp="javascript:return content_length_check(this,500);" ></textarea> 
	function content_length_check(obj_Content,argMAX_SIZE)
	{
	
		  if(obj_Content.value.length  > argMAX_SIZE) 
			{
	        	alert( argMAX_SIZE  + "�� �̳��� ����� �ּ���." );
		  	  	obj_Content.value  = obj_Content.value.substring(0,argMAX_SIZE);
	   	 }

	}
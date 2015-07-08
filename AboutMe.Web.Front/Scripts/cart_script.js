//주문/장바구니/찜 처리모듈 --------------------------------------------
jQuery.extend({ stringify: function (a) { var c = typeof a; if (c != "object" || a === null) return c == "string" && (a = '"' + a + '"'), String(a); else { var d, b, f = [], e = a && a.constructor == Array; for (d in a) b = a[d], c = typeof b, a.hasOwnProperty(d) && (c == "string" ? b = '"' + b + '"' : c == "object" && b !== null && (b = jQuery.stringify(b)), f.push((e ? "" : '"' + d + '":') + String(b))); return (e ? "[" : "{") + String(f) + (e ? "]" : "}") } } });

/*
$.ajax({
    cache: false,
    type: "POST",
    url: cnt.data("href"),
    success: function (data) {
        cnt.empty().html(data);
    },
    complete: function (jqXHR, textStatus) {
        tool.removeClass("loading").addClass("loaded");
        if (_.isFunction(fn)) {
            fn.apply(this);
        }
    }
});
*/

function getHttpRequest(URL, METHOD, post_param) {
    var xmlhttp = null;
    // FF일 경우 window.XMLHttpRequest 객체가 존재한다.
    if (window.XMLHttpRequest) {
        // FF 로 객체선언
        xmlhttp = new XMLHttpRequest();
    } else {
        // IE 경우 객체선언
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    if (METHOD == "POST") {
        xmlhttp.open("POST", URL, false);
        xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xmlhttp.send(post_param);
    }
    else {
        xmlhttp.open("GET", URL, false);
        xmlhttp.send('');
    }

    xmlhttp.onreadystatechange = function () {

        // readyState 가 4 고 status 가 200 일 경우 올바르게 가져옴
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200 && xmlhttp.statusText == 'OK') {
            // responseText 에 값을 저장
            responseText = xmlhttp.responseText;
        }
    }
    return responseText = xmlhttp.responseText;
}

// 장바구니담기
//var arr = [ {"p_code":"1111111", "p_count":"1" }, {"p_code":"1111111", "p_count":"1" }];
//Cart_Input(arr);

function Cart_Input(arrayOfObjects) {

    var jobj = encodeURIComponent(jQuery.stringify(arrayOfObjects));

    var src_url = "/Cart/CartInput";
    var param = "data=" + jobj;
    var result = jQuery.parseJSON(getHttpRequest(src_url, 'POST', param));

    $.ajax({
        cache: false,
        type: "POST",
        url: src_url,
        success: function (data) {
            console.log(data);

            if (result.cart_count > -1) {
                $("#div_QUICK_CART_CNT").text(result.cart_count);
            }

            if (data.result == "true") {
                //  ow_no("/order/cart_confirm.asp",'cart_confirm', "416","270");
                alert("장바구니에 담았습니다.");
            }
            else {
                if (data.msg != "") {
                    alert(data.msg);
                }
            }
        },
        complete: function (jqXHR, textStatus) {
        }
    });


}
// 장바구니 개수
function Cart_Count() {
    var src_url = "/Cart/CartCount";

    var result = jQuery.parseJSON(getHttpRequest(src_url));

    if (result) {
        if (result.cart_count > -1) {
            $("#div_QUICK_CART_CNT").text(result.cart_count);
        }
    }

}

//찜하기 저장
function Wish_Input(group_pcode, display_gbn) {

    var src_url = "/order/wish_input.asp?GROUP_CODE=" + group_pcode + "&DISPLAY_GBN=" + display_gbn;

    var result = jQuery.parseJSON(getHttpRequest(src_url));
    if (result.wish_count > -1) {
        $("#div_QUICK_JJIM_CNT").text(result.wish_count);
    }

    if (result.result == "true") {
        //  ow_no("/order/wish_confirm.asp",'cart_confirm', "416","270");
        alert("찜하였습니다.");
    }
    else {
        if (result.msg != "") {
            alert(result.msg);
        }
    }
    return result.result;
}

//찜하기 카운트
function Wish_Count() {

    var src_url = "/order/wish_count.asp";

    var result = jQuery.parseJSON(getHttpRequest(src_url));
    if (result) {
        if (result.wish_count > -1) {
            $("#div_QUICK_JJIM_CNT").text(result.wish_count);
        }
    }
}

// 찜하기 토글
function Wish_Toggle(group_code, display_gbn, obj) {
    var result = false;

    if (obj) {
        if ($(obj).hasClass("btn_wishlist_on") != true) {
            result = Wish_Input(group_code, display_gbn);
            if (result == "true") {
                $(obj).addClass("btn_wishlist_on");
            }
        }
        else {
            result = Wish_Delete(group_code, display_gbn)
            if (result == "true") {
                $(obj).removeClass("btn_wishlist_on");
            }
        }
    }
}

// 찜하기 토글 Red
function Wish_Toggle_red(group_code, display_gbn, obj) {
    var result = false;

    if (obj) {
        if ($(obj).hasClass("btn_wishlist_on_red") != true) {
            result = Wish_Input(group_code, display_gbn);
            if (result == "true") {
                $(obj).addClass("btn_wishlist_on_red");
            }
        }
        else {
            result = Wish_Delete(group_code, display_gbn)
            if (result == "true") {
                $(obj).removeClass("btn_wishlist_on_red");
            }
        }
    }
}

//찜하기 삭제
function Wish_Delete(group_code, display_gbn) {

    var src_url = "/order/wish_delete.asp?P_CODE=" + group_code + "&DISPLAY_GBN=" + display_gbn;
    var result = jQuery.parseJSON(getHttpRequest(src_url));

    if (result.wish_count > -1) {
        $("#div_QUICK_JJIM_CNT").text(result.wish_count);
    }

    if (result.result == "true") {
        alert("찜을 삭제하였습니다.");
    }
    else {
        if (result.msg != "") {
            alert(result.msg);
        }
    }
    return result.result;
}


//바로구매 하기
function Now_Order_Input(arrayOfObjects) {
    var jobj = jQuery.stringify(arrayOfObjects);
    $("#quick_objdata").val(jobj);
    var f = $("#mybuyform");
    f.submit();

}


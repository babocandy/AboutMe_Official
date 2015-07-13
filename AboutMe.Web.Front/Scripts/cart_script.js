//주문/장바구니/찜 처리모듈 --------------------------------------------
jQuery.extend({ stringify: function (a) { var c = typeof a; if (c != "object" || a === null) return c == "string" && (a = '"' + a + '"'), String(a); else { var d, b, f = [], e = a && a.constructor == Array; for (d in a) b = a[d], c = typeof b, a.hasOwnProperty(d) && (c == "string" ? b = '"' + b + '"' : c == "object" && b !== null && (b = jQuery.stringify(b)), f.push((e ? "" : '"' + d + '":') + String(b))); return (e ? "[" : "{") + String(f) + (e ? "]" : "}") } } });

// 장바구니담기
//var arr = [ {"p_code":"1111111", "p_count":"1" }, {"p_code":"1111111", "p_count":"1" }];
//Cart_Input(arr);

function Cart_Input(arrayOfObjects) {

    var jobj = encodeURIComponent(jQuery.stringify(arrayOfObjects));

    var src_url = "/Cart/CartInput";
    var param = "data=" + jobj;
  
    $.ajax({
        cache: false,
        type: "POST",
        url: src_url,
        data: param,
        success: function (data) {
            if (data.cart_count > -1) {
                $("#div_QUICK_CART_CNT").text(data.cart_count);
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
    //var result = jQuery.parseJSON(getHttpRequest(src_url, 'POST', param));

    $.ajax({
        cache: false,
        type: "POST",
        url: src_url,
        data: "",
        success: function (data) {
            if (data.cart_count > -1) {
                $("#div_QUICK_CART_CNT").text(data.cart_count);
            }

        },
        complete: function (jqXHR, textStatus) {
        }
    });

}

//찜하기 저장
function Wish_Input(arrayOfObjects) {

    var jobj = encodeURIComponent(jQuery.stringify(arrayOfObjects));

    var src_url = "/Cart/WishInput";
    var param = "data=" + jobj;
    //var result = jQuery.parseJSON(getHttpRequest(src_url, 'POST', param));

    $.ajax({
        cache: false,
        type: "POST",
        url: src_url,
        data: param,
        success: function (data) {
            if (data.wish_count > -1) {
               // $("#div_QUICK_CART_CNT").text(data.wish_count);
            }

            if (data.result == "true") {
                alert("찜하였습니다.");
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

//찜하기 카운트
function Wish_Count() {
    var src_url = "/Cart/WishCount";
    var param = "";
    $.ajax({
        cache: false,
        type: "POST",
        url: src_url,
        data: param,
        success: function (data) {
            if (data.wish_count > -1) {
                // $("#div_QUICK_CART_CNT").text(data.wish_count);
            }
        },
        complete: function (jqXHR, textStatus) {
        }
    });
}

//찜하기 삭제
function Wish_Delete(group_code, display_gbn) {

    var jobj = encodeURIComponent(jQuery.stringify(arrayOfObjects));

    var src_url = "/Cart/WishDelete";
    var param = "data=" + jobj;

    $.ajax({
        cache: false,
        type: "POST",
        url: src_url,
        data: param,
        success: function (data) {
            if (data.wish_count > -1) {
                // $("#div_QUICK_CART_CNT").text(data.wish_count);
            }

            if (data.result == "true") {
                alert("찜을 삭제하였습니다.");
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


//바로구매 하기
function Now_Order_Input(arrayOfObjects) {
    var jobj = jQuery.stringify(arrayOfObjects);
    $("#quick_objdata").val(jobj);
    var f = $("#mybuyform");
    f.submit();
}


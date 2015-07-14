

//바로구매 하기
function Now_Order_Input(arrayOfObjects) {
    var jobj = jQuery.stringify(arrayOfObjects);
    $("#quick_objdata").val(jobj);
    var f = $("#mybuyform");
    f.submit();
}


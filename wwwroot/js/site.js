function openErrorModal(strMessage) {
    var myDiv = document.getElementById("MyModalErrorAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalError').modal('show');
}

function openSuccessModal(strMessage) {
    var myDiv = document.getElementById("MyModalSuccessAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalSuccess').modal('show');
}


 /*Runtime Calculate Total Price */
$("#GRUPrice, #GRQty").keyup(function () {
    var total = 0;
    var x = $("#GRUPrice").val();
    var y = $("#GRQty").val();

    var total = x * y;

    $("#GRTPrice").val(total);
});


$("#StoreGIssueDetails_0__GIDUPrice, #StoreGIssueDetails_0__GIDQty").keyup(function () {
    var total = 0;
    var x = $("#StoreGIssueDetails_0__GIDUPrice").val();
    var y = $("#StoreGIssueDetails_0__GIDQty").val();

    var total = x * y;

    $("#StoreGIssueDetails_0__GIDTPrice").val(total);
});

/*Item GIssue js*/
function DeleteItem(btn) {
    var table = document.getElementById('ExpTable');
    var rows = document.getElementsByTagName('tr');
    if (rows.length == 2) {
        alert("This Row Cannot be Deleted")
        return;
    }
    $(btn).closest('tr').remove();
}


function AddItem(btn) {
    var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');

    var rowOuterHtml = rows[rows.length - 1].outerHTML;

    var lastrowIdx = document.getElementById('hdnLastIndex').value;

    var nextrowIdx = eval(lastrowIdx) + 1;
    document.getElementById('hdnLastIndex').value = nextrowIdx;

    rowOuterHtml = rowOuterHtml.replaceAll("_" + lastrowIdx + "_", "_" + nextrowIdx + "_");
    rowOuterHtml = rowOuterHtml.replaceAll("[" + lastrowIdx + "]", "[" + nextrowIdx + "]");
    rowOuterHtml = rowOuterHtml.replaceAll("_" + lastrowIdx + "_" + nextrowIdx);



    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;


    var x = document.getElementsByTagName("INPUT");

    for (var cnt = 0; cnt < x.length; cnt++) {
        if (x[cnt].type == "text" && [cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
            x[cnt].value = '';

    }
}





//fetch('/StoreDClose/GetDateValue')
//    .then(response => response.json())
//    .then(data => {
//        // Do something with the data here, such as displaying it on the page
//        $("#GRDate").val(data);
//        alert(data);
//});
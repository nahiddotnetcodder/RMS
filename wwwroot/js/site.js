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
    console.log(total);

    $("#GRTPrice").val(total);
});

$("#GIUPrice, #GIQty").keyup(function () {
    var total = 0;
    var x = $("#GIUPrice").val();
    var y = $("#GIQty").val();

    var total = x * y;
    console.log(total);

    $("#GITPrice").val(total);
});


//get date
$.ajax({
    url: '/StoreDClose',
    type: 'GET',
    success: function (data) {
        $("#RDCDate").val(data);
    }
});



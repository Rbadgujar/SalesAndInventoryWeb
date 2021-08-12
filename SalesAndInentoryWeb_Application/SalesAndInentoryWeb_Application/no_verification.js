

window.onload = function () {
    render();
};
function render() {
    window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container');
    recaptchaVerifier.render();
}
function phoneAuth() {
    //get the number
    //var cc = '+91';
    //var number = document.getElementById('txtmobile_no').value;
    //var str,
    debugger
    var number = document.getElementById('number').value;
    //if (number != null) {
    //    str = number.value;
    //}
    //else {
    //    str = null;
    //}
    //phone number authentication function of firebase
    //it takes two parameter first one is number,,,second one is recaptcha
    firebase.auth().signInWithPhoneNumber(number, this.window.recaptchaVerifier).then(function (confirmationResult) {
        //s is in lowercase
        this.window.confirmationResult = confirmationResult;
        coderesult = confirmationResult;
        console.log(coderesult);
        alert("Message Sent")
    }).catch(function (error) {
        alert(error.message);
    });
    $(document).ready(function () {
        
        $("#btnlogin").click(function () {
            if (fruitId != "") {
                $.ajax
               ({
                   type: "POST",
                   url: "/MainLogin/GetFruitName1",
                   data: "id=" + fruitId,
                   success: function (data) {

                   }
               });
            }
        });

    });
}
function codeverify() {
    var code = document.getElementById('verificationCode').value;
    coderesult.confirm(code).then(function (result) {
       
       alert("Message Verified")
        var user = result.user;
        console.log(user);
    }).catch(function (error) {
        alert(error.message);
    });
    $(document).ready(function () {
        debugger
        $("#btnsendotp").click(function () {
            var fruitId = $("#number").val();
        });
    });
}

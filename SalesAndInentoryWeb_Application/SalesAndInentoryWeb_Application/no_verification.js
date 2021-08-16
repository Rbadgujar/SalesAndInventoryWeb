window.onload = function () {
    render();
};
function render() {
    window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container');
    recaptchaVerifier.render();
}
function phoneAuth() {
    debugger
    var a = document.getElementById('number').value;
    var b = "+91";
    var number=b+a;
  
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
        debugger
        $("#btnsendotp").click(function () {
            var fruitId
             number=fruitId;
        });
    });
    //$(document).ready(function () {
        
    //    $("#btnlogin").click(function () {
    //        if (fruitId != "") {
    //            $.ajax
    //           ({
    //               type: "POST",
    //               url: "/MainLogin/GetFruitName1",
    //               data: "id=" + fruitId,
    //               success: function (data) {

    //               }
    //           });
    //        }
    //    });

}
function codeverify() {
    debugger
    var code = document.getElementById('verificationCode').value;
    coderesult.confirm(code).then(function (result) {       
        alert("Message Verified");
        var user = result.user;
        console.log(user);
        window.location.href = "/StartPage/registration";    
    }).catch(function (error) {
        alert(error.message);
    });
   
}

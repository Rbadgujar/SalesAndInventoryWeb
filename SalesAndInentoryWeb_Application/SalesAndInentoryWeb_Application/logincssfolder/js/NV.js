
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
    var number = document.getElementById('mobile').value;
    //if (number != null) {
    //    str = number.value;
    //}
    //else {
    //    str = null;
    //}
    //phone number authentication function of firebase
    //it takes two parameter first one is number,,,second one is recaptcha
    firebase.auth().signInWithPhoneNumber(number, window.recaptchaVerifier).then(function (confirmationResult) {
        //s is in lowercase
        window.confirmationResult = confirmationResult;
        coderesult = confirmationResult;
        console.log(coderesult);
        alert("Message sent");
    }).catch(function (error) {
        alert(error.message);
    });
}
function codeverify() {
    var code = document.getElementById('otp').value;
    coderesult.confirm(code).then(function (result) {
        alert("Mobile Number Verified");
        var user = result.user;
        console.log(user);
    }).catch(function (error) {
        alert(error.message);
    });
}
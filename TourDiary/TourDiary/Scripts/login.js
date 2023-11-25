var div__loginLottie = bodymovin.loadAnimation({
    container: document.getElementById("div__loginLottie"),
    renderer: 'svg',
    loop: true,
    autoplay: true,
    path: 'https://lottie.host/4542aa51-055c-424b-9976-faddafd2c20a/MKUQPGFMca.json'
});

function validateLogin() {
    let username = document.getElementById("username");
    let password = document.getElementById("password");
    let btn__login = document.getElementById("submit");

    console.log(username.value.length + " || " + password.value.length);

    if (username.value.length == "" || password.value.length == "") {
        console.log("1");
        $(btn__login).prop("disabled", true);
    } else {
        console.log("2");
        $(btn__login).prop("disabled", false);
    }
}
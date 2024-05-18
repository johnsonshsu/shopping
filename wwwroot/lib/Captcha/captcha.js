/**-------------------------------------------------
 * Simple Captcha System
 * @package Code Snippets
 * @link http://rhythmshahriar.com/codes/
 * @author Rhythm Shahriar <rhy@rhythmshahriar.com>
 * @link http://rhythmshahriar.com
 * @copyright Copyright © 2017, Rhythm Shahriar
 * https://gist.github.com/RhythmShahriar/6bac24d4698fcd7e330d6da67198f4f5
 ---------------------------------------------------*/

//email verification
$('#email').on('change', function () {
    if (!validateEmail($(this).val())) {
        $('#errEmail').html('<span style="color: red;"><i class="ion-close"></i> Invalid email address.</span>');
        $(this).val('');
    } else {
        $('#errEmail').html('');
    }
});

//password verification
$('#cpwd').on('change', function () {
    if ($(this).val() != $('#pwd').val()) {
        $('#errPwd').html('<span style="color: red;"><i class="ion-close"></i> Password not matched.</span>');
        $(this).val('');
    } else {
        $('#errPwd').html('');
    }
});


//email validation
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

//allow only number input
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    return (charCode > 47 && charCode < 58 || charCode == 8 || charCode == 9 || charCode == 46 || charCode > 36 && charCode < 41);
}


//allow only number input
function isAlpha(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    return (charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || charCode == 8 || charCode == 9 || charCode == 46 || charCode > 36 && charCode < 41);
}

//generate captcha
function generateCaptcha(length, chars) {
    var result = '';
    for (var i = length; i > 0; --i) result += chars[Math.round(Math.random() * (chars.length - 1))];
    return result;
}

//default captcha
$('.dynamic-code').text(generateCaptcha(5, '0123456789'));

$('.captcha-reload').on('click', function () {
    $('.dynamic-code').text(generateCaptcha(5, '0123456789'));
});

//check captcha
$('#captcha-input').on('change', function () {
    if ($(this).val() != $('.dynamic-code').text()) {
        $('#errCaptcha').html('<span style="color: red;"><i class="ion-close"></i> 驗證碼不相符.</span>');
        $(this).val('');
    } else {
        $('#errCaptcha').html('');
    }
});
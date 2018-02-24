
$('#contact-form').submit(function () {

    // validate the form
    ValidateForm();

    return false;
});

// form validation
function ValidateForm() {
    // disable the submit button
    $('#contact-form-submit').attr('disabled', 'disabled');

    $('.loader-icon').show();

    var email = $('#email').val();
    var subject = $('#subject').val();
    var message = $('#message').val();

    // make sure the user filled in all the fields
    if (email.length > 0 && subject.length > 0 && message.length > 0) {
        if (isValidEmailAddress(email)) {
            SendMessage();
        } else {
            $('.loader-icon').hide();
            ShowStatusMessage('Not a valid e-mail address', 'error', 3000);
            $('#email').focus().select();
        }
    }
    else {
        $('.loader-icon').hide();
        ShowStatusMessage('You have to fill in all fields', 'error', 3000);
    }

    // re-enable the submit button
    $('#contact-form-submit').removeAttr('disabled');

    return false;
}

function SendMessage() {
    $.ajax({
        url: "/Email/SendMail",
        type: "POST",
        data: {
            email: $('#email').val(),
            subject: $('#subject').val(),
            message: $('#message').val(),
            timestamp: $('#timestamp').val(),
            cheater: $('#cheater').val()
        }
    }).done(function (data) {
        if (data.success) {
            // clear form values
            $('#email').val('');
            $('#subject').val('');
            $('#message').val('');

            $('.loader-icon').hide();

            // request was successful
            ShowStatusMessage('Your message was sent successfully!', 'notice', 6000);
        }
        else {
            $('.loader-icon').hide();
            ShowStatusMessage('An error occured.<br><br>Details:<br>' + data.errorMessage, 'error', 6000);
        }
    });
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/);
    return pattern.test(emailAddress);
};
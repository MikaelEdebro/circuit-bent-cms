
$(function () {
    InitComponents();


    $('.article-image').css('border', '1px solid red');
});


// initialize some components we're using on the site
function InitComponents() {

    // init TinyMCE with full menu bars
    tinymce.init({
        mode: "exact",
        elements: "Text, Content, ContactPageText, FooterText, Description",
        statusbar: false,
        relative_urls: false,
        remove_linebreaks: false,
        entity_encoding: "raw",
        content_css: "/Content/bootstrap/css/bootstrap.min.css,/Content/TinyMce.css?" + new Date().getTime(),
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code fullscreen",
            "insertdatetime table contextmenu paste media"
        ],
        toolbar: "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | insertfiles",
        setup: function (editor) {
            editor.addButton('insertfiles', {
                text: 'Insert image',
                icon: true,
                image: '/Images/icons/actions/file_(add)_16x16.gif',
                onclick: function () {
                    OpenInsertFiles();
                }
            });
        }
    });

    // init TinyMCE for image info
    tinymce.init({
        mode: "exact",
        elements: "ImageText",
        menubar: false,
        statusbar: false,
        relative_urls: false,
        remove_linebreaks: false,
        entity_encoding: "raw",
        content_css: "/Content/bootstrap/css/bootstrap.min.css,/Content/TinyMce.css?" + new Date().getTime(),
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code fullscreen",
            "insertdatetime table contextmenu paste media"
        ],
        toolbar: "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | link"
    });

    /**
     * this workaround makes magic happen
     * thanks @harry: http://stackoverflow.com/questions/18111582/tinymce-4-links-plugin-modal-in-not-editable
     */
    $(document).on('focusin', function (e) {
        if ($(event.target).closest(".mce-window").length) {
            e.stopImmediatePropagation();
        }
    });


    // init the datepicker
    $("input[type=datetime]").datepicker({
        dateFormat: "yy-mm-dd",
        firstDay: 1
    });


    // init fancybox to get lightbox behaviour on selected images
    $(".fancybox").fancybox();
}

function OpenInsertFiles() {
    var url = "/Admin/Files/InsertFile";
    var width = 1000;
    var height = window.innerHeight - 50;


    var left = parseInt((screen.availWidth / 2) - (width / 2));
    var top = parseInt((screen.availHeight / 2) - (height / 2));
    var windowFeatures = "width=" + width + ",height=" + height +
        ",status,resizable,left=" + left + ",top=" + top +
        "screenX=" + left + ",screenY=" + top + ",scrollbars=yes";

    window.open(url, "subWind", windowFeatures, "POS");
}

// displays a status message to the user
function ShowStatusMessage(message, type, duration) {
    $().toastmessage('showToast', {
        text: message,
        type: type,
        stayTime: duration,
        position: 'middle-center'
    });
}
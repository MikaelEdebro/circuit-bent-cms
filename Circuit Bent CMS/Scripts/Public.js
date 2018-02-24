$(window).load(function () {
    // format images added from the TinyMce
    $('.article-image').each(function () {
        // get variables from the image
        var imageAlt = $(this).attr('alt');
        var imageText = $(this).attr('data-image-text');
        var imageSrc = $(this).attr('src');
        var imageAlign = "" + $(this).attr('style');
        var imageClass = "image-default";
        var imageWidth = $(this).width();

        // find out if the image should be aligned
        if (imageAlign.indexOf("float: left") > -1) {
            imageClass = "image-left";
        }
        else if (imageAlign.indexOf("float: right") > -1) {
            imageClass = "image-right";
        }
        else if ((imageAlign.indexOf("margin-left: auto") > -1) || (imageAlign.indexOf("margin-right: auto") > -1)) {
            imageClass = "image-centered";
        }

        // apply a frame if the checkbox was checked
        if ($(this).hasClass('frame')) {
            imageClass += " thumbnail";
        }

        if (imageText.length > 0) {
            imageText = '<div class="caption">' + imageText + '</div>';
        }

        // code to be inserted
        var insertCode = '<div class="' + imageClass + '" style="max-width: ' + (imageWidth + 10) + 'px !important"><img src="' + imageSrc + '" alt="' + imageAlt + '" title="' + imageAlt + '">' + imageText + '</div>'

        $(this).replaceWith(insertCode);
    });

    // make sure the links in the image text opens in new tab
    $('.caption a').each(function () {
        $(this).attr('target', '_blank');
    });

});


$(function () {
    // show current menu object highlighted
    var url = window.location.pathname.toLowerCase();
    var checkString;

    // now grab every link from the navigation
    // also the links in the left column
    $('nav ul a, .column-left a').each(function () {
        // and test its href against the url pathname
        checkString = url.match($(this).attr('href').toLowerCase());
        if ((checkString != null && $(this).attr('href') != '/') || (url == $(this).attr('href'))) {
            $(this).addClass('current');
        }
    });

    // Find all YouTube & Vimeo videos
    var $allVideos = $("iframe[src*='vimeo.com'], iframe[src*='youtube.com'], iframe[src^='https://www.youtube-nocookie.com']"),

    // The element that is fluid width
    // if the .column-left exists, we are on the home page, otherwise use the .body wrapper
    $fluidEl = ($('.column-main').length > 0 ? $(".column-main") : $('.body'));

    // Figure out and save aspect ratio for each video
    $allVideos.each(function () {

        $(this)
            .data('aspectRatio', this.height / this.width)

            // and remove the hard coded width/height
            .removeAttr('height')
            .removeAttr('width');

    });

    // When the window is resized
    $(window).resize(function () {

        var newWidth = $fluidEl.width();

        // Resize all videos according to their own aspect ratio
        $allVideos.each(function () {

            var $el = $(this);
            $el
                .width(newWidth)
                .height(newWidth * $el.data('aspectRatio'));

        });

        // Kick off one resize to fix all videos on page load
    }).resize();


    // init image slider & fancybox
    $('.bxslider').bxSlider();
    $('.fancybox').fancybox();
});

// displays a status message to the user
function ShowStatusMessage(message, type, duration) {
    $().toastmessage('showToast', {
        text: message,
        type: type,
        stayTime: duration,
        position: 'middle-center'
    });
}
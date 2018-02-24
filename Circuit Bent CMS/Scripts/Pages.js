$(function () {
    InitSortable('/Admin/Pages/UpdateOrder');

    // expand or contract the sub pages list
    $('.expand').click(function () {
        var subPages = $(this).parents('ul').find('li[data-subpage-id=' + $(this).parents('li').attr('data-id') + ']');

        if (subPages.is(':visible')) {
            $(subPages).hide();
            $(this).find('i').removeClass('fa-chevron-circle-up').addClass('fa-chevron-circle-down');
        }
        else {
            $(subPages).show();
            $(this).find('i').removeClass('fa-chevron-circle-down').addClass('fa-chevron-circle-up');
        }

    });

    // handle clicks on the arrows
    $(document).delegate('.arrow-left:not(.disabled)', 'click', function () {
        SetAsMainPage($(this).parents('li').attr('data-id'));
    });
    $(document).delegate('.arrow-right:not(.disabled)', 'click', function () {
        SetAsSubPage($(this).parents('li').attr('data-id'));
    });


    // handle clicks for the "Set as homepage" icons
    $('.homepage-icon').each(function () {
        $(this).click(function () {
            SetAsHomepage($(this).attr('data-id'));
        });
    });

    // close popup layer
    $('.popup-close').click(function () {
        $('.popup-layer').hide();
    });
});


function InitSortable(ajaxUrl) {
    // enable the sortable plugin
    $(".sortable").sortable({
        cursor: "move",
        delay: 150,
        create: function (event, ui) {
            $('.sub-page').hide();
        },
        start: function (event, ui) {
            // make sure that the sub pages are moved as well
            ui.item.siblings('li[data-subpage-id=' + $(ui.item).attr('data-id') + ']').appendTo(ui.item);

            // hide the sub pages
            // otherwise it gets messy when sorting
            if ($(ui.item).attr('data-subpage-id') == 0) {
                $('.sub-page').hide();
                $(ui.item).find('.expand i').removeClass('fa-chevron-circle-up').addClass('fa-chevron-circle-down');
            }
            
            
        },
        // this event runs when you drop a moved object
        stop: function (event, ui) {
            // make sure that the sub pages are moved as well
            ui.item.after(ui.item.find('li[data-subpage-id=' + $(ui.item).attr('data-id') + ']'));

            // store all the id's in an array
            var orderById = [];
            // store the subPageId to be able to only send along the items that belong to the correct main page
            var subPageId = $(ui.item).attr('data-subpage-id');

            // find the list items that have the same subPageId as the dragged item
            $(ui.item).parent('.sortable').find('li[data-subpage-id=' + subPageId + ']').each(function () {
                orderById.push($(this).attr('data-id'));
            });

            // make the array to a normal comma separated string
            var orderByIdString = orderById + "";

            // send ajax calls to update order
            $.ajax({
                url: ajaxUrl,
                type: 'POST',
                data: {
                    arrId: orderByIdString
                }
            }).done(function (data) {
                // this function runs when the ajax call is done

            });
        }
    }).disableSelection();
}


function SetAsHomepage(id) {
    $.ajax({
        url: "/Admin/Pages/SetAsHomepage",
        type: "POST",
        data: {
            id: id
        }
    }).done(function (result) {
        if (result.success) {
            // remove the class that dims the icon
            $('i[data-id]').addClass('set-homepage');

            // apply the same class on the current home page
            $('i[data-id=' + id + ']').removeClass('set-homepage');

            //display message
            ShowStatusMessage(result.message, 'notice', 2000)
        }
        else {
            //display message
            ShowStatusMessage(result.message, 'error', 5000)
        }
    });
}

// change the page to a main page
function SetAsMainPage(id) {
    var item = $('li[data-id=' + id + ']');
    var mainPage = $(item).parents('.sortable').find('li[data-id=' + $(item).attr('data-subpage-id') + ']');
    
    
    $.ajax({
        url: '/Admin/Pages/SetAsMainPage',
        type: 'POST',
        data: {
            id: id
        }
    }).done(function (data) {
        if (data.success) {
            
            // show the "Set as homepage" icon
            $(item).find('.homepage-icon').removeClass('hide').addClass('set-homepage');

            // remove the sub page class (with margin etc)
            $(item).removeClass('sub-page');

            // change the url shown for the page
            $(item).find('.url').html('http://' + document.location.hostname + '/' + $(item).attr('data-slug'));

            // make the arrows point in the correct way
            $(item).find('.arrow-left').addClass('disabled');
            $(item).find('.arrow-right').removeClass('disabled');

            // remove the related subpage id
            $(item).attr('data-subpage-id', 0);

            // add item to the main pages list
            // this also removes the item from it's original location
            $('.sortable').append(item);

            // get the subpages
            var subPages = $('li[data-subpage-id=' + $(mainPage).attr('data-id') + ']');

            
            // hide the expand button if there are no longer any sub pages related to that page
            if ($(subPages).length < 1) {
                $(mainPage).find('.expand').addClass('hide');
                $(mainPage).find('.arrow-right').removeClass('disabled');
            }
        }
    });
}

// change the page to a sub page
function SetAsSubPage(id) {
    var item = $('li[data-id=' + id + ']');
    var previousItem = $(item).prevAll('li[data-subpage-id=0]').first();    

    $.ajax({
        url: '/Admin/Pages/SetAsSubPage',
        type: 'POST',
        data: {
            id: id,
            subPageId: $(previousItem).attr('data-id')
        }
    }).done(function (data) {
        if (data.success) {
            
            // hide the "Set as homepage" icon
            $(item).find('.homepage-icon').addClass('hide');

            // add sub page class
            $(item).addClass('sub-page');

            // change the url shown for the page
            $(item).find('.url').html('http://' + document.location.hostname + '/' + $(previousItem).attr('data-slug') + '/' + $(item).attr('data-slug'));

            // make the arrows point in the correct way
            $(item).find('.arrow-left').removeClass('disabled');
            $(item).find('.arrow-right').addClass('disabled');

            // change the data-subpage to indicate that it is a sub page
            $(item).attr('data-subpage-id', $(previousItem).attr('data-id'));
            
            // show the expand icon
            $(previousItem).find('.expand').removeClass('hide');
            $(previousItem).find('.expand img').attr('src', '/Images/icons/actions/collapse.png');
            
            var subPages = $('li[data-subpage-id=' + $(previousItem).attr('data-id') + ']');
            $(subPages).after(item);

            // disable the right arrow when you add sub pages to it
            if ($(subPages).length > 0) {
                $(previousItem).find('.arrow-right').addClass('disabled');
            }
            
            $('li[data-subpage-id=' + $(previousItem).attr('data-id') + ']').show();
        }
    });
}
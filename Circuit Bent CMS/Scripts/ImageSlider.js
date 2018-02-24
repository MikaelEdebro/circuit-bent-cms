function InitSortable(ajaxUrl) {
    // enable the sortable plugin
    $(".sortable").sortable({
        cursor: "move",
        delay: 150,
        // this event runs when you drop a moved object
        stop: function (event, ui) {
            // get the image gallery id
            var imageSliderId = $(ui.item).attr('data-imageslider-id');
            
            console.log(imageSliderId)
            // store all the id's in an array
            var orderById = [];
            $('.sortable li[data-imageslider-id=' + imageSliderId + ']').each(function () {
                orderById.push($(this).attr('data-id'));
            });

            // make the array to a normal comma separated string
            var orderByIdString = orderById + "";

            console.log(orderByIdString);
            // send ajax calls to update order
            $.ajax({
                url: ajaxUrl,
                type: 'POST',
                data: {
                    arrId: orderByIdString,
                    imageSliderId: imageSliderId
                }
            }).done(function (data) {
                // this function runs when the ajax call is done
            });
        }
    }).disableSelection();
}
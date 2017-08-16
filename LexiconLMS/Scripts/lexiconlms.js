
$(function () { // will trigger when the document is ready
    $('#message').fadeOut(5000);
    var options = $.extend(
    {},                                  // empty object
    $.datepicker.regional["sv"],         
    { dateFormat: "yy-mm-dd", format: "yyyy-mm-dd" }
);
    $.datepicker.setDefaults(options);
    $('.datepicker').datepicker(); //Initialise any date pickers
});

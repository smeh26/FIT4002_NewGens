function updatePositionContentItem(id, inputid, successMessageId, errorMessageId) {
    $("#" + successMessageId).hide();
    $("#" + errorMessageId).hide();
    var position = $("#" + inputid).val();
    if (position == "") {
        position = 0;
    }
    $.ajax({
        dataType: "json",
        type: 'Post',
        url: aplicationPath + '/ContentItems/UpdatePosition',
        data: { contentItemId: id, position: position },
        success: function (data) {
            if (data.Success) {
                $("#" + successMessageId).show();
            } else {
                $("#" + errorMessageId).show();
            }
        },
        error: function (data) {
            $("#" + errorMessageId).show();
        }
    });
}
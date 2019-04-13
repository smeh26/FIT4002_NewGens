function updatePosition(type, id, inputid, successMessageId, errorMessageId) {
    $("#" + successMessageId).hide();
    $("#" + errorMessageId).hide();
    var position = $("#" + inputid).val();
    if (position == "") {
        position = 0;
    }
    $.ajax({
        dataType: "json",
        type: 'Post',
        url: aplicationPath + '/' + type + '/UpdatePosition',
        data: { questionId: id, position: position },
        success: function (data) {
            if (data.Success) {
                $("#"+successMessageId).show();
            } else {
                $("#"+errorMessageId).show();
            }
        },
        error: function (data) {
            $("#" + errorMessageId).show();
        }
    });
}

function updatePositionField(type, id, inputid, selectId, successMessageId, errorMessageId) {
    $("#" + successMessageId).hide();
    $("#" + errorMessageId).hide();
    var position = $("#" + inputid).val();
    if (position == "") {
        position = 0;
    }

    var field = $("#" + selectId).val();
    $.ajax({
        dataType: "json",
        type: 'Post',
        url: aplicationPath + '/' + type + '/UpdatePosition',
        data: { questionId: id, position: position, fieldName: field, updateField : true },
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



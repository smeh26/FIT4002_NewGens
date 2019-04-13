$(document).ready(function () {
    $("input[name='text-link-line-group-check']").click(function () {
        var checked = $(this).is(":checked");
        $("input[name='text-link-line-group-check']").each(function (i, item) {
            $(item).prop("checked", false);
        });
        $(this).prop("checked", checked);
    });
});
function validateLineLink() {

        var textLink = {};
        var textLinkName = $("#name-text-link-line").val();

        if (textLinkName === "") {
            $("#name-link-line-text-error").show();
            return false;
        }

        textLink.text = textLinkName;
        var href = "";
        $("#line-link input[name='text-link-line-group-check']").each(function (i, item) {
            var itemQ = $(item);
            if (itemQ.is(":checked")) {
                var chkVal = itemQ.val();

                switch (chkVal) {
                case "text":
                    href = $("#line-link .text-link-select input[type='text']").val();
                    if (href === "" || href === null) {
                        $("#text-link-line-text-error").show();
                    }
                    textLink.type = "text";
                    break;
                case "sections":
                    href = $("#line-link .sections-link-select  select").val();
                    textLink.type = "sections";
                    break;
                case "articles":
                    href = $("#line-link  .articles-link-select  select").val();
                    textLink.type = "articles";
                    break;
                case "roles":
                    href = $("#line-link  .roles-link-select  select").val();
                    textLink.type = "roles";
                    break;
                case "domains":
                    href = $("#line-link .domains-link-select  select").val();
                    textLink.type = "domains";
                    break;
                case "aspects":
                    href = $("#line-link  .aspects-link-select  select").val();
                    textLink.type = "aspects";
                    break;
                }
                textLink.href = href;
            }
        });
        if (href === "") {
            return false;
        }
    $("#Link").val(JSON.stringify(textLink));
    return true;
}
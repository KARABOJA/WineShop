/***********************************************
****    Etendue JavaScript                  ****
***********************************************/
var popupTitle = "WineShop";
var popupIconTitleSrc = "/Images/Logo/logo.png";

popup = (function (options, callback) {

    var message = null;
    var title = null;
    var iconTitleSrc = null;
    var state = null;
    var button = null;
    var icon = null;

    if (typeof options !== 'undefined' && options != null) {
        if (typeof options.message !== 'undefined') {
            message = options.message;
        }
        if (typeof options.title !== 'undefined') {
            title = options.title;
        }
        if (typeof options.state !== 'undefined') {
            state = options.state;
        }
        if (typeof options.button !== 'undefined') {
            button = options.button;
        }
        if (typeof options.icon !== 'undefined') {
            icon = options.icon;
        }
    }

    var btnDefault = null;
    var elementHeader = $("#dlgAlert").find(".modal-header");
    var elementTitle = $("#dlgAlert").find(".modal-title");
    var elementBody = $("#dlgAlert").find(".modal-body").find(".row");
    $(elementHeader).removeAttr("class");
    $(elementHeader).addClass("modal-header");

    $(elementTitle).html(null);

    if (typeof iconTitleSrc !== 'undefined' && iconTitleSrc != null) {
        var elementIconTitle = document.createElement("img");
        $(elementIconTitle).attr("src", iconTitleSrc);
        $(elementIconTitle).addClass("icon");
        $(elementTitle).append(elementIconTitle);
    }
    else if (typeof popupIconTitleSrc !== 'undefined' && popupIconTitleSrc != null) {
        var elementIconTitle = document.createElement("img");
        $(elementIconTitle).attr("src", popupIconTitleSrc);
        $(elementIconTitle).addClass("icon");
        $(elementTitle).append(elementIconTitle);
    }

    if (typeof title !== 'undefined' && title != null) { $(elementTitle).append(title); }
    else if (typeof popupTitle !== 'undefined' && popupTitle != null) { $(elementTitle).append(popupTitle); }
    else { $(elementTitle).append("Message"); }

    $(elementBody).empty();
    $(".modal-footer").empty();
    if (typeof message !== 'undefined' && message != null) {
        if (typeof state !== 'undefined' && state != null) {
            switch (state) {
                case "info":
                case "success":
                case "error":
                case "warning":
                    $(elementHeader).addClass(state);
            }
        }
        var classIcon = "col-xs-1";
        var classMessage = "col-xs-12";

        var divIcon = document.createElement("div");
        var divMessage = document.createElement("div");
        if (typeof icon !== 'undefined' && icon != null) {
            classMessage = "col-xs-11";

            $(divIcon).addClass(classIcon);
            var elementIcon = document.createElement("i");
            if (typeof icon.iconFamily !== 'undefined' && typeof icon.iconName !== 'undefined' && icon.iconFamily != null && icon.iconName != null) {
                $(elementIcon).addClass(icon.iconFamily);
                $(elementIcon).addClass(icon.iconFamily + "-" + icon.iconName);
                var iconState = state;
                if (typeof icon.iconState !== 'undefined' && icon.iconState != null) { iconState = icon.iconState; }
                if (iconState != null) {
                    switch (iconState) {
                        case "info":
                            $(elementIcon).addClass("icon-info");
                            break;
                        case "success":
                            $(elementIcon).addClass("icon-success");
                            break;
                        case "error":
                            $(elementIcon).addClass("icon-danger");
                            break;
                        case "warning":
                            $(elementIcon).addClass("icon-warning");
                            break;
                    }
                }
                $(elementIcon).attr("style", "font-size:32px;")
            }
            $(divIcon).append(elementIcon);
            $(elementBody).append(divIcon);
        }
        $(divMessage).addClass(classMessage);
        $(divMessage).html("<p>" + message + "</p>");
        $(elementBody).append(divMessage);
    }
    if (typeof button !== 'undefined' && button != null) {
        for (i in button) {
            /*************************************/
            /* Création du bouton                */
            var element = document.createElement("button");
            $(element).attr("type", "button");
            $(element).html(button[i].text);
            $(element).addClass("btn");
            /*************************************/
            /* Si bouton par défaut              */
            if (button[i].default) {
                $(element).addClass("btn-default");
                btnDefault = element;
            }
            /*************************************/
            if (button[i].state != null) {
                switch (button[i].state) {
                    case "info":
                        $(element).addClass("btn-info");
                        break;
                    case "success":
                        $(element).addClass("btn-success");
                        break;
                    case "error":
                        $(element).addClass("btn-danger");
                        break;
                    case "warning":
                        $(element).addClass("btn-warning");
                        break;
                }
            }
            $(element).click(function () { $("#dlgAlert").modal("hide"); });
            if (button[i].callback != null) { $(element).click({ paramCallback: button[i].callback, paramValue: button[i].value }, element_OnClick); }
            if (callback != null) { $(element).click({ paramCallback: callback, paramValue: button[i].value }, element_OnClick); }
            $(".modal-footer").append(element);
        }
    } else { $(".modal-footer").html("<button type='button' class='btn btn-default' data-dismiss='modal'>Ok</button>"); }
    $("#dlgAlert").modal("show");
    if (btnDefault != null) { $(btnDefault).focus(); }

    function element_OnClick(event) {
        //Si event contient un callback, on l'execute.
        if (event.data.paramCallback && event.data.paramCallback != null) {
            //On attend le fade de bootstrap (correspond à 0.15s).
            setTimeout(function () {
                event.data.paramCallback(event.data.paramValue);
            }, 500);
        }
    }
});

/***********************************************
****    Etendue jQuery                      ****
***********************************************/
$.fn.extend({
    isVisible: function () {
        return $(this).is(":visible");
    },
    formValidator: function (options, callback) {
        var inst = this;
        inst.options = $.extend({
            iconError: "iconCSMoon-cross2",
            iconSuccess: "iconCSMoon-checkmark3",

            requiredValue: "required",
            requiredErrorMessage: "Merci de remplir les champs obligatoires.",

            buttonReset: null,
            buttonSubmit: null,
        }, options);

        $(inst).find("input, textarea").each(function (index, element) {
            $(element).change(function () {
                if ($(this).val() == "" || $(this).val().length == 0) { SetErrorField($(this)); }
                else { SetSuccessField($(this)); }
            });
        });
        $(inst).on("reset", function () {
            $("input, textarea").each(function (index, element) { SetResetField($(this)); });
        });

        if (inst.options.buttonSubmit != null) {
            $(inst.options.buttonSubmit).click(function () {
                var bSend = true;
                $(inst).find("input, textarea").each(function (index, element) {
                    if ($(element).attr(inst.options.requiredValue)) {
                        if ($(this).val() == "" || $(this).val().length == 0) {
                            SetErrorField($(this));
                            bSend = false;
                        }
                    }
                });
                if (bSend) { callback(); }
                else { popup({ message: "Merci de remplir les champs obligatoires.", state: "error" }); }
            });
        }

        function SetResetField(element) {
            $(element).next().find("i").removeClass(inst.options.iconError);
            $(element).parent().parent().removeClass("has-error");

            $(element).next().find("i").removeClass(inst.options.iconSuccess);
            $(element).parent().parent().removeClass("has-success");
        }

        function SetErrorField(element) {
            if ($(element).attr("required")) {
                $(element).next().find("i").removeClass(inst.options.iconSuccess);
                $(element).parent().parent().removeClass("has-success");

                $(element).next().find("i").addClass(inst.options.iconError);
                $(element).parent().parent().addClass("has-error");
            }
        }
        function SetSuccessField(element) {
            if ($(element).attr("required")) {
                $(element).next().find("i").removeClass(inst.options.iconError);
                $(element).parent().parent().removeClass("has-error");

                $(element).next().find("i").addClass(inst.options.iconSuccess);
                $(element).parent().parent().addClass("has-success");
            }
        }
    },
    emailValidator: function (options, callback) {
        var email;
        email = $(this).val();
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        if (reg.test(email) == false) { return false; }
        return true;
    }
});


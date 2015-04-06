"use strict";

var AJAXSubmit = (function () {
    var ajaxHandler = function ajaxSuccess() {
        alert(this.responseText);
    }

    function submitData(oData) {
        /* the AJAX request... */
        var oAjaxReq = new XMLHttpRequest();
        oAjaxReq.submittedData = oData;
        oAjaxReq.onload = ajaxHandler;
        /* method is POST */
        oAjaxReq.open("post", oData.receiver, true);
        /* enctype is multipart/form-data */
        var sBoundary = "---------------------------" + Date.now().toString(16);
        oAjaxReq.setRequestHeader("Content-Type", "multipart\/form-data; boundary=" + sBoundary);
        oAjaxReq.send("--" + sBoundary + "\r\n" + oData.segments.join("--" + sBoundary + "\r\n") + "--" + sBoundary + "--\r\n");
    }

    function SubmitRequest(oTarget) {
        var nFile, sFieldType, oField, oSegmReq, oFile, bIsPost = oTarget.method.toLowerCase() === "post";
        this.contentType = bIsPost && oTarget.enctype ? oTarget.enctype : "application\/x-www-form-urlencoded";
        this.receiver = oTarget.action;
        this.status = 0;
        this.segments = [];
        var fFilter = escape;
        for (var nItem = 0; nItem < oTarget.elements.length; nItem++) {
            oField = oTarget.elements[nItem];
            if (!oField.hasAttribute("name")) { continue; }
            sFieldType = oField.nodeName.toUpperCase() === "INPUT" ? oField.getAttribute("type").toUpperCase() : "TEXT";
            if (sFieldType === "FILE" && oField.files.length > 0) {
                continue;
            } else if ((sFieldType !== "RADIO" && sFieldType !== "CHECKBOX") || oField.checked) {
                /* field type is not FILE or is FILE but is empty */
                this.segments.push(
                    /* enctype is multipart/form-data */
                    "Content-Disposition: form-data; name=\"" + oField.name + "\"\r\n\r\n" + oField.value + "\r\n"
                );
            }
        }

        submitData(this);
    }

    return function (oFormElement, xhrHandler) {
        if (!oFormElement.action) {
            return;
        }

        if (typeof xhrHandler === 'function') {
            ajaxHandler = xhrHandler;
        }

        new SubmitRequest(oFormElement);
    };
})();
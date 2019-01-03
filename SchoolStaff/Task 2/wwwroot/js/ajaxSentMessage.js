"use strict";

function Enter_func(idSentMessage, idSchoolStaff, paramUrl) {

    var objectButton = document.getElementById(idSentMessage +" ;Button");
    objectButton.disabled = true;
    objectButton.style.color = "#ffffff";

    $.ajax({
        url: paramUrl,
        contentType: "application/json",
        method: "POST",
        dataType: "json",
        data: JSON.stringify({
            idSentMessage: idSentMessage,
            idSchoolStaff: idSchoolStaff
        }),
        success: function (ret) {
            if (ret.success) {
                objectButton.disabled = false;
                objectButton.style.color = "black";

                //--------------------------------------------------------------------------------------
                var elemMessageSuccess = document.getElementById(idSentMessage + " ;Res");

                elemMessageSuccess.outerHTML =
                    '<td id="' + idSentMessage + ' ;Res"><span style="color: #008000">&#10004;</span></td>';
                elemMessageSuccess.innerHTML =
                    '<td id="' + idSentMessage + ' ;Res"><span style="color: #008000">&#10004;</span></td>';
                //--------------------------------------------------------------------------------------
            } else {
                objectButton.disabled = false;
                objectButton.style.color = "black";

                //--------------------------------------------------------------------------------------
                var elemMessageErrorWarning = document.getElementById(idSentMessage + " ;Res");

                if (ret.message == "Error") {
                    elemMessageErrorWarning.outerHTML = '<td id="' + idSentMessage + ' ;Res"><span style="color: #ff0000">&#10008;</span></td>';
                    elemMessageErrorWarning.innerHTML = '<td id="' + idSentMessage + ' ;Res"><span style="color: #ff0000">&#10008;</span></td>';
                } else {
                    elemMessageErrorWarning.outerHTML = '<td id="' + idSentMessage + ' ;Res" title="Phone not entered"><span style="color: #ffd700">&#9888;</span></td>';
                    elemMessageErrorWarning.innerHTML = '<td id="' + idSentMessage + ' ;Res" title="Phone not entered"><span style="color: #ffd700">&#9888;</span></td>';
                }
            }
        },
        error: function (data) {

            console.assert(data.message);
            
            objectButton.disabled = false;
            objectButton.style.color = "black";

        //--------------------------------------------------------------------------------------
            var elemMessage = document.getElementById(idSentMessage + " ;Res");

            if(data.message == "Error") {
                elemMessage.outerHTML = '<td id="' + idSentMessage + ' ;Res"><span style="color: #ff0000">&#10008;</span></td>';
                elemMessage.innerHTML = '<td id="' + idSentMessage + ' ;Res"><span style="color: #ff0000">&#10008;</span></td>';
            } else {
                elemMessage.outerHTML = '<td id="' + idSentMessage + ' ;Res"><span style="color: #ff0000">&#9888;</span></td>';
                elemMessage.innerHTML = '<td id="' + idSentMessage + ' ;Res"><span style="color: #ff0000">&#9888;</span></td>';
            }
        //--------------------------------------------------------------------------------------

        }
    }).done(function(result) {
        //var mesRes = document.getElementById(idSentMessage + " ;Button");
        //mesRes.disabled = false;
        //mesRes.style.color = "black";
    });
}
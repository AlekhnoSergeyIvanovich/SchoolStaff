"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalRAdd").build();

connection.on("AddMess", function(message) {
    $('#messagesList').append(htmlEncode(message));

    connection.start().catch(function(err) {
        return console.error(err.toString());
    });
})

function htmlEncode(value) {
    $('#messagesList').append($('<tr>' +
        ('<td />').text(value).html(value.Name + ' ' + value.Patronymic + ' ' + (value.Surname)) +
        ('<td />').text(value).html(value.DateOfBirth) +
        ('<td />').text(value).html(value.Email) +
        ('<td />').text(value).html(value.Phone) +
        '</tr>'));
}

StartupHubProxy.server.getAllStocks().done(function (stocks) {
    $.each(stocks, function () {
        htmlEncode(this);
    });
});
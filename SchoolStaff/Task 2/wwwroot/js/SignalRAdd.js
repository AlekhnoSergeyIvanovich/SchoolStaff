"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/serg/actgo").build();

connection.on("AddMessage", (schoolStaff) /*(id, name, dateOfBirth, email, phone)*/ => {
    var consText = "Add: " + schoolStaff.name + " " + schoolStaff.dateOfBirth + " " + schoolStaff.email /*+ " " + schoolStaff.phone*/;
    var tr = document.createElement("tr");
    //var text = document.getElementsByTagName("edit")[0];
    //var val = text.innerHTML;

    console.log(consText);
    var text = document.getElementById("edit");
    var val = text.value;
    console.log(text.value);
    if ((val == null) || (val == "") || ((schoolStaff.name.toUpperCase().indexOf(val.toUpperCase()) != -1) || (schoolStaff.email.toUpperCase().indexOf(val.toUpperCase()) != -1) || /*(schoolStaff.phone.toUpperCase().indexOf(val.toUpperCase()) != -1) ||*/ (schoolStaff.dateOfBirth.toUpperCase().indexOf(val.toUpperCase()) != -1))) {
        tr.id = schoolStaff.id;

        console.log(consText);

        var tdName = document.createElement("td");
        tr.appendChild(tdName);
        tdName.textContent = schoolStaff.name + " " + schoolStaff.patronymic + " " + schoolStaff.surname;
        tdName.id = schoolStaff.id + ";Name";

        var tdDateOfBirth = document.createElement("td");
        tr.appendChild(tdDateOfBirth);

        var dateNew = new Date(schoolStaff.dateOfBirth);
        var date1 = dateNew.getDate();
        
        var month1 = dateNew.getMonth();
        
        var year1 = dateNew.getFullYear();
        tdDateOfBirth.textContent = date1 + "." + month1 + "." + year1; //+ schoolStaff.dateOfBirth;
        tdDateOfBirth.id = schoolStaff.id + ";DateOfBirth";

        var tdemail = document.createElement("td");
        tr.appendChild(tdemail);
        tdemail.outerHTML = '<td><a href="mailto:' + schoolStaff.email + '">' + schoolStaff. email + "</a></td>";
        tdemail.innerHTML = '<td><a href="mailto:' + schoolStaff.email + '">' + schoolStaff. email + "</a></td>";
        tdemail.id = schoolStaff.id + ";Email";

        //var tdphone = document.createElement("td");
        //tr.appendChild(tdphone);
        //tdphone.textContent = schoolStaff.phone;
        //tdphone.id = schoolStaff.id + ";Phone";

        var tdEdit = document.createElement("td");
        tr.appendChild(tdEdit);
        tdEdit.outerHTML = '<td><a href="/SchoolStaff/Edit/' +
            schoolStaff.id +
            '?collection=Business.Entities.SchoolStaff">Edit</a> | <a href="/SchoolStaff/Details/' +
            schoolStaff.id +
            '">Details</a> | <a href="/SchoolStaff/Delete/' +
            schoolStaff.id +
            '?collection=Business.Entities.SchoolStaff">Delete</a>|' +
            ' <a href="/SchoolStaff/AdjustmentSchoolStaffSubject/' +
            schoolStaff.id + '">Set link SchoolStaff-Subject</a>|' +
            ' <a href="/SchoolStaff/AdjustmentSchoolStaffAdministration/' +
            schoolStaff.id + '">Set link SchoolStaff-Profession</a>' +
            "</td>";
        tdEdit.innerHTML = '<td><a href="/SchoolStaff/Edit/' +
            schoolStaff.id +
            '?collection=Business.Entities.SchoolStaff">Edit</a> | <a href="/SchoolStaff/Details/' +
            schoolStaff.id +
            '">Details</a> | <a href="/SchoolStaff/Delete/' +
            schoolStaff.id +
            '?collection=Business.Entities.SchoolStaff">Delete</a>|' +
            ' <a href="/SchoolStaff/AdjustmentSchoolStaffSubject/' +
            schoolStaff.id + '">Set link SchoolStaff-Subject</a>|' +
            ' <a href="/SchoolStaff/AdjustmentSchoolStaffAdministration/' +
            schoolStaff.id + '">Set link SchoolStaff-Profession</a>' +
            "</td>";
        document.getElementById("messagesList").appendChild(tr);
    }
});

connection.on("RemoveMessage", (schoolStaff) => {
    console.log(schoolStaff);
    var consText = "Remove: " + schoolStaff.id;
    var object = document.getElementById(schoolStaff.id);
    if (object != null) {
        object.innerText = null;
        object.innerHTML = null;
        object.outerHTML = null;
        object = null;
        console.log(consText);
    }
});

connection.on("UpdateMessage", (schoolStaff) => {
    var consText = "Update: " + schoolStaff.id;
    var object = document.getElementById(schoolStaff.id);
    if (object != null) {
        var objectName = document.getElementById(schoolStaff.id + ";Name");
        objectName.outerHTML = '<td id="' + schoolStaff.id + ';Name>"' + schoolStaff.name + " " + schoolStaff.name + " " + schoolStaff.name + "</td>";
        objectName.innerHTML = '<td id="' + schoolStaff.id + ';Name>"' + schoolStaff.name + "</td>";

        var objectDateOfBirth = document.getElementById(schoolStaff.id + ";DateOfBirth");
        objectDateOfBirth.outerHTML = '<td id="' + schoolStaff.id + ';DateOfBirth">' + schoolStaff.dateOfBirth + "</td>";
        objectDateOfBirth.innerHTML = '<td id="' + schoolStaff.id + ';DateOfBirth">' + schoolStaff.dateOfBirth + "</td>";

        var objectEmail = document.getElementById(schoolStaff.id + ";Email");
        objectEmail.outerHTML = '<td id="' + schoolStaff.id + ';Email"><a href="mailto:' + schoolStaff.email + '"/>' + schoolStaff. email + "</td>";
        objectEmail.innerHTML = '<td id="' + schoolStaff.id + ';Email"><a href="mailto:' + schoolStaff.email + '"/>' + schoolStaff.email + "</td>";

        //var objectPhone = document.getElementById(schoolStaff.id + ";Phone");
        //objectPhone.outerHTML = '<td id="' + schoolStaff.id + ';Phone">' + schoolStaff.phone + "</td>";
        //objectPhone.innerHTML = '<td id="' + schoolStaff.id + ';Phone">' + schoolStaff.phone + "</td>";

        console.log(consText);
    }
});
//----------------------------------------------------------------------------------------------
connection.on("AddMessageProfession", (profession) /*(id, name, dateOfBirth, email, phone)*/ => {
    var consText = "Add: " + profession.name;
    var tr = document.createElement("tr");
    //var text = document.getElementsByTagName("edit")[0];
    //var val = text.innerHTML;
    console.log(profession.nameProfession);
    console.log(consText);
    var text = document.getElementById("edit");
    var val = null; //text.value;
    //console.log(text.value);
    if ((val == null) || (val == "") || ((profession.name.toUpperCase().indexOf(val.toUpperCase()) != -1))) {
        tr.id = profession.id;

        console.log(consText);

        var tdName = document.createElement("td");
        tr.appendChild(tdName);
        tdName.textContent = profession.nameProfession;
        tdName.id = profession.id + ";Name";

        var tdCountPeopleProfession = document.createElement("td");
        tr.appendChild(tdCountPeopleProfession);
        tdCountPeopleProfession.textContent = profession.countPeopleProfession;
        tdCountPeopleProfession.id = profession.id + ";CountPeopleProfession";

        var tdEdit = document.createElement("td");
        tr.appendChild(tdEdit);
        tdEdit.outerHTML = '<td><a href="/Administration/Edit/' +
            profession.id +
            '?collection=DAL.Entities.Profession">Edit</a> | <a href="/SchoolStaff/Details/' +
            profession.id +
            '">Details</a> | <a href="/Administration/Delete/' +
            profession.id +
            '?collection=DAL.Entities.Profession">Delete</a> | ' +
            '<a href="/Administration/AdjustmentSchoolStaffAdministration/' +
            profession.id + '">Set link SchoolStaff-Profession</a>'+
            "</td>";
        tdEdit.innerHTML = '<td><a href="/SchoolStaff/Edit/' +
            profession.id +
            '?collection=Business.Entities.SchoolStaff">Edit</a> | <a href="/SchoolStaff/Details/' +
            profession.id +
            '">Details</a> | <a href="/SchoolStaff/Delete/' +
            profession.id +
            '?collection=Business.Entities.SchoolStaff">Delete</a>|' +
            ' <a href="/SchoolStaff/AdjustmentSchoolStaffSubject/' +
            profession.id + '">Set link SchoolStaff-Subject</a>|' +
            ' <a href="/SchoolStaff/AdjustmentSchoolStaffAdministration/' +
            profession.id + '">Set link SchoolStaff-Profession</a>' +
            "</td>";
        document.getElementById("messagesList").appendChild(tr);
    }
});

connection.on("RemoveMessageProfession", (profession) => {
    console.log(profession);
    var consText = "Remove: " + profession.id;
    var object = document.getElementById(profession.id);
    if (object != null) {
        object.innerText = null;
        object.innerHTML = null;
        object.outerHTML = null;
        object = null;
        console.log(consText);
    }
});

connection.on("UpdateMessageProfession", (profession) => {
    var consText = "Update: " + profession.id;
    var object = document.getElementById(profession.id);
    if (object != null) {
        var objectName = document.getElementById(profession.id + ";Name");
        objectName.outerHTML = '<td id="' + profession.id + ';Name">' + profession.name + "</td>";
        objectName.innerHTML = '<td id="' + profession.id + ';Name">' + profession.name + "</td>";

        var objectCountPeopleProfession = document.getElementById(profession.id + ";CountPeopleProfession");
        objectCountPeopleProfession.outerHTML = '<td id="' + profession.id + ';CountPeopleProfession">' + profession.countPeopleProfession + "</td>";
        objectCountPeopleProfession.innerHTML = '<td id="' + profession.id + ';CountPeopleProfession">' + profession.countPeopleProfession + "</td>";

        console.log(consText);
    }
});
//----------------------------------------------------------------------------------------------
connection.start().catch(function (err) {
    return console.error(err.toString());
});
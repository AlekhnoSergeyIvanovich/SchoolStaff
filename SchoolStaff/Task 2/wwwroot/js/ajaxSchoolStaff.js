var lengthVal;

function search_func(value, paramUrl) {
    var kCode = window.event
        ? window.event.keyCode
        : (event.keyCode ? event.keyCode : (event.which ? event.which : null));
    if
    (
    (lengthVal == undefined) ||
    (
        (
            (
                (value.length > 2) || ((kCode == 13) && (value.length == 2))) &&
                (lengthVal != value)
            ) ||
            (
                ((lengthVal.length > 0) && (value.length == 0))
            )
        )
    )
    {
        lengthVal = value;
    $.ajax({
        type: "GET",
        url: paramUrl + "?handler=List",
        contentType: "application/json;",
        dataType: "json",
        data: { name: JSON.stringify(value) },
        success: function(response) {
            var dvItems = "messagesList";

            var idBefore = 0;

            $.each(response,
                function(i, item) {
                    var promise = new Promise((resolve, reject) => {
                        var elem = document.getElementById(item.id);
                        if (elem != null) {
                            if (!item.isExist) {
                                elem.innerText = null;
                                elem.innerHTML = null;
                                elem.outerHTML = null;
                                elem = null;
                            } else {
                                idBefore = item.id;
                            }
                        } else {
                            if (item.isExist) {
                                var tr = document.createElement("tr");
                                tr.id = item.id;

                                var tdName = document.createElement("td");
                                tr.appendChild(tdName);
                                tdName.textContent = item.name + " " + item.patronymic + " " + item.surname;
                                tdName.id = item.id + ";Name";

                                var tdDateOfBirth = document.createElement("td");
                                tr.appendChild(tdDateOfBirth);
                                tdDateOfBirth.textContent = item.dateOfBirth;
                                tdDateOfBirth.id = item.id + ";DateOfBirth";

                                var tdemail = document.createElement("td");
                                tr.appendChild(tdemail);
                                tdemail.outerHTML =
                                    '<td><a href="mailto:' + item.email + '">' + item.email + "</a></td>";
                                tdemail.innerHTML =
                                    '<td><a href="mailto:' + item.email + '">' + item.email + "</a></td>";
                                tdemail.id = item.id + ";Email";

                                
                                var tdphone = document.createElement("td");
                                tr.appendChild(tdphone);

                                var phone = "";

                                $.each(item.schoolStaffPhones,
                                    function (i, item) {
                                        phone = phone + "<ol>" + item.phone + "</ol>";
                                    });

                                tdphone.outerHTML =
                                    "<td>" + phone +"</td>";
                                tdphone.innerHTML =
                                    "<td>" + phone + "</td>";

                                tdphone.id = item.id + ";Phone";
                                

                                var tdEdit = document.createElement("td");
                                tr.appendChild(tdEdit);
                                tdEdit.outerHTML = '<td><a href="/SchoolStaff/Edit/' +
                                    item.id +
                                    '?collection=Business.Entities.SchoolStaff">Edit</a> | <a href="/SchoolStaff/Details/' +
                                    item.id +
                                    '">Details</a> | <a href="/SchoolStaff/Delete/' +
                                    item.id +
                                    '?collection=Business.Entities.SchoolStaff">Delete</a>|' +
                                    ' <a href="/SchoolStaff/AdjustmentSchoolStaffSubject/' +
                                    item.id + '">Set link SchoolStaff-Subject</a>|' +
                                    ' <a href="/SchoolStaff/AdjustmentSchoolStaffAdministration/' +
                                    item.id + '">Set link SchoolStaff-Profession</a>' +

                                    "</td>";
                                tdEdit.innerHTML = '<td><a href="/SchoolStaff/Edit/' +
                                    item.id +
                                    '?collection=Business.Entities.SchoolStaff">Edit</a> | <a href="/SchoolStaff/Details/' +
                                    item.id +
                                    '">Details</a> | <a href="/SchoolStaff/Delete/' +
                                    item.id +
                                    '?collection=Business.Entities.SchoolStaff">Delete</a>|' +
                                    ' <a href="/SchoolStaff/AdjustmentSchoolStaffSubject/' +
                                    item.id + '">Set link SchoolStaff-Subject</a>|' +
                                    ' <a href="/SchoolStaff/AdjustmentSchoolStaffAdministration/' +
                                    item.id + '">Set link SchoolStaff-Profession</a>' +
                                    "</td>";

                                if (idBefore == 0) {
                                    document.getElementById(dvItems).prepend(tr);
                                } else if (idBefore < item.id) {
                                    $(tr).insertAfter("#" + idBefore);
                                }

                                if (item.isExist) {
                                    idBefore = item.id;
                                }
                            }
                        }
                        resolve("The man likes to keep his word");
                    });
                });
        },
        failure: function(response) {
            alert(response);
        }
        });
    }
}
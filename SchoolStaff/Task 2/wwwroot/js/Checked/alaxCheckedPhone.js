function checkedPhone_func(idSchoolStaff, idPhone, paramUrl) {
    $.ajax({
        url: paramUrl,
        contentType: "application/json",
        method: "POST",
        dataType: "json",
        data: JSON.stringify({
            idSchoolStaff: idSchoolStaff,
            idPhone: idPhone
        }),
        success: function (ret) {
        },
        error: function (data) {
        }
    }).done(function (result) {
    });
}
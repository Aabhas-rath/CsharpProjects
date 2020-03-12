window.onload = function () {
    var linkElements = $("a[class='nav-link text-light']");
    $.each(linkElements, function (index, element) {
        if (element.getAttribute("href") != "#")
            removeDivandDisableNav(element.attributes["href"].value);
    });

    $('#CreateAlbum').submit(function (e) {
        e.preventDefault();
        var formData = new FormData(this);
        $.ajax({
            url: this.attr('action'),
            type: "POST",
            dataType: "html",
            contentType: "multipart/form-data",
            data: formData,
            processData: false,
            success: function (result) {
                $('#result').html(result);
            },
            error: function (result) {

            }
        });
    });
};


var modalfunc = function (e) { $('#manageAlbums .modal-body').html(''); };
$('#manageAlbums').on('hidden.bs.modal', modalfunc);


function removeDivandDisableNav(divId) {
    if ($(divId).length == 0) {
        var navElement = $("a[href:'" + divId.slice(1) + "']");
        if (navElement.next().is("br")) {
            navElement.next().remove();
            navElement.remove();
        }
        else
            navElement.remove();
    }
};
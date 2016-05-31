$(document).ready(function () {

    $('#SendMessage').on('click', function () {
        $('#searchResult').text('Fetching search predictions...');

        $.ajax({
            method: 'Post',
            url: '/Message/Create',
            data: {
                messageString: $('#message-text').val(),
                userId: $('#userId').val(),
                subject: $('#subjectString').val()
            },
            dataType: 'json',
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
            },
            success: function (data) {
                if (data.Name === undefined)
                    $('#test').html(data);
                else
                    $('#test').html("Letade du efter " + data.Name + "? id=" + data.Id);
            }
        });
    });//on input
});//document.ready
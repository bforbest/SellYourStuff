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

    $('#messageListId').on('click', function () {

        $.ajax({
            method: 'Post',
            url: '/Message/Index',
            dataType: 'json',
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
            },
            success: function (data) {
                if (data === undefined) {
                    alert('no data send');
                }
                else {
                    var div1Content = '<table class="table">'+
                   '<tr>'+
                        '<th>' + '@Email'+
                        '</th>'+
                        '<th>' + 'Subject'+
                        '</th>'+
                        '<th>' + 'Date'+
                        '</th>'+
                        '</tr>';
                    var div3Content = '';
                    for (var i = 0; i < data.length; i++) {
                        div3Content += '<tr>' + '<td>' + '<a href="#">' + data[i].Sender.Email +'</a>' + '</td>';
                        div3Content += '<td>' + data[i].Subject +  '</td>';
                        div3Content += '<td>' + data[i].DateRecieved +  '</td>' + '</tr>';// if Name is property of your Person object
                        //div3Content += '<p>' + data[i].Id + '</p>';
                    }
                  
                    $('#divMessage').append(div1Content + div3Content + '</table>');

                }
                    //$('#test').html("Letade du efter " + data.Name + "? id=" + data.Id);
            }
        });
    });//on input
    
});//document.ready

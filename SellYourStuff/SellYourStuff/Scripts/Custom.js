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
                    $('#divMessage').empty();
                    var div2Content = '';
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
                        div3Content += '<tr>' + '<td>' + '<a href="#div'+i+'" onclick="show('+'\'div'+i+'\')" >' + data[i].Sender.Email +'</a>' + '</td>';
                        div3Content += '<td>' + data[i].Subject +  '</td>';
                        div3Content += '<td>' + data[i].DateRecieved +  '</td>' + '</tr>';// if Name is property of your Person object
                        //div3Content += '<p>' + data[i].Id + '</p>';
                        div3Content += '<tr id="div'+i+'" style="display:none;">' + '<td colspan="3">' + data[i].MessageRecieved + '</td>' + '</tr>';
                    }
                  
                    $('#divMessage').append(div1Content + div3Content + '</table>');

                }
                    //$('#test').html("Letade du efter " + data.Name + "? id=" + data.Id);
            }
        });
    });//on input
    
});//document.ready

function show(target) {
    document.getElementById(target).style.display = 'table-row';
}

function showMessage(target, len, userAppId) {
    for (var i = 1; i < len+1; i++) {
        document.getElementById('id' + i).style.display = 'none';
        
    }
    document.getElementById(target).style.display = 'block';
    document.getElementById("userId").value = userAppId;
    
}
function changeValue(o) {
    document.getElementById('userId').value = o.innerHTML;
}
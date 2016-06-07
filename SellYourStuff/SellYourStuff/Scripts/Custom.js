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

function showMessage(target, len, userAppId, id) {
    for (var i = 1; i < len + 1; i++) {
        document.getElementById('id' + i).style.display = 'none';
        document.getElementById
    }
    $(".list-group-item").removeClass("NotSeen");
    document.getElementById(target).style.display = 'block';
    document.getElementById("userId").value = userAppId;
    $.ajax({
        method:'post',
        url: '/Message/Seen',
        data: { id: id },
        dataType: 'json',
        error: function (jqXHR, textStatus, errorThrown) {
            alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
        }
    }).done(function () {
       
    });
}
function changeValue(o) {
    document.getElementById('userId').value = o.innerHTML;
}

            $(document).ready(function()
            {
                var markers = [
        { latLng: [61.3543, 18.1454], name: 'Whole Sweden' }
                ];
                var map =    $('#vmap').vectorMap(
                    {
                        map: 'se_mill',
                        markers: markers,
                        onMarkerClick: function (e, index) {
                            $('#text').text(markers[index].text);
                            $.ajax({
                                method: 'post',
                                url: '/Product/RegionCookie',
                                data: {
                                    regionName: "Sweden",
                                },
                                dataType: 'json',
                                error: function (jqXHR, textStatus, errorThrown) {
                                    alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
                                }
                            }).done(function () {

                            });
                        },
                        backgroundColor: 'transparent',
                        enableZoom: true,
                        regionsSelectable: true,
                        regionsSelectableOne: true,
                        
                        zoomOnScroll: true,
                        regionStyle: {
                            initial: {
                                fill: '#8d8d8d',
                            
                            },
                        
                            selected: {
                                fill: '#acc1da'
                            }
                        },
                        onRegionClick: function(event,code)
                        {
                            var map = $('#vmap').vectorMap('get', 'mapObject');
                            var name = map.getRegionName(code);
                            var message = 'You clicked "' + name + '" which has the code: ' + code.toUpperCase();
                            $.ajax({
                                method: 'post',
                                url: '/Product/RegionCookie',
                                data: {
                                    code: code,
                                    regionName: name,
                                },
                                dataType: 'json',
                                error: function (jqXHR, textStatus, errorThrown) {
                                    alert('Något gick fel! status:' + textStatus + "\nerror: " + errorThrown);
                                }
                            }).done(function () {

                            });
                        }
                    });
            
            });
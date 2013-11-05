function LoadCountNewNumbers() {
    $.getJSON('/user/GetNewMsgRequests', function (data) {
        $.each(data, function (key, val) {
            switch (key) {
            case 'countNewMessages':
                if (val != 0) {
                    $('#countNewMessages').html(val);
                    $('#countNewMessages').show();
                } else {
                    $('#countNewMessages').empty();
                    $('#countNewMessages').hide();
                }
                break;
            case 'countNewRequests':
                if (val != 0) {
                    $('#countNewRequests').html(val);
                    $('#countNewRequests').show();
                    ;
                } else {
                    $('#countNewRequests').empty();
                    $('#countNewRequests').hide();
                }
                break;
            }
        });
    });
}
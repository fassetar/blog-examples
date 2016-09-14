

var prodArray = [1, 2, 3, 4, 5];
var numEle = 5;


$(function() {
    $('.flashcontent').hide();


    $('#ad' + prodArray[0]).show();
    $('#nextAd').attr('curr', 0);
    $('#prevAd').attr('curr', 0);

    $('#nextAd').click(function() {
        $('#play').stopTime('scroll').show();
        $('#pause').hide();
        var next = $(this).attr('curr');
        var next = parseInt(next);
        if (next == numEle - 1) {
            $('#ad' + prodArray[next]).hide();
            var next = 0;
            $('#ad' + prodArray[next]).show();
            $('#nextAd').attr('curr', next);
            $('#prevAd').attr('curr', next);
        } else {
            $('#ad' + prodArray[next]).hide();
            var next = next + 1;
            $('#ad' + prodArray[next]).show();
            $('#nextAd').attr('curr', next);
            $('#prevAd').attr('curr', next);
        }
    });

    $('#prevAd').click(function() {
        $('#play').stopTime('scroll').show();
        $('#pause').hide();
        var prev = $(this).attr('curr');
        var prev = parseInt(prev);
        if (prev == 0) {
            $('#ad' + prodArray[prev]).hide();
            var prev = numEle - 1;
            $('#ad' + prodArray[prev]).show();
            $('#prevAd').attr('curr', prev);
            $('#nextAd').attr('curr', prev);
        } else {
            $('#ad' + prodArray[prev]).hide();
            var prev = prev - 1;
            $('#ad' + prodArray[prev]).show();
            $('#prevAd').attr('curr', prev);
            $('#nextAd').attr('curr', prev);
        }
    });


    $('#play').click(function() {
        $(this).everyTime("8s", "scroll", function() {
            var next = $('#nextAd').attr('curr');
            var next = parseInt(next);
            if (next == numEle - 1) {
                $('#ad' + prodArray[next]).hide();
                var next = 0;
                $('#ad' + prodArray[next]).show();
                $('#nextAd').attr('curr', next);
                $('#prevAd').attr('curr', next);
            } else {
                $('#ad' + prodArray[next]).hide();
                var next = next + 1;
                $('#ad' + prodArray[next]).show();
                $('#nextAd').attr('curr', next);
                $('#prevAd').attr('curr', next);
            }
        }).hide();
        $('#pause').show();
    });

    $('#pause').click(function() {
        $('#play').stopTime('scroll').show();
        $(this).hide();
    });

    $('#pause').hide();
    $('#play').click();

});
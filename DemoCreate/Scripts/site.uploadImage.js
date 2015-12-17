var jcrop_api,
    boundx,
    boundy,
    xsize,
    ysize,
    voteId;


var maxSizeAllowed = 2;     // Upload limit in MB
var maxSizeInBytes = maxSizeAllowed * 1024 * 1024;
var keepUploadBox = false;  // ToDo - Remove if you want to keep the upload box
var keepCropBox = false;    // ToDo - Remove if you want to keep the crop box

function previewImage(id) {
    voteId = id
    $('#inputVoteImage' + id).addClass('hidden');
    $('#confirmImageBtn' + id).removeClass('hidden');
    var temppreview = document.querySelector("#VoteImage"+id);
    var prv = $('#VoteImage' + id);
    var file = document.querySelector("#inputVoteImage"+id).files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        temppreview.src = reader.result;
        temppreview.alt = file.name;
        $('#avatar-upload-box').addClass('hidden');
        $('#avatar-crop-box').removeClass('hidden');
        var img = $('#VoteImage'+id);

        initAvatarCrop(img);
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
        temppreview.src = "";
    }
}

function initAvatarCrop(img) {
    img.Jcrop({
        onChange: updatePreviewPane,
        onSelect: updatePreviewPane,
        aspectRatio: xsize / ysize
    }, function () {
        var bounds = this.getBounds();
        boundx = bounds[0];
        boundy = bounds[1];

        jcrop_api = this;
        jcrop_api.setOptions({ allowSelect: true });
        jcrop_api.setOptions({ allowMove: true });
        jcrop_api.setOptions({ allowResize: true });
        jcrop_api.setOptions({ aspectRatio: 1 });

        // Maximise initial selection around the centre of the image,
        // but leave enough space so that the boundaries are easily identified.
        var padding = 10;
        var shortEdge = (boundx < boundy ? boundx : boundy) - padding;
        var longEdge = boundx < boundy ? boundy : boundx;
        var xCoord = longEdge / 2 - shortEdge / 2;
        jcrop_api.animateTo([xCoord, padding, shortEdge, shortEdge]);

        var pcnt = $('#previewContainer' + voteId);
        xsize = pcnt.width();
        ysize = pcnt.height();
        $('#preview-pane' + voteId).appendTo(jcrop_api.ui.holder);
        jcrop_api.focus();
    });
}

function updatePreviewPane(c) {
    if (parseInt(c.w) > 0) {
        var rx = xsize / c.w;
        var ry = ysize / c.h;

        $('#preview' + voteId).css({
            width: c.w + 'px',
            height: c.h + 'px',
            marginLeft: c.x + 'px',
            marginTop: c.y + 'px'
        });
    }
}

function saveAvatar(id) {
    var img = $('#preview' + id);
    $('#confirmImageBtn'+id).addClass('disabled');
    var preview = document.querySelector("#VoteImage" + id);
    var voteimg = $('#VoteImage' + id);

    $.ajax({
        type: "POST",
        url: "/Questionnaire/Save",
        traditional: true,
        data: {
            w: img.css('width'),
            h: img.css('height'),
            l: img.css('marginLeft'),
            t: img.css('marginTop'),
            o: voteimg.css('height'),
            fileData: preview.src
        }
    }).done(function (data) {
        if (data.success === true) {
            $('#VoteImageResult' + id).attr('src', "/UploadImages/" + data.fileName);
            $('#voteImagePath' + id).attr('value', "/UploadImages/" + data.fileName);
            $('.jcrop-holder').addClass('hidden');
            $('#voteImageResultArea' + id).removeClass('hidden');
            $('#croppingImage'+id).addClass('hidden');
        } else {
            alert(data.errorMessage)
        }
    }).fail(function (e) {
        alert('Cannot upload avatar at this time');
    });
}
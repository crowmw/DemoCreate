function vote(voteId) {
    $.ajax({
        type: "POST",
        url: "/Questionnaire/Vote",
        traditional: true,
        data: {
            id: voteId
        }
    }).done(function (data) {
        if (data.success === true) {
            $('#' + data.questionnaireId + ' button').addClass('hidden');

            var v = $(' [id^="resultVote' + data.voteId + '"] p');
            var val = parseInt(v[0].innerHTML);
            val = val + 1;
            v[0].innerHTML = val;

            var votesValues = $('#' + data.questionnaireId + ' [id^="resultVote"] p');
            var vote1 = parseInt(votesValues[0].innerHTML);
            var vote2 = parseInt(votesValues[1].innerHTML);
            var allVotes = vote1 + vote2;
            var vote1Procentage = vote1 / allVotes * 100;
            var vote2Procentage = vote2 / allVotes * 100;
            votesValues[0].innerHTML = vote1 + ' ( ' + vote1Procentage.toFixed(1) + '% )';
            votesValues[1].innerHTML = vote2 + ' ( ' + vote2Procentage.toFixed(1) + '% )';
            $('#' + data.questionnaireId + ' [id^="resultVote"]').removeClass('hidden');
        }
    });
}
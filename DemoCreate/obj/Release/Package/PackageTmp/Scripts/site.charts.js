var genderVotesData = null;
var ageVotesData = null;
var educationVotesData = null;
var provincesVotesData = null;
var options = {
    'height': 350,
    'legend': { position: 'none' },
    'animation': { startup: true, duration: 2000, easing: 'out' }
}

google.load("visualization", "1.0", {
    packages: ["corechart"]
});

//google.setOnLoadCallback(function () {
//    getVote1Data('54d383db-4843-4f6b-a8bd-4e8a6ee9e514');
//});

function getVote1Data(id) {
    getVote1GenderData(id);
    getVote1AgeData(id);
    getVote1EducationData(id);
    getVote1ProvincesData(id);
}

function getVote1GenderData(id) {
    genderVotesData = new google.visualization.DataTable();
    genderVotesData.addColumn('string', 'Gender');
    genderVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetGenderVote1Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                genderVotesData.addRow([data[i].Gender, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'animation': { startup: true, duration: 2000, easing: 'out' }
            }
            drawChart('genderChart', 'GenderChart', genderVotesData, options);
        });
}

function getVote1AgeData(id) {
    ageVotesData = new google.visualization.DataTable();
    ageVotesData.addColumn('string', 'AgeRange');
    ageVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetAgeVote1Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                ageVotesData.addRow([data[i].AgeRange, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'legend': { position: 'none' },
                'animation': { startup: true, duration: 2000, easing: 'out' }
            };
            drawChart('ageChart', 'AgeChart', ageVotesData, options);
        });
}

function getVote1EducationData(id) {
    educationVotesData = new google.visualization.DataTable();
    educationVotesData.addColumn('string', 'EducationName');
    educationVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetEducationVote1Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                educationVotesData.addRow([data[i].EducationName, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'legend': { position: 'none' },
                'animation': { startup: true, duration: 2000, easing: 'out' }
            };
            drawChart('educationChart', 'EducationChart', educationVotesData, options);
        });
}

function getVote1ProvincesData(id) {
    provincesVotesData = new google.visualization.DataTable();
    provincesVotesData.addColumn('string', 'ProvinceName');
    provincesVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetProvinceVote1Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                provincesVotesData.addRow([data[i].ProvinceName, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'legend': { position: 'none' },
                'animation': { startup: true, duration: 2000, easing: 'out' }
            };
            drawChart('provincesChart', 'ProvincesChart', provincesVotesData, options);
        });
}

function getVote2Data(id) {
    getVote2GenderData(id);
    getVote2AgeData(id);
    getVote2EducationData(id);
    getVote2ProvincesData(id);
}

function getVote2GenderData(id) {
    genderVotesData = new google.visualization.DataTable();
    genderVotesData.addColumn('string', 'Gender');
    genderVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetGenderVote2Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                genderVotesData.addRow([data[i].Gender, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'animation': { startup: true, duration: 2000, easing: 'out' }
            }
            drawChart('genderChart', 'GenderChart', genderVotesData, options);
        });
}

function getVote2AgeData(id) {
    ageVotesData = new google.visualization.DataTable();
    ageVotesData.addColumn('string', 'AgeRange');
    ageVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetAgeVote2Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                ageVotesData.addRow([data[i].AgeRange, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'legend': { position: 'none' },
                'animation': { startup: true, duration: 2000, easing: 'out' }
            };
            drawChart('ageChart', 'AgeChart', ageVotesData, options);
        });
}

function getVote2EducationData(id) {
    educationVotesData = new google.visualization.DataTable();
    educationVotesData.addColumn('string', 'EducationName');
    educationVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetEducationVote2Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                educationVotesData.addRow([data[i].EducationName, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'legend': { position: 'none' },
                'animation': { startup: true, duration: 2000, easing: 'out' }
            };
            drawChart('educationChart', 'EducationChart', educationVotesData, options);
        });
}

function getVote2ProvincesData(id) {
    provincesVotesData = new google.visualization.DataTable();
    provincesVotesData.addColumn('string', 'ProvinceName');
    provincesVotesData.addColumn('number', 'Votes');

    $.get('/Questionnaire/GetProvinceVote2Data', { id: id },
        function (data) {
            for (var i = 0; i < data.length; i++) {
                provincesVotesData.addRow([data[i].ProvinceName, data[i].Votes]);
            }
            var options = {
                'height': 350,
                'legend': { position: 'none' },
                'animation': { startup: true, duration: 2000, easing: 'out' }
            };
            drawChart('provincesChart', 'ProvincesChart', provincesVotesData, options);
        });
}

function drawChart(chartType, containerId, dataTable, options) {
    var containerDiv = document.getElementById(containerId);
    var chart = false;
    if (chartType.toUpperCase() == 'GENDERCHART') {
        chart = new google.visualization.PieChart(containerDiv, options);
        genderChart = chart;
    } else if (chartType.toUpperCase() == 'AGECHART') {
        chart = new google.visualization.ColumnChart(containerDiv, options);
        ageChart = chart;
    } else if (chartType.toUpperCase() == 'EDUCATIONCHART') {
        chart = new google.visualization.ColumnChart(containerDiv, options);
        educationChart = chart;
    } else if (chartType.toUpperCase() == 'PROVINCESCHART') {
        chart = new google.visualization.BarChart(containerDiv, options);
        provinceChart = chart;
    }
    if (chart == false) {
        return false;
    }
    
    chart.draw(dataTable, options);
}
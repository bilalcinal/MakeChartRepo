$(document).ready(function () {
    $('#loadObjectsBtn').on('click', function (event) {
        event.preventDefault();

        let dbConnection = {
            server: $('#server').val(),
            database: $('#database').val(),
            username: $('#username').val(),
            password: $('#password').val()
        };

        console.log('Sending AJAX request to get database objects');
        console.log('Connection details:', dbConnection);

        $.ajax({
            url: 'https://localhost:5001/api/Chart/get-database-objects',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dbConnection),
            success: function (data) {
                console.log('Received data:', data);
                let objectSelect = $('#objectName');
                objectSelect.empty();
                objectSelect.append('<option value="">Select an object</option>');
                data.forEach(function (object) {
                    objectSelect.append('<option value="' + object + '">' + object + '</option>');
                });
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                console.error('Status:', status);
                console.error('Response:', xhr.responseText);
                alert('Failed to load database objects. Please check your connection details.');
            }
        });
    });

    $('#chartForm').on('submit', function (event) {
        event.preventDefault();

        let dbConnection = {
            server: $('#server').val(),
            database: $('#database').val(),
            username: $('#username').val(),
            password: $('#password').val()
        };

        let objectName = $('#objectName').val();

        console.log('Sending AJAX request to get dataset');
        console.log('Connection details:', dbConnection);
        console.log('Object name:', objectName);

        $.ajax({
            url: 'https://localhost:5001/api/Chart/get-dataset?tableName=' + encodeURIComponent(objectName),
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dbConnection),
            success: function (data) {
                console.log('Received data:', data);
                let ctx = $('#myChart')[0].getContext('2d');
                let chartType = $('#chartType').val();

                new Chart(ctx, {
                    type: chartType,
                    data: {
                        labels: data.labels,
                        datasets: [{
                            label: 'Dataset',
                            data: data.data,
                            backgroundColor: 'rgba(75, 192, 192, 0.2)',
                            borderColor: 'rgba(75, 192, 192, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                console.error('Status:', status);
                console.error('Response:', xhr.responseText);
                alert('Failed to fetch data. Please check your connection details and object name.');
            }
        });
    });
});
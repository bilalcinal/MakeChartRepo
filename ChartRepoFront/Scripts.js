$(document).ready(function () {
    let chart; // Chart.js nesnesi için bir değişken oluşturun

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

    $('#objectName').on('change', function () {
        let dbConnection = {
            server: $('#server').val(),
            database: $('#database').val(),
            username: $('#username').val(),
            password: $('#password').val()
        };

        let objectName = $('#objectName').val();

        console.log('Sending AJAX request to get columns');
        console.log('Connection details:', dbConnection);
        console.log('Object name:', objectName);

        $.ajax({
            url: `https://localhost:5001/api/Chart/get-columns?tableName=${encodeURIComponent(objectName)}`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dbConnection),
            success: function (data) {
                console.log('Received columns:', data);
                let columnsDiv = $('#columns');
                columnsDiv.empty();
                data.forEach(function (column) {
                    columnsDiv.append(`<div class="form-check">
                        <input class="form-check-input" type="checkbox" value="${column}" id="${column}" checked>
                        <label class="form-check-label" for="${column}">
                            ${column}
                        </label>
                    </div>`);
                });
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                console.error('Status:', status);
                console.error('Response:', xhr.responseText);
                alert('Failed to load columns. Please check your connection details.');
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
        let selectedColumns = $('#columns input:checked').map(function () {
            return $(this).val();
        }).get();
        let chartType = $('#chartType').val();

        console.log('Sending AJAX request to get dataset');
        console.log('Connection details:', dbConnection);
        console.log('Object name:', objectName);
        console.log('Selected columns:', selectedColumns);

        $.ajax({
            url: `https://localhost:5001/api/Chart/get-dataset?tableName=${encodeURIComponent(objectName)}&columns=${encodeURIComponent(selectedColumns.join(','))}`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dbConnection),
            success: function (data) {
                console.log('Received data:', data);
                let ctx = $('#myChart')[0].getContext('2d');

                if (chart) {
                    chart.destroy();
                }

                chart = new Chart(ctx, {
                    type: chartType,
                    data: {
                        labels: data.labels,
                        datasets: [{
                            label: 'Dataset',
                            data: data.data,
                            backgroundColor: generateRandomColors(data.data.length),
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

    function generateRandomColors(numColors) {
        let colors = [];
        for (let i = 0; i < numColors; i++) {
            colors.push(`rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, 0.2)`);
        }
        return colors;
    }
});

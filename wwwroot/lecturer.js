$(document).ready(function () {
    const lecturerApiUrl = '/api/Lecturer'; // API base URL for lecturer endpoints

    // Submit Lecturer Details
    $('#lecturerForm').on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        const lecturerData = {
            name: $('#lecturerName').val(), // Get lecturer name
            email: $('#lecturerEmail').val(), // Get lecturer email
            department: $('#lecturerDepartment').val() // Get lecturer department
        };

        // AJAX request to submit lecturer details
        $.ajax({
            url: `${lecturerApiUrl}/SubmitLecturer`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(lecturerData), // Convert data to JSON
            success: function (response) {
                alert(response.message); // Show success message
                $('#lecturerForm')[0].reset(); // Clear the form
            },
            error: function (xhr) {
                alert(`Error: ${xhr.responseText}`); // Show error message
            }
        });
    });

    // Submit Claim
    $('#claimForm').on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        const claimData = {
            lecturerId: 1, // Example lecturer ID
            hoursWorked: parseInt($('#hoursWorked').val(), 10),
            hourlyRate: parseFloat($('#hourlyRate').val())
        };

        // AJAX request to submit claim
        $.ajax({
            url: `${lecturerApiUrl}/SubmitClaim`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(claimData),
            success: function (response) {
                alert(response.message); // Show success message
                loadClaims(); // Reload claims
                $('#claimForm')[0].reset(); // Clear the form
            },
            error: function (xhr) {
                alert(`Error: ${xhr.responseText}`); // Show error message
            }
        });
    });

    // Load Claims
    function loadClaims() {
        $.ajax({
            url: `${lecturerApiUrl}/GetAllClaims`,
            type: 'GET',
            success: function (claims) {
                const tbody = $('#claimsTableBody');
                tbody.empty(); // Clear table body

                claims.forEach(c => {
                    const row = `
                        <tr>
                            <td>${c.id}</td>
                            <td>${c.hoursWorked}</td>
                            <td>${c.hourlyRate}</td>
                            <td>${c.totalPayment}</td>
                            <td>${c.status}</td>
                        </tr>`;
                    tbody.append(row); // Add row for each claim
                });
            },
            error: function (xhr) {
                alert(`Error: ${xhr.responseText}`);
            }
        });
    }

    // Initial load of claims on page load
    loadClaims();
});

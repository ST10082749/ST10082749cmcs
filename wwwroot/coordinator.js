$(document).ready(function () {
    const apiUrl = '/api/Coordinator';

    function loadPendingClaims() {
        $.ajax({
            url: `${apiUrl}/GetPendingClaims`,
            type: 'GET',
            success: function (claims) {
                const tbody = $('#pendingClaimsTableBody');
                tbody.empty();
                if (claims.length === 0) {
                    tbody.append('<tr><td colspan="5" class="text-center">No pending claims.</td></tr>');
                } else {
                    claims.forEach(c => {
                        const row = `
                            <tr>
                                <td>${c.id}</td>
                                <td>${c.hoursWorked}</td>
                                <td>${c.hourlyRate}</td>
                                <td>${c.totalPayment}</td>
                                <td>
                                    <button onclick="approveClaim(${c.id})">Approve</button>
                                    <button onclick="rejectClaim(${c.id})">Reject</button>
                                </td>
                            </tr>`;
                        tbody.append(row);
                    });
                }
            },
            error: function (xhr) {
                alert(`Error loading claims: ${xhr.responseText}`);
            }
        });
    }

    window.approveClaim = function (claimId) {
        $.ajax({
            url: `${apiUrl}/ApproveClaim/${claimId}`,
            type: 'PUT',
            success: function () {
                alert('Claim approved successfully!');
                loadPendingClaims();
            },
            error: function (xhr) {
                alert(`Error approving claim: ${xhr.responseText}`);
            }
        });
    };

    window.rejectClaim = function (claimId) {
        $.ajax({
            url: `${apiUrl}/RejectClaim/${claimId}`,
            type: 'PUT',
            success: function () {
                alert('Claim rejected successfully!');
                loadPendingClaims();
            },
            error: function (xhr) {
                alert(`Error rejecting claim: ${xhr.responseText}`);
            }
        });
    };

    loadPendingClaims();
});

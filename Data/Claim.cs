namespace cmcsapp;
public class Claim
{
    public int Id { get; set; }
    public int LecturerId { get; set; }
    public int HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal TotalPayment { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string? Status { get; set; } = "Pending"; 
}

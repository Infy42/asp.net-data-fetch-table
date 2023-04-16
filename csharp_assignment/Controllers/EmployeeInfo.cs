namespace csharp_assignment.Controllers
{
    public class EmployeeInfo                           //klasa koja definise radnika
    {
        public string? EmployeeName { get; set; }       //samo potrebni properities, izostavljeni json podaci: Id, EntryNotes, DeletedOn
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public double TotalHours;                       //properity za potpunih radnih sati
    }
}

namespace DoctorsAppointment;

public record TimeSchedule(DateOnly Day, IEnumerable<Appointment> Appointments); 

public record Appointment(DateTime When, Guid PatientId, string PatientName);
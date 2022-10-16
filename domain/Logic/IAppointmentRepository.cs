namespace domain.Logic;

using Models;

public interface IAppointmentRepository : IRepository<Appointment>
{
  bool CreateAppointment(Appointment appointment);
  IEnumerable<Appointment> GetAppointments(int doctorId);
  IEnumerable<Appointment> GetAppointments(Specialization specialization);
}

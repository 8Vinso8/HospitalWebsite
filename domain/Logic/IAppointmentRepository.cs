namespace domain.Logic;

using Models;

public interface IAppointmentRepository : IRepository<Appointment>
{
  bool CreateAppointment(Appointment appointment);
  IEnumerable<Appointment> GetAppointments(int doctorId);
  IEnumerable<IEnumerable<DateTime>> GetFreeAppointments(int doctorId);
  IEnumerable<IEnumerable<DateTime>> GetFreeAppointments(Specialization specialization);
}

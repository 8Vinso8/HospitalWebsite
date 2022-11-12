namespace domain.Logic;

using Models;

public interface IAppointmentRepository : IRepository<Appointment>
{
  IEnumerable<Appointment> GetAppointments(int doctorId);
  IEnumerable<Appointment> GetFreeAppointments(int doctorId);
  IEnumerable<Appointment> GetFreeAppointments(Specialization specialization);
}
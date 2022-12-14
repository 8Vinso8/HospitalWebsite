namespace domain.Logic;

using Models;

public interface IAppointmentRepository : IRepository<Appointment>
{
  IEnumerable<Appointment> GetAppointments(int doctorId);
  IEnumerable<Appointment> GetFreeAppointments(int doctorId, DateOnly date);
  IEnumerable<Appointment> GetFreeAppointments(Specialization specialization, DateOnly date);
}
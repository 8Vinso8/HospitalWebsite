namespace domain.Logic;

using Models;

public interface IDoctorRepository : IRepository<Doctor>
{
  IEnumerable<Doctor> GetDoctors(Specialization specialization);
}

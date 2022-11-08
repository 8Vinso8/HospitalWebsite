namespace domain.Logic;

using Models;

public interface IDoctorRepository : IRepository<Doctor>
{
  IEnumerable<Doctor> GetAllDoctors();
  Doctor GetDoctor(int id);
  IEnumerable<Doctor> GetDoctor(Specialization specialization);
}

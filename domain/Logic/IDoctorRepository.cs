namespace domain.Logic;

using Models;

public interface IDoctorRepository : IRepository<Doctor>
{
  bool AddDoctor(Doctor doctor);
  bool RemoveDoctor(int id);
  IEnumerable<Doctor> GetAllDoctors();
  Doctor GetDoctor(int id);
  Doctor GetDoctor(Specialization specialization);
}

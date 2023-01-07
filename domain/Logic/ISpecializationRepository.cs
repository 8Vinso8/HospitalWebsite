namespace domain.Logic;

using Models;

public interface ISpecializationRepository : IRepository<Specialization>
{
  public Specialization? GetItem(string name);
  bool IsSpecializationExists(string name);
  bool IsSpecializationExists(int id);
  bool IsSpecializationExists(Specialization specialization);
}
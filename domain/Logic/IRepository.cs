namespace domain.Logic;

public interface IRepository<T> where T : class
{
  IEnumerable<T> GetAll();
  bool Create(T item);
  bool Update(T item);
  bool Delete(int id);
  void Save();
}
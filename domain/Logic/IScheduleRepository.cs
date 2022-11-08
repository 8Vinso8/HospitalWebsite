namespace domain.Logic;

using Models;

public interface IScheduleRepository : IRepository<Schedule>
{
  IEnumerable<Schedule> GetSchedule(Doctor doctor);
}

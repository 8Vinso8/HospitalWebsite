namespace domain.Logic;

using Models;

public interface IScheduleRepository
{
  IEnumerable<Schedule> GetSchedule(Doctor doctor);
  bool CreateSchedule(Doctor doctor, Schedule schedule);
  bool UpdateSchedule(Doctor doctor, Schedule schedule);
}

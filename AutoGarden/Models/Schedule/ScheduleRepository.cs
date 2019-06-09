namespace AutoGarden.Models.Schedule
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    using Utility;

    public class ScheduleRepository : IRepository<Schedule>
    {
        private readonly List<Schedule> schedules;

        private int idIncrementation;

        public ScheduleRepository()
        {
            schedules = new List<Schedule>();
        }

        public void Delete(int id)
        {
            Schedule schedule = schedules.First(s => s.Id == id);
            schedules.Remove(schedule);
        }

        public IEnumerable<Schedule> GetAll()
        {
            return schedules;
        }

        public Maybe<Schedule> GetOne(int id)
        {
            Schedule schedule = schedules.FirstOrDefault(x => x.Id == id);
            if (schedule == null)
            {
                return new Maybe<Schedule>();
            }

            return new Maybe<Schedule>(schedule);
        }

        public void Update(Schedule schedule)
        {
            Schedule foundSchedule = schedules.Find(s => s.Id == schedule.Id);
            foundSchedule = schedule;
        }

        public int Add(Schedule schedule)
        {
            if (schedules.All(x => x.Id != schedule.Id))
            {
                idIncrementation++;
                schedule.Id = idIncrementation;
                schedules.Add(schedule);
            }

            return schedule.Id;
        }
    }
}
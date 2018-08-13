namespace AutoGarden.Models.Schedule
{
    using System;
    using System.Collections.Generic;

    using AutoGarden.Models.Interfaces;

    public class ScheduleRepository : IRepository<Schedule>
    {
        public List<Schedule> GetAll()
        {
            return new List<Schedule>
                       {
                           CreateSchedule("Watering Plants", DateTime.Now, DateTime.Now, ScheduleFrequency.Recurring),
                           CreateSchedule(
                               "Take Pictures of Plants",
                               DateTime.MinValue,
                               DateTime.MaxValue,
                               ScheduleFrequency.Singular)
                       };
        }

        private Schedule CreateSchedule(
            string description,
            DateTime startDate,
            DateTime stopDate,
            ScheduleFrequency frequency)
        {
            return new Schedule
                       {
                           Description = description, StartDate = startDate, StopDate = stopDate,
                           ScheduleFrequency = frequency
                       };
        }
    }
}
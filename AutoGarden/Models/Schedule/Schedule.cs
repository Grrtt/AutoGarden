namespace AutoGarden.Models.Schedule
{
    using System;

    public class Schedule
    {
        public string Description { get; set; }

        public bool Enabled { get; set; }

        public int Id { get; set; }

        public ScheduleFrequency ScheduleFrequency { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StopDate { get; set; }
    }
}
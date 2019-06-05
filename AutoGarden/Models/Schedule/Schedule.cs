namespace AutoGarden.Models.Schedule
{
    using System;
    using System.ComponentModel;

    public class Schedule
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public int Id { get; set; }

        [DisplayName("Frequency")]
        public string Recurrence { get; set; }

        [DisplayName("Stop")]
        public DateTime? StopDate { get; set; }

        public string EndTime { get; set; }

        public string StartTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public DateTime? StartDate { get; set; }

        public string RangeOccurrences { get; set; }
    }
}
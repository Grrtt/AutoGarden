namespace AutoGarden.Api.Contracts.DataObjects
{
    using System;

    public class Schedule
    {
        public string Description { get; set; }

        public bool Enabled { get; set; }

        public DateTime End { get; set; }

        public DateTime Start { get; set; }

        public string Title { get; set; }
    }
}
namespace AutoGarden.Controllers
{
    using System;
    using System.Collections.Generic;

    using AutoGarden.Models.Interfaces;
    using AutoGarden.Models.Schedule;

    using Microsoft.AspNetCore.Mvc;

    using Utility;

    public class ScheduleController : Controller
    {
        private readonly IRepository<Schedule> scheduleRepository;

        public ScheduleController(IRepository<Schedule> scheduleRepository)
        {
            this.scheduleRepository = scheduleRepository;
        }

        public IActionResult Add(Schedule schedule)
        {
            int id = scheduleRepository.Add(schedule);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Maybe<Schedule> schedule = scheduleRepository.GetOne(id);
            return View(schedule.Match(new Schedule { Id = id}, x => x));
        }

        public IActionResult Update(Schedule schedule)
        {
            scheduleRepository.Update(schedule);

            IEnumerable<Schedule> schedules = scheduleRepository.GetAll();
            return View("Index", schedules);
        }
        
        public IActionResult Delete(int id)
        {
            scheduleRepository.Delete(id);

            IEnumerable<Schedule> schedules = scheduleRepository.GetAll();
            return View("Index", schedules);
        }

        public IActionResult Index()
        {
            IEnumerable<Schedule> schedules = scheduleRepository.GetAll();
            return View(schedules);
        }

        [HttpPost]
        public IActionResult PeriodicSequence(string value)
        {
            if (!string.Equals(value, "multiple", StringComparison.CurrentCultureIgnoreCase))
            {
                return new EmptyResult();
            }

            return PartialView("_PeriodicSequence");
        }

        [HttpPost]
        public IActionResult PatternRecurrences()
        {
            return new EmptyResult();
        }
    }
}
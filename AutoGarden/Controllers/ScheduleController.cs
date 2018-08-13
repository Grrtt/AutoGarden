namespace AutoGarden.Controllers
{
    using System.Collections.Generic;

    using AutoGarden.Models.Interfaces;
    using AutoGarden.Models.Schedule;

    using Microsoft.AspNetCore.Mvc;

    public class ScheduleController : Controller
    {
        private readonly IRepository<Schedule> scheduleRepository;

        public ScheduleController(IRepository<Schedule> scheduleRepository)
        {
            this.scheduleRepository = scheduleRepository;
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Index()
        {
            List<Schedule> schedules = scheduleRepository.GetAll();
            return View(schedules);
        }
    }
}
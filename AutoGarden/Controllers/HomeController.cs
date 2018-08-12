namespace AutoGarden.Controllers
{
    using System;
    using System.Timers;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            GlobalTime globalTime = new GlobalTime();
            return View(globalTime);
        }
    }

    public class GlobalTime
    {
        public DateTime time;

        private readonly Timer timer;

        public GlobalTime()
        {
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Enabled = true;

            timer.Elapsed += UpdateTime;
        }

        private void UpdateTime(object sender, ElapsedEventArgs e)
        {
            time = DateTime.Now;
        }
    }
}
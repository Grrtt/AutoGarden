﻿using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AutoGarden
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using Models.Interfaces;
    using Models.Schedule;

    using Quartz;
    using Quartz.Impl;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes => routes.MapRoute("default", "{controller=Schedule}/{action=Index}/{id?}"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler().Result;
            IJobDetail job = JobBuilder.Create<Job>().WithIdentity(new JobKey("potato")).Build();
            ITrigger trigger = TriggerBuilder.Create().WithSchedule(
                CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(
                    23,
                    0,
                    DayOfWeek.Monday,
                    DayOfWeek.Saturday)).Build();
            Task<DateTimeOffset> offset = scheduler.ScheduleJob(job, trigger);
            scheduler.Start();

            IWindsorContainer container = CreateWindsorRegistration();
            return services.AddWindsor(container);
        }

        private IWindsorContainer CreateWindsorRegistration()
        {
            WindsorContainer container = new WindsorContainer();

            container.Register(Component.For<IRepository<Schedule>>().ImplementedBy<ScheduleRepository>()
                .LifestyleSingleton());

            return container;
        }
    }

    public class Job : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
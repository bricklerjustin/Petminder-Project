using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Shiny;
using Shiny.Jobs;

namespace PetminderApp
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.UseNotifications(true);

            //var ReminderJob = new JobInfo(typeof(NotificationJob), nameof(NotificationJob));
            //ReminderJob.Repeat = true;
            //ReminderJob.PeriodicTime = TimeSpan.FromMinutes(15);
            //services.RegisterJob(ReminderJob);
        }
    }
}

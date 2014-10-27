using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.ClientScheduler
{
    using System.Collections.Specialized;
    using Quartz;
    using Quartz.Impl;

    public sealed class QuartzScheduler
    {
        private static QuartzScheduler instance = null;
        
        private static readonly object padlock = new object();

        private readonly ISchedulerFactory SchedulerFactory = null;
        private readonly IScheduler Scheduler = null;

        QuartzScheduler()
        {
            this.SchedulerFactory = new StdSchedulerFactory();
            this.Scheduler = this.SchedulerFactory.GetScheduler();
        }

        public static IScheduler Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new QuartzScheduler();
                    }
                    return instance.Scheduler;
                }
            }
        }
    }
}

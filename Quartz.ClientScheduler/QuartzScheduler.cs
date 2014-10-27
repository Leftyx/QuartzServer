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

    public class QuartzScheduler
    {
        public readonly IScheduler Instance;
        public string Address { get; private set; }
        public string JobName { get; set; }
        public string JobGroup { get; set; }
        public int Priority { get; set; }

        private readonly ISchedulerFactory _schedulerFactory = null;

        public QuartzScheduler(string server, int port, string scheduler)
        {
            Address = string.Format("tcp://{0}:{1}/{2}", server, port, scheduler);
            _schedulerFactory = new StdSchedulerFactory(GetProperties(Address));

            try
            {
                Instance = _schedulerFactory.GetScheduler();

                if (!Instance.IsStarted)
                {
                    Instance.Start();
                }
            }
            catch (SchedulerException ex)
            {
                throw new Exception(string.Format("Failed: {0}", ex.Message));
            }
        }

        private static NameValueCollection GetProperties(string address)
        {
            var properties = new NameValueCollection();
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.scheduler.instanceName"] = "RemoteClient";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.scheduler.proxy.address"] = address;
            return properties;
        }

        public IScheduler GetScheduler()
        {
            return Instance;
        }
    }
}

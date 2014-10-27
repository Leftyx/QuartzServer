using Common.Logging;
using Quartz;
using System;
using System.Threading;

namespace Quartz.Jobs
{
    /// <summary>
    /// A sample job that just prints info on console for demostration purposes.
    /// </summary>
    public class SampleJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SampleJob));

        /// <summary>
        /// Called by the <see cref="IScheduler" /> when a <see cref="ITrigger" />
        /// fires that is associated with the <see cref="IJob" />.
        /// </summary>
        /// <remarks>
        /// The implementation may wish to set a  result object on the 
        /// JobExecutionContext before this method exits.  The result itself
        /// is meaningless to Quartz, but may be informative to 
        /// <see cref="IJobListener" />s or 
        /// <see cref="ITriggerListener" />s that are watching the job's 
        /// execution.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        public void Execute(Quartz.IJobExecutionContext context)
        {
            string message = string.Format("Job Key:{0} - Trigger Key:{1} - Start Time:{2} - Schedule Fire Time: {3}", context.JobDetail.Key, context.Trigger.Key, context.Trigger.StartTimeUtc, context.ScheduledFireTimeUtc);

            logger.Info("SampleJob running...");
            logger.Info(message);
            logger.Info("SampleJob run finished.");


            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}

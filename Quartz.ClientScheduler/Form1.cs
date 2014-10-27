using Quartz.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quartz.ClientScheduler
{
    public partial class Form1 : Form
    {
        private static IScheduler Scheduler = null;

        public Form1()
        {
            InitializeComponent();
            Scheduler = new QuartzScheduler("127.0.0.1", 555, "QuartzScheduler").Instance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IJobDetail postbagjob = null;
            ITrigger postbagJobTrigger = null;
            try
            {
                postbagjob = JobBuilder.Create<SampleJob>()
                    .WithIdentity("SampleJob", "QUARTZGROUP")
                    .Build();
                postbagJobTrigger = (ISimpleTrigger)TriggerBuilder.Create()
                    .WithIdentity("SampleTrigger", "QUARTZGROUP")
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())
                    .StartNow()
                    .Build();

                Scheduler.ScheduleJob(postbagjob, postbagJobTrigger);
            }
            catch (SchedulerException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Scheduler.Standby();
        }
    }
}

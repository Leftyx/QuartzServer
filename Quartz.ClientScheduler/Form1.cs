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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IScheduler sched = new QuartzScheduler("127.0.0.1", 555, "QuartzScheduler").Instance;
            IJobDetail postbagjob = null;
            ITrigger postbagJobTrigger = null;
            try
            {
                postbagjob = JobBuilder.Create<SampleJob>()
                    .WithIdentity("SampleJob", "QUARTZGROUP")
                    .Build();
                postbagJobTrigger = (ISimpleTrigger)TriggerBuilder.Create()
                    .WithIdentity("SampleTrigger", "QUARTZGROUP")
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                    .StartNow()
                    .Build();

                sched.ScheduleJob(postbagjob, postbagJobTrigger);
            }
            catch (SchedulerException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

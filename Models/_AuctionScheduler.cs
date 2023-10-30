using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
    public class _AuctionScheduler
    {
        private IScheduler _scheduler;

        public _AuctionScheduler()
        {
            // Inizializza un'istanza di scheduler
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result; // Otteniamo il scheduler

            // Avvia il scheduler
            _scheduler.Start().Wait();
        }

        public void PianificaChiusuraAsta(DateTime dataScadenzaAsta, int auctionId)
        {
            IJobDetail job = JobBuilder.Create<_CloseAuctionJob>()
                .UsingJobData("AuctionId", auctionId)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(dataScadenzaAsta)
                .Build();

            _scheduler.ScheduleJob(job, trigger).Wait();
        }
    }
}
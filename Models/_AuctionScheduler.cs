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
			// Initialize a scheduler instance
			ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
			_scheduler = schedulerFactory.GetScheduler().Result;
			_scheduler.Start().Wait();
		}

		public void PlanAuctionClosing(DateTime dataExpireAction, int auctionId)
		{
			IJobDetail job = JobBuilder.Create<_CloseAuctionJob>()
				.UsingJobData("AuctionId", auctionId)
				.Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartAt(dataExpireAction)
				.Build();

			_scheduler.ScheduleJob(job, trigger).Wait();
		}
	}
}
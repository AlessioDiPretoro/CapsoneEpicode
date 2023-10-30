using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;

namespace Stones.Models
{
    public class _CloseAuctionJob : IJob
    {
        private readonly ModelDbContext db = new ModelDbContext();

        public Task Execute(IJobExecutionContext context)
        {
            int auctionId = context.JobDetail.JobDataMap.GetInt("AuctionId");
            CloseAuction(auctionId);
            return Task.CompletedTask;
        }

        private void CloseAuction(int auctionId)
        {
            // Trova l'asta nel database per l'ID specificato e chiudila
            var asta = db.AuctionsProducts.FirstOrDefault(a => a.id == auctionId);

            if (asta != null)
            {
                asta.isActive = false;
                db.SaveChanges();
            }
        }
    }
}
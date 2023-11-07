using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly _Mailer _mailer;

        public _CloseAuctionJob()
        {
            _mailer = new _Mailer(); // Inizializzazione di Mailer
        }

        public Task Execute(IJobExecutionContext context)
        {
            int auctionId = context.JobDetail.JobDataMap.GetInt("AuctionId");
            CloseAuction(auctionId);
            return Task.CompletedTask;
        }

        private void CloseAuction(int auctionId)
        {
            // Trova l'asta nel database per l'ID specificato, chiude ed invia la mail al vincitore
            AuctionsProducts auction = db.AuctionsProducts.FirstOrDefault(a => a.id == auctionId);

            if (auction != null)
            {
                if (auction.AuctionsDetails.Count > 0)
                {
                    decimal price = 0;
                    foreach (AuctionsDetails d in auction.AuctionsDetails)
                    {
                        if (d.price > price)
                        {
                            price = d.price;
                            auction.idWinner = d.idUser;
                            auction.idAuctionWinner = d.id;
                        }
                    }
                    auction.endPrice = price;
                    db.SaveChanges();
                    //invia l'email al vincitore dell'asta
                    if (auction.idWinner != null)
                    {
                        Users winner = db.Users.FirstOrDefault(u => u.id == auction.idWinner);
                        _mailer.SendEmail(winner.email, "Ti sei aggiudicato l'asta",
                            $"Complimenti hai vinto l'asta per il prodotto {auction.Product.name}." +
                            $"<br>Ricordati di effettuare il pagamento di {price.ToString("C2")}." +
                            $"<br><br>Puoi verificare le tue aste in attesa di pagamento del menù del tuo profilo nella sezione 'Le tue aste'." +
                            $"<br><br>Mail inviata automaticamente da LePieCreazioni, non rispondere a questa mail." +
                            $"<br><br>&#169; LePieCreazioni." +
                            $"<br><br>&#169; ADP."
                            );
                    }
                }

                auction.isActive = false;
                db.SaveChanges();
                //mette come non disponibile il prodotto acquistato
                Product p = db.Product.Find(auction.idProduct);
                p.isAvaiable = false;
                db.SaveChanges();
            }
        }
    }
}
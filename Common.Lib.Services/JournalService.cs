using Common.Lib.Data.DAL;

using Common.Lib.Entities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Common.Lib.Services
{
    public class JournalService
    {
        private UnitOfWork unitOfWork;

        public JournalService(IConfiguration configuration)
        {
            Configuration = configuration;
            unitOfWork = new UnitOfWork(Configuration);
        }

        private IConfiguration Configuration { get; set; }

        public IQueryable<JournalDetails> GetAll()
        {
            return unitOfWork.JournalDetailsRepository.GetAll();
        }

        public  IQueryable<JournalDetails> GetByCurrency(string currency)
        {
            return unitOfWork.JournalDetailsRepository.GetAll(e => e.CurrencyCode == currency);
        }

        public IQueryable<JournalDetails> GetByStatus(string status)
        {
            return unitOfWork.JournalDetailsRepository.GetAll(e => e.Status == status);
        }

        public IQueryable<JournalDetails> GetByDateRange(DateTime fromdate, DateTime todate)
        {
            return unitOfWork.JournalDetailsRepository
                .GetAll(e => e.TransactionDate >= fromdate && e.TransactionDate <= todate);
        }
    }
}

using Common.Lib.Contract;
using Common.Lib.Data.Context;
using Common.Lib.Data.Repository;
using Microsoft.Extensions.Configuration;
using System;

namespace Common.Lib.Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IConfiguration Configuration { get; set; }
        private PITJOURNALContext _context;

        public UnitOfWork(IConfiguration configuration)
        {
            Configuration = configuration;
            _context = new PITJOURNALContext(Configuration);
        }
        private IJournalDetailsRepository journalDetailsRepository;
        public IJournalDetailsRepository JournalDetailsRepository
        {
            get { return journalDetailsRepository ?? (journalDetailsRepository = new JournalDetailsRepository(_context)); }
        }

        internal int innerRowCount = 0;
        public bool ExecuteTransaction(Action executeCommand)
        {
            this.BeginSave();
            executeCommand();
            this.Commit();
            return true;
        }

        public void BeginSave()
        {
            innerRowCount++;
        }

        public void Commit()
        {
            innerRowCount--;
            _context.SaveChanges();
        }

    }
}

using Common.Lib.Contract;
using Common.Lib.Data.Models;
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

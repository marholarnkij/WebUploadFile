using Common.Lib.Entities.Models;
using Common.Lib.Contract;
using Microsoft.EntityFrameworkCore;

namespace Common.Lib.Data.Repository
{
    public class JournalDetailsRepository : Repository<JournalDetails>, IJournalDetailsRepository
    {
        public JournalDetailsRepository(DbContext context) : base(context)
        {

        }
    }
}

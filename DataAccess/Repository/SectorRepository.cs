using DataAccess.Repository.IRepository;
using DataAccess.Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SectorRepository : Repository<Sector> , ISectorRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SectorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}

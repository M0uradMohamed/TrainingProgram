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
    public class ImplementationTypeRepository : Repository<ImplementationType> , IImplementationTypeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ImplementationTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}

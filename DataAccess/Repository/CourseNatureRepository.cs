using DataAccess.Repository.IRepository;
using ETickets.Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CourseNatureRepository : Repository<CourseNature> , ICourseNatureRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CourseNatureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}

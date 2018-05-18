using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Data.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext dataContext;
        public UnitOfWork(ApplicationDbContext DataContext)
        {
            dataContext = DataContext;
        }
        public int Commit()
        {
            return dataContext.SaveChanges();
        }
    }
}

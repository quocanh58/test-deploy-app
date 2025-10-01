using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ServiceBase<T> : GenericRepository<T> where T : class
    {
        // Change DbContext
        public ServiceBase(WatercolorsPainting2024DbContext context) : base(context)
        {
        }
    }
}

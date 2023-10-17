using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(SecondHandDbContext secondHandDbContext) : base(secondHandDbContext)
        {
        }
    }
}

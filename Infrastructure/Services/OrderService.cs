using Application;
using Application.IServices;
using Domain.Models;
using Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

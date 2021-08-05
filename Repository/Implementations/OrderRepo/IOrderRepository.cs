﻿using Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations.OrderRepo
{
    interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();

        Task<Order> GetOrder(int id);

        void Create(Order entity);

        void Update(Order entity);

        void Delete(Task<Order> entity);
    }
}
﻿using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;
using NorthWind.Entities.Specifications;
using NorthWind.Repositories.EFCore.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Repositories.EFCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly NorthWindContext Context;
        public OrderRepository(NorthWindContext context) => Context = context;
        public void Create(Order order)
        {
            Context.Add(order);
        }

        public IEnumerable<Order> GeOrderBySpecification(Specification<Order> specification)
        {
            var ExpressionDelegate = specification.Expression.Compile();
            return Context.Orders.Where(ExpressionDelegate);
        }
    }
}

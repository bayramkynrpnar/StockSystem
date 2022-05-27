﻿
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IStockOrdersRepository : IEntityRepository<StockOrders>
    {
        Task <List<Report>> GetReportDto();
    }
}
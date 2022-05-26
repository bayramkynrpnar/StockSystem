using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Stocks.Commands
{
    /// <summary>
    /// Belirlenen ürünü, belirlenen depodan istenen başka bir depoya taşınmasını sağlar.
    /// </summary>
    public class StockTransferCommand : IRequest<IResult>
    {

        public int CardId { get; set; }
        public int StorageId { get; set; }
        public int ToStorageId { get; set; }


        public class StockTransferHandler : IRequestHandler<StockTransferCommand, IResult>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IMediator _mediator;
            private readonly IStorageRepository _storageRepository;
            private readonly IStockOrdersRepository _stockOrdersRepository;
            public StockTransferHandler(IStockRepository stockRepository, IMediator mediator, IStorageRepository storageRepository, IStockOrdersRepository stockOrdersRepository)
            {
                _stockRepository = stockRepository;
                _storageRepository = storageRepository;
                _stockOrdersRepository = stockOrdersRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(StockTransferCommand request, CancellationToken cancellationToken)
            {
                var storage = _storageRepository.GetById(request.ToStorageId);
                if (storage == null) return new SuccessResult("Gönderilecek depo bulunamadı");

                var oldStock = _stockRepository.Get(x => x.StorageId == request.StorageId);
                if (oldStock == null) return new SuccessResult("Stock bulunamadı");


                var newStock = new Stock
                {
                    CardId = request.CardId,
                    Quantity = oldStock.Quantity,
                    Cost = oldStock.Cost,
                    StorageId = request.ToStorageId
                };


                _stockRepository.Update(newStock);
                _stockOrdersRepository.Add(new StockOrders()
                {
                    CardId = request.CardId,
                    OrderBy = storage.Name,
                    OrderDate = DateTime.Now,
                    OrderType = Entities.Enum.OrderType.Transfer
                });
                await _stockRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}

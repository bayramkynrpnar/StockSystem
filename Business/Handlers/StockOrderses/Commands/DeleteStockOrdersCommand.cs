
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.StockOrderses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteStockOrdersCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteStockOrdersCommandHandler : IRequestHandler<DeleteStockOrdersCommand, IResult>
        {
            private readonly IStockOrdersRepository _stockOrdersRepository;
            private readonly IMediator _mediator;

            public DeleteStockOrdersCommandHandler(IStockOrdersRepository stockOrdersRepository, IMediator mediator)
            {
                _stockOrdersRepository = stockOrdersRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteStockOrdersCommand request, CancellationToken cancellationToken)
            {
                var stockOrdersToDelete = _stockOrdersRepository.Get(p => p.Id == request.Id);

                _stockOrdersRepository.Delete(stockOrdersToDelete);
                await _stockOrdersRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}


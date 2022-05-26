
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.StockOrderses.ValidationRules;


namespace Business.Handlers.StockOrderses.Commands
{


    public class UpdateStockOrdersCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string OrderBy { get; set; }

        public class UpdateStockOrdersCommandHandler : IRequestHandler<UpdateStockOrdersCommand, IResult>
        {
            private readonly IStockOrdersRepository _stockOrdersRepository;
            private readonly IMediator _mediator;

            public UpdateStockOrdersCommandHandler(IStockOrdersRepository stockOrdersRepository, IMediator mediator)
            {
                _stockOrdersRepository = stockOrdersRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateStockOrdersValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateStockOrdersCommand request, CancellationToken cancellationToken)
            {
                var isThereStockOrdersRecord = await _stockOrdersRepository.GetAsync(u => u.Id == request.Id);


                isThereStockOrdersRecord.CardId = request.CardId;
                isThereStockOrdersRecord.OrderDate = request.OrderDate;
                isThereStockOrdersRecord.OrderBy = request.OrderBy;


                _stockOrdersRepository.Update(isThereStockOrdersRecord);
                await _stockOrdersRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}


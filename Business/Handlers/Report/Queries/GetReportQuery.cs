using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Report.Queries
{
    public class GetReportQuery : IRequest<IDataResult<IEnumerable<Entities.Dtos.Report>>>
    {
        public class GetReportQueryHandler : IRequestHandler<GetReportQuery,
                IDataResult<IEnumerable<Entities.Dtos.Report>>>
        {
            private readonly IStockOrdersRepository _stockRepository;
            private readonly IMediator _mediator;

            public GetReportQueryHandler(IMediator mediator, IStockOrdersRepository stockRepository)
            {
                _mediator = mediator;
                _stockRepository = stockRepository;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Entities.Dtos.Report>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Entities.Dtos.Report>>(await _stockRepository.GetReportDto());
            }
        }
    }
}

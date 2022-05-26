
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


namespace Business.Handlers.Cards.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCardCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, IResult>
        {
            private readonly ICardRepository _cardRepository;
            private readonly IMediator _mediator;

            public DeleteCardCommandHandler(ICardRepository cardRepository, IMediator mediator)
            {
                _cardRepository = cardRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
            {
                var cardToDelete = _cardRepository.Get(p => p.Id == request.Id);

                _cardRepository.Delete(cardToDelete);
                await _cardRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}


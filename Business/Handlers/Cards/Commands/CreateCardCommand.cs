
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
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Cards.ValidationRules;

namespace Business.Handlers.Cards.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCardCommand : IRequest<IResult>
    {

        public string Model { get; set; }
        public string Name { get; set; }
        public string SerialNo { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }


        public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, IResult>
        {
            private readonly ICardRepository _cardRepository;
            private readonly IMediator _mediator;
            public CreateCardCommandHandler(ICardRepository cardRepository, IMediator mediator)
            {
                _cardRepository = cardRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCardValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCardCommand request, CancellationToken cancellationToken)
            {
                var isThereCardRecord = _cardRepository.Query().Any(u => u.Model == request.Model);

                if (isThereCardRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCard = new Card
                {
                    Model = request.Model,
                    Name = request.Name,
                    SerialNo = request.SerialNo,
                    ImageUrl = request.ImageUrl,
                    Status = request.Status,
                    Price = request.Price,

                };

                _cardRepository.Add(addedCard);
                await _cardRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
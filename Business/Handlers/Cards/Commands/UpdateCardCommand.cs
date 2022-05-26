
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
using Business.Handlers.Cards.ValidationRules;


namespace Business.Handlers.Cards.Commands
{


    public class UpdateCardCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string SerialNo { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }

        public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, IResult>
        {
            private readonly ICardRepository _cardRepository;
            private readonly IMediator _mediator;

            public UpdateCardCommandHandler(ICardRepository cardRepository, IMediator mediator)
            {
                _cardRepository = cardRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCardValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
            {
                var isThereCardRecord = await _cardRepository.GetAsync(u => u.Id == request.Id);


                isThereCardRecord.Model = request.Model;
                isThereCardRecord.Name = request.Name;
                isThereCardRecord.SerialNo = request.SerialNo;
                isThereCardRecord.ImageUrl = request.ImageUrl;
                isThereCardRecord.Status = request.Status;
                isThereCardRecord.Price = request.Price;


                _cardRepository.Update(isThereCardRecord);
                await _cardRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}


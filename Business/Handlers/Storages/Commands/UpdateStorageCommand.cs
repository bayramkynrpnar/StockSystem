
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
using Business.Handlers.Storages.ValidationRules;


namespace Business.Handlers.Storages.Commands
{


    public class UpdateStorageCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public class UpdateStorageCommandHandler : IRequestHandler<UpdateStorageCommand, IResult>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public UpdateStorageCommandHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateStorageValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
            {
                var isThereStorageRecord = await _storageRepository.GetAsync(u => u.Id == request.Id);


                isThereStorageRecord.Name = request.Name;
                isThereStorageRecord.Status = request.Status;


                _storageRepository.Update(isThereStorageRecord);
                await _storageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}


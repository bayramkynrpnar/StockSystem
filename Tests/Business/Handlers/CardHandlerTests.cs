
using Business.Handlers.Cards.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Cards.Queries.GetCardQuery;
using Entities.Concrete;
using static Business.Handlers.Cards.Queries.GetCardsQuery;
using static Business.Handlers.Cards.Commands.CreateCardCommand;
using Business.Handlers.Cards.Commands;
using Business.Constants;
using static Business.Handlers.Cards.Commands.UpdateCardCommand;
using static Business.Handlers.Cards.Commands.DeleteCardCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CardHandlerTests
    {
        Mock<ICardRepository> _cardRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _cardRepository = new Mock<ICardRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Card_GetQuery_Success()
        {
            //Arrange
            var query = new GetCardQuery();

            _cardRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Card, bool>>>())).ReturnsAsync(new Card()
//propertyler buraya yazılacak
//{																		
//CardId = 1,
//CardName = "Test"
//}
);

            var handler = new GetCardQueryHandler(_cardRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CardId.Should().Be(1);

        }

        [Test]
        public async Task Card_GetQueries_Success()
        {
            //Arrange
            var query = new GetCardsQuery();

            _cardRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Card, bool>>>()))
                        .ReturnsAsync(new List<Card> { new Card() { /*TODO:propertyler buraya yazılacak CardId = 1, CardName = "test"*/ } });

            var handler = new GetCardsQueryHandler(_cardRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Card>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Card_CreateCommand_Success()
        {
            Card rt = null;
            //Arrange
            var command = new CreateCardCommand();
            //propertyler buraya yazılacak
            //command.CardName = "deneme";

            _cardRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Card, bool>>>()))
                        .ReturnsAsync(rt);

            _cardRepository.Setup(x => x.Add(It.IsAny<Card>())).Returns(new Card());

            var handler = new CreateCardCommandHandler(_cardRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cardRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Card_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCardCommand();
            //propertyler buraya yazılacak 
            //command.CardName = "test";

            _cardRepository.Setup(x => x.Query())
                                           .Returns(new List<Card> { new Card() { /*TODO:propertyler buraya yazılacak CardId = 1, CardName = "test"*/ } }.AsQueryable());

            _cardRepository.Setup(x => x.Add(It.IsAny<Card>())).Returns(new Card());

            var handler = new CreateCardCommandHandler(_cardRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Card_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCardCommand();
            //command.CardName = "test";

            _cardRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Card, bool>>>()))
                        .ReturnsAsync(new Card() { /*TODO:propertyler buraya yazılacak CardId = 1, CardName = "deneme"*/ });

            _cardRepository.Setup(x => x.Update(It.IsAny<Card>())).Returns(new Card());

            var handler = new UpdateCardCommandHandler(_cardRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cardRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Card_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCardCommand();

            _cardRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Card, bool>>>()))
                        .ReturnsAsync(new Card() { /*TODO:propertyler buraya yazılacak CardId = 1, CardName = "deneme"*/});

            _cardRepository.Setup(x => x.Delete(It.IsAny<Card>()));

            var handler = new DeleteCardCommandHandler(_cardRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _cardRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}


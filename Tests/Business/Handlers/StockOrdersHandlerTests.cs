
using Business.Handlers.StockOrderses.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.StockOrderses.Queries.GetStockOrdersQuery;
using Entities.Concrete;
using static Business.Handlers.StockOrderses.Queries.GetStockOrdersesQuery;
using static Business.Handlers.StockOrderses.Commands.CreateStockOrdersCommand;
using Business.Handlers.StockOrderses.Commands;
using Business.Constants;
using static Business.Handlers.StockOrderses.Commands.UpdateStockOrdersCommand;
using static Business.Handlers.StockOrderses.Commands.DeleteStockOrdersCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class StockOrdersHandlerTests
    {
        Mock<IStockOrdersRepository> _stockOrdersRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _stockOrdersRepository = new Mock<IStockOrdersRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task StockOrders_GetQuery_Success()
        {
            //Arrange
            var query = new GetStockOrdersQuery();

            _stockOrdersRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockOrders, bool>>>())).ReturnsAsync(new StockOrders()
//propertyler buraya yazılacak
//{																		
//StockOrdersId = 1,
//StockOrdersName = "Test"
//}
);

            var handler = new GetStockOrdersQueryHandler(_stockOrdersRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.StockOrdersId.Should().Be(1);

        }

        [Test]
        public async Task StockOrders_GetQueries_Success()
        {
            //Arrange
            var query = new GetStockOrdersesQuery();

            _stockOrdersRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<StockOrders, bool>>>()))
                        .ReturnsAsync(new List<StockOrders> { new StockOrders() { /*TODO:propertyler buraya yazılacak StockOrdersId = 1, StockOrdersName = "test"*/ } });

            var handler = new GetStockOrdersesQueryHandler(_stockOrdersRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<StockOrders>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task StockOrders_CreateCommand_Success()
        {
            StockOrders rt = null;
            //Arrange
            var command = new CreateStockOrdersCommand();
            //propertyler buraya yazılacak
            //command.StockOrdersName = "deneme";

            _stockOrdersRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockOrders, bool>>>()))
                        .ReturnsAsync(rt);

            _stockOrdersRepository.Setup(x => x.Add(It.IsAny<StockOrders>())).Returns(new StockOrders());

            var handler = new CreateStockOrdersCommandHandler(_stockOrdersRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _stockOrdersRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task StockOrders_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateStockOrdersCommand();
            //propertyler buraya yazılacak 
            //command.StockOrdersName = "test";

            _stockOrdersRepository.Setup(x => x.Query())
                                           .Returns(new List<StockOrders> { new StockOrders() { /*TODO:propertyler buraya yazılacak StockOrdersId = 1, StockOrdersName = "test"*/ } }.AsQueryable());

            _stockOrdersRepository.Setup(x => x.Add(It.IsAny<StockOrders>())).Returns(new StockOrders());

            var handler = new CreateStockOrdersCommandHandler(_stockOrdersRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task StockOrders_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateStockOrdersCommand();
            //command.StockOrdersName = "test";

            _stockOrdersRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockOrders, bool>>>()))
                        .ReturnsAsync(new StockOrders() { /*TODO:propertyler buraya yazılacak StockOrdersId = 1, StockOrdersName = "deneme"*/ });

            _stockOrdersRepository.Setup(x => x.Update(It.IsAny<StockOrders>())).Returns(new StockOrders());

            var handler = new UpdateStockOrdersCommandHandler(_stockOrdersRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _stockOrdersRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task StockOrders_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteStockOrdersCommand();

            _stockOrdersRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<StockOrders, bool>>>()))
                        .ReturnsAsync(new StockOrders() { /*TODO:propertyler buraya yazılacak StockOrdersId = 1, StockOrdersName = "deneme"*/});

            _stockOrdersRepository.Setup(x => x.Delete(It.IsAny<StockOrders>()));

            var handler = new DeleteStockOrdersCommandHandler(_stockOrdersRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _stockOrdersRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}


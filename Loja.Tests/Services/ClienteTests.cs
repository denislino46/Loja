using FluentValidation;
using FluentValidation.Results;
using Loja.Application.Contract.Cliente;
using Loja.Application.Entities;
using Loja.Application.Interfaces;
using Loja.Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Loja.Tests.Services
{
    [TestClass]
    public class ClienteTests
    {
        private readonly Mock<IClienteRepository> _repositoryMock;
        private readonly Mock<ClienteValidatorRequest> _validatorMock;
        private Guid _id = Guid.NewGuid();
        public ClienteTests()
        {
            _repositoryMock = new Mock<IClienteRepository>() { CallBase = true };
            _validatorMock = new Mock<ClienteValidatorRequest>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task AtualizarAsync_Sucesso()
        {
            ConfigureMock();
            var service = new ClienteService(_repositoryMock.Object, _validatorMock.Object);
            var retorno = await service.AtualizarAsync(ObterMock()).ConfigureAwait(false);

            Assert.IsNotNull(retorno);
            Assert.IsTrue(retorno.Data);
        }

        [TestMethod]
        public async Task InserirAsync_Sucesso()
        {
            ConfigureMock();
            var service = new ClienteService(_repositoryMock.Object, _validatorMock.Object);
            var retorno = await service.InserirAsync(ObterMock()).ConfigureAwait(false);

            Assert.IsNotNull(retorno);
            Assert.AreEqual(_id, retorno.Data.Id);
        }

        [TestMethod]
        public async Task DeletarAsync_Sucesso()
        {
            ConfigureMock();
            var service = new ClienteService(_repositoryMock.Object, _validatorMock.Object);
            var retorno = await service.DeletarAsync(It.IsAny<Guid>()).ConfigureAwait(false);

            Assert.IsNotNull(retorno);
            Assert.IsTrue(retorno);
        }

        [TestMethod]
        public async Task ObterAsync_Sucesso()
        {
            ConfigureMock();
            var service = new ClienteService(_repositoryMock.Object, _validatorMock.Object);
            var retorno = await service.ObterAsync(It.IsAny<Guid>()).ConfigureAwait(false);

            Assert.IsNotNull(retorno);
            Assert.AreEqual(_id, retorno.Data.Id);
        }

        [TestMethod]
        public async Task ListarAsync_Sucesso()
        {
            ConfigureMock();
            var service = new ClienteService(_repositoryMock.Object, _validatorMock.Object);
            var retorno = await service.ListarAsync().ConfigureAwait(false);

            Assert.IsNotNull(retorno);
            Assert.IsTrue(retorno.Data.Any(x => x.Id == _id));
        }

        private ClienteRequest ObterMock()
            => new ClienteRequest
            {
                Id = _id,
                Nome = "Naruto",
                Email = "naruto@gmail.com",
                Aldeia = "Folha"
            };

        private void ConfigureMock()
        {
            _repositoryMock
                .Setup(x => x.ObterAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Cliente { Id = _id });

            _repositoryMock
                .Setup(x => x.ValidarEmailExiste(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            _repositoryMock
                .Setup(x => x.AtualizarAsync(It.IsAny<Cliente>()))
                .ReturnsAsync(true);

            _repositoryMock
                .Setup(x => x.InserirAsync(It.IsAny<Cliente>()))
                .ReturnsAsync(new Cliente { Id = _id });

            _repositoryMock
                .Setup(x => x.DeletarAsync(It.IsAny<Cliente>()))
                .ReturnsAsync(true);
            _repositoryMock
                .Setup(x => x.ListarAsync())
                .ReturnsAsync(new List<Cliente>() { new Cliente { Id = _id } });

            _validatorMock
                .Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<ClienteRequest>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
        }
    }
}

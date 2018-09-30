using System;
using System.Collections.Generic;
using AutoMapper;
using Eventos.IO.Application.Interfaces;
using Eventos.IO.Application.ViewModels;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Eventos.Commands;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Services.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Eventos.IO.Tests.API.Unit_Tests
{
    public class EventosControllerTests
    {
        public EventosController eventosController { get; set; }
        public EventoViewModel eventoViewModel { get; set; }
        public RegistrarEventoCommand eventoCommand { get; set; }
        public Mock<IMapper> mockMapper { get; set; }
        public Mock<IBus> mockBus { get; set; }
        public Mock<IEventoAppService> mockEventoAppService { get; set; }        
        public Mock<DomainNotificationHandler> mockNotification { get; set; }

        public EventosControllerTests()
        {
            mockMapper = new Mock<IMapper>();
            mockBus = new Mock<IBus>();
            mockNotification = new Mock<DomainNotificationHandler>();
            mockEventoAppService = new Mock<IEventoAppService>();

            var mockUser = new Mock<IUser>();
            var mockRepository = new Mock<IEventoRepository>();

            eventoViewModel = new EventoViewModel();

            eventoCommand = new RegistrarEventoCommand("Teste", "", "", DateTime.Now, DateTime.Now.AddDays(1), true,
                0, true, "Teste", Guid.NewGuid(), Guid.NewGuid(),
                new IncluirEnderecoEventoCommand(Guid.NewGuid(), "", "", "", "", "", "", "", Guid.NewGuid()));

            eventosController = new EventosController(
                mockNotification.Object,
                mockUser.Object,
                mockBus.Object,
                mockEventoAppService.Object,
                mockRepository.Object,
                mockMapper.Object);
        }


        // Registrar um evento com sucesso
        // Registrar um evento com falha na viewmodel
        // Registrar um evento com falha na validacao de dominio

        // AAA => Arrange, Act, Assert
        [Fact(DisplayName = "Registrar evento com sucesso")]
        [Trait("Category", "Testes Eventos Controller")]
        public void EventosController_RegistrarEvento_RetornarComSucesso()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<RegistrarEventoCommand>(eventoViewModel)).Returns(eventoCommand);

            // Act
            var result = eventosController.Post(eventoViewModel);

            // Assert
            mockBus.Verify(m => m.SendCommands(eventoCommand), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Registrar evento com erro de ModelState")]
        [Trait("Category", "Testes Eventos Controller")]
        public void EventosController_RegistrarEvento_RetornarComErrosDeModelState()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<RegistrarEventoCommand>(eventoViewModel)).Returns(eventoCommand);
            var notificationList = new List<DomainNotification>
            {
                new DomainNotification("Erro","Model Error")
            };

            mockNotification.Setup(c => c.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(c => c.HasNotifications()).Returns(true);

            eventosController.ModelState.AddModelError("Erro", "Model Error");

            // Act
            var result = eventosController.Post(eventoViewModel);

            // Assert
            mockMapper.Verify(m => m.Map<RegistrarEventoCommand>(eventoViewModel), Times.Never);
            mockBus.Verify(m => m.SendCommands(eventoCommand), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Registrar evento com erro de Dominio")]
        [Trait("Category", "Testes Eventos Controller")]
        public void EventosController_RegistrarEvento_RetornarComErrosDeDominio()
        {
            // Arrange
            mockMapper.Setup(m => m.Map<RegistrarEventoCommand>(eventoViewModel)).Returns(eventoCommand);
            var notificationList = new List<DomainNotification>
            {
                new DomainNotification("Erro","Domain Error")
            };

            mockNotification.Setup(c => c.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(c => c.HasNotifications()).Returns(true);

            // Act
            var result = eventosController.Post(eventoViewModel);

            // Assert
            mockBus.Verify(m => m.SendCommands(eventoCommand), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
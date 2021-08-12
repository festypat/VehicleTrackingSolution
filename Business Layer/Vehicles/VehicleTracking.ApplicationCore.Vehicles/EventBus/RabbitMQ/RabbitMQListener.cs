﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace VehicleTracking.ApplicationCore.Vehicles.EventBus.RabbitMQ
{
    public class RabbitMQListener : IEventListener
    {
        private readonly IBusClient _busClient;
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly RabbitMQOptions _options;

        public RabbitMQListener(
            IBusClient busClient,
            IOptions<RabbitMQOptions> options,
            IServiceScopeFactory serviceFactory)
        {
            _busClient = busClient;
            _serviceFactory = serviceFactory;
            _options = options.Value;
        }

        public void Subscribe<T>() where T : IEvent
        {
            _busClient.SubscribeAsync(
                (Func<T, Task>)(async (@event) =>
                {
                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetService<IMediator>();
                        await mediator.Publish(@event);
                    }
                }),
                cfg => cfg.UseSubscribeConfiguration(
                    c => c
                    .OnDeclaredExchange(GetExchangeDeclaration<T>())
                    .FromDeclaredQueue(q => q.WithName((_options.QueueOption.Name ?? AppDomain.CurrentDomain.FriendlyName).Trim().Trim('_') + "_" + typeof(T).Name)))
            );
        }


        public async Task Publish<T>(T @event) where T : IEvent
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event), "Event can not be null.");
            }

            await _busClient.PublishAsync(
                @event,
                cfg => cfg.UsePublishConfiguration(
                    c => c
                    .OnDeclaredExchange(GetExchangeDeclaration<T>())
                )
            );
        }

        public async Task Publish(string message, string type)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Event message can not be null.");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException(nameof(type), "Event type can not be null.");
            }

            await _busClient.PublishAsync(
                message,
                cfg => cfg.UsePublishConfiguration(
                    c => c
                    .OnDeclaredExchange(GetExchangeDeclaration(type))
                )
            );
        }

        private Action<RawRabbit.Configuration.Exchange.IExchangeDeclarationBuilder> GetExchangeDeclaration<T>()
        {
            var name = EventBusHelper.GetTypeName<T>();

            return GetExchangeDeclaration(name);
        }

        private Action<RawRabbit.Configuration.Exchange.IExchangeDeclarationBuilder> GetExchangeDeclaration(string name)
        {
            return e => e
                .WithName(_options.ExchangeOption.Name)
                .WithArgument("key", name);
        }
    }

}

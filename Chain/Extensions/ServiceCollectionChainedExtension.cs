using Chain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Chain.Extensions {
    public static class ServiceCollectionChainedExtension {
        public static IChainedServiceCollection<TService> Chain<TService, TImplementation>(this IServiceCollection collection, ServiceLifetime lifeTime = ServiceLifetime.Singleton)
            where TService : class
            where TImplementation : class, TService =>
            new ChainedServiceCollection<TService>(typeof(TImplementation), lifeTime, collection);

        public static IChainedServiceCollection<TService> Chain<TService>(this IServiceCollection collection, ServiceLifetime lifeTime = ServiceLifetime.Singleton)
            where TService : class =>
            new ChainedServiceCollection<TService>(typeof(TService), lifeTime, collection);

        public static IChainedServiceCollection<TService> Next<TService, TImplementation>(
            this IChainedServiceCollection<TService> collection)
            where TService : class => collection.Next<TImplementation>();

        public static IServiceCollection Configure<TService>(
            this IChainedServiceCollection<TService> collection)
            where TService : class => collection.Configure();

    }
}
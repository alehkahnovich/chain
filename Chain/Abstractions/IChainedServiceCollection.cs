using Microsoft.Extensions.DependencyInjection;

namespace Chain.Abstractions {
    public interface IChainedServiceCollection<TService> where TService : class {
        IChainedServiceCollection<TService> Next<TImplementation>();
        IServiceCollection Configure();
    }
}
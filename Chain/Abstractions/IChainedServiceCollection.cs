using Microsoft.Extensions.DependencyInjection;

namespace Chain.Abstractions {
    public interface IChainedServiceCollection<in TService> where TService : class {
        IChainedServiceCollection<TService> Next<TImplementation>() where TImplementation : class, TService;
        IServiceCollection Configure();
    }
}
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Chain.Abstractions {
    internal sealed class ChainedServiceCollection<TService> : IChainedServiceCollection<TService> where TService : class {
        private readonly Type _implementation;
        private readonly IServiceCollection _collection;
        private readonly ServiceLifetime _lifeTime;
        private readonly List<Type> _next;
        public ChainedServiceCollection(Type implementation, ServiceLifetime lifeTime, IServiceCollection collection) {
            _implementation = implementation;
            _collection = collection;
            _lifeTime = lifeTime;
            _next = new List<Type>();
        }

        public IChainedServiceCollection<TService> Next<TImplementation>() {
            _next.Add(typeof(TImplementation));
            return this;
        }

        public IServiceCollection Configure() {
            _next.Insert(0, _implementation);

            var descriptors = new ServiceDescriptor[_next.Count];
            for (var step = 0; step < _next.Count; step++) {
                var implementation = _next[step];
                var next = step < _next.Count - 1 ? _next[step + 1] : null;
   
                descriptors[_next.Count - 1 - step] = new ServiceDescriptor(step == 0 ? typeof(TService) : implementation,
                    ChainedFactory.Make<TService>(implementation, next),
                    _lifeTime);
            }

            foreach (var descriptor in descriptors)
                _collection.Add(descriptor);

            return _collection;
        }
    }
}
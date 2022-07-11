using System;
using System.Linq;

namespace Chain.Abstractions {
    internal sealed class ChainedFactory {
        public static Func<IServiceProvider, TService> Make<TService>(Type implementation, Type next) where TService : class {
            return provider => {
                var constructors = implementation.GetConstructors()
                .Select(ctor => new {
                    Info = ctor,
                    Parameters = ctor.GetParameters()
                });

                foreach (var ctor in constructors.OrderByDescending(src => src.Parameters.Length)) {
                    var parameters = ctor.Parameters;
                    var args = new object[parameters.Length];
                    for (var param = 0; param < parameters.Length; param++) {
                        var info = parameters[param];
                        if (typeof(TService).IsAssignableFrom(info.ParameterType) && next != null) {
                            args[param] = provider.GetService(next);
                            continue;
                        }

                        args[param] = next != null ? provider.GetService(info.ParameterType) : default(TService);
                    }

                    return Activator.CreateInstance(implementation, args) as TService;
                }

                return provider.GetService(typeof(TService)) as TService;
            };
        }
    }
}
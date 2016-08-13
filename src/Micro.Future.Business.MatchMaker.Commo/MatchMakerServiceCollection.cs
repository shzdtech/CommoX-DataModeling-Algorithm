using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Micro.Future.Business.MatchMaker.Commo.Service;
using Micro.Future.Business.MatchMaker.Commo.Config;

namespace Micro.Future.Business.MatchMaker.Commo
{
    public static class MatchMakerServiceCollection
    {
        public static IServiceCollection AddMatchMakerSystem(this IServiceCollection services, MatcherConfig config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var service = new MatchMakerService(config);

            services.AddSingleton(service);

            if (config.START_BYDEFAULT)
            {
                service.start();
            }

            return services;
        }
    }
}

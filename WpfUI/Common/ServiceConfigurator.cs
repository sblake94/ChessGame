using Domain.Models.Data.Result;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Infrastructure.ServiceImplementations;
using Infrastructure.Attributes.ServiceAttributes;
using CommunityToolkit.Mvvm.DependencyInjection;
using Application.ServiceAbstracts;

namespace Presentation_WPF.Common
{
    public static class ServiceConfigurator
    {
        const string INFRASTRUCTURE_ASSEMBLY_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\Infrastructure\\bin\\Debug\\netstandard2.0";
        //const string APPLICATION_ASSEMBLY_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\Application\\Application.csproj";

        internal static Result<IServiceProvider> Configure()
        {
            IServiceCollection services = new ServiceCollection();

            // and register them as services
            // AutomaticallyFindAndAddServices(services);
            ManuallyAddServices(services);

            var provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);
            return new Success<IServiceProvider>(provider);
        }

        private static void ManuallyAddServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IChessLogicFacadeService), typeof(ChessLogicFacadeService));
            services.AddSingleton(typeof(IGameStateEngineService), typeof(GameStateEngineService));
            services.AddSingleton(typeof(ILoggerFactoryService), typeof(LoggerFactoryService));
            services.AddSingleton(typeof(IMoveHistoryService), typeof(MoveHistoryService));

            services.AddTransient(typeof(IMoveBlueprintingService), typeof(MoveBlueprintingService));
            services.AddTransient(typeof(INotationService), typeof(NotationService));
        }

        private static void AutomaticallyFindAndAddServices(IServiceCollection services)
        {
            // Get the Infrastructure assembly
            var infrastructureAssembly = Assembly.LoadFrom(INFRASTRUCTURE_ASSEMBLY_PATH);

            //var applicationAssembly = Assembly.LoadFrom(APPLICATION_ASSEMBLY_PATH);

            var allTypes = infrastructureAssembly.GetTypes();
            var serviceTypes = allTypes
                .Where(type => type.Namespace == nameof(Infrastructure.ServiceImplementations))
                .Where(type => type.BaseType == typeof(ServiceBase<>));

            // Get all Types in the Infrastructure.ServiceImplementations namespace
            foreach (Type type in serviceTypes)
            {
                // Check if the type has the SingletonService attribute
                if (type.GetCustomAttributes().Any(attribute => attribute.GetType() == typeof(SingletonService)))
                {
                    // Get the interface that the type implements
                    var interfaceType = type.GetInterfaces().First();

                    // Register it as a singleton
                    services.AddSingleton(interfaceType, type);
                }

                // Check if the type has the TransientService attribute
                if (type.GetCustomAttributes().Any(attribute => attribute.GetType() == typeof(TransientService)))
                {
                    // Get the interface that the type implements
                    var interfaceType = type.GetInterfaces().First();

                    // Register it as a transient
                    services.AddTransient(interfaceType, type);
                }
            }
        }
    }
}

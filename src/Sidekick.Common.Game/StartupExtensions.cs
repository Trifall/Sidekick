using Microsoft.Extensions.DependencyInjection;
using Sidekick.Common.Game.Languages;

namespace Sidekick.Common.Game
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSidekickCommonGame(this IServiceCollection services)
        {
            services.AddSingleton<IGameLanguageProvider, GameLanguageProvider>();

            return services;
        }
    }
}
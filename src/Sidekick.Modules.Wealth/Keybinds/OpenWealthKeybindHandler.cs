using Sidekick.Common;
using Sidekick.Common.Blazor.Views;
using Sidekick.Common.Keybinds;
using Sidekick.Common.Platform;

namespace Sidekick.Modules.Wealth.Keybinds
{
    public class OpenWealthKeybindHandler : KeybindHandler
    {
        private readonly IViewLocator viewLocator;
        private readonly ISettings settings;
        private readonly IProcessProvider processProvider;

        public OpenWealthKeybindHandler(
            IViewLocator viewLocator,
            ISettings settings,
            IProcessProvider processProvider)
        {
            this.viewLocator = viewLocator;
            this.settings = settings;
            this.processProvider = processProvider;
        }

        public List<string> GetKeybinds() => new() { settings.Wealth_Key_Open };

        public bool IsValid(string _) => (processProvider.IsPathOfExileInFocus || processProvider.IsSidekickInFocus);

        public Task Execute(string _)
        {
            viewLocator.Open("/wealth");
            return Task.CompletedTask;
        }
    }
}

using LibraryApp.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.WPF
{
    public class ServiceLocator
    {
        public MainWindowViewModel MainModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}

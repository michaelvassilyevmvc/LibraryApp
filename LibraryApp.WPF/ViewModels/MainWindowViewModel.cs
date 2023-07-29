using LibraryApp.DAL.Entities;
using LibraryApp.Interfaces.Base.Repositories;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LibraryApp.WPF.ViewModels
{
    public class MainWindowViewModel : TitledViewModel
    {
        private readonly IRepository<DataSource> _dataSources;

        public MainWindowViewModel(IRepository<DataSource> dataSource)
        {
            _dataSources = dataSource;
            Title = "Главное окно программы";
        }

        public ObservableCollection<DataSource> DataSources { get; } = new();

        

        private LambdaCommand _loadDataSourcesCommand;

        public ICommand LoadDataSourcesCommand => _loadDataSourcesCommand ??= new(OnLoadDataSourcesCommandExecuted);

        public async void OnLoadDataSourcesCommandExecuted()
        {
            DataSources.Clear();
            foreach (var source in await _dataSources.GetAll())
            {
                DataSources.Add(source);
            }
        } 

    }
}

using CompCompanyClients1.Views;
namespace CompCompanyClients1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _CpuUserController = new CpuUserController();
            _CpuUserController.DataContext = new CpuUserControllerViewModel();
        }
        public CpuUserController _CpuUserController { get; set; }
    }
}
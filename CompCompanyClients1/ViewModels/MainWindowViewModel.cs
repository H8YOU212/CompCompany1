using CompCompanyClients1.Views;
namespace CompCompanyClients1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _CpuUserController = new CpuUserController();
            _CpuUserController.DataContext = new CpuUserControllerViewModel();

            _GpuUserController = new GpuUserController();
            _GpuUserController.DataContext = new GpuUserControllerViewModel();
        }
        public CpuUserController _CpuUserController { get; set; }
        public GpuUserController _GpuUserController { get; set; }

    }
}
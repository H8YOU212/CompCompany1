using CompCompanyClients1.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompCompanyClients1.ViewModels
{
    public class GpuUserControllerViewModel : ViewModelBase
    {
        private Gpu _selectedGpu;
        public Gpu SelectedGpu
        {
            get => _selectedGpu;
            set => this.RaiseAndSetIfChanged(ref _selectedGpu, value);
        }
        private HttpClient client = new HttpClient();
        private ObservableCollection<Gpu> _gpus;
        public ObservableCollection<Gpu> Gpus
        {
            get => _gpus;
            set => this.RaiseAndSetIfChanged(ref _gpus, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        public GpuUserControllerViewModel()
        {
            client.BaseAddress = new Uri("http://localhost:7232");
            Update();
        }
        public async Task Update()
        {
            var response = await client.GetAsync("/gpus");
            if (!response.IsSuccessStatusCode)
            {
                Message = $"Ошибка сервера{response.StatusCode}";
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            if (content == null)
            {
                Message = "Пустой ответ от сервера";
                return;
            }
            Gpus = JsonSerializer.Deserialize<ObservableCollection<Gpu>>(content);
            Message = "";
        }
        public async Task Delete()
        {
            if (SelectedGpu == null) return;
            var response = await client.DeleteAsync($"/gpus/{SelectedGpu.Id}");
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка удаления со стороны сервера";
                return;
            }
            Gpus.Remove(SelectedGpu);
            SelectedGpu = null;
            Message = "";
        }

        public async Task Add()
        {
            var gpu = new Gpu();
            var response = await client.PostAsJsonAsync($"/gpus", gpu);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка добавления со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Gpu>();
            if (content == null)
            {
                Message = "При добавлении сервер отправил пустой ответ";
                return;
            }
            gpu = content;
            Gpus.Add(gpu);
            SelectedGpu = gpu;
        }
        public async Task Edit()
        {
            var response = await client.PutAsJsonAsync($"/gpus", SelectedGpu);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка изменения со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Gpu>();
            if (content == null)
            {
                Message = "При изменении сервер отправил пустой ответ";
                return;
            }
            SelectedGpu = content;
        }
    }

}

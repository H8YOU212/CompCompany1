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
    public class CpuUserControllerViewModel : ViewModelBase
    {
        private Cpu _selectedCpu;
        public Cpu SelectedCpu
        {
            get => _selectedCpu;
            set => this.RaiseAndSetIfChanged(ref _selectedCpu, value);
        }
        private HttpClient client = new HttpClient();
        private ObservableCollection<Cpu> _cpus;
        public ObservableCollection<Cpu> Cpus
        {
            get => _cpus;
            set => this.RaiseAndSetIfChanged(ref _cpus, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        public CpuUserControllerViewModel()
        {
            client.BaseAddress = new Uri("http://localhost:5068");
            Update();
        }
        public async Task Update()
        {
            var response = await client.GetAsync("/cpus");
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
            Cpus = JsonSerializer.Deserialize<ObservableCollection<Cpu>>(content);
            Message = "";
        }
        public async Task Delete()
        {
            if (SelectedCpu == null) return;
            var response = await client.DeleteAsync($"/cpus/{SelectedCpu.Id}");
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка удаления со стороны сервера";
                return;
            }
            Cpus.Remove(SelectedCpu);
            SelectedCpu = null;
            Message = "";
        }

        public async Task Add()
        {
            var cpu = new Cpu();
            var response = await client.PostAsJsonAsync($"/cpus", cpu);
            if (!response.IsSuccessStatusCode) 
            {
                Message = "Ошибка добавления со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Cpu>();
            if (content == null)
            {
                Message = "При добавлении сервер отправил пустой ответ";
                return;
            }
            cpu = content;
            Cpus.Add(cpu);
            SelectedCpu = cpu;
        }
        public async Task Edit()
        {
            var response = await client.PutAsJsonAsync($"/cpus", SelectedCpu);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Ошибка изменения со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Cpu>();
            if (content == null)
            {
                Message = "При изменении сервер отправил пустой ответ";
                return;
            }
            SelectedCpu = content;
        }
    }
}

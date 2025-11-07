using Blazored.LocalStorage;

namespace MiApp.Blazor.Services
{
    public class TaskService
    {
        private readonly ILocalStorageService _localStorage;
        private const string Key = "tareas";

        public TaskService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<string>> GetTareasAsync()
        {
            return await _localStorage.GetItemAsync<List<string>>(Key) ?? new List<string>();
        }

        public async Task SaveTareasAsync(List<string> tareas)
        {
            await _localStorage.SetItemAsync(Key, tareas);
        }

        public async Task ClearTareasAsync()
        {
            await _localStorage.RemoveItemAsync(Key);
        }
    }
}

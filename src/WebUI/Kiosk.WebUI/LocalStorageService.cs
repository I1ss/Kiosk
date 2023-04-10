namespace Kiosk.WebUI
{
    using Microsoft.JSInterop;

    using System.Text.Json;

    /// <summary>
    /// Локальное хранилище.
    /// </summary>
    public interface ILocalStorageService
    {
        /// <summary>
        /// Получить значение по ключу.
        /// </summary>
        /// <typeparam name="T"> Тип значения. </typeparam>
        /// <param name="key"> Ключ. </param>
        /// <returns> Значение. </returns>
        Task<T> GetItem<T>(string key);

        /// <summary>
        /// Добавить значение к ключу.
        /// </summary>
        /// <typeparam name="T"> Тип значения. </typeparam>
        /// <param name="key"> Ключ. </param>
        /// <param name="value"> Значение. </param>
        Task SetItem<T>(string key, T value);

        /// <summary>
        /// Удалить значение по ключу.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        Task RemoveItem(string key);
    }

    /// <inheritdoc cref="ILocalStorageService"/>
    public class LocalStorageService : ILocalStorageService
    {
        /// <summary>
        /// Экземпляр, чтобы пересылать запросы.
        /// </summary>
        private IJSRuntime _jsRuntime;

        /// <summary>
        /// Локальное хранилище.
        /// </summary>
        /// <param name="jsRuntime"> Экземпляр, чтобы пересылать запросы. </param>
        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <inheritdoc />
        public async Task<T> GetItem<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (json == null)
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        /// <inheritdoc />
        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
        }

        /// <inheritdoc />
        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}

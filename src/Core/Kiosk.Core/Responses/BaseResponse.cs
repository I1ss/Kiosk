namespace Kiosk.Core.Responses
{
    /// <summary>
    /// Базовый класс ответа.
    /// </summary>
    /// <typeparam name="T"> Тип данных в ответе. </typeparam>
    public interface IBaseResponse<T>
    {
        /// <summary>
        /// Описание.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Данные.
        /// </summary>
        T Data { get; }
    }

    /// <inheritdoc cref="IBaseResponse{T}"/>
    public class BaseResponse<T> : IBaseResponse<T>
    {
        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public T Data { get; set; }
    }
}

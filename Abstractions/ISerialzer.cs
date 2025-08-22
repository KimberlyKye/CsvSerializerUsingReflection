
namespace CsvSerializerUsingReflection.Abstractions
{
    /// <summary>
    /// Интерфейс сериализатора.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Сериализует объект в CSV.
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string Serialize<T>(T obj);

        /// <summary>
        /// Десериализует объект из CSV.
        /// </summary>
        /// <param name="csv"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Deserialize<T>(string csv) where T : new();
    }
}
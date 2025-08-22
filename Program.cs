
using System.Diagnostics;
using CsvSerializerUsingReflection.Services;
using CsvSerializerUsingReflection.Classes;
using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        var serializer = new CsvSerializer();

        const int iterations = 100000;
        var obj = F.Get();

        // Тестирование сериализации с помощью рефлексии
        var reflectionSerializationTime = MeasureTime(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                var csv = serializer.Serialize(obj);
            }
        });

        // Тестирование десериализации с помощью рефлексии
        var csv = serializer.Serialize(obj);
        var reflectionDeserializationTime = MeasureTime(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                var deserialized = serializer.Deserialize<F>(csv);
            }
        });

        // Тестирование сериализации с помощью Newtonsoft.Json
        var jsonSerializationTime = MeasureTime(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                var json = JsonConvert.SerializeObject(obj);
            }
        });

        // Тестирование десериализации с помощью Newtonsoft.Json
        var json = JsonConvert.SerializeObject(obj);
        var jsonDeserializationTime = MeasureTime(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                var deserialized = JsonConvert.DeserializeObject<F>(json);
            }
        });

        // Замер времени вывода в консоль
        var outputTime = MeasureTime(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine("Test output");
            }
        });

        Console.Clear();

        // Вывод результатов
        Console.WriteLine($"Сериализуемый класс: class F {{ int i1, i2, i3, i4, i5; }}");
        Console.WriteLine($"Количество замеров: {iterations} итераций");
        Console.WriteLine($"Время на вывод в консоль {iterations} строк = {outputTime} мс");

        Console.WriteLine();
        Console.WriteLine("Мой рефлекшен:");
        Console.WriteLine($"Время на сериализацию = {reflectionSerializationTime} мс");
        Console.WriteLine($"Время на десериализацию = {reflectionDeserializationTime} мс");
        Console.WriteLine();
        Console.WriteLine("Стандартный механизм (NewtonsoftJson):");
        Console.WriteLine($"Время на сериализацию = {jsonSerializationTime} мс");
        Console.WriteLine($"Время на десериализацию = {jsonDeserializationTime} мс");

    }

    private static long MeasureTime(Action action)
    {
        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}
using System.Globalization;
using System.Reflection;
using CsvSerializerUsingReflection.Abstractions;

namespace CsvSerializerUsingReflection.Services
{
    public class CsvSerializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var values = new List<string>();

            foreach (var field in fields)
            {
                values.Add(Convert.ToString(field.GetValue(obj), CultureInfo.InvariantCulture));
            }

            foreach (var property in properties)
            {
                values.Add(Convert.ToString(property.GetValue(obj), CultureInfo.InvariantCulture));
            }

            return string.Join(",", values);
        }

        public T Deserialize<T>(string csv) where T : new()
        {
            var values = csv.Split(',');
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var obj = new T();
            int valueIndex = 0;

            foreach (var field in fields)
            {
                if (valueIndex >= values.Length) break;

                var value = Convert.ChangeType(values[valueIndex], field.FieldType, CultureInfo.InvariantCulture);
                field.SetValue(obj, value);
                valueIndex++;
            }

            foreach (var property in properties)
            {
                if (valueIndex >= values.Length) break;

                var value = Convert.ChangeType(values[valueIndex], property.PropertyType, CultureInfo.InvariantCulture);
                property.SetValue(obj, value);
                valueIndex++;
            }

            return obj;
        }
    }
}
using System;

namespace Comm.InterceptorCaching
{
    public interface ICachingSerializer
    {

        /// <summary>
        /// Serialize the specified value.
        /// </summary>
        /// <returns>The serialize.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        string Serialize<T>(T value);

        /// <summary>
        /// Deserialize the specified bytes.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T Deserialize<T>(string sourceStr);


        /// <summary>
        /// Deserialize the specified bytes.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <param name="sourceStr"></param>
        /// <param name="type">Type.</param>
        object Deserialize(string sourceStr, Type type);
    }

    public class JsonCachingSerializer : ICachingSerializer
    {
        public string Serialize<T>(T value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        public T Deserialize<T>(string sourceStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(sourceStr);
        }

        public object Deserialize(string sourceStr, Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(sourceStr, type);
        }
    }
}

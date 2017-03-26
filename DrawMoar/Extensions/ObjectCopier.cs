using System;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace DrawMoar.Extensions
{
    public static class ObjectCopier
    {
        public static T Clone<T>(this T sourse) {
            if (!typeof(T).IsSerializable) {
                throw new ArgumentException("Тип должен реализовывать интерфейс ISerializable.");
            }


            // Не сериализуйте null
            if (Object.ReferenceEquals(sourse, null)) {
                return default(T);
            }


            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream) {
                formatter.Serialize(stream, sourse);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }

        }
    }
}

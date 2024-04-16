using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Utkarsh.UnityCore
{
    /// <summary>
    /// General utilities script.
    /// Consists of static functions that are needed a lot
    /// </summary>
    public static class Utilities
    {

        public const int MILLISECONDS_IN_SECOND = 1000;

        public static int PositiveModulo(this int a, int b)
        {
            return ((a % b) + b) % b;
        }

        /// <summary>
        /// Recursively sets the layer of the specified GameObject and all its children to the specified layer
        /// </summary>
        /// <param name="GO"></param>
        /// <param name="layer"></param>
        public static void SetLayerRecursively(Transform GO, int layer)
        {
            GO.gameObject.layer = layer;

            foreach (Transform child in GO)
            {
                SetLayerRecursively(child, layer);
            }
        }

        /// <summary>
        /// Finds all indices of a given element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="val"></param>
        /// <returns>List containing all indices where element occurs in specified list. Returns an empty list if there are no occurences.</returns>
        public static List<int> FindAllIndexOf<T>(this IEnumerable<T> list, T val)
        {
            List<int> indices = list.Select((x, i) => object.Equals(x, val) ? i : -1).Where(i => i != -1).ToList();
            return indices;
        }

        /// <summary>
        /// Takes a list and a predicate and returns indices of all elements that meet the condition specified in the predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<int> FindAllIndexOfConditionMet<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            List<int> indices = list.Select((x, i) => predicate(x) ? i : -1).Where(i => i != -1).ToList();
            return indices;
        }

        /// <summary>
        /// Find the first element in array that meets the condition specified in the predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T FindFirstElementWithConditionMet<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            List<int> idxs = list.FindAllIndexOfConditionMet(predicate);

            if (idxs != null && idxs.Count > 0)
            {
                return list.ElementAt(idxs[0]);
            }
            return default(T);
        }

        /// <summary>
        /// Returns if this list contains at least one element that matches the condition specified in the predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool HasAtLeastOneMatchingElement<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            List<int> idxs = list.FindAllIndexOfConditionMet(predicate);

            return (idxs != null && idxs.Count > 0);
        }

        /// <summary>
        /// Converts a System.Object object to a byte array
        /// https://stackoverflow.com/a/10502856
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(System.Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts a byte array to a System.Object object
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static System.Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        /// <summary>
        /// Gets time formatted to minutes:seconds:milliseconds from a given float value in seconds
        /// </summary>
        /// <param name="t">float value in seconds</param>
        /// <returns></returns>
        public static string GetTimeFormatted(float t, bool showMilliseconds = true)
        {
            if (t < 0f || t > float.MaxValue) // failsafe
            {
                return ("00:00:00");
            }

            string minutes = ((int)t / 60).ToString("d2");
            string seconds = ((int)(t % 60)).ToString("d2");
            string milliseconds = "";
            if (showMilliseconds)
            {
                milliseconds = ":" + ((int)(((t % 60) % 1) * 100)).ToString("d2");
            }

            return (minutes + ":" + seconds + milliseconds);
        }

        /// <summary>
        /// Gets the next value in this enum
        /// Sourced from: https://stackoverflow.com/a/643438
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Next<T>(this T src) where T : Enum
        {
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        /// <summary>
        /// Gets the previous value in this enum
        /// Sourced from: https://stackoverflow.com/a/643438
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Previous<T>(this T src) where T : Enum
        {
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) - 1;
            return (j < 0) ? Arr[Arr.Length - 1] : Arr[j];
        }
    }

    /// <summary>
    /// This class is to serialize DataContract values to Binary format!
    /// We used DataContractSerializer to save files instead of simply using BinaryFormatters, because we were getting a bunch of exceptions and I don't know why.
    /// </summary>
    public static class BinarySerializer
    {
        //public static byte[] Serialize<T>(T obj)
        //{
        //    var serializer = new DataContractSerializer(typeof(T));
        //    var stream = new MemoryStream();
        //    using (var writer =
        //        XmlDictionaryWriter.CreateBinaryWriter(stream))
        //    {
        //        serializer.WriteObject(writer, obj);
        //    }
        //    return stream.ToArray();
        //}

        //public static T Deserialize<T>(byte[] data)
        //{
        //    var serializer = new DataContractSerializer(typeof(T));
        //    using (var stream = new MemoryStream(data))
        //    using (var reader =
        //        XmlDictionaryReader.CreateBinaryReader(
        //            stream, XmlDictionaryReaderQuotas.Max))
        //    {
        //        return (T)serializer.ReadObject(reader);
        //    }
        //}

        /// <summary>
        /// https://stackoverflow.com/a/10502856
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// https://stackoverflow.com/a/10502856
        /// </summary>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return (T)obj;
            }
        }

        /// <summary>
        /// Copies this string to the system wide clipboard
        /// </summary>
        /// <param name="str"></param>
        public static void CopyTextToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
    }

}

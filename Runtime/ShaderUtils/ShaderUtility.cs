using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Utkarsh.UnityCore.ShaderUtils
{
    /// <summary>
    /// Class that contains useful functions for writing shaders.
    /// </summary>
    public static class ShaderUtility
    {
        /// <summary>
        /// Get a <see cref="ComputeBuffer"/> filled with the provided data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ComputeBuffer BuildComputeBuffer<T>(T[] data) where T : struct
        {
            // Get the size of the type to ensure correct buffer size.
            int size = UnsafeUtility.SizeOf<T>();

            ComputeBuffer buffer = new ComputeBuffer(data.Length, size);

            buffer.SetData(data);

            return buffer;
        }

        /// <summary>
        /// Get a <see cref="ComputeBuffer"/> filled with the provided data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ComputeBuffer BuildComputeBuffer<T>(T data) where T : struct
        {
            T[] dataArr = new T[] { data };
            return BuildComputeBuffer(dataArr);
        }
    }

}
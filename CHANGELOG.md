# 1.0.2
- Added Shader Utility functions that help with building compute buffers for sending data to GPU
  - `public static ComputeBuffer BuildComputeBuffer<T>(T[] data)`
  - `public static ComputeBuffer BuildComputeBuffer<T>(T data)`
# 1.0.1
- Added Helper Math Functions
  - DivideBy(this float value, Vector3 vec) - divides a float by a Vector3 and returns a Vector3
  - DivideBy(this float value, Vector2 vec) - divides a float by a Vector2 and returns a Vector2
  - Reciprocal(this Vector3 vec) - returns 1 / vec (the reciprocal of the given Vector3)
  - Reciprocal(this Vector2 vec) - returns 1 / vec (the reciprocal of the given Vector2)
  - Vector3 Abs
  - Vector2 Abs
# 1.0.0
- Added Pooling Utilities
  - StaticPool: Uses List<T> internally, cannot change size after initialization
  - DynamicPool: Uses List<T> internally, can change size after initialization
  - ArrayStackPool: Uses a stack made of a C# array, cannot change size after initialization
  - QueuePool: Not fully functional yet.
- Added array based DataStructures:
  - CircularQueue<T>
  - CircularStack<T>
- Added DDOL (DontDestroyOnLoad) MonoBehaviour
- Added Utilities script with common utiltiies and extension methods, as well as binary serialization for saving and loading
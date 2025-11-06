using UnityEngine;

namespace Utilities
{
    // A static helper class full of functions to be accessed anywhere at any given time
    // Intend to build up a utility class that can be used in all projects, starting with this one

    public static class Utils
    {
        #region Isometric Functions


        private static Matrix4x4 _isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(this Vector3 input) => _isometricMatrix.MultiplyPoint3x4(input);

        #endregion
    }
}



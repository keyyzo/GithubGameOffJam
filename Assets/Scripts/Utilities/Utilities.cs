using UnityEngine;

namespace Utilities
{
    // A static helper class full of functions to be accessed anywhere at any given time
    // Intend to build up a utility class that can be used in all projects, starting with this one

    public static class Utils
    {
       

        private static float _isometricAngle = 45f;

        public static float ToIso(this float newAngle) => _isometricAngle = newAngle;

        #region Isometric Functions


        private static Matrix4x4 _isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, _isometricAngle, 0));
        public static Vector3 ToIso(this Vector3 input) => _isometricMatrix.MultiplyPoint3x4(input);

        public static void SetIsometricAngle(float newAngle)
        {
            if (newAngle < 0.0f)
            {
                newAngle += 360f;
            }

            else if (newAngle > 360.0f)
            {
                newAngle -= 360f;
            }




            _isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, newAngle, 0));
        }

        

        #endregion
    }
}



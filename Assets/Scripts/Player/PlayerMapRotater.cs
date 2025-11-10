using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using Utilities;

namespace GameOffJam.Player
{
    public class PlayerMapRotater : MonoBehaviour
    {
        [Header("Rotation Settings")]

        [SerializeField] float rotationSpeed = 10.0f;
        [SerializeField] float timeToRotate = 0.5f;

        const string MAP_OBJECT_STRING = "Environment";

        const float MAP_ROTATE_VALUE = 90f;

        // private variables

        bool _rotateLeftInput = false;

        bool _rotateRightInput = false;

        bool _isRotating = false;

        float _xRotation;

        // Cached objects

        GameObject mapObject;

        CinemachineCamera cinemachineCamera;

        private IEnumerator Start()
        {
            yield return null;
            cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
            _xRotation  = cinemachineCamera.transform.rotation.eulerAngles.x;
            
        }

        private void Update()
        {
            
        }

        private void LateUpdate()
        {
            if (_rotateRightInput && !_isRotating)
            {
                _isRotating = true;

                RotateMapRight();

            }

            if (_rotateLeftInput && !_isRotating)
            {
                _isRotating = true;

                RotateMapLeft();
            }

            if (!_rotateRightInput && !_rotateLeftInput && _isRotating)
            {
                _isRotating = false;
            }
        }


        private void RotateMapRight()
        {
            float newAngle = cinemachineCamera.transform.rotation.eulerAngles.y - MAP_ROTATE_VALUE;

            RemoveNegativeEulerAngle(newAngle);

            Quaternion newRotation = Quaternion.Euler(_xRotation, newAngle, 0.0f);
            //cinemachineCamera.transform.rotation = Quaternion.RotateTowards(cinemachineCamera.transform.rotation, newRotation, MAP_ROTATE_VALUE);
            cinemachineCamera.transform.rotation = Quaternion.Slerp(cinemachineCamera.transform.rotation, newRotation, Time.deltaTime / timeToRotate);
            Utilities.Utils.SetIsometricAngle(newAngle);
        }

        private void RotateMapLeft()
        {
            float newAngle = cinemachineCamera.transform.rotation.eulerAngles.y + MAP_ROTATE_VALUE;

            RemoveNegativeEulerAngle(newAngle);

            Quaternion newRotation = Quaternion.Euler(_xRotation, newAngle, 0.0f);
            //cinemachineCamera.transform.rotation = Quaternion.RotateTowards(cinemachineCamera.transform.rotation, newRotation, MAP_ROTATE_VALUE);
            cinemachineCamera.transform.rotation = Quaternion.Slerp(cinemachineCamera.transform.rotation, newRotation, Time.deltaTime / timeToRotate);
            Utilities.Utils.SetIsometricAngle(newAngle);
        }

        private float RemoveNegativeEulerAngle(float angleToFix)
        {
            if (angleToFix < 0.0f)
            {
                angleToFix += 360f;
            }

            else if (angleToFix > 360.0f)
            {
                angleToFix -= 360f;
            }

            return angleToFix;
        }


        public void GetRotateRightInput(bool rotateRight)
        {
            _rotateRightInput = rotateRight;
        }

        public void GetRotateLeftInput(bool rotateLeft)
        { 
            _rotateLeftInput = rotateLeft;
        }

        IEnumerator RotateMapRightRoutine()
        {
            _isRotating = true;

            float newAngle = cinemachineCamera.transform.rotation.eulerAngles.y - MAP_ROTATE_VALUE;

            RemoveNegativeEulerAngle(newAngle);

            Quaternion newRotation = Quaternion.Euler(_xRotation, newAngle, 0.0f);
            //cinemachineCamera.transform.rotation = Quaternion.RotateTowards(cinemachineCamera.transform.rotation, newRotation, MAP_ROTATE_VALUE);



            cinemachineCamera.transform.rotation = Quaternion.Slerp(cinemachineCamera.transform.rotation, newRotation, Time.deltaTime / timeToRotate);
            Utilities.Utils.SetIsometricAngle(newAngle);

            yield return null;
        }

        IEnumerator RotateMapLeftRoutine() 
        {
            yield return null;
        }

    }
}



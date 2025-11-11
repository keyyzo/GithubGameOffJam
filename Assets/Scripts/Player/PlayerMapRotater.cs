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

        const float STARTING_ISO_ANGLED = 45f;

        const float MAP_ROTATE_VALUE = 90f;

        // private variables

        bool _rotateLeftInput = false;

        bool _rotateRightInput = false;

        bool _isRotating = false;

        float _xRotation;

        float _rotatingTimer = 0.0f;

        // Cached objects

        GameObject mapObject;

        CinemachineCamera cinemachineCamera;

        private IEnumerator Start()
        {
            yield return null;
            cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
            _xRotation  = cinemachineCamera.transform.rotation.eulerAngles.x;
            Utilities.Utils.SetIsometricAngle(STARTING_ISO_ANGLED);
            
        }

        private void Update()
        {
            
        }

        private void LateUpdate()
        {
            if (_rotateRightInput && !_isRotating)
            {
                //_isRotating = true;

                RotateMapRight();

            }

            if (_rotateLeftInput && !_isRotating)
            {
                
                RotateMapLeft();
            }

            
        }


        private void RotateMapRight()
        {
            //float newAngle = cinemachineCamera.transform.rotation.eulerAngles.y - MAP_ROTATE_VALUE;

            //RemoveNegativeEulerAngle(newAngle);

            //Quaternion newRotation = Quaternion.Euler(_xRotation, newAngle, 0.0f);
            ////cinemachineCamera.transform.rotation = Quaternion.RotateTowards(cinemachineCamera.transform.rotation, newRotation, MAP_ROTATE_VALUE);
            //cinemachineCamera.transform.rotation = Quaternion.Slerp(cinemachineCamera.transform.rotation, newRotation, Time.deltaTime / timeToRotate);
            //Utilities.Utils.SetIsometricAngle(newAngle);

            StartCoroutine(RotateMapRightRoutine());
        }

        private void RotateMapLeft()
        {
            //float newAngle = cinemachineCamera.transform.rotation.eulerAngles.y + MAP_ROTATE_VALUE;

            //RemoveNegativeEulerAngle(newAngle);

            //Quaternion newRotation = Quaternion.Euler(_xRotation, newAngle, 0.0f);
            ////cinemachineCamera.transform.rotation = Quaternion.RotateTowards(cinemachineCamera.transform.rotation, newRotation, MAP_ROTATE_VALUE);
            //cinemachineCamera.transform.rotation = Quaternion.Slerp(cinemachineCamera.transform.rotation, newRotation, Time.deltaTime / timeToRotate);
            //Utilities.Utils.SetIsometricAngle(newAngle);

            StartCoroutine(RotateMapRightRoutine(false));
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

        /// <summary>
        /// Rotates the camera clockwise / right if the "isRotatingRight" parameter is true.
        /// If false, will rotate the camera anti-clockwise / left.
        /// </summary>
        /// <param name="isRotatingRight"></param>
        /// <returns></returns>
        IEnumerator RotateMapRightRoutine(bool isRotatingRight = true)
        {
            
            _isRotating = true;

            float newAngle;

            if (isRotatingRight)
            {
                newAngle = cinemachineCamera.transform.rotation.eulerAngles.y - MAP_ROTATE_VALUE;
            }

            else
            {
                newAngle = cinemachineCamera.transform.rotation.eulerAngles.y + MAP_ROTATE_VALUE;
            }



                RemoveNegativeEulerAngle(newAngle);

            Quaternion newRotation = Quaternion.Euler(_xRotation, newAngle, 0.0f);

            while (_rotatingTimer < timeToRotate)
            {
                cinemachineCamera.transform.rotation = Quaternion.Lerp(cinemachineCamera.transform.rotation, newRotation, (_rotatingTimer / timeToRotate));
                Utilities.Utils.SetIsometricAngle(newAngle);

                _rotatingTimer += Time.deltaTime;

                yield return null;
            }

            cinemachineCamera.transform.rotation = newRotation;
            Utilities.Utils.SetIsometricAngle(newAngle);
            _rotatingTimer = 0.0f;

            _isRotating = false;

            yield return null;

        }

        

    }
}



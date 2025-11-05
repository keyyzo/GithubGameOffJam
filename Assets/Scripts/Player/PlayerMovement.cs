using System.Security.Cryptography;
using UnityEngine;

namespace GameOffJam.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Editor References

        [Header("References")]

        [SerializeField] GameObject playerModelPivot;

        #endregion

        #region Base Movement

        [Header("Base Movement")]

        [SerializeField] float maxMovementSpeed = 8.0f;
        [SerializeField] float accelerationFactor = 6.0f;
        [SerializeField] float decelerationFactor = 10.0f;

        [Space(2)]

        [Header("Rotation")]

        [SerializeField] float moveRotationSpeed = 360f;

        [Space(2)]

        [Header("Gravity")]

        [SerializeField] float maxGravitySpeed = -12.0f;

        #endregion

        #region Private Variables

        float _currentSpeed;

        Vector3 _velocity;

        // Inputs

        Vector3 _movementInput;

        #endregion


        #region References

        CharacterController characterController;

        #endregion

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            // ----- Gravity Functions -----


            // ----- Direction Functions -----

            ProcessLookDirection();

            // ----- General Movement Functions -----

            CalculateSpeed();
            ProcessMovement();

            


        }

        #region Gravity Functions

        private void ProcessGravity()
        { 
            
        }

        #endregion

        #region Movement Functions

        private void CalculateSpeed()
        {
            if (_movementInput == Vector3.zero && _currentSpeed > 0.0f)
            {
                _currentSpeed -= decelerationFactor * Time.deltaTime;
            }

            else if (_movementInput != Vector3.zero && _currentSpeed < maxMovementSpeed)
            {
                _currentSpeed += accelerationFactor * Time.deltaTime;
            }

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, maxMovementSpeed);
        }

        private void ProcessMovement()
        {
            Vector3 moveDirection = transform.forward * _currentSpeed * _movementInput.magnitude * Time.deltaTime;
            Debug.Log(moveDirection);
            characterController.Move(moveDirection);
        }

        private void ProcessLookDirection()
        {
            if (_movementInput == Vector3.zero)
            {
                playerModelPivot.transform.position = characterController.transform.position;
                return;
            }

            Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(_movementInput);

            //Quaternion rotation = Quaternion.LookRotation(multipliedMatrix, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, baseMoveRotationSpeed * Time.deltaTime);

            Vector3 relative = (transform.position + multipliedMatrix) - transform.position;
            Quaternion rot = Quaternion.LookRotation(relative, Vector3.up);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, baseMoveRotationSpeed * Time.deltaTime);



            playerModelPivot.transform.position = characterController.transform.position;
            transform.rotation = rot;
            playerModelPivot.transform.rotation = Quaternion.RotateTowards(playerModelPivot.transform.rotation, rot, moveRotationSpeed * Time.deltaTime);
        }

        #endregion


        #region Public Functions

        // Movement

        public void GetMovementInput(Vector2 movementInput)
        {
            _movementInput = new Vector3(movementInput.x, 0.0f, movementInput.y);
            //Debug.Log(_movementInput);
        }

        #endregion


    }

}

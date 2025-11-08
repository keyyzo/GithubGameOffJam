using System;
using UnityEngine;
using Utilities;

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

        [SerializeField] float moveSpeed = 8.0f;
        [SerializeField] float accelerationFactor = 6.0f;
        [SerializeField] float decelerationFactor = 10.0f;
        [SerializeField] float turnSpeed = 10.0f;

        [Space(2)]

        [Header("Rotation")]

        [SerializeField] float rotationSpeed = 360f;
        [SerializeField] float smoothTime = 0.05f;

        [Space(2)]

        [Header("Gravity")]

        [SerializeField] float gravitySpeed = -12.0f;
        [SerializeField] float terminalSpeed = 20.0f;
        [SerializeField] float lowGravityMultiplier = 2.0f;
        [SerializeField] float strongGravityMultiplier = 2.5f;

        #endregion

        #region Private Variables

        // General Movement

        float _currentSpeed;
        float _velocity;

        bool _isGrounded;

        // Wave Riding

        float _waveStrength;

        bool _isWaveRiding;

        Vector3 _waveDirection;


        // Alternative method to movement

        Vector3 _forward;
        Vector3 _right;
        
        Vector3 _direction;

        Vector3 _altVelocity;
        Vector3 _gravityVector;

        // Inputs

        Vector3 _movementInput;

        // Rotation Input Vectors
        Vector3 inputVectorRot, inputVelocityRot;

        // Movement Input Vectors
        Vector3 inputVectorMove, inputVelocityMove;

        #endregion


        #region References

        CharacterController _characterController;

        #endregion

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _isWaveRiding = false;
        }

        private void Update()
        {
            // ----- Alt Movement Test-----

            _isGrounded = IsGrounded();
            AlternativeSmoothMovement();

            // ----- Direction Functions -----

            
            // ----- Gravity Functions -----
        
           

            // ----- General Movement Functions -----

           

            


        }

        #region Alternative Functions

        

        private void AlternativeSmoothMovement()
        {
            inputVectorRot = Vector3.SmoothDamp(inputVectorRot, _movementInput, ref inputVelocityRot, 0.1f, _isGrounded ? Mathf.Infinity : 6);

            inputVectorMove = Vector3.SmoothDamp(inputVectorMove, _movementInput, ref inputVelocityMove, 0.05f, turnSpeed);

            if (inputVectorRot != Vector3.zero)
            { 
                Quaternion targetRot = Quaternion.LookRotation(inputVectorRot.ToIso(), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

                playerModelPivot.transform.rotation = Quaternion.RotateTowards(playerModelPivot.transform.rotation, targetRot, 100.0f * Time.deltaTime);
            }

            _altVelocity.x = inputVectorMove.ToIso().x * moveSpeed;
            _altVelocity.z = inputVectorMove.ToIso().z * moveSpeed;

            //if (_altVelocity.y > 0.0f)
            //{
            //    _gravityVector.y = (gravitySpeed * lowGravityMultiplier);
            //}

            if (_isWaveRiding)
            {
                Debug.Log("Wave riding should be occuring");
                _gravityVector = (_waveDirection * _waveStrength);
            }

            else if (IsGrounded())
            {
                _gravityVector.x = 0.0f;
                _gravityVector.z = 0.0f;
                _gravityVector.y = (gravitySpeed / 2);
                _altVelocity.y = _gravityVector.y;
            }

            else
            {
                _gravityVector.x = 0.0f;
                _gravityVector.z = 0.0f;
                _gravityVector.y = (gravitySpeed * strongGravityMultiplier);
            }

            if (!_isWaveRiding)
            {
                _altVelocity.y += (_gravityVector.y) * Time.deltaTime;

                _altVelocity.y = Mathf.Clamp(_altVelocity.y, -terminalSpeed, terminalSpeed);

                
            }

            else
            {
                _altVelocity += (_gravityVector) * Time.deltaTime;

                _altVelocity.x = Mathf.Clamp(_altVelocity.x, -terminalSpeed, terminalSpeed);
                _altVelocity.y = Mathf.Clamp(_altVelocity.y, -terminalSpeed, terminalSpeed);
                _altVelocity.z = Mathf.Clamp(_altVelocity.z, -terminalSpeed, terminalSpeed);
            }

            _characterController.Move(_altVelocity * Time.deltaTime);

        }

        #endregion
        

        #region Gravity and Grounded Functions

        

        private bool IsGrounded()
        { 
            return _characterController.isGrounded;
        }

        #endregion

        #region Movement Functions

       

        #endregion

        
        #region Public Functions

        // Movement

        public void GetMovementInput(Vector2 movementInput)
        {
            _movementInput = new Vector3(movementInput.x, 0.0f, movementInput.y);
            //Debug.Log(_movementInput);
        }

        // Wave Detection

        public void ActivateWaveRiding(float waveStrength, Vector3 waveDirection)
        {
            
            _isWaveRiding = true;
            _waveStrength = waveStrength;
            _waveDirection = waveDirection;

            if (_altVelocity.y < 0.0f)
            { 
                _altVelocity.y = 0.0f;
            }

            //else
            //{ 
            //    _isWaveRiding = false;
            //    _waveStrength = 0.0f;
            //    _waveDirection = Vector3.zero;
            //}
        }

        public void DisableWaveRiding()
        {
            _isWaveRiding = false;
            _waveStrength = 0.0f;
            _waveDirection = Vector3.zero;
        }

        #endregion

        // The point of this region is due to my tendencies as a programmer
        // I look back on old implementations incase there is something I can use
        // Or how to progress on from that for future problems

        #region Previously Implemented / No Longer Use Functions

        // Movement

        private void CalculateSpeed()
        {
            if (_movementInput == Vector3.zero && _currentSpeed > 0.0f)
            {
                _currentSpeed -= decelerationFactor * Time.deltaTime;
            }

            if (_movementInput != Vector3.zero && _currentSpeed < moveSpeed)
            {
                _currentSpeed += accelerationFactor * Time.deltaTime;
            }

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, moveSpeed);
        }

        private void ProcessMovement()
        {
            // _direction.x = _movementInput.x;
            // _direction.z = _movementInput.z;
            //_direction += new Vector3(_movementInput.x, 0.0f, _movementInput.z);
            Vector3 moveDirection = transform.forward * (_currentSpeed * _movementInput.magnitude * Time.deltaTime);
            moveDirection.y = _velocity;
            Debug.Log(moveDirection);
            _characterController.Move(moveDirection);
        }

        // Look

        private void ProcessLookDirection()
        {
            // if (_movementInput == Vector3.zero)
            // {
            //     playerModelPivot.transform.position = _characterController.transform.position;
            //     return;
            // }

            if (_movementInput == Vector3.zero)
                return;


            //Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            //Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(_movementInput);

            Vector3 relative = (transform.position + _movementInput.ToIso()) - transform.position;
            Quaternion rot = Quaternion.LookRotation(relative, Vector3.up);





            //playerModelPivot.transform.position = characterController.transform.position;
            transform.rotation = rot;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, moveRotationSpeed * Time.deltaTime);
            //layerModelPivot.transform.rotation = Quaternion.RotateTowards(playerModelPivot.transform.rotation, rot, moveRotationSpeed * Time.deltaTime);
        }

        // Gravity

        private void ProcessGravity()
        {


            if (_characterController.isGrounded && _velocity < 0.0f)
            {
                _velocity = -1f * Time.deltaTime;

                Debug.Log("Character is Grounded: " + _characterController.isGrounded);

            }

            else
            {
                _velocity += gravitySpeed * lowGravityMultiplier * Time.deltaTime;

                Debug.Log("Character is not grounded: " + _characterController.isGrounded);
            }

            _direction.y = _velocity;
            //_movementInput.y = _velocity;

        }

        // Old alternative approach

        private void SetupForwardAndRight()
        {
            if (Camera.main != null) _forward = Camera.main.transform.forward;
            _forward.y = 0;
            _forward = Vector3.Normalize(_forward);
            _right = Quaternion.Euler(0, 90, 0) * _forward;

        }

        private void AlternativeProcessMovement()
        {
            Vector3 rightMovement = _right * (_currentSpeed * Time.deltaTime * _movementInput.x);
            Vector3 upMovement = _forward * (_currentSpeed * Time.deltaTime * _movementInput.z);

            Vector3 forwardMovement = Vector3.Normalize(rightMovement + upMovement);
            transform.forward += Vector3.RotateTowards(transform.forward, forwardMovement, 360f, rotationSpeed * Time.deltaTime);
            _characterController.Move((rightMovement + upMovement) + _direction);
        }

        #endregion

    }

}

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
        [SerializeField] float smoothTime = 0.05f;

        [Space(2)]

        [Header("Gravity")]

        [SerializeField] float maxGravitySpeed = -12.0f;
        [SerializeField] float gravityMultiplier = 2.0f;

        #endregion

        #region Private Variables

        float _currentSpeed;
        float _velocity;
        
        // Alternative method to movement

        Vector3 _forward;
        Vector3 _right;
        
        Vector3 _direction;

        // Inputs

        Vector3 _movementInput;

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
            SetupForwardAndRight();
        }

        private void Update()
        {
            // ----- Direction Functions -----

            ProcessLookDirection();
            
            // ----- Gravity Functions -----
        
            ProcessGravity();

            // ----- General Movement Functions -----

            CalculateSpeed();
            ProcessMovement();
            //AlternativeProcessMovement();

            


        }

        #region Alternative Functions

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
            transform.forward += Vector3.RotateTowards(transform.forward, forwardMovement, 360f , moveRotationSpeed * Time.deltaTime);
            _characterController.Move((rightMovement + upMovement) + _direction);
        }

        #endregion
        

        #region Gravity Functions

        private void ProcessGravity()
        {
            
            
            if (_characterController.isGrounded && _velocity < 0.0f)
            {
                _velocity = -1f * Time.deltaTime;
                
                Debug.Log("Character is Grounded: " + _characterController.isGrounded);
                
            }

            else
            {
                _velocity += maxGravitySpeed * gravityMultiplier * Time.deltaTime;
                
                Debug.Log("Character is not grounded: " + _characterController.isGrounded);
            }

            _direction.y = _velocity;
            //_movementInput.y = _velocity;
            
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
            // _direction.x = _movementInput.x;
            // _direction.z = _movementInput.z;
            //_direction += new Vector3(_movementInput.x, 0.0f, _movementInput.z);
            Vector3 moveDirection = transform.forward * (_currentSpeed * _movementInput.magnitude * Time.deltaTime);
            moveDirection.y = _velocity;
            Debug.Log(moveDirection);
            _characterController.Move(moveDirection);
        }

        private void ProcessLookDirection()
        {
            // if (_movementInput == Vector3.zero)
            // {
            //     playerModelPivot.transform.position = _characterController.transform.position;
            //     return;
            // }

            if (_movementInput == Vector3.zero)
                return;
            
            
            Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(_movementInput);

            Vector3 relative = (transform.position + multipliedMatrix) - transform.position;
            Quaternion rot = Quaternion.LookRotation(relative, Vector3.up);

            



            //playerModelPivot.transform.position = characterController.transform.position;
            transform.rotation = rot;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, moveRotationSpeed * Time.deltaTime);
            //layerModelPivot.transform.rotation = Quaternion.RotateTowards(playerModelPivot.transform.rotation, rot, moveRotationSpeed * Time.deltaTime);
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

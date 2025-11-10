using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameOffJam.Input
{
    public class PlayerInput : MonoBehaviour
    {

        // References

        GameOffIA playerInputActions;

        // private variables

        Vector2 moveInput;

        bool interactInput = false;
        bool interactAlreadyDown = false;

        bool pauseInput = false;
        bool pauseAlreadyDown = false;

        bool rotateRightInput = false;

        bool rotateLeftInput = false;

        // public properties

        public Vector2 MoveInput => moveInput;

        public bool InteractInput => interactInput;
        public bool PauseInput => pauseInput;

        public bool RotateLeftInput => rotateLeftInput;

        public bool RotateRightInput => rotateRightInput;

        private void OnEnable()
        {
            playerInputActions.PlayerMap.Enable();

            SubscribePlayerActions();
        }

        private void OnDisable()
        {
            playerInputActions.PlayerMap.Disable();

            UnsubscribePlayerActions();
        }

        private void Awake()
        {
            playerInputActions = new GameOffIA();
        }

        private void Update()
        {
            moveInput = playerInputActions.PlayerMap.Move.ReadValue<Vector2>();
            Debug.Log(interactInput);



        }

        private void SubscribePlayerActions()
        {
            playerInputActions.PlayerMap.Interact.started += OnInteract;
            playerInputActions.PlayerMap.Interact.performed += OnInteract;
            playerInputActions.PlayerMap.Interact.canceled += OnInteract;

            playerInputActions.PlayerMap.Pause.performed += OnPause;

            playerInputActions.PlayerMap.RotateRight.performed += OnRotateRight;
            playerInputActions.PlayerMap.RotateRight.canceled += OnRotateRight;

            playerInputActions.PlayerMap.RotateLeft.performed += OnRotateLeft;
            playerInputActions.PlayerMap.RotateLeft.canceled += OnRotateLeft;

        }

        private void UnsubscribePlayerActions()
        {
            playerInputActions.PlayerMap.Interact.started -= OnInteract;
            playerInputActions.PlayerMap.Interact.performed -= OnInteract;
            playerInputActions.PlayerMap.Interact.canceled -= OnInteract;

            playerInputActions.PlayerMap.Pause.performed -= OnPause;

            playerInputActions.PlayerMap.RotateRight.performed -= OnRotateRight;
            playerInputActions.PlayerMap.RotateRight.canceled -= OnRotateRight;

            playerInputActions.PlayerMap.RotateLeft.performed -= OnRotateLeft;
            playerInputActions.PlayerMap.RotateLeft.canceled -= OnRotateLeft;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();

            Debug.Log("Movement: " + context);
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                interactInput = true;
            }

            else if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Canceled)
            {
                interactInput = false;
            }
        }

        // Will clean up old functions later down the line

        private void OnInteract_Started(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                interactInput = true;
            }
        }

        private void OnInteract_Performed(InputAction.CallbackContext context)
        {
            //interactInput = context.performed;

            Debug.Log("Interact: " + context);

            //if (context.phase.IsInProgress() && !context.action.IsPressed())
            //{
            //    interactInput = true;
            //    interactAlreadyDown = true;
                
            //}

            

            Debug.Log("Interact context: " + context);
            Debug.Log("Interact bool: " + interactInput);
        }

        private void OnInteract_Canceled(InputAction.CallbackContext context)
        {
            //interactInput = context.performed;

            Debug.Log("Interact: " + context);

            if (context.phase == InputActionPhase.Canceled)
            {
                interactInput = false;
                interactAlreadyDown = false;
                
            }

            Debug.Log("Interact context: " + context);
            Debug.Log("Interact bool: " + interactInput);
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                pauseInput = true;
            }

            if (context.canceled)
            {
                pauseInput = false;
            }

            

            //pauseInput = context.performed;

            //Debug.Log("Pause context: " + context);
            //Debug.Log("Pause bool: " + pauseInput);
        }

        private void OnRotateRight(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            { 
                rotateRightInput = true;
            }

            if (context.phase == InputActionPhase.Canceled)
            { 
                rotateRightInput = false;
            }
        }

        private void OnRotateLeft(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                rotateLeftInput = true;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                rotateLeftInput = false;
            }
        }

    }
}



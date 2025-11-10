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

        // public properties

        public Vector2 MoveInput => moveInput;

        public bool InteractInput => interactInput;
        public bool PauseInput => pauseInput;

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

           
        }

        private void UnsubscribePlayerActions()
        {
            //playerInputActions.PlayerMap.Move.performed -= OnMove;
            playerInputActions.PlayerMap.Interact.started -= OnInteract;
            playerInputActions.PlayerMap.Interact.performed -= OnInteract;
            playerInputActions.PlayerMap.Interact.canceled -= OnInteract;
            playerInputActions.PlayerMap.Pause.performed -= OnPause;
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

    }
}



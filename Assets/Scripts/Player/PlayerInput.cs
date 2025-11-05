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
        bool pauseInput = false;

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
            //Debug.Log(moveInput);
        }

        private void SubscribePlayerActions()
        {
            //playerInputActions.PlayerMap.Move.performed += OnMove;
            playerInputActions.PlayerMap.Interact.performed += OnInteract;
            playerInputActions.PlayerMap.Pause.performed += OnPause;
        }

        private void UnsubscribePlayerActions()
        {
            //playerInputActions.PlayerMap.Move.performed -= OnMove;
            playerInputActions.PlayerMap.Interact.performed -= OnInteract;
            playerInputActions.PlayerMap.Pause.performed -= OnPause;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();

            Debug.Log("Movement: " + context);
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            interactInput = context.performed;

            Debug.Log("Interact: " + context);
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            pauseInput = context.performed;

            Debug.Log("Pause: " + context);
        }

    }
}



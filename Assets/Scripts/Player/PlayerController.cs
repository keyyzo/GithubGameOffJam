using GameOffJam.Input;
using UnityEngine;

namespace GameOffJam.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        // References

        PlayerMovement playerMovement;
        PlayerInput playerInput;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            playerMovement.GetMovementInput(playerInput.MoveInput);
        }
    }
}


using GameOffJam.Input;
using UnityEngine;

namespace GameOffJam.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        // References

        PlayerMovement _playerMovement;
        PlayerInput _playerInput;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            _playerMovement.GetMovementInput(_playerInput.MoveInput);
        }
    }
}


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
        PlayerInteractor _playerInteractor;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInteractor = GetComponent<PlayerInteractor>();
            
        }

        private void Update()
        {
            _playerMovement.GetMovementInput(_playerInput.MoveInput);
            _playerInteractor.SetInteracting(_playerInput.InteractInput);
        }
    }
}


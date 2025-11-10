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
        PlayerMapRotater _playerMapRotater;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInteractor = GetComponent<PlayerInteractor>();
            _playerMapRotater = GetComponent<PlayerMapRotater>();
            
        }

        private void Update()
        {
            _playerMovement.GetMovementInput(_playerInput.MoveInput);
            _playerInteractor.SetInteracting(_playerInput.InteractInput);
            _playerMapRotater.GetRotateLeftInput(_playerInput.RotateLeftInput);
            _playerMapRotater.GetRotateRightInput(_playerInput.RotateRightInput);
        }
    }
}


using GameOffJam.Interactable;
using UnityEngine;

namespace GameOffJam.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        bool isInteracting = false;
        bool hasInteracted = false;
        public bool IsInteracting => isInteracting;

        //BaseInteractable currentInteractable;
        IInteractable currentInteractable;

        private void Update()
        {
            ActivateInteract();
        }

        private void ActivateInteract()
        {
            if ((Component)currentInteractable != null && isInteracting && !hasInteracted)
            {
                hasInteracted = true;
                currentInteractable.OnInteract();
                hasInteracted = false;
            }

            
        }

        private void SetCurrentInteractable(IInteractable newInteractable)
        {
            if (newInteractable == null)
                return;

            currentInteractable = newInteractable;
            currentInteractable.OnInteractionPrompt();
            Debug.Log("Interactable Set");

        }

        private void DisableCurrentInteractable()
        {
            if (currentInteractable == null)
                return;

            currentInteractable.CancelInteractionPrompt();
            currentInteractable = null;
            hasInteracted = false;
            Debug.Log("Interactable disabled");
        }

        public void SetInteracting(bool interactInput)
        { 
            isInteracting = interactInput;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactableObject))
            {
                SetCurrentInteractable(interactableObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactableObject))
            {
                DisableCurrentInteractable();
            }
        }
    }
}



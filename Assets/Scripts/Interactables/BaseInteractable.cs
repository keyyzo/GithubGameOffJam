using UnityEngine;

namespace GameOffJam.Interactable
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {

        public abstract void CancelInteractionPrompt();
        public abstract void OnInteract();
        public abstract void OnInteractionPrompt();

        
    }
}



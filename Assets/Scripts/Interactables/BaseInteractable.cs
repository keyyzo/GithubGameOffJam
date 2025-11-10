using UnityEngine;

namespace GameOffJam.Interactable
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {

        protected bool hasBeenPrompted = false;
        protected bool hasBeenInteracted = false;


        public abstract void OnInteract();

        public abstract void OnInteractionPrompt();

        public abstract void CancelInteractionPrompt();
       
        
        

        

    }
}



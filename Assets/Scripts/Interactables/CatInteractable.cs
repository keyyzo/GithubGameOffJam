using UnityEngine;

namespace GameOffJam.Interactable
{
    public class CatInteractable : BaseInteractable, ICollectable
    {
        [SerializeField] GameObject interactText;


        // private variables

        

        private void Start()
        {
            interactText?.SetActive(false);
        }


        private void ProcessPickup()
        { 
            Destroy(gameObject);
        }

        #region Interactable Interface Functions

        public override void OnInteract()
        {
            if ((UnityEngine.Object)this != null && hasBeenPrompted && !hasBeenInteracted)
            {
                hasBeenInteracted = true;
                interactText.SetActive(false);

                OnCollect();

            }
        }

        public override void OnInteractionPrompt()
        {
            if (!hasBeenPrompted && !hasBeenInteracted)
            {
                hasBeenPrompted = true;
                interactText?.SetActive(true);
            }


        }

        public override void CancelInteractionPrompt()
        {
            if (interactText.activeSelf)
            {
                interactText.SetActive(false);
            }

            hasBeenPrompted = false;

        }

        public void OnCollect()
        {
            ProcessPickup();
        }

        #endregion

    }
}



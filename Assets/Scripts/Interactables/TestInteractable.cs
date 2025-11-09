using UnityEngine;

namespace GameOffJam.Interactable
{
    public class TestInteractable : BaseInteractable
    {
        [SerializeField] GameObject interactText;
        [SerializeField] Material interactMaterial;
        [SerializeField] Material originalMaterial;

        bool hasBeenPrompted = false;
        bool hasBeenInteracted = false;

        MeshRenderer meshRenderer;

        private void Start()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();

            interactText?.SetActive(false);
        }

        public override void OnInteract()
        {
            if (hasBeenPrompted && !hasBeenInteracted)
            {
                hasBeenInteracted = true;
                meshRenderer.material = interactMaterial;

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

        
    }
}



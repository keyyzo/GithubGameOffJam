using UnityEngine;

namespace Utilities
{
    public class Billboarding : MonoBehaviour
    {
        Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.forward = mainCamera.transform.forward;
        }
    }
}



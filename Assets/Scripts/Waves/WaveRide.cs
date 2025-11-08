using GameOffJam.Player;
using UnityEngine;

namespace GameOffJam.Waves
{
    public class WaveRide : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] BoxCollider waveTriggerCollider;
        [SerializeField] GameObject waveObject;

        [Space(2)]

        [Header("Wave Settings")]

        [SerializeField]
        [Range(10.0f,150.0f)]
        float waveStrength = 12.0f;

        [SerializeField]
        Vector3 waveDirection = Vector3.up;

        private void Start()
        {
            waveTriggerCollider.size = waveObject.transform.localScale;
            waveTriggerCollider.center = waveObject.transform.localPosition;
            waveObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Entered Wave");
                other.gameObject.GetComponent<PlayerMovement>().ActivateWaveRiding(waveStrength, waveDirection);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Left Wave");
                other.gameObject.GetComponent<PlayerMovement>().DisableWaveRiding();
            }

        }

    }
}



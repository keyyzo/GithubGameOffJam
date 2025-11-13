using GameOffJam.Managers;
using UnityEngine;

namespace GameOffJam.Level
{
    public class EndOfLevel : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                LevelManager.Instance.LevelStateTransition(LevelState.COMPLETE);
            }
        }

    }


}


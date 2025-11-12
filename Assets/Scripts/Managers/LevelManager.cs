using GameOffJam.Player;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace GameOffJam.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("Global References")]

        [SerializeField] GameObject startingArea;
        [SerializeField] GameObject finishArea;

        public GameObject playerReference;
        public PlayerController playerController;

        [Space(5)]

        [Header("Game State References")]

        [SerializeField] GameObject pauseScreen;
        [SerializeField] GameObject gameOverScreen;

        public LevelState levelState;

        const string PLAYER_TAG_STRING = "Player";

        protected override void Awake()
        {
            base.Awake();


        }

        private void Start()
        {
            playerReference = GameObject.FindWithTag(PLAYER_TAG_STRING);
            playerController = playerReference.GetComponent<PlayerController>();

            levelState = LevelState.STARTING;
        }

        private void Update()
        {
            switch (levelState)
            {
                case LevelState.STARTING:
                    HandleStartingState();
                    break;
                case LevelState.IN_PROGRESS:
                    HandleInProgressState();
                    break;
                case LevelState.FAILED:
                    HandleFailedState();
                    break;
                case LevelState.RESTARTING:
                    HandleRestartingState();
                    break;
                case LevelState.COMPLETE:
                    HandleCompleteState();
                    break;
            }
        }

        private void HandleStartingState()
        { 
            
        }

        private void HandleInProgressState()
        { 
        
        }

        private void HandleFailedState()
        { 
            
        }

        private void HandleRestartingState()
        { 
            
        }

        private void HandleCompleteState()
        { 
            
        }
   
    }

    public enum LevelState
    {
        STARTING,
        IN_PROGRESS,
        FAILED,
        RESTARTING,
        COMPLETE
    }
}
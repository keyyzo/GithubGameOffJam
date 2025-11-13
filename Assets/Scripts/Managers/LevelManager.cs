using GameOffJam.Player;
using System.Collections;
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

        [Space(2)]

        [SerializeField] float startingYOffset = 2.0f;

        public LevelState levelState;

        const string PLAYER_TAG_STRING = "Player";

        
        #region Level State Variables

        bool _hasLevelStarted = false;
        bool _isLevelComplete = false;

        bool _isGamePaused = false;

        #endregion


        protected override void Awake()
        {
            base.Awake();


        }

        private void Start()
        {
            playerReference = GameObject.FindWithTag(PLAYER_TAG_STRING);
            playerController = playerReference.GetComponent<PlayerController>();

            if (playerController)
            {
                float trueYStartingPos = playerReference.transform.position.y + (playerController.GetComponent<CharacterController>().height / 2) + startingYOffset;
                playerReference.transform.position = new Vector3(startingArea.transform.position.x, trueYStartingPos, startingArea.transform.position.z);
            }

            

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
            // TODO: Build a transition-like component, showing the level fading in / the player
            // essentially arriving into the level if time is willing
            // Once the player is in / around the correct starting position, transition to InProgress

            StartCoroutine(LevelStartupWaitRoutine(3f));
            LevelStateTransition(LevelState.IN_PROGRESS);

        }

        private void HandleInProgressState()
        {

            if (_hasLevelStarted && !_isGamePaused)
            { 
                playerController.ToggleInputActive();
            }

            


        }

        private void HandleFailedState()
        { 
            // TODO: Create a "death" state, mainly for if the player falls off / below the level
            // Have a trigger collider detect the player falling from the environment and trigger this state

        }

        private void HandleRestartingState()
        { 
            // TODO: Resets everything relating to the level, such as the player's position, what has
            // and hasn't been picked up (unless already previously finished the level and collected items),
            // resetting switches states and wave position


        }

        private void HandleCompleteState()
        {
            // TODO: Stops player movement and shows the LevelComplete screen
            // This screen will show the player's time taken to complete the level
            // and how many collectibles / coins(?) were found during the level run

            if (!_isLevelComplete)
            { 
                _isLevelComplete = true;

                Debug.Log("Player has reached the exit! Showing Level Complete screen");
            }

            
        }

        public void LevelStateTransition(LevelState stateToTransitionTo)
        {
            if (stateToTransitionTo != levelState)
            {
                levelState = stateToTransitionTo;
                Debug.Log("New state active: " + stateToTransitionTo.ToString());
            }

            else
            {
                Debug.LogWarning("Cannot transition to the current active state - Please choose another state to transition to.");
            }
        }

        IEnumerator LevelStartupWaitRoutine(float timeToWait)
        { 
            yield return new WaitForSeconds(timeToWait);
            _hasLevelStarted = true;
            yield break;
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
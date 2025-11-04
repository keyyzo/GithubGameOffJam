using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOffJam.Menu
{
    public class MenuManager : MonoBehaviour
    {


        public void OnPlayButton()
        {
            int tempNextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(tempNextScene);

        }

        public void OnSettingsButton()
        { 
            
        }

        public void OnControlsButton()
        { 
        
        }

        public void OnAboutButton()
        { 
            
        }

        public void OnExitButton()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }

    }
}



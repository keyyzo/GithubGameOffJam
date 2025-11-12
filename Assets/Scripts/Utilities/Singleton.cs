using UnityEngine;

namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindFirstObjectByType(typeof(T));

                    if (_instance == null)
                    {
                        SetUpInstance();
                    }

                    else
                    {
                        string typeName = typeof(T).Name;

                        Debug.Log("[Singleton] " + typeName + " instance already created: " + _instance.gameObject.name);
                    }
                }

                return _instance;
            }


        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }

            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private static void SetUpInstance()
        {
            _instance = (T)FindFirstObjectByType(typeof(T));

            if (_instance == null)
            {
                GameObject gameObj = new GameObject();
                gameObj.name = typeof(T).Name;

                _instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            }
        }

    }
}



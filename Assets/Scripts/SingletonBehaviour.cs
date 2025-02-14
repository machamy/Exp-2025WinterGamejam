using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();
                    if(instance == null)
                    {
                        Debug.Log("There is no instance of SingletonBehaviour<" + typeof(T).Name + ">");
                        var go = new GameObject($"@{typeof(T).Name}");
                        instance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }
                }

                return instance;
            }
        }
        private static T instance;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("There are multiple instances of SingletonBehaviour<" + typeof(T).Name + ">");
                Destroy(gameObject);
            }
        }
    }
}
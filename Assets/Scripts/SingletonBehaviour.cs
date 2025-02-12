using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {

        public T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<T>();
                    if(instance == null)
                    {
                        var go = new GameObject($"@{typeof(T).Name}");
                        instance = go.AddComponent<T>();
                    }
                }

                return instance;
            }
        }
        private T instance;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Debug.LogError("There are multiple instances of SingletonBehaviour<" + typeof(T).Name + ">");
                Destroy(gameObject);
            }
        }
    }
}
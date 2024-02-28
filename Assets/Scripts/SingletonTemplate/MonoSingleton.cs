using UnityEngine;

namespace SingletonTemplate
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                Debug.LogWarning(typeof(T) + " is null.");

                var managerGameObject = GameObject.FindWithTag("Managers");
                instance = managerGameObject.GetComponentInChildren<T>();

                if (instance != null)
                    return instance;

                var instanceGameObject = new GameObject(typeof(T) + " - Singleton", typeof(T));
                instance = instanceGameObject.GetComponentInChildren<T>();

                DontDestroyOnLoad(instanceGameObject);
                return instance;
            }
        }

        private static T instance;

        public virtual void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
            else
            {
                if (instance != (T)this)
                {
                    Destroy(this);
                }
            }
        }
    }
}

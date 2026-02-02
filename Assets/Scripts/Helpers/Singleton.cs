using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    protected static bool isThrowNullInstance = false;

    public static T Instance
    {
        get
        {
            if(_instance)
                return _instance;
            
            if (isThrowNullInstance)
            {
                Debug.LogError($"{typeof(T)} instance is null");
                throw new ArgumentNullException();
            }
            else
            {
                Debug.LogWarning($"{typeof(T)} instance is null");
                return null;
            }
        }
        private set => _instance = value;
    }
    
    protected virtual void Awake() => _instance = this as T;

    protected void OnApplicationQuit()
    {
        _instance = null;
        Destroy(gameObject);
    }
}

public abstract class SingletonPersistence<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }
}
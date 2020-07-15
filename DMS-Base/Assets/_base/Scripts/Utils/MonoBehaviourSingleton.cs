using UnityEngine;
using System.Collections;

public class MonoBehaviourSingleton<T> : MonoBehaviour
    where T : Component
{
    public bool isDestructivelyUnique = false;
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                }
                else if (_instance == null)
                {
                    Debug.LogError("There is no " + typeof(T).Name + " in the scene.");
                }
            }
            return _instance;
        }
    }
    public virtual void Awake()
    {
        if (Instance != this)
        {
            if (isDestructivelyUnique)
            {
                Destroy(gameObject);
            }
        }
    }

}


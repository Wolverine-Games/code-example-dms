using UnityEngine;
using System.Collections;


public class MonoBehaviourSingletonPersistent<T> : MonoBehaviourSingleton<T>
    where T : Component
{

    public override void Awake()
    {
        if (Instance == this)
        {
            DontDestroyOnLoad(this);
        }
        base.Awake();
    }
}
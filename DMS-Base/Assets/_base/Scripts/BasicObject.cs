using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasicObject
{
    public int id;
    [SerializeField]
    public string displayName { get; set; }
    public void Init(int newId)
    {
        id = newId;
    }
}

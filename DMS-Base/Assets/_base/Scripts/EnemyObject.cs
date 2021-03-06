﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : BasicObject, IActiveObject
{
    public event MoveToEventHandler OnMoveTo;
    public int damage;
    private float _speed;
    public float speed { get { return _speed; } set { _speed = value; } }
    public Vector3 position { get; set; }

    public void MoveTo(Vector3 newPos)
    {
        //TODO: write code to move to newPos at _speed
        Debug.Log("Moving " + displayName + " to new location: " + newPos.ToString());
        if (OnMoveTo != null)
        {
            OnMoveTo(this, newPos);
        }
    }
}

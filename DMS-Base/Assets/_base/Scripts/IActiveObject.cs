using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveObject
{
    Vector3 position { get; set; }
    float speed { get; set; }
    string displayName { get; set; }
    void MoveTo(Vector3 newPos);

    event MoveToEventHandler OnMoveTo;
}

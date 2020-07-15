using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviourSingletonPersistent<GlobalGameManager>
{
    public string displayName;

	override public void Awake()
    {
		MessageManager.dataLoadedEvent += DataLoadedHandler;
	}

	protected void DataLoadedHandler(string dataType, string filename)
	{
		Debug.Log("GameManager heard notification that data " + dataType + " loaded from " + filename);
	}
}

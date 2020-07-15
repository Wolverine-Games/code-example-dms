using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MessageManager : MonoBehaviourSingletonPersistent<MessageManager>
{
	public delegate void DataSavedEvent();
	public delegate void DataLoadedEvent(string dataType, string dataFileName);
	public static DataSavedEvent dataSavedEvent;
	public static event DataLoadedEvent dataLoadedEvent;
	// Start is called before the first frame update

	private void Start()
	{
	}

	public static void SendDataSavedEvent()
	{
		if (dataSavedEvent != null)
			dataSavedEvent.Invoke();
	}

	public static void SendDataLoadedEvent(string dataType, string filename)
	{
		if (dataLoadedEvent != null)
			dataLoadedEvent.Invoke(dataType, filename);
	}
}

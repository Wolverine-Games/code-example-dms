using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void MoveToEventHandler(IActiveObject sender, Vector3 newPos);

public class UIManager : MonoBehaviourSingleton<UIManager>
{

    public string displayName;
	public Text eventNotifyText;
	// Start is called before the first frame update
	override public void Awake()
	{
		MessageManager.dataSavedEvent += DataSavedHandler;
		MessageManager.dataLoadedEvent += DataLoadedHandler;

	}

	protected void DataSavedHandler()
	{
		StartCoroutine(ShowEventText("Player Data Saved!", 4f));
	}

	protected void DataLoadedHandler(string dataType, string filename)
	{
		StartCoroutine(ShowEventText(dataType + " Loaded from " + filename, 3f));
	}

	IEnumerator ShowEventText(string displayText, float holdTime)
	{
		if (eventNotifyText != null)
		{
			eventNotifyText.text += displayText + "\n";
			eventNotifyText.enabled = true;
		}
		yield return new WaitForSeconds(holdTime);
		if (eventNotifyText != null)
		{
			eventNotifyText.enabled = false;
		}
	}
	private void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupListeners(List<IActiveObject> objects)
    {
        foreach(IActiveObject obj in objects)
        {
            obj.OnMoveTo += OnObjectMoved;
        }
    }
    private void OnObjectMoved(IActiveObject sender, Vector3 newPos)
    {

        Debug.Log("UIManager detected " + sender.displayName + " moving to new location: " + newPos.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
	public const string RESOURCE_DB_SAMPLE_KEY_OCCUPATION = "Occupation";
	public const string RESOURCE_DB_SAMPLE_KEY_ID = "Id";

	public static string SAMPLE_CSV_FILENAME = "Sample.csv";
    public static string SAMPLE_CSV_RESOURCE_FILENAME = "Sample";
	public static string LOCALIZATION_CSV_RESOURCE_FILENAME = "LocalizationText";

	public static string DATA_CURRENCY_TYPE_CASH = "Cash";
    public static string DATA_CURRENCY_TYPE_GEMS = "Gems";

	public static string LOC_KEY_HEADER_TEXT_ID = "Text_ID";
	public static string LOC_KEY_HEADER_LANGUAGE_ENGLISH = "EN";
	public static string LOC_KEY_HEADER_LANGUAGE_FRENCH = "FR";
	public static string LOC_KEY_HEADER_LANGUAGE_GERMAN = "DE";
	public static string LOC_KEY_BUTTON_SAVE_NAME = "BUTTON_SAVE_NAME";
	public static string LOC_KEY_BUTTON_BUY_CASH_5 = "BUTTON_BUY_CASH_5";
	public static string LOC_KEY_BUTTON_BUY_GEMS_1 = "BUTTON_BUY_GEMS_1";
	public static string LOC_KEY_LABEL_PLAYER_NAME = "LABEL_PLAYER_NAME";
	public static string LOC_KEY_LABEL_CURRENT_WEAPON = "LABEL_CURRENT_WEAPON";
	public static string LOC_KEY_LABEL_FAVORED_ENEMY = "LABEL_FAVORED_ENEMY";

	public static List<CSVTestDataClass> csvData;
    public static List<Dictionary<string, object>> csvResourceDictionary;
    public static List<Dictionary<string, object>> csvFileDictionary;
    public static Dictionary<string, Dictionary<string, string>> localizationDictionary;
	// Start is called before the first frame update
	public static string currentLanguage = LOC_KEY_HEADER_LANGUAGE_ENGLISH;
    void Start()
	{
		if (StaticDataManager.LoadData(Application.dataPath))
		{
			// do something with data

		}
		else
		{
			StaticDataManager.SetupDefaultData();
		}
		//string locText = localizationDictionary["MyKey"]["en"];
		if (PersistentDataManager.LoadData())
		{
			// do something with data
		}
		else
		{
			PersistentDataManager.CreateDefaultData();
			PersistentDataManager.SaveData();
		}

		LoadLocalizationData();

	}

	private static void LoadLocalizationData()
	{
		localizationDictionary = new Dictionary<string, Dictionary<string, string>>();
		List<Dictionary<string, object>> loadLocList = CSVReader.ReadFromResources(LOCALIZATION_CSV_RESOURCE_FILENAME);
		if (loadLocList != null && loadLocList.Count > 0)
		{
			foreach(Dictionary<string, object> translation in loadLocList)
			{
				Dictionary<string, string> curTranslation = new Dictionary<string, string>();
				foreach(string key in translation.Keys)
				{
					if (key != LOC_KEY_HEADER_TEXT_ID)
					{
						curTranslation.Add(key, translation[key].ToString());
					}
				}
				localizationDictionary.Add(translation[LOC_KEY_HEADER_TEXT_ID].ToString(), curTranslation);
			}
		}
	}

	public static string GetTranslation(string locKey, string languageKey)
	{
		if (localizationDictionary != null && localizationDictionary.Count > 0)
		{
			return localizationDictionary[locKey][languageKey];
		}
		return "";
	}

	public void TestChangePlayerData()
    {
        PersistentDataManager.UpdatePlayerData("Robert Jones", "rjones");
    }

    public void TestCurrencyChange()
    {
        PersistentDataManager.ChangeCurrency(DATA_CURRENCY_TYPE_CASH, 5);
        PersistentDataManager.ChangeCurrency(DATA_CURRENCY_TYPE_GEMS, 1);
    }

    public void TestDeleteData()
    {
        PersistentDataManager.ClearSaveData();
    }

    public void TestLoadCSVData()
    {
        csvData = CSVLoader.LoadCSVFromFile(Application.dataPath + Path.DirectorySeparatorChar + "Resources", SAMPLE_CSV_FILENAME);
        csvResourceDictionary = CSVReader.ReadFromResources(SAMPLE_CSV_RESOURCE_FILENAME);
        if (csvResourceDictionary.Count == csvData.Count)
        {
            if (csvData[0].Id == (int)csvResourceDictionary[0][RESOURCE_DB_SAMPLE_KEY_ID])
            {
                Debug.Log("Item 0 ID match in Loader and Resource Read");
            }
			if (csvData[2].Occupation == (string)csvResourceDictionary[2][RESOURCE_DB_SAMPLE_KEY_OCCUPATION])
            {
                Debug.Log("Item 2 Occupation match in Loader and Resource Read");
            }
        }
        //csvFileDictionary = CSVReader.ReadFromFilePath(Application.dataPath + Path.DirectorySeparatorChar + "Resources", SAMPLE_CSV_FILENAME);
        //if (csvFileDictionary.Count == csvData.Count)
        //{
        //    if (csvData[0].Id == (int)csvFileDictionary[0]["Id"])
        //    {
        //        Debug.Log("Item 0 ID match in Loader and File Read");
        //    }
        //    if (csvData[2].Occupation == (string)csvFileDictionary[2]["Occupation"])
        //    {
        //        Debug.Log("Item 2 Occupation match in Loader and File Read");
        //    }
        //}
    }
    public void SavePlayerData()
    {
        PersistentDataManager.SaveData();
    }

    public WeaponData GetWeaponById(string id)
    {
        return StaticDataManager.GetDataById<WeaponData>(id);
    }

	public T GetDataByDName<T>(string dName) where T : BaseStaticData
	{
		List<T> curDataList = StaticDataManager.GetDataList<T>();
		foreach (T dataItem in curDataList)
		{
			if (dataItem.displayName == dName)
			{
				return dataItem;
			}
		}
		return null;
	}

	public List<WeaponData> GetWeapons()
    {
        return StaticDataManager.GetDataList<WeaponData>();
    }

    public void SelectWeapon(string dName)
    {
        WeaponData selWeapon = GetDataByDName<WeaponData>(dName);
        PersistentDataManager.UpdatePlayerData(selWeapon);
    }

	public void SelectLanguage(string langName)
	{
		currentLanguage = langName;
	}

	public void SelectEnemyType(string dName)
	{
		EnemyData selection = GetDataByDName<EnemyData>(dName);
		PersistentDataManager.UpdatePlayerData(selection);
	}

	void Update()
    {
		// Debugging and testing code
		// TODO: remove once there are ui buttons for these in the test scene
        if (Input.GetKeyDown(KeyCode.S))
        {
            SavePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            TestChangePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TestDeleteData();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            TestLoadCSVData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TestCurrencyChange();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            StaticDataManager.SaveGameData(Application.dataPath);
        }
    }
}

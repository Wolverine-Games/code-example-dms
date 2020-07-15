using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PersistentDataManager
{
    public static string PLAYER_FILENAME_PREFIX = "SaveData_";
    public static string PLAYER_FILENAME_JSON_EXT = ".json";
    public static string PREF_KEY_JSON_SAVE = "PlayerDataJson";
    public static string PREF_KEY_CURRENT_USERID = "CurrentPlayerUserId";
    public static string PLAYER_DATA_DEFAULT_NAME = "Player1";
    public static string PLAYER_DATA_DEFAULT_USERNAME = "Player_1";
    private static PlayerData _currentPlayer;

    public static void ClearSaveData()
    {
        PlayerPrefs.DeleteKey(PREF_KEY_JSON_SAVE);
        CreateDefaultData();
    }
    public static void CreateDefaultData()
    {
        _currentPlayer = new PlayerData();
        _currentPlayer.id = 1;
        _currentPlayer.name = PLAYER_DATA_DEFAULT_NAME;
        _currentPlayer.userId = PLAYER_DATA_DEFAULT_USERNAME;
    }
    public static bool LoadData()
    {
        string lastUserId = PlayerPrefs.GetString(PREF_KEY_CURRENT_USERID, "");
        if (!string.IsNullOrEmpty(lastUserId)){
            string filename = Application.persistentDataPath + Path.DirectorySeparatorChar + PLAYER_FILENAME_PREFIX + lastUserId + PLAYER_FILENAME_JSON_EXT;
            bool isLoaded = LoadFromJson(filename);
            if (isLoaded)
            {
                Debug.Log("Player Loaded: " + _currentPlayer.name.ToString() + " Userid: " + _currentPlayer.userId);
				MessageManager.SendDataLoadedEvent("PlayerData", filename);

			}
			return isLoaded;
        }
        else
        {
            return false;
        }
    }

    private static bool LoadFromJson(string filename)
    {
        string jsonString = File.ReadAllText(filename);
        //        string jsonString = PlayerPrefs.GetString(PREF_KEY_JSON_SAVE);
        //        Debug.Log("Loading Json: " + jsonString);
        if (!string.IsNullOrEmpty(jsonString))
        {
            PlayerData tempObj = JsonUtility.FromJson<PlayerData>(jsonString);
            if (tempObj != null)
            {
                _currentPlayer = tempObj;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private static bool LoadFromPrefs()
    {
        _currentPlayer = new PlayerData();
        _currentPlayer.name = PlayerPrefs.GetString("PlayerName", "Player1");
        //_currentPlayer.id = PlayerPrefs.GetString("PlayerId", "0");
        _currentPlayer.userId = PlayerPrefs.GetString("PlayerUserId", "Player_1");
        return true;

    }

    private static string GetCurrentFilename()
    {
        if (_currentPlayer != null)
        {
            return PLAYER_FILENAME_PREFIX + _currentPlayer.userId.ToString() + PLAYER_FILENAME_JSON_EXT;
        }
        else
        {
            return "";
        }
    }
    public static bool SaveData()
    {
        string filename = GetCurrentFilename();
        if (!string.IsNullOrEmpty(filename))
        {
            bool isSaved = SaveToJson(filename);
            if (isSaved)
            {
                PlayerPrefs.SetString(PREF_KEY_CURRENT_USERID, _currentPlayer.userId);
                Debug.Log("Player saved: " + _currentPlayer.ToString() + "Userid: " + _currentPlayer.userId);
				MessageManager.SendDataSavedEvent();
			}

			return isSaved;
        }
        return false;
    }

    private static bool SaveToPrefs()
    {
        return false;
    }
    private static bool SaveToJson(string filename)
    {
        if (_currentPlayer != null)
        {
            string curPath = Application.persistentDataPath + Path.DirectorySeparatorChar + filename;
            string jsonString = JsonUtility.ToJson(_currentPlayer);
            File.WriteAllText(curPath, jsonString);
//            PlayerPrefs.SetString(PREF_KEY_JSON_SAVE, jsonString);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void UpdatePlayerData(string newName, string newUserId)
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.name = newName;
            _currentPlayer.userId = newUserId;
            SaveData();
        }
    }
    public static void UpdatePlayerData(string newName)
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.name = newName;
            SaveData();
        }
    }

    public static void UpdatePlayerData(WeaponData newWeapon)
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.currentWeapon = newWeapon;
            _currentPlayer.currentWeaponId = newWeapon.id;
            SaveData();
        }
    }

	public static void UpdatePlayerData(EnemyData newEnemy)
	{
		if (_currentPlayer != null)
		{
			_currentPlayer.favoredEnemy = newEnemy;
			_currentPlayer.favoredEnemyId = newEnemy.id;
			SaveData();
		}
	}

	public static string GetName()
    {
        if (_currentPlayer != null)
        {
            return _currentPlayer.name;
        }
        else
        {
            return "";
        }
    }

	public static string GetCurrentWeaponId()
	{
		if (_currentPlayer != null)
		{
			return _currentPlayer.currentWeaponId;
		}
		else
		{
			return "";
		}
	}

	public static string GetFavoredEnemyId()
	{
		if (_currentPlayer != null)
		{
			return _currentPlayer.favoredEnemyId;
		}
		else
		{
			return "";
		}
	}

	public static int GetCurrency(string type)
    {
        if (_currentPlayer != null)
        {
            if (_currentPlayer.wallet != null)
            {
                foreach (PlayerCurrencyData currency in _currentPlayer.wallet)
                {
                    if (currency.currencyType == type)
                    {
                        return currency.value;
                    }
                }
            }
        }
        return 0;
    }
    public static void ChangeCurrency(string type, int valueChange)
    {
        if (_currentPlayer != null)
        {
            if (_currentPlayer.wallet != null)
            {
                bool isFound = false;
                foreach(PlayerCurrencyData currency in _currentPlayer.wallet)
                {
                    if (currency.currencyType == type)
                    {
                        currency.value += valueChange;
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    PlayerCurrencyData newCurrency = new PlayerCurrencyData();
                    newCurrency.id = _currentPlayer.wallet.Count + 1;
                    newCurrency.currencyType = type;
                    newCurrency.value = valueChange;
                    _currentPlayer.wallet.Add(newCurrency);
                }
                SaveData();
            }
        }
    }
}

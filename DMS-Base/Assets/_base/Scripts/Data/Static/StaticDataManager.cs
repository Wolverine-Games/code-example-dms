﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StaticDataManager
{
    public static string GAMEDATA_FILENAME_PREFIX = "GameData";
    public static string GAMEDATA_FILENAME_JSON_EXT = ".json";
    private static GameData _gameData;

    public static void SetupDefaultData()
    {
		Debug.LogWarning("StaticDataManager is using default data. This should not happen. Make sure GameData.json is in the Resources folder.");
        _gameData = new GameData();
        _gameData.version = 1;

        WeaponData curWeapon = new WeaponData();
        curWeapon.id = "weapon_sword";
        curWeapon.displayName = "Sword";
        curWeapon.damage = 10;
        _gameData.weapons.Add(curWeapon);

        curWeapon = new WeaponData();
        curWeapon.id = "weapon_bow";
        curWeapon.displayName = "Bow";
        curWeapon.damage = 5;
        _gameData.weapons.Add(curWeapon);

        EnemyData curEnemy = new EnemyData();
        curEnemy.id = "enemy_melee_weak";
        curEnemy.displayName = "Orc - Weak";
        curEnemy.hp = 10;
        _gameData.enemies.Add(curEnemy);

        curEnemy = new EnemyData();
        curEnemy.id = "enemy_melee_tough";
        curEnemy.displayName = "Orc - Tough";
        curEnemy.hp = 50;
        _gameData.enemies.Add(curEnemy);

        curEnemy = new EnemyData();
        curEnemy.id = "enemy_ranged_weak";
        curEnemy.displayName = "Archer - Weak";
        curEnemy.hp = 5;
        _gameData.enemies.Add(curEnemy);
    }

    public static T GetDataById<T>(string getId) where T : BaseStaticData
    {
        if (typeof(T) == typeof(WeaponData))
        {
            foreach (WeaponData data in _gameData.weapons)
            {
                if (data.id == getId)
                {
                    return data as T;
                }
            }
        }
        if (typeof(T) == typeof(EnemyData))
        {
            foreach (EnemyData data in _gameData.enemies)
            {
                if (data.id == getId)
                {
                    return data as T;
                }
            }
        }
        return null;

    }
    public static List<T> GetDataList<T>() where T : BaseStaticData
    {
        if (typeof(T) == typeof(WeaponData))
        {
            return _gameData.weapons as List<T>;
        }
        if (typeof(T) == typeof(EnemyData))
        {
            return _gameData.enemies as List<T>;
        }
        return null;

    }

	// NOTE: actual game data should never change on client
	// TODO: remove before production release
	// this should only be used initially for creating default game data
	// Note this will save out to the path location, so you will need to 
	// copy it from there into the Resources folder
    public static bool SaveGameData(string path)
    {
        if (_gameData == null)
        {
            SetupDefaultData();
        }
        if (_gameData != null)
        {
            string filename = path + Path.DirectorySeparatorChar + GAMEDATA_FILENAME_PREFIX + GAMEDATA_FILENAME_JSON_EXT; ;
            if (!string.IsNullOrEmpty(filename))
            {
                string jsonString = JsonUtility.ToJson(_gameData, true);
                File.WriteAllText(filename, jsonString);

                return File.Exists(filename);
            }
        }
        return false;
    }

    public static bool LoadData(string path)
    {
		string filename = GAMEDATA_FILENAME_PREFIX;// + GAMEDATA_FILENAME_JSON_EXT;
		TextAsset gameDataText = Resources.Load<TextAsset>(filename);
        if (gameDataText != null)
        {
            bool isLoaded = LoadFromJson(gameDataText.text);
            if (isLoaded)
            {
				MessageManager.SendDataLoadedEvent("GameData", filename);

			}
			return isLoaded;
        }
        else
        {
            Debug.LogWarning("GameData not loaded. File Not Found: " + filename);
            return false;
        }
    }

    private static bool LoadFromJson(string jsonString)
    {
        //        string jsonString = PlayerPrefs.GetString(PREF_KEY_JSON_SAVE);
        //        Debug.Log("Loading Json: " + jsonString);
        if (!string.IsNullOrEmpty(jsonString))
        {
            GameData tempObj = JsonUtility.FromJson<GameData>(jsonString);
            if (tempObj != null)
            {
                _gameData = tempObj;
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

}

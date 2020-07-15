using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData: BaseStaticData
{
    public int version;
    public List<WeaponData> weapons;
    public List<EnemyData> enemies;
    public GameData()
    {
        weapons = new List<WeaponData>();
        enemies = new List<EnemyData>();
    }
}

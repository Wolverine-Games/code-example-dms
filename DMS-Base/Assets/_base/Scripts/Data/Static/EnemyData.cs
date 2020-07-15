using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData: BaseStaticData
{
    public int hp;
    [NonSerialized]
    public WeaponData currentWeapon;
    public string weaponId;
}

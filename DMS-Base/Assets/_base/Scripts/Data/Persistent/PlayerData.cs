using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData: BasePersistentData
{
    public string name;
    public string userId;
    public string password;
    [NonSerialized]
    public WeaponData currentWeapon;
    public string currentWeaponId;
    public List<PlayerCurrencyData> wallet = new List<PlayerCurrencyData> ();

    public PlayerData()
    {
        if (wallet == null)
        {
            wallet = new List<PlayerCurrencyData>();
        }
    }
}

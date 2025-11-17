using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;




[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Scriptable Objects/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
   [ShowInInspector] public Dictionary<string, WeaponData> data;


    public WeaponData GetWeapon(string id)
    {
        data.TryGetValue(id, out WeaponData value);
        return value;
    }
}

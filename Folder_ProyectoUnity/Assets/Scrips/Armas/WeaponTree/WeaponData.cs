using UnityEngine;

public enum PlayerType
{

    None,
    Player1,
    Player2
}

public enum WeaponType
{

    None,
    Banana,
    Coco,
    Papaya
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponID;

    public WeaponType type;
    public uint weaponLevel;


    
    public uint LevelRequirement;

    public int MaxXP;

    public float shootForce = 20f;
    public int range;

}

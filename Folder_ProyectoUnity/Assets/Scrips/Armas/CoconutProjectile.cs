using UnityEngine;

public class CoconutProjectile : Weapon
{
    [SerializeField] private int stunTime;
    public override void Fire()
    {
        Debug.Log(weaponName + " lanza un coco que aturde por " + stunTime + " segundos");
    }
    public void explode()
    {
        Debug.Log(weaponName + "uy, exploto");
    }
    public override void Reload()
    {
        Debug.Log(weaponName + " recarga sus cocos.");
    }
}

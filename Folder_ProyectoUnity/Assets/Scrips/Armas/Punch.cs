using UnityEngine;

public class Punch : Weapon
{
    [SerializeField] private Transform punchModel;
    public override void Fire()
    {
        Debug.Log("Go puñete");
        //Aún falta trabajar en la logica para que los players golpeén, posiblemente con DotWeen
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon[] allWeaponsInInspector;
    [SerializeField] private Transform weaponHolder;

    private LinkedList<Weapon> weaponLinkedList;
    private LinkedListNode<Weapon> currentlyEquippedWeaponNode;

    private void Start()
    {
        InitializeWeaponLinkedList();

        if (currentlyEquippedWeaponNode != null)
        {
            EquipWeapon(currentlyEquippedWeaponNode.Value);
        }
        else
        {
            Debug.LogWarning("No hay armas asignadas en WeaponManager.");
        }
    }

    private void InitializeWeaponLinkedList()
    {
        weaponLinkedList = new LinkedList<Weapon>();

        for (int i = 0; i < allWeaponsInInspector.Length; i++)
        {
            weaponLinkedList.AddLast(allWeaponsInInspector[i]);
        }

        currentlyEquippedWeaponNode = weaponLinkedList.First;
    }

    public void OnNextWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed || currentlyEquippedWeaponNode == null)
        {
            return;
        }

        currentlyEquippedWeaponNode = currentlyEquippedWeaponNode.Next ?? weaponLinkedList.First;
        EquipWeapon(currentlyEquippedWeaponNode.Value);
    }

    public void OnPreviousWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed || currentlyEquippedWeaponNode == null)
        {
            return;
        }

        currentlyEquippedWeaponNode = currentlyEquippedWeaponNode.Previous ?? weaponLinkedList.Last;
        EquipWeapon(currentlyEquippedWeaponNode.Value);
    }

    public void OnFireWeapon(InputAction.CallbackContext context)
    {
        if (context.performed && currentlyEquippedWeaponNode != null)
        {
            currentlyEquippedWeaponNode.Value.Fire();
        }
    }

    public void OnReloadWeapon(InputAction.CallbackContext context)
    {
        if (context.performed && currentlyEquippedWeaponNode != null)
        {
            currentlyEquippedWeaponNode.Value.Reload();
        }
    }

    private void EquipWeapon(Weapon weaponToEquip)
    {
        LinkedListNode<Weapon> node = weaponLinkedList.First;
        while (node != null)
        {
            node.Value.gameObject.SetActive(false);
            node = node.Next;
        }

        weaponToEquip.gameObject.SetActive(true);

        weaponToEquip.transform.SetParent(weaponHolder, false);
        weaponToEquip.transform.localPosition = Vector3.zero;
        weaponToEquip.transform.localRotation = Quaternion.identity;

        WeaponEffects weaponEffects = weaponToEquip.GetComponent<WeaponEffects>();
        if (weaponEffects != null)
        {
            weaponEffects.PlaySwitchTween();
        }

        Debug.Log($"Arma quipada: {weaponToEquip.name}");
    }
}

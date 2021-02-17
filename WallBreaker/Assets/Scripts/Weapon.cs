using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    float strength = 10.0f;

    bool bIsActivated = false;

    Player _owner;

    private void Start()
    {
        _owner = GetComponentInParent<Player>();
    }

    public bool Activate
    {
        get { return bIsActivated; }
        set { bIsActivated = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bIsActivated)
            return;

        Wall target = other.GetComponent<Wall>();
        if (target)
            target.TakeDamage(strength);

        Pickup newWeapon = other.GetComponent<Pickup>();
        if (newWeapon)
        {
            GameObject weapon = Instantiate(newWeapon.GetWeaponFromPickup(), transform.parent);
            weapon.name = newWeapon.GetWeaponFromPickup().name;
            newWeapon.ChangeDisplay(gameObject);
            _owner.ResetWeapon(weapon.GetComponent<Weapon>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    float strength = 10.0f;

    [SerializeField]
    float speedEffect = 0.9f;

    [SerializeField]
    float maxDurability = 100.0f;

    float currentDurability = 0.0f;

    public float Speed
    {
        get { return speedEffect; }
    }

    bool bIsActivated = false;

    Player _owner;

    private void Start()
    {
        currentDurability = maxDurability;
        _owner = GetComponentInParent<Player>();
    }

    public bool Activate
    {
        get { return bIsActivated; }
        set { bIsActivated = value; }
    }

    private void DamageDurability(float damage)
    {
        currentDurability -= damage;
        if (currentDurability <= 0)
        {
            _owner.ResetWeapon(null);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bIsActivated)
            return;

        Wall target = other.GetComponent<Wall>();
        if (target)
        {

            DamageDurability(target.DurabilityDamage);
            target.TakeDamage(strength);
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float strength = 10.0f;

    [SerializeField]
    protected float speedEffect = 0.9f;

    [SerializeField]
    protected float maxDurability = 100.0f;

    [SerializeField]
    protected float pointsForSpecial = 100.0f;

    protected float _currentDurability = 0.0f;

    protected float _currentPoints = 0.0f;

    public float Speed
    {
        get { return speedEffect; }
    }

    protected bool _bIsActivated = false;

    public bool Activate
    {
        get { return _bIsActivated; }
        set { _bIsActivated = value; }
    }

    protected Player _owner;

    protected void Start()
    {
        _currentDurability = maxDurability;
        _owner = GetComponentInParent<Player>();
    }

    protected void DamageDurability(float damage)
    {
        _currentDurability -= damage;
        if (_currentDurability <= 0)
        {
            _owner.ResetWeapon(null);
            Destroy(gameObject);
        }
    }

    public bool IsSpecialReady()
    {
        return _currentPoints == pointsForSpecial;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (!_bIsActivated)
            return;

        Wall target = other.GetComponent<Wall>();
        if (target)
        {
            _currentPoints = Mathf.Min(_currentPoints + target.checkBonus(strength), pointsForSpecial);
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

    public virtual void UseSpecialAttack()
    {

    }
}

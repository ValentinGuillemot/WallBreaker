using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum EWeaponType
    {
        Staff,
        Sword,
        Hammer
    }

    protected EWeaponType _type;

    public EWeaponType Type
    {
        get { return _type; }
    }

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

    protected Vector3 _initLocalPos = Vector3.zero;
    protected Vector3 _initLocalScale = Vector3.one;
    protected Quaternion _initLocalRot = Quaternion.identity;

    protected bool _bIsUsingSpecial = false;

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

    public Player Owner
    {
        get { return _owner; }
        set { _owner = value; }
    }

    public void ResetTransform()
    {
        transform.localPosition = _initLocalPos;
        transform.localRotation = _initLocalRot;
        transform.localScale = _initLocalScale;
    }

    protected virtual void Start()
    {
        _currentDurability = maxDurability;
        _owner = GetComponentInParent<Player>();

        _initLocalPos = transform.localPosition;
        _initLocalScale = transform.localScale;
        _initLocalRot = transform.localRotation;
    }

    protected void DamageDurability(float damage)
    {
        _currentDurability -= damage;
        if (_currentDurability <= 0)
        {
            _owner.ChangeWeapon(null);
            DestroyWeapon();
        }
    }

    protected virtual void DestroyWeapon()
    {
        Destroy(gameObject);
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
            AttackWall(target);

        Pickup newWeapon = other.GetComponent<Pickup>();
        if (newWeapon && !_bIsUsingSpecial)
            ChangeWeapon(newWeapon);
    }

    protected void AttackWall(Wall target)
    {
        float potentialBonus = target.checkBonus(strength);
        if (potentialBonus > 0)
        {
            _currentPoints = Mathf.Min(_currentPoints + target.checkBonus(strength), pointsForSpecial);
            _owner.UpdateSpecialUI(_currentPoints / pointsForSpecial);
        }
        DamageDurability(target.DurabilityDamage);
        _owner.UpdateDurabilityUI(_currentDurability / maxDurability);
        target.TakeDamage(strength);
    }

    protected void ChangeWeapon(Pickup newWeapon)
    {
        GameObject equippedObject = newWeapon.GivePickupToTransform(transform.parent);
        Weapon equippedWeapon = equippedObject.GetComponent<Weapon>();
        _owner.ChangeWeapon(equippedWeapon);
        equippedWeapon.Owner = _owner;
        equippedWeapon.ResetTransform();
        _owner = null;

        transform.parent = newWeapon.GetParentForPickup().transform;
        ResetTransform();
    }
    
    public float GetDurabilityRatio()
    {
        return _currentDurability / maxDurability;
    }

    public float GetSpecialRatio()
    {
        return _currentPoints / pointsForSpecial;
    }

    public virtual void UseSpecialAttack()
    {

    }
}

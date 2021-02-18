using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField]
    GameObject damageZonePrefab = null;

    GameObject _damageZone;

    protected override void Start()
    {
        base.Start();

        _type = EWeaponType.Hammer;
    }

    public override void UseSpecialAttack()
   {
        StartCoroutine(StartSpecialAttack());
   }

    public IEnumerator StartSpecialAttack()
    {
        bool hasStarted = false;

        if (!hasStarted)
        {
            hasStarted = true;
            yield return new WaitForSeconds(0.5f);
        }

        _bIsUsingSpecial = true;
        _damageZone = Instantiate(damageZonePrefab);
        _damageZone.transform.position = _owner.transform.position;
        _damageZone.transform.rotation = Quaternion.identity;

        _currentPoints = 0.0f;
        _owner.UpdateSpecialUI(0.0f);
        StartCoroutine(DestroyDamage());
    }

    IEnumerator DestroyDamage()
    {
        bool hasPassed = false;

        if (!hasPassed)
        {
            hasPassed = true;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(_damageZone);
        _bIsUsingSpecial = false;
    }

    protected override void DestroyWeapon()
    {
        Destroy(_damageZone);
        base.DestroyWeapon();
    }
}

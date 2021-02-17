using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField]
    GameObject damageZonePrefab;

    GameObject _damageZone;

   public override void UseSpecialAttack()
   {
        _damageZone = Instantiate(damageZonePrefab);
        _damageZone.transform.position = _owner.transform.position;
        _damageZone.transform.rotation = Quaternion.identity;

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
    }
}

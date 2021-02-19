using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField]
    GameObject attackPrefab = null;

    [SerializeField]
    GameObject feedbackPrefab = null;

    [SerializeField]
    float specialDist = 1.0f;

    [SerializeField]
    float specialSpeed = 1.0f;

    GameObject _slashObject = null;
    GameObject _slashFeedback = null;

    protected override void Start()
    {
        base.Start();

        _type = EWeaponType.Sword;
    }

    public override void UseSpecialAttack()
    {
        if (_bIsUsingSpecial)
            return;

        _bIsUsingSpecial = true;
        StartCoroutine(StartSpecialAttack());
    }

    IEnumerator StartSpecialAttack()
    {
        bool hasStarted = false;

        if (!hasStarted)
        {
            hasStarted = true;
            yield return new WaitForSeconds(1.2f);
        }

        _slashObject = Instantiate(attackPrefab, _owner.transform);
        _slashObject.transform.localPosition = new Vector3(-specialDist, 0.0f, specialDist);

        _slashFeedback = Instantiate(feedbackPrefab, _owner.transform);
        _slashFeedback.transform.localPosition = new Vector3(0.0f, 1.0f, 1.0f);

        _currentPoints = 0.0f;
        _owner.UpdateSpecialUI(0.0f);
        StartCoroutine(MoveSlash());
    }

    IEnumerator MoveSlash()
    {
        for (float i = -specialDist; i < specialDist;)
        {
            i += Time.deltaTime * specialSpeed;
            _slashObject.transform.localPosition = new Vector3(i, 0.0f, specialDist);
            yield return new WaitForFixedUpdate();
        }

        Destroy(_slashObject);
        Destroy(_slashFeedback);
        _bIsUsingSpecial = false;
    }

    protected override void DestroyWeapon()
    {
        Destroy(_slashObject);
        Destroy(_slashFeedback);
        base.DestroyWeapon();
    }
}

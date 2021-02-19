using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    [SerializeField]
    int nbProjectile = 6;

    [SerializeField]
    float specialRadius = 1.0f;

    [SerializeField]
    float rotationSpeed = 1.0f;

    [SerializeField]
    float timeSpeed = 1.0f;

    [SerializeField]
    GameObject projectilePrefab = null;

    GameObject _specialMiddle;

    protected override void Start()
    {
        base.Start();

        _type = EWeaponType.Staff;
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
            yield return new WaitForSeconds(0.75f);
        }

        _specialMiddle = Instantiate(new GameObject(), _owner.transform);
        _specialMiddle.name = "SpecialAttack";

        float angleMove = 2 * Mathf.PI / (float)nbProjectile;
        for (float currentAngle = 0.0f; currentAngle < 2 * Mathf.PI; currentAngle += angleMove)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, _specialMiddle.transform);
            Vector3 relativePos = new Vector3(Mathf.Cos(currentAngle) * specialRadius, 0.0f, Mathf.Sin(currentAngle) * specialRadius);
            newProjectile.transform.localPosition = relativePos;
        }

        StartCoroutine(StopSpecial());
    }

    IEnumerator StopSpecial()
    {
        while (_currentPoints > 0.0f)
        {
            _currentPoints -= Time.deltaTime * timeSpeed;
            if (_currentPoints < 0.0f)
                _currentPoints = 0.0f;

            _owner.UpdateSpecialUI(_currentPoints / pointsForSpecial);
            _specialMiddle.transform.rotation *= Quaternion.Euler(0.0f, Time.deltaTime * rotationSpeed, 0.0f);
            yield return new WaitForFixedUpdate();
        }

        Destroy(_specialMiddle);
        _bIsUsingSpecial = false;
    }

    protected override void DestroyWeapon()
    {
        _currentPoints = 0.0f;
        Destroy(_specialMiddle);
        base.DestroyWeapon();
    }
}

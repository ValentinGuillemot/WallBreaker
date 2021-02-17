﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float runningSpeed = 1.0f;

    [SerializeField]
    float rotationSpeed = 2.0f;

    [SerializeField]
    Transform weaponHandle;

    public Transform WeaponHandle
    {
        get { return weaponHandle; }
    }

    Rigidbody _rb;
    Vector3 _dir;
    Animator _anim;
    Weapon _equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _equippedWeapon = GetComponentInChildren<Weapon>();
    }

    public void ResetWeapon(Weapon newWeapon)
    {
        if (_equippedWeapon)
            Destroy(_equippedWeapon.gameObject);
        _equippedWeapon = newWeapon;
    }

    public bool HasWeapon()
    {
        return _equippedWeapon != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_equippedWeapon)
                _equippedWeapon.Activate = true;
            _anim.SetTrigger("Attack");
            StartCoroutine(ResetAttack());
        }

        ComputeMovement();
    }

    IEnumerator ResetAttack()
    {
        bool hasWaited = false;

        if (!hasWaited)
        {
            hasWaited = true;
            yield return new WaitForSeconds(1.0f);
        }

        if (_equippedWeapon)
            _equippedWeapon.Activate = false;
        _anim.ResetTrigger("Attack");
    }

    void ComputeMovement()
    {
        ComputeForwardMovement();
        ComputeRotation();
    }

    void ComputeForwardMovement()
    {
        float movingSpeed = runningSpeed * (_equippedWeapon ? _equippedWeapon.Speed : 1.0f);
        float z = Input.GetAxis("Vertical");
        Vector3 moveZ = new Vector3(0, 0.0f, z);
        moveZ = transform.rotation * moveZ;
        _rb.velocity = (moveZ * 1000.0f * Time.deltaTime * movingSpeed);
    }

    void ComputeRotation()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 move = transform.rotation * new Vector3(x, 0.0f, 0.0f);

        if (move.sqrMagnitude != 0)
            _dir = move.normalized;
        else
        {
            _rb.rotation = transform.rotation;
            return;
        }

        Quaternion rotate = Quaternion.FromToRotation(transform.forward, _dir);
        _rb.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * rotate, Time.deltaTime * rotationSpeed);
    }
}

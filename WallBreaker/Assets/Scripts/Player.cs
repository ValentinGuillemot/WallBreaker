using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    float runningSpeed = 1.0f;

    [SerializeField]
    float rotationSpeed = 2.0f;

    [SerializeField]
    Transform weaponHandle = null;

    public Transform WeaponHandle
    {
        get { return weaponHandle; }
    }

    #region UI
    [SerializeField]
    Image durabilityBar = null;
    float _initDurabilityWidth;

    [SerializeField]
    Image specialBar = null;
    float _initSpecialWidth;

    [SerializeField]
    Image weaponIcon = null;
    [SerializeField]
    Sprite staffIcon = null;
    [SerializeField]
    Sprite swordIcon = null;
    [SerializeField]
    Sprite hammerIcon = null;
    #endregion

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

        _initDurabilityWidth = 300;
        _initSpecialWidth = 200;
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        _equippedWeapon = newWeapon;

        UpdateDurabilityUI( _equippedWeapon ? _equippedWeapon.GetDurabilityRatio() : 0.0f);
        UpdateSpecialUI( _equippedWeapon ? _equippedWeapon.GetSpecialRatio() : 0.0f);

        Color c = weaponIcon.color;
        if (!_equippedWeapon)
        {
            c.a = 0.0f;
            weaponIcon.color = c;
            return;
        }

        c.a = 1.0f;
        weaponIcon.color = c;
        switch (_equippedWeapon.Type)
        {
            case Weapon.EWeaponType.Staff: weaponIcon.sprite = staffIcon; break;
            case Weapon.EWeaponType.Sword: weaponIcon.sprite = swordIcon; break;
            case Weapon.EWeaponType.Hammer: weaponIcon.sprite = hammerIcon; break;
        }
    }

    public bool HasWeapon()
    {
        return _equippedWeapon != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            Application.Quit();

        if (Input.GetMouseButtonDown(0))
        {
            if (_equippedWeapon)
                _equippedWeapon.Activate = true;
            _anim.SetTrigger("Attack");
            StartCoroutine(ResetAttack());
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_equippedWeapon && _equippedWeapon.IsSpecialReady())
                SpecialAttack();
        }

        if (Input.GetKeyDown("space"))
            _rb.AddForce(0.0f, 250.0f, 0.0f);

        ComputeMovement();
    }

    void SpecialAttack()
    {
        switch (_equippedWeapon.Type)
        {
            case Weapon.EWeaponType.Staff: _anim.SetTrigger("StaffSpecial"); break;
            case Weapon.EWeaponType.Sword: _anim.SetTrigger("SwordSpecial"); break;
            case Weapon.EWeaponType.Hammer: _anim.SetTrigger("HammerSpecial"); break;
        }

        _equippedWeapon.UseSpecialAttack();
        StartCoroutine(ResetAttack());
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
        _rb.velocity = (moveZ * 10.0f * movingSpeed) + new Vector3(0.0f, _rb.velocity.y, 0.0f);
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
        _rb.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * rotate, 0.005f * rotationSpeed);
    }

    public void UpdateDurabilityUI(float ratio)
    {
        float y = durabilityBar.rectTransform.sizeDelta.y;
        durabilityBar.rectTransform.sizeDelta = new Vector2(ratio * _initDurabilityWidth, y);
    }

    public void UpdateSpecialUI(float ratio)
    {
        float y = specialBar.rectTransform.sizeDelta.y;
        specialBar.rectTransform.sizeDelta = new Vector2(ratio * _initSpecialWidth, y);
    }
}

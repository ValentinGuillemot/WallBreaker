using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    float strength = 10.0f;

    bool bIsActivated = false;

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
    }
}

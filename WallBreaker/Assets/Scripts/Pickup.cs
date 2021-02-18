using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject GetParentForPickup()
    {
        return GetComponentsInChildren<Transform>()[1].gameObject;
    }

    public GameObject GivePickupToTransform(Transform newOwner)
    {
        GameObject weapon = GetComponentsInChildren<Transform>()[2].gameObject;
        weapon.transform.parent = newOwner;
        return weapon;
    }

    public void ChangeDisplay(GameObject newDisplay)
    {
        Destroy(GetComponentsInChildren<Transform>()[2].gameObject);
        GameObject newPickup = Instantiate(newDisplay, GetComponentsInChildren<Transform>()[1]);
        newPickup.name = newDisplay.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            if (!player.HasWeapon())
            {
                GameObject weapon = GetComponentsInChildren<Transform>()[2].gameObject;
                weapon.transform.parent = player.WeaponHandle;
                weapon.GetComponent<Weapon>().ResetTransform();
                weapon.GetComponent<Weapon>().Owner = player;
                player.ChangeWeapon(weapon.GetComponent<Weapon>());
                Destroy(gameObject);
            }
        }
    }
}

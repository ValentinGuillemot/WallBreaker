using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject GetWeaponFromPickup()
    {
        return GetComponentsInChildren<Transform>()[2].gameObject;
    }

    public void ChangeDisplay(GameObject newDisplay)
    {
        Destroy(GetComponentsInChildren<Transform>()[2].gameObject);
        GameObject newPickup = Instantiate(newDisplay, GetComponentsInChildren<Transform>()[1]);
        newPickup.name = newDisplay.name;
    }
}

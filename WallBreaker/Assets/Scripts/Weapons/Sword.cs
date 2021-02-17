using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override void UseSpecialAttack()
    {
        Debug.Log("Ultra Slash!");
    }
}

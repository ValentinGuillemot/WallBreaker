using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    Material material = null;

    [SerializeField]
    float maxHP = 50.0f;

    [SerializeField]
    float damageOnDurability = 10.0f;

    [SerializeField]
    float pointsWhenDestroyed = 10.0f;

    public float DurabilityDamage
    {
        get { return damageOnDurability; }
    }

    float _currentHP = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        material = new Material(material);
        GetComponent<MeshRenderer>().material = material;
        _currentHP = maxHP;
        material.SetFloat("destructionRatio", 0.0f);
    }

    public float checkBonus(float damage)
    {
        return ( (damage >= _currentHP) ? pointsWhenDestroyed : 0.0f );
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        Debug.Log(_currentHP);
        if (_currentHP <= 0)
            Destroy(gameObject);

        float ratio = _currentHP / maxHP;
        material.SetFloat("destructionRatio", 1.0f - ratio);
    }
}

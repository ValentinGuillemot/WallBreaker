using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    float strength = 1.0f;

    bool pierce = true;

    Transform cameraTransform;

    private void Start()
    {
        cameraTransform = GameObject.FindObjectOfType<Camera>().transform;
    }

    private void Update()
    {
        transform.forward = cameraTransform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Wall target = other.GetComponent<Wall>();
        if (target)
        {
            target.TakeDamage(strength);
            if (!pierce)
                Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    [SerializeField]
    int nbSprites = 3;

    [SerializeField]
    float radius = 1.0f;

    [SerializeField]
    Sprite toDisplay = null;

    [SerializeField]
    float rotationSpeed = 1.0f;

    List<GameObject> _sprites = new List<GameObject>();

    Transform _cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = GameObject.FindObjectOfType<Camera>().transform;

        float angleMove = 2 * Mathf.PI / (float)nbSprites;
        for (float currentAngle = 0.0f; currentAngle < 2 * Mathf.PI; currentAngle += angleMove)
        {
            GameObject sprite = new GameObject();
            sprite.transform.parent = transform;
            sprite.name = "RotatingSprite";
            SpriteRenderer r = sprite.AddComponent<SpriteRenderer>();
            r.sprite = toDisplay;

            Vector3 relativePos = new Vector3(Mathf.Cos(currentAngle) * radius, 0.0f, Mathf.Sin(currentAngle) * radius);
            sprite.transform.localPosition = relativePos;

            _sprites.Add(sprite);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _sprites.Count; i++)
            _sprites[i].transform.forward = _cameraTransform.forward;

        transform.rotation *= Quaternion.Euler(0.0f, Time.deltaTime * rotationSpeed, 0.0f);
    }
}

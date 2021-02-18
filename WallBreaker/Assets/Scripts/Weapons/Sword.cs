using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField]
    GameObject attackPrefab;

    [SerializeField]
    GameObject feedbackPrefab;

    [SerializeField]
    float specialDist = 1.0f;

    [SerializeField]
    float specialSpeed = 1.0f;

    GameObject _slashObject;
    GameObject _slashFeedback;

    public override void UseSpecialAttack()
    {
        _bIsUsingSpecial = true;
        _slashObject = Instantiate(attackPrefab, _owner.transform);
        _slashObject.transform.localPosition = new Vector3(-specialDist, 0.0f, specialDist);

        _slashFeedback = Instantiate(feedbackPrefab, _owner.transform);
        _slashFeedback.transform.localPosition = new Vector3(0.0f, 1.0f, 1.0f);

        _owner.UpdateSpecialUI(0.0f);
        StartCoroutine(MoveSlash());
    }

    IEnumerator MoveSlash()
    {
        for (float i = -specialDist; i < specialDist;)
        {
            i += Time.deltaTime * specialSpeed;
            _slashObject.transform.localPosition = new Vector3(i, 0.0f, specialDist);
            yield return new WaitForFixedUpdate();
        }

        Destroy(_slashObject);
        Destroy(_slashFeedback);
        _bIsUsingSpecial = false;
    }
}

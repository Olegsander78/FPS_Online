using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private const string GROUNDED = "Grounded";
    private const string SPEED = "Speed";

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private CheckFly _checkFly;

    [SerializeField]
    private Character _character;

    void Update()
    {
        var localVelocity = _character.transform.InverseTransformVector(_character.Velocity);
        var speed = localVelocity.magnitude / _character.Speed;
        var sign = Mathf.Sign(localVelocity.z);

        _animator.SetFloat(SPEED, speed * sign);
        _animator.SetBool(GROUNDED, _checkFly.IsFly == false);
    }
}

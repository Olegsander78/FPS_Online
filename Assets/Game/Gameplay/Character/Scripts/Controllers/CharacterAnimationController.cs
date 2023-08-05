using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private const string GROUNDED = "Grounded";
    private const string SPEED = "Speed";
    private const string CROUCHE = "isCrouche";

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Animator _animatorFullBody;

    [SerializeField]
    private CheckFly _checkFly;

    [SerializeField]
    private Character _character;

    private void Start()
    {
        _character.OnCrouched += OnCrouche;
    }

    private void OnCrouche(bool isCrouche)
    {
        _animatorFullBody.SetBool(CROUCHE, isCrouche);
    }

    void Update()
    {
        var localVelocity = _character.transform.InverseTransformVector(_character.Velocity);
        var speed = localVelocity.magnitude / _character.Speed;
        var sign = Mathf.Sign(localVelocity.z);

        _animator.SetFloat(SPEED, speed * sign);
        _animator.SetBool(GROUNDED, _checkFly.IsFly == false);
    }

    private void OnDestroy()
    {
        _character.OnCrouched -= OnCrouche;
    }
}

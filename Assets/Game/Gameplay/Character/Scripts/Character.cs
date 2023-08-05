using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected const float CROUCHED_HEIGHT = 1.7f;
    protected const float CROUCHED_CENTER_Y = -0.15f;
    protected const float STANDUP_HEIGHT = 2f;
    protected const float STANDUP_CENTER_Y = 0f;

    public Action<bool> OnCrouched;
    [field: SerializeField] public float Speed { get; protected set; } = 2f;
    public Vector3 Velocity { get; protected set; }
}

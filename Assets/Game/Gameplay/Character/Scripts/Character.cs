using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Action<bool> OnCrouched;
    //public Action<bool> OnStandUped;
    [field: SerializeField] public float Speed { get; protected set; } = 2f;
    public Vector3 Velocity { get; protected set; }
}

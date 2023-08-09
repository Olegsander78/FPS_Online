using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Weapon Configuration",
    menuName = "Weapon Configuration",
    order = 51)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField]
    public string ID;

    [SerializeField]
    public WeaponType WeaponType;

    [SerializeField] 
    public GameObject PrefabWeapon;
    
    [SerializeField]
    public float BulletSpeed;
    
    [SerializeField]
    public int Damage;
    
    [SerializeField]
    public float ShootDelay;    
}

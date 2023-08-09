using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public string WeaponID
    {
        get { return _weaponConfig.ID; }
    }
    
    public WeaponType WeaponType
    {
        get { return _weaponConfig.WeaponType; } 
    }

    public float BulletSpeed
    {
        get { return _weaponConfig.BulletSpeed; }
    }

    public int Damage
    {
        get { return _weaponConfig.Damage; }
    }

    public float ShootDelay
    {
        get { return _weaponConfig.ShootDelay; }
    }

    [SerializeField]
    private WeaponConfig _weaponConfig;
}

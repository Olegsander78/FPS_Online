using UnityEngine;

public class GunAnimationController : MonoBehaviour
{
    private const string SHOOT = "Shoot";
    
    [SerializeField]
    private PlayerGun _gun;

    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        _gun.OnShooted += OnShoot;
    }

    private void OnShoot()
    {
        _animator.SetTrigger(SHOOT);
    }

    private void OnDestroy()
    {
        _gun.OnShooted -= OnShoot;
    }
}

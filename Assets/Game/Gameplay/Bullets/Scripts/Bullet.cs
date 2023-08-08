using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _lifeTime = 5f;

    private int _damage;

    public void Init(Vector3 velocity, int damage = 0)
    {
        _rigidbody.velocity = velocity;

        _damage = damage;

        StartCoroutine(DelayDestroyRoutine());
    }

    private IEnumerator DelayDestroyRoutine()
    {
        yield return new WaitForSecondsRealtime(_lifeTime);
        Destroy();
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<EnemyCharacter>() != null)
        {
            var enemy = collision.collider.GetComponentInChildren<EnemyCharacter>();
            enemy.ApllyDamage(_damage);
        }

        //    if (collision.collider.TryGetComponent(out EnemyCharacter enemy))
        //{
        //    enemy.ApllyDamage(_damage);
        //}

        Destroy();
    }
}

using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float HEAD_HEIGHT = 0.6f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private int _critDamageMultiplier = 3;

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

            CapsuleCollider enemyCollider = collision.collider.GetComponentInChildren<CapsuleCollider>();

            if (enemyCollider != null)
            {
                Vector3 hitPosition = enemyCollider.transform.InverseTransformPoint(collision.contacts[0].point);

                var height = enemyCollider.height;

                var headHeight = height - HEAD_HEIGHT;

                if (hitPosition.y >= headHeight)
                {
                    enemy.ApllyDamage(_damage * _critDamageMultiplier);
                }
                else
                {
                    enemy.ApllyDamage(_damage);
                }
            }
        }

        Destroy();
    }
}

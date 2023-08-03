using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _lifeTime = 5f;

    public void Init(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;

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
        Destroy();
    }
}

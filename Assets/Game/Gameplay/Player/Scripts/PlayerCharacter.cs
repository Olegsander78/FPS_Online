using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _speed = 2f;

    [SerializeField]
    private Transform _head;

    private float _inputH, _inputV;

    public void RotateX(float value)
    {
        _head.Rotate(value, 0f, 0f);
    }

    public void SetInput(float h, float v)
    {
        _inputH = h;
        _inputV = v;
    }
    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = _rigidbody.velocity;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //var direction = new Vector3(_inputH, 0f, _inputV).normalized;
        //transform.position += direction * Time.deltaTime * _speed;

        var velocity = (transform.forward * _inputV + transform.right * _inputH).normalized * _speed;
        _rigidbody.velocity = velocity;
    }
}

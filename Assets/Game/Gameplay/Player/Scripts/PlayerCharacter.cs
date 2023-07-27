using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    private float _inputH, _inputV;
    private Vector3 _lastPosition;
    private Vector3 _velocity;

    public void SetInput(float h, float v)
    {
        _inputH = h;
        _inputV = v;
    }

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = _velocity;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var direction = new Vector3(_inputH, 0f, _inputV).normalized;
        transform.Translate(direction * Time.deltaTime * _speed, Space.World);

        _velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;
    }
}

//public class PlayerCharacter : MonoBehaviour
//{
//    [SerializeField]
//    private float _speed = 2f;

//    private float _inputH, _inputV;
//    private Vector3 _lastPosition;
//    private Vector3 _velocity;

//    public void SetInput(float h, float v)
//    {
//        _inputH = h;
//        _inputV = v;
//    }

//    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
//    {
//        position = transform.position;
//        velocity = _velocity;
//    }
//    public Vector3 GetPredictedPosition(float time)
//    {
//        var direction = new Vector3(_inputH, 0f, _inputV).normalized;
//        return transform.position + direction * _speed * time;
//    }
//    private void Update()
//    {
//        Move();
//    }

//    private void Move()
//    {
//        var direction = new Vector3(_inputH, 0f, _inputV).normalized;
//        transform.position += direction * Time.deltaTime * _speed;

//        _velocity = (transform.position - _lastPosition) / Time.deltaTime;
//        _lastPosition = transform.position;
//    }
//}

//public class PlayerCharacter : MonoBehaviour
//{
//    [SerializeField]
//    private float _speed = 2f;

//    private float _inputH, _inputV;

//    public void SetInput(float h, float v)
//    {
//        _inputH = h;
//        _inputV = v;
//    }
//    public void GetMoveInfo(out Vector3 position)
//    {
//        position = transform.position;
//    }

//    private void Update()
//    {
//        Move();
//    }

//    private void Move()
//    {
//        var direction = new Vector3(_inputH, 0f, _inputV).normalized;
//        transform.position += direction * Time.deltaTime * _speed;
//    }
//}

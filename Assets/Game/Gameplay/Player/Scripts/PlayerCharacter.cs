using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _interpoliationTime = 0.8f;

    private float _inputH, _inputV;
    private Vector3 _lastPosition;
    private Vector3 _predictedPosition;
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

    private void Start()
    {
        _lastPosition = transform.position;
        _predictedPosition = transform.position;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var direction = new Vector3(_inputH, 0f, _inputV).normalized;
        transform.position += direction * Time.deltaTime * _speed;

        var displacement = transform.position - _lastPosition;
        var displacementMagnitude = displacement.magnitude;

        if (displacementMagnitude > 0f)
        {
            _velocity = displacement / Time.deltaTime;
        }
        else
        {
            _velocity = Vector3.zero;
        }

        _lastPosition = transform.position;

        _predictedPosition = transform.position + _velocity * Time.deltaTime;

        if (_predictedPosition != _lastPosition)
        {
            transform.position = Vector3.Lerp(transform.position, _predictedPosition, _interpoliationTime);
        }
    }

    //private void Move()
    //{
    //    var direction = new Vector3(_inputH, 0f, _inputV).normalized;
    //    transform.position += direction * Time.deltaTime * _speed;

    //    _velocity = (transform.position - _lastPosition) / Time.deltaTime;
    //    _lastPosition = transform.position;

    //    _predictedPosition = transform.position + _velocity * Time.deltaTime;

    //    transform.position = Vector3.Lerp(transform.position, _predictedPosition, _interpoliationTime);
    //}
}


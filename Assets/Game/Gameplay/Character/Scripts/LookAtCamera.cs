using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _camera;

    void Start()
    {
        _camera = Camera.main.transform;
    }
    
    void LateUpdate()
    {
        transform.LookAt(_camera);
    }
}

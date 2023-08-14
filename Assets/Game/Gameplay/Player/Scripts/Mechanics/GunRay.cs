using UnityEngine;

public class GunRay : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _point;
    [SerializeField] private float _pointSize;

    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        Ray ray = new Ray(_center.position, _center.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, 50f, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _center.localScale = new Vector3(1f, 1f, hit.distance);
            _point.position = hit.point;
            var distance = Vector3.Distance(_camera.position, hit.point);
            _point.localScale = Vector3.one * distance * _pointSize;
        }
    }
}

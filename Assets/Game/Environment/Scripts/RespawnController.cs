using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance { get; private set; }

    public Vector3 CurrentRespawnPoint
    {
        get { return _currentRespawnPoint; }
    }

    [SerializeField]
    private Transform[] _respawnPoints;

    [SerializeField, ReadOnlyInInspector]
    private Vector3 _currentRespawnPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Vector3 GetRandomRespawnPoint()
    {
        int index = Random.Range(0, _respawnPoints.Length);

        _currentRespawnPoint = _respawnPoints[index].position;

        return _currentRespawnPoint;
    }

}
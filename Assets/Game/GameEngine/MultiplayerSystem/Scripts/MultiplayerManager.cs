using Colyseus;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    [field: SerializeField] public Skins Skins;    
    [field: SerializeField] public LossCounter LossCounter { get; private set; }
    [field: SerializeField] public SpawnPoints SpawnPoints { get; private set; }
    
    [SerializeField] private PlayerCharacter _player;

    [SerializeField] private Transform _parent;

    [SerializeField] private EnemyController _enemy;

    [SerializeField] private Transform _parentEnemy;

    private ColyseusRoom<State> _room;

    private Dictionary<string, EnemyController> _enemies = new();

    public void SendMessage(string key, Dictionary<string,object> data)
    {
        _room.Send(key, data);
    }

    public void SendMessage(string key, string data)
    {
        _room.Send(key, data);
    }

    public string GetSessionID() => _room.SessionId;

    protected override void Awake()
    {
        base.Awake();

        Instance.InitializeClient();
        Connect();
    }

    private async void Connect()
    {
        SpawnPoints.GetPoint(Random.Range(0, SpawnPoints.Lenght)
            , out Vector3 spawnPosition
            , out Vector3 spawnRotation);
        
        var data = new Dictionary<string, object>()
        {
            {"skins",Skins.Lenght },
            {"points", SpawnPoints.Lenght },
            {"speed", _player.Speed },
            {"hp", _player.MaxHealth },
            {"pX",spawnPosition.x },
            {"pY",spawnPosition.y },
            {"pZ",spawnPosition.z },
            {"rY",spawnRotation.y }
        };

        _room = await Instance.client.JoinOrCreate<State>("state_handler", data);

        _room.OnStateChange += OnChange;

        //Handlers inbound messages 
        _room.OnMessage<string>("Shoot", ApllyShoot);
    }

    private void ApllyShoot(string jsonShootInfo)
    {
        var shootInfo = JsonUtility.FromJson<ShootInfo>(jsonShootInfo);

        if (!_enemies.ContainsKey(shootInfo.key))
        {
            Debug.LogError("Enemy tried to shoot, bu enemy not found!");
            return;
        }

        _enemies[shootInfo.key].Shoot(shootInfo);
    }

    private void OnChange(State state, bool isFirstState)
    {
        if (!isFirstState)
            return;       

        state.players.ForEach((key, player) =>
        {
            if (key == _room.SessionId)
                CreatePlayer(player);
            else
                CreateEnemy(key,player);
        });

        _room.State.players.OnAdd += CreateEnemy;
        _room.State.players.OnRemove += RemoveEnemy;
    }

    private void CreatePlayer(Player player)
    {
        var position = new Vector3(player.pX, player.pY, player.pZ);
        Quaternion rotation = Quaternion.Euler(0f, player.rY, 0f);
        var playerCharacter = Instantiate(_player, position, rotation, _parent);

        player.OnChange += playerCharacter.OnChange;

        //Handlers inbound messages 
        _room.OnMessage<int>("Restart",playerCharacter.GetComponent<InputController>().Restart);

        playerCharacter.GetComponent<SetSkin>().Set(Skins.GetMaterial(player.skin));
    }

    private void CreateEnemy(string key, Player player)
    {
        var position = new Vector3(player.pX, player.pY, player.pZ);

        var enemy = Instantiate(_enemy, position, Quaternion.identity, _parentEnemy);
        enemy.Init(key, player);
        enemy.GetComponent<SetSkin>().Set(Skins.GetMaterial(player.skin));

        _enemies.Add(key, enemy);
    }


    private void RemoveEnemy(string key, Player player)
    {
        if (!_enemies.ContainsKey(key))
            return;

        _enemies[key].Destroy();

        _enemies.Remove(key);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();       

        _room.Leave();
    }
}

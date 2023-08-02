using Colyseus;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    [SerializeField]
    private PlayerCharacter _player;

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private EnemyController _enemy;

    [SerializeField]
    private Transform _parentEnemy;

    private ColyseusRoom<State> _room;

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
        var data = new Dictionary<string, object>()
        {
            {"speed", _player.Speed }
        };

        _room = await Instance.client.JoinOrCreate<State>("state_handler", data);

        _room.OnStateChange += OnChange;
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

        Instantiate(_player, position, Quaternion.identity, _parent);
    }

    private void CreateEnemy(string key, Player player)
    {
        var position = new Vector3(player.pX, player.pY, player.pZ);

        var enemy = Instantiate(_enemy, position, Quaternion.identity, _parentEnemy);
        enemy.Init(player);
    }


    private void RemoveEnemy(string key, Player player)
    {

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();       

        _room.Leave();
    }
}

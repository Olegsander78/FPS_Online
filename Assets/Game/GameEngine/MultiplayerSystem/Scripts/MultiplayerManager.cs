using Colyseus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private Transform _parentEnemy;

    private ColyseusRoom<State> _room;

    protected override void Awake()
    {
        base.Awake();

        Instance.InitializeClient();
        Connect();
    }

    private async void Connect()
    {
        _room = await Instance.client.JoinOrCreate<State>("state_handler");

        _room.OnStateChange += OnChange;
    }

    private void OnChange(State state, bool isFirstState)
    {
        if (!isFirstState)
            return;

        var player = state.players[_room.SessionId];
        var position = new Vector3(player.x - 200, 0f, player.y - 200) / 8;

        Instantiate(_player, position, Quaternion.identity, _parent);

        state.players.ForEach(ForEachEnemy);
    }

    private void ForEachEnemy(string key, Player player)
    {
        if (key == _room.SerializerId)
            return;

        var position = new Vector3(player.x - 200, 0f, player.y - 200) / 8;

        Instantiate(_enemy, position, Quaternion.identity, _parentEnemy);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _room.Leave();
    }
}

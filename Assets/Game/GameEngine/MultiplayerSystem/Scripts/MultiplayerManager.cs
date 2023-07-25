using Colyseus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _room.Leave();
    }
}

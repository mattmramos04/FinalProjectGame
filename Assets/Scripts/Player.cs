using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevelopersHub.RealtimeNetworking.Client;

public class Player : MonoBehaviour
{

    public enum RequestsID
    {
        AUTH = 1, SYNC = 2, BUILD = 3
    }

    private void Start()
    {
        RealtimeNetworking.OnLongReceived += ReceivedLong;
        RealtimeNetworking.OnPacketReceived += ReceivedPacket;
        ConnectToServer();
    }

    private void ReceivedLong(int id, long value)
    {
        switch(id)
        {
            case 1:
                Debug.Log(value);
                Sender.TCP_Send((int)RequestsID.SYNC, SystemInfo.deviceUniqueIdentifier);
                break;
        }
    }

    private void ReceivedPacket(Packet packet)
    {
        int id = packet.ReadInt();
        switch(id)
        {
            case 2:
            string playerClass = packet.ReadString();
                Data.Player player = Data.Deserialize<Data.Player>(playerClass);
                UI_Main.instance._goldText.text = player.gold.ToString();
                UI_Main.instance._elixirText.text = player.elixir.ToString();
                UI_Main.instance._gemsText.text = player.gems.ToString();
                break;
            case 3:
                int response = packet.ReadInt();
                switch(response)
                {
                    case 0:
                        Debug.Log("No Resources");
                        break;
                    case 1:
                        Debug.Log("Placed successfully");
                        break;
                    case 2:
                        Debug.Log("Taken");
                        break;
                }
                break;
        }
    }

    private void ConnectionResponse(bool successful)
    {
        if (successful)
        {
            RealtimeNetworking.OnDisconnectedFromServer += DisconnectedFromServer;
            string device = SystemInfo.deviceUniqueIdentifier;
            Sender.TCP_Send((int)RequestsID.AUTH, device);
        }
        else
        {
            //TODO: connection failed message box with retry button.
        }
        RealtimeNetworking.OnConnectingToServerResult -= ConnectionResponse;
    }

    private void ConnectToServer()
    {
        RealtimeNetworking.OnConnectingToServerResult += ConnectionResponse;
        RealtimeNetworking.Connect();
    }

    private void DisconnectedFromServer()
    {
        RealtimeNetworking.OnDisconnectedFromServer -= DisconnectedFromServer;
        //TODO: connection failed message box with retry button
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*** Refresh lobby read packet ***/
public class rRefreshLobby : ReadPacket {

    public static List<GameObject> lobbyComponents = new List<GameObject>();

    public override int GetId() {
        return 4;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Clears current lobby elements ***/
    public static void Clear() {
        foreach (GameObject obj in lobbyComponents) {
            GameObject.Destroy(obj);
        }

        lobbyComponents.Clear();
    }

    /*** Creates and stores buttons based off of active lobbies ***/
    public override void Gather(Reader reader) {
        Main.que.QueEvent(() => {
            Clear();
            int length = reader.ReadByte();
            float gap = 30f;
            for (int count = 0; count < length; count++) {
                lobbyComponents.Add(ButtonHandler.CreateButton(reader.ReadString(), 89.5f, 340f - (gap * count)));
            }
        });
    }
}

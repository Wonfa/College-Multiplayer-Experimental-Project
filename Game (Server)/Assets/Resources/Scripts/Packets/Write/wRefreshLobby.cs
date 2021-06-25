using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Refresh lobby write packet ***/
public class wRefreshLobby : WritePacket {

    public override int GetId() {
        return 4;
    }

    /*** Constructs a byte array using a writer which will be sent to the client for decoding ***/
    // Writes packet id
    // Writes amount of available lobbies
    // Writes names of available lobbies
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        writer.WriteByte(LobbyManager.lobbies.Count);
        foreach (Lobby lobby in LobbyManager.lobbies) {
            writer.WriteString(lobby.owner.name);
        }
        return new ByteStream(writer.buffer);
    }
}

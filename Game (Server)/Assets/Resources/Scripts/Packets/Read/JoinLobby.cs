using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Join lobby read packet ***/
public class JoinLobby : ReadPacket {

    public override int GetId() {
        return 5;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Joins the session into a lobby with the given name ***/
    public override void Gather(Session session, Reader reader) {
        LobbyManager.JoinLobby(session, reader.ReadString());
    }
}

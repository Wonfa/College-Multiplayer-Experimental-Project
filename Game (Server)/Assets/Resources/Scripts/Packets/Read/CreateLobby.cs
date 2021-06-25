using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Create lobby read packet ***/
public class CreateLobby : ReadPacket {

    public override int GetId() {
        return 1;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Creates a lobby with the given name ***/
    public override void Gather(Session session, Reader reader) {
        LobbyManager.CreateLobby(session, reader.ReadString());
    }

}

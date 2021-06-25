using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Leave lobby read packet ***/
public class LeaveLobby : ReadPacket {

    public override int GetId() {
        return 6;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Leaves a lobby ***/
    public override void Gather(Session session, Reader reader) {
        LobbyManager.LeaveLobby(session);
    }
}

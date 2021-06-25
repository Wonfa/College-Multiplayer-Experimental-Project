using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Refresh lobby read packet ***/
public class RefreshLobby : ReadPacket {

    public override int GetId() {
        return 3;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Refreshes a lobby ***/
    public override void Gather(Session session, Reader reader) {
        LobbyManager.RefreshLobby(session);
    }
}

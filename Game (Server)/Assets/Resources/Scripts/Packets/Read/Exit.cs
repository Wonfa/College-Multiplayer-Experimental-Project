using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Exit read packet ***/
public class Exit : ReadPacket {

    public override int GetId() {
        return 20;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Handles the player leaving the game ***/
    public override void Gather(Session session, Reader reader) {
        if (session == null || session.GetLobby() == null)
            return;
        session.GetLobby().Disband(1 | 2 | 4);
    }
}

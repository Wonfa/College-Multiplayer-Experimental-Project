using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Attack read packet ***/
public class Attack : ReadPacket {

    public override int GetId() {
        return 15;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Handles a player's choice to attack ***/
    public override void Gather(Session session, Reader reader) {
        if (session.player == null)
            return;
        session.player.SetProperty("animation", true);
        if (reader.ReadByte() == 1) {
            session.player.SetProperty("damaged", true);
        }
    }
}

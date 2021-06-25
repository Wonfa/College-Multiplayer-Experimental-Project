using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*** Movement read packet ***/
public class Movement : ReadPacket {

    public override int GetId() {
        return 11;
    }

    public override int GetSize() {
        return 1;
    }

    /*** Sets player properties based on what the players actions have been ***/
    public override void Gather(Session session, Reader reader) {
        int flag = reader.ReadByte();

        if (session == null || session.player == null)
            return;

        if ((flag & 1) == 1) {
            session.player.SetProperty("forward", true);
        }

        if ((flag & 2) == 2) {
            session.player.SetProperty("left", true);
        }

        if ((flag & 4) == 4) {
            session.player.SetProperty("right", true);
        }

        if ((flag & 8) == 8) {
            session.player.SetProperty("backward", true);
        }

        if ((flag & 16) == 16) {
            try {
                session.player.SetProperty("rotation", (short) reader.ReadShort());
            } catch (Exception e) {
            }
        }

        if ((flag & 32) == 32) {
            session.player.SetProperty("animation", true);
        }

        if ((flag & 64) == 64) {
            session.player.SetProperty("damaged", true);
        }
    }
}

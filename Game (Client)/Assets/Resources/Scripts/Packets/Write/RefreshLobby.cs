using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Refresh lobby write packet ***/
public class RefreshLobby : WritePacket {

    public override int GetId() {
        return 3;
    }

    /*** Creates a byte array to be sent which asks for lobby information to be returned from the server ***/
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        return new ByteStream(writer.buffer);
    }
}

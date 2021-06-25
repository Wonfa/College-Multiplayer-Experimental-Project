using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Leave lobby write packet ***/
public class LeaveLobby : WritePacket {

    public override int GetId() {
        return 6;
    }

    /*** Writes the packetid ***/
    // used as a request
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        return new ByteStream(writer.buffer);
    }
}

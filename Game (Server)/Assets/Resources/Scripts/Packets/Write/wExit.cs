using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Exit write packet ***/
public class wExit : WritePacket {

    public override int GetId() {
        return 21;
    }

    /*** Constructs a byte array using a writer which will be sent to the client for decoding ***/
    // Writes packet id
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        return new ByteStream(writer.buffer);
    }
}

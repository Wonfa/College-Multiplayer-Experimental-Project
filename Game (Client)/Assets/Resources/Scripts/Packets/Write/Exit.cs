using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Exit write packet ***/
public class Exit : WritePacket {

    public override int GetId() {
        return 20;
    }

    /*** Writes simply the packetid which will be used as a request for return ***/
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        return new ByteStream(writer.buffer);
    }
}

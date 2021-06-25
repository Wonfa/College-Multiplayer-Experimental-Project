using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Attack write packet ***/
public class Attack : WritePacket {

    public override int GetId() {
        return 15;
    }

    /*** Writes information to a byte array read to send to the server ***/
    // packet id
    // player
    // damaged or not
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        writer.WriteByte((int) args[0]);
        return new ByteStream(writer.buffer);
    }
}

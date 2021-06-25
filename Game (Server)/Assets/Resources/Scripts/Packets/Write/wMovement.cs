using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Movement write packet ***/
public class wMovement : WritePacket {

    public override int GetId() {
        return 10;
    }

    /*** Constructs a byte array using a writer which will be sent to the client for decoding ***/
    // Writes packet id
    // Writes information passed in the object array
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        writer.WriteByte((int) args[0]);
        if (args[1] != null) {
            writer.WriteShort((short) args[1]);
        }
        return new ByteStream(writer.buffer);
    }
}

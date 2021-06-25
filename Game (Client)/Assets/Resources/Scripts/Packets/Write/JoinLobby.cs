using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Join lobby write packet ***/
public class JoinLobby : WritePacket {

    public override int GetId() {
        return 5;
    }

    /*** Writes information into a byte array ***/
    // packet id
    // lobby name to join
    public override ByteStream Gather(object[] args) {
        rRefreshLobby.Clear();
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        writer.WriteString((string) args[0]);
        return new ByteStream(writer.buffer);
    }
}

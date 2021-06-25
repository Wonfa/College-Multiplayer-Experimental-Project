using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Create lobby write packet ***/
public class CreateLobby : WritePacket {

    public override int GetId() {
        return 1;
    }

    /*** Creates a byte array containing information for creating a lobby ***/
    // The packet Id
    // The lobby name
    public override ByteStream Gather(object[] args) {
        Writer writer = new Writer();
        writer.WriteByte(GetId());
        writer.WriteString(ButtonHandler.INPUT_TEXT.text);
        return new ByteStream(writer.buffer);
    }
}

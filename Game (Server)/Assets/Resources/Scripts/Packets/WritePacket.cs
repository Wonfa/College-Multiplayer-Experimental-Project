using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** An extension of packet that focuses on written information ***/
public abstract class WritePacket : Packet {

    public abstract ByteStream Gather(object[] args);
}

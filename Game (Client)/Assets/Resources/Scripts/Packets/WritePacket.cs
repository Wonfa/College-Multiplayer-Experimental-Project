using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** A write packet ***/
public abstract class WritePacket : Packet {

    public abstract ByteStream Gather(object[] args);
}

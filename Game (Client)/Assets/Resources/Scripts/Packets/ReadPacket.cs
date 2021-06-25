using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** A read packet ***/
public abstract class ReadPacket : Packet {

    public abstract int GetSize();

    public abstract void Gather(Reader reader);
}

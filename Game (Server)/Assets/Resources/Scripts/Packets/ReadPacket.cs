using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** An extension of packet that focuses on reading information ***/
public abstract class ReadPacket : Packet {

    public abstract int GetSize();

    public abstract void Gather(Session session, Reader reader);
}

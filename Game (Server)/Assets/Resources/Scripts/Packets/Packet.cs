using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** The base class for all packets ***/
public abstract class Packet : PluginReq {

    /*** The packet id ***/
    public abstract int GetId();

    public override T[] GetKeys<T>() {
        return new int[] { GetId() } as T[];
    }
}

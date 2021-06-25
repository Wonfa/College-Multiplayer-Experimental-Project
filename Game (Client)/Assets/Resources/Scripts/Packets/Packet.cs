using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Base packet class ***/
public abstract class Packet : PluginReq {

    /*** Gets the packet id ***/
    public abstract int GetId();

    public override T[] GetKeys<T>() {
        return new int[] { GetId() } as T[];
    }
}

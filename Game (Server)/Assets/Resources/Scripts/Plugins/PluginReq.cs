using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PluginReq : Object {

    public abstract T[] GetKeys<T>();
}

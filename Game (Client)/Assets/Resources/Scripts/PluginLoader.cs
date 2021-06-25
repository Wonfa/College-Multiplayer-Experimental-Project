using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;

/*** Handles the loading of plugins ***/
public class PluginLoader {

    /*** Takes a dictionary and finds all classes of the values generic type and loads them into this given dictionary ***/
    public static void Load<O, T>(ref Dictionary<O, T> map) where T : PluginReq {
        foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes()) {
            if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(T))) {
                try {
                    Debug.Log(type.Name);
                    T subClass = (T) type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    foreach (O key in (O[]) subClass.GetKeys<O>()) {
                        map[key] = subClass;
                    }
                } catch (Exception e) {
                    Debug.Log(e.ToString());
                    Debug.Log("Failed Loading Class: " + type.Name);
                }
            }
        }
    }
}

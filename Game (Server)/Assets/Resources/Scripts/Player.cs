using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Contains any information based around a player character ***/
public class Player {

    public byte deaths = 0;

    public Dictionary<string, object[]> properties = new Dictionary<string, object[]>();

    /*** Checks if the property exists for this player ***/
    public bool HasProperty(string name) {
        name = name.ToLower();
        if (properties.ContainsKey(name)) {
            return (bool) properties[name][0];
        }
        return false;
    }

    /*** Gets the value of a given named property ***/
    public object GetProperty(string name) {
        name = name.ToLower();
        if (properties.ContainsKey(name)) {
            return properties[name][1];
        }
        return null;
    }

    /*** Sets the given property a value ***/
    public void SetProperty(string name, object value) {
        name = name.ToLower();
        if (!properties.ContainsKey(name)) {
            properties.Add(name, new object[] { true, value });
        }
    }
}

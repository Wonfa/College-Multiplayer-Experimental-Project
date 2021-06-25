using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*** Handles a system to queue events ***/
// This is used to run processes that are not currently in the main thread in the main thread
public class EventQue {

    public delegate void Executable();

    public Executable execute;

    private List<Executable> events = new List<Executable>();

    public void QueEvent(Executable e) {
        events.Add(e);
    }

    public void Execute() {
        if (events.Capacity > 0) {
            Executable[] localEvents = events.ToArray();
            events.Clear();
            if (localEvents.Length > 0) {
                foreach (Executable e in localEvents) {
                    try {
                        e.Invoke();
                    } catch (Exception ex) {
                        Debug.Log(ex.ToString());
                    }
                }
            }
        }
    }
}

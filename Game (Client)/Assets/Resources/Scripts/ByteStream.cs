using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Provides a base for the reader and writer ***/
public class ByteStream {

    protected int position;
    public byte[] buffer { get; set; }
    public ByteStream(byte[] buffer) {
        if (buffer == null)
            buffer = new byte[1];
        this.buffer = buffer;
        this.position = 0;
    }

}

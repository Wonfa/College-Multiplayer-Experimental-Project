using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Handles the writing of information into a byte array ***/
public class Writer : ByteStream {

    public Writer(byte[] buffer = default(byte[])) : base(buffer) { }

    /// <summary>
    /// <para>Checks if the buffer can fit another byte.</para>
    /// <para>If it can not then create space for one.</para>
    /// </summary>
    public void CheckByte() {
        if ((position) >= buffer.Length) {
            byte[] newBuffer = new byte[buffer.Length + 1];
            buffer.CopyTo(newBuffer, 0);
            buffer = newBuffer;
        }
    }

    /// <summary>
    /// Writes a byte to the buffer.
    /// </summary>
    /// <param name="b">The byte being written.</param>
    public void WriteByte(int b) {
        CheckByte();
        buffer[position++] = (byte) b;
    }

    /// <summary>
    /// Writes a short to the buffer.
    /// </summary>
    /// <param name="s">The short being written.</param>
    public void WriteShort(short s) {
        WriteByte(s >> 8);
        WriteByte(s);
    }

    /// <summary>
    /// Writes an integer to the buffer.
    /// </summary>
    /// <param name="i">The integer being written.</param>
    public void WriteInt(int i) {
        WriteByte(i >> 24);
        WriteByte(i >> 16);
        WriteByte(i >> 8);
        WriteByte(i);
    }

    /// <summary>
    /// Writes a string to the buffer.
    /// </summary>
    /// <param name="str">The string being written.</param>
    public void WriteString(string str) {
        if (str.Length > byte.MaxValue) {
            Debug.Log("String too large");
            return;
        }
        WriteByte(str.Length);
        foreach (char character in str) {
            WriteByte((byte) character);
        }
    }
}

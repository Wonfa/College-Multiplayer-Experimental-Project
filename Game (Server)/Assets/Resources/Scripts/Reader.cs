using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*** Handles the reading of a byte array ***/
public class Reader : ByteStream {

    public Reader(byte[] buffer) : base(buffer) { }

    /// <summary>
    /// Reads an unsigned byte from the buffer.
    /// </summary>
    /// <returns>The byte.</returns>
    public int ReadByte() {
        return (buffer.Length - position) > 0 ? buffer[position++] : 0;
    }

    /// <summary>
    /// Reads a signed short from the buffer.
    /// </summary>
    /// <returns>The short.</returns>
    public int ReadShort() {
        return (ReadByte() << 8) + (ReadByte());
    }

    /// <summary>
    /// Reads a signed integer from the buffer.
    /// </summary>
    /// <returns>The int.</returns>
    public int ReadInt() {
        return (ReadByte() << 24) + (ReadByte() << 16) + (ReadByte() << 8) + (ReadByte());
    }

    /// <summary>
    /// Reads a string from the buffer.
    /// </summary>
    /// <returns>The string.</returns>
    public string ReadString() {
        string result = "";
        int length = ReadByte();
        for (int index = 0; index < length; index++) {
            int byteChar = ReadByte();
            result += (char) byteChar;
        }
        return result;
    }
}

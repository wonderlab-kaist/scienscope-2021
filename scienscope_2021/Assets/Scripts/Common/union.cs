using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public struct union_int
{
    [FieldOffset(0)]
    public int i;
    [FieldOffset(0)]
    public byte b0;
    [FieldOffset(1)]
    public byte b1;
    [FieldOffset(2)]
    public byte b2;
    [FieldOffset(3)]
    public byte b3;
}

[StructLayout(LayoutKind.Explicit)]
public struct union_float
{
    [FieldOffset(0)]
    public float f;
    [FieldOffset(0)]
    public byte b0;
    [FieldOffset(1)]
    public byte b1;
    [FieldOffset(2)]
    public byte b2;
    [FieldOffset(3)]
    public byte b3;
}

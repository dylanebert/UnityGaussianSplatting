using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class SplatData : ScriptableObject {
    public Vector3[] Positions;
    public Vector3[] Axes;
    public Color[] Colors;

    private GraphicsBuffer _positionsBuffer;
    private GraphicsBuffer _axesBuffer;
    private GraphicsBuffer _colorsBuffer;

    public GraphicsBuffer PositionsBuffer => GetBuffer(ref _positionsBuffer, Positions);
    public GraphicsBuffer AxesBuffer => GetBuffer(ref _axesBuffer, Axes);
    public GraphicsBuffer ColorsBuffer => GetBuffer(ref _colorsBuffer, Colors);

    public int Count => Positions != null ? Positions.Length : 0;

    private GraphicsBuffer GetBuffer<T>(ref GraphicsBuffer buffer, T[] data) where T : unmanaged {
        if (buffer == null) {
            buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, data.Length, Marshal.SizeOf<T>());
            buffer.SetData(data);
        }
        return buffer;
    }

    public void Dispose() {
        _positionsBuffer?.Dispose();
        _axesBuffer?.Dispose();
        _colorsBuffer?.Dispose();
    }

    private void OnDisable() {
        Dispose();
    }

    public void LoadFromFile(string filePath) {
        byte[] bytes = File.ReadAllBytes(filePath);

        int count = bytes.Length / 32;

        Positions = new Vector3[count];
        Axes = new Vector3[count * 3];
        Colors = new Color[count];

        ReadOnlySpan<ReadData> records = MemoryMarshal.Cast<byte, ReadData>(bytes);

        for (int i = 0; i < count; i++) {
            var record = records[i];

            float rotX = (record.rx - 128f) / 128f;
            float rotY = (record.ry - 128f) / 128f;
            float rotZ = (record.rz - 128f) / 128f;
            float rotW = (record.rw - 128f) / 128f;
            Quaternion rot = new(-rotX, -rotY, rotZ, rotW);

            Positions[i] = new(-record.px, -record.py, -record.pz);

            Axes[i * 3 + 0] = rot * new Vector3(record.sx, 0, 0);
            Axes[i * 3 + 1] = rot * new Vector3(0, record.sy, 0);
            Axes[i * 3 + 2] = rot * new Vector3(0, 0, record.sz);

            Colors[i] = new Color(record.r / 255f, record.g / 255f, record.b / 255f, record.a / 255f);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ReadData {
        public float px, py, pz; // position
        public float sx, sy, sz; // scale
        public byte r, g, b, a; // color
        public byte rw, rx, ry, rz; // rotation
    }
}

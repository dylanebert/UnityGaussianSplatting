using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[VFXBinder("Splat Data")]
public class SplatDataBinder : VFXBinderBase {
    public SplatData Data;

    [VFXPropertyBinding("System.UInt32")]
    private ExposedProperty _countProperty = "Count";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer")]
    private ExposedProperty _positionBufferProperty = "PositionBuffer";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer")]
    private ExposedProperty _axisBufferProperty = "AxisBuffer";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer")]
    private ExposedProperty _colorBufferProperty = "ColorBuffer";

    public override bool IsValid(VisualEffect component) {
        return Data != null &&
               component.HasUInt(_countProperty) &&
               component.HasGraphicsBuffer(_positionBufferProperty) &&
               component.HasGraphicsBuffer(_axisBufferProperty) &&
               component.HasGraphicsBuffer(_colorBufferProperty);
    }

    public override void UpdateBinding(VisualEffect component) {
        component.SetUInt(_countProperty, (uint)Data.Count);
        component.SetGraphicsBuffer(_positionBufferProperty, Data.PositionsBuffer);
        component.SetGraphicsBuffer(_axisBufferProperty, Data.AxesBuffer);
        component.SetGraphicsBuffer(_colorBufferProperty, Data.ColorsBuffer);
    }

    public override string ToString() {
        return $"Splat Data Binder: {_countProperty}, {_positionBufferProperty}, {_axisBufferProperty}, {_colorBufferProperty}";
    }
}

using UnityEngine;

public class SplatGizmoDrawer : MonoBehaviour {
    public SplatData SplatData;
    public int MaxPoints = 1000;
    public float Radius = 0.05f;

    private void OnDrawGizmos() {
        if (!SplatData) return;

        int count = SplatData.Count;
        int step = Mathf.Max(1, count / MaxPoints);

        for (int i = 0; i < count; i += step) {
            var position = transform.TransformPoint(SplatData.Positions[i]);
            var color = SplatData.Colors[i];
            Gizmos.color = color;
            Gizmos.DrawSphere(position, Radius);
        }
    }
}

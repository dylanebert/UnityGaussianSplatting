using UnityEditor;

[CustomEditor(typeof(SplatData))]
public class SplatDataInspector : Editor {
    public override void OnInspectorGUI() {
        var data = target as SplatData;
        EditorGUILayout.LabelField("Count", data.Count.ToString());
    }
}

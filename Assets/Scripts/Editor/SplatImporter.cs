using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;

[ScriptedImporter(1, "splat")]
public class SplatImporter : ScriptedImporter {
    public override void OnImportAsset(AssetImportContext context) {
        var data = ScriptableObject.CreateInstance<SplatData>();
        data.name = Path.GetFileNameWithoutExtension(context.assetPath);

        data.LoadFromFile(context.assetPath);

        context.AddObjectToAsset("Data", data);
        context.SetMainObject(data);
    }
}

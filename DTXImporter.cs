using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

public enum TextrueType
{ 
    Texture2D,
    Sprite
};

[ScriptedImporter(1, "dtx")]
public class DTXImporter : ScriptedImporter
{
    public TextrueType TextrueType;
    public bool AlphaIsTransparency = true;
    public override void OnImportAsset(AssetImportContext ctx)
    {
        DTXFile dtxFile = new DTXFile(ctx.assetPath);
        
        Texture2D texture = dtxFile.GetTexture();
        // 启用透明通道
        texture.alphaIsTransparency = AlphaIsTransparency;
        ctx.AddObjectToAsset("Texture", texture);

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        
        if (TextrueType == TextrueType.Sprite)
        {
            if (sprite == null)
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            ctx.AddObjectToAsset("Texture", sprite);
            ctx.SetMainObject(sprite);
        }
        else
        {
            ctx.SetMainObject(texture);
            DestroyImmediate(sprite);
        }

        //byte[] bytes = texture.EncodeToPNG();
        //string fileName = Application.dataPath + '/' + ctx.assetPath.Split('/').Last() + ".png";
        //File.WriteAllBytes(fileName, bytes);
    }
}

[CustomEditor(typeof(DTXImporter))]
public class DTXImporterEditor : ScriptedImporterEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    protected override void Apply()
    {
        base.Apply();
    }
}
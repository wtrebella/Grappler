using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility {
	public static T CreateAsset<T> (string name) where T : ScriptableObject {
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") path = "Assets";
		else if (Path.GetExtension (path) != "") path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/" + name + ".asset");
		
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;

		return asset;
	}

	public static T CreateAsset<T> () where T : ScriptableObject {
		return CreateAsset<T>("New " + typeof(T).ToString());
	}
}
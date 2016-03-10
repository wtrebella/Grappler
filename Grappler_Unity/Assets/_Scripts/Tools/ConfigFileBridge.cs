using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Collections.Generic;

public class ConfigFileBridge : Singleton<ConfigFileBridge> {
	[SerializeField] private string configLink_iPhone = "https://s3.amazonaws.com/grappler/iPhone/grapplerConfig_iPhone.txt";
	[SerializeField] private string configLink_Android = "https://s3.amazonaws.com/grappler/android/grapplerConfig_Android.txt";

	#if UNITY_EDITOR
	[MenuItem("Utilities/Create File")]
	public static void CreateFile() {
		string json = SerializeFile(CreateConfigFile());
		string path = "../../../Desktop/grapplerConfig_iPhone.txt";

		if (File.Exists(path)) {
			Debug.Log(path + " already exists.");
			return;
		}

		var sr = File.CreateText(path);
		sr.Write(json);
		sr.Close();
	}
	#endif

	private void Awake() {
		StartCoroutine(DownloadRoutine());
	}

	public static ConfigFile CreateConfigFile() {
		ConfigPacket[] configPackets = CreateConfigPackets();
		ConfigFile configFile = new ConfigFile();
		configFile.configPackets = configPackets;
		return configFile;
	}

	public static ConfigPacket[] CreateConfigPackets(int numPackets = 10) {
		ConfigPacket[] configPackets = new ConfigPacket[numPackets];
		for (int i = 0; i < numPackets; i++) configPackets[i] = CreateConfigPacket();
		return configPackets;
	}
		
	public static ConfigPacket CreateConfigPacket() {
		ConfigPacket testObject = new ConfigPacket();
		testObject.a = Random.Range(0, 100);
		testObject.b = Random.Range(0.0f, 100.0f);
		testObject.c = Random.value < 0.5f ? true : false;
		return testObject;
	}

	public static string SerializeFile(ConfigFile configFile) {
		return JsonUtility.ToJson(configFile, true);
	}

	public IEnumerator DownloadRoutine() {
		WWW www = new WWW(configLink_iPhone);
		yield return www;
		ConfigFile file = JsonUtility.FromJson<ConfigFile>(www.text);
		for (int i = 0; i < file.configPackets.Length; i++) {
			ConfigPacket packet = file.configPackets[i];
			Debug.Log("Packet " + i + ": a: " + packet.a + ", b: " + packet.b + ", c: " + packet.c);
		}
	}
}

using UnityEngine;
using System.Collections;

public class DiffusionDemoGUI : MonoBehaviour {

	public Diffusion diffusion;

	// Use this for initialization
	void Start () {
		diffusion.eventReceiver = gameObject;
	}
	

	void OnGUI () {
		if (GUI.Button(new Rect(20, 20, 100, 50), "Share")) {
			diffusion.Share();
		}

		if (GUI.Button(new Rect(20, 100, 100, 50), "Share pic")) {
			diffusion.Share("Check out my photo!", null, "file://" + Application.streamingAssetsPath + "/diffusionDemo.png");
		}

		if (GUI.Button(new Rect(20, 200, 100, 50), "Post Twitter")) {
			diffusion.PostToTwitter("Check out my photo!", null, "file://" + Application.streamingAssetsPath + "/diffusionDemo.png");
		}

		if (GUI.Button(new Rect(20, 300, 100, 50), "Post Facebook")) {
			diffusion.PostToFacebook("Check out my photo!", null, "file://" + Application.streamingAssetsPath + "/diffusionDemo.png");
		}

		if (GUI.Button(new Rect(20, 400, 100, 50), "Check Logins")) {
			Debug.Log("Facebook: " + Diffusion.isFacebookConnected());
			Debug.Log("Twitter: " + Diffusion.isTwitterConnected());
		}
	}


	void OnCompleted(DiffusionPlatform platform) {
		Debug.Log("Thanks for sharing on " + platform.ToString());
		if (platform == DiffusionPlatform.SaveToCameraRoll)
			Camera.main.backgroundColor = Color.magenta;
		else if (platform == DiffusionPlatform.Twitter)
			Camera.main.backgroundColor = Color.cyan;
		else
			Camera.main.backgroundColor = Color.green;
		Invoke("ResetColor", 2f);
	}


	void OnCancelled() {
		Debug.Log("User cancelled!");
		Camera.main.backgroundColor = Color.red;
		Invoke("ResetColor", 2f);
	}


	void ResetColor() {
		Camera.main.backgroundColor = Color.blue;
	}
}

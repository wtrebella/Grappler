using UnityEngine;
using System.Collections;

public class UIManagerGlobal : UIManager {
	public void AddRandomMenuToQueue() {
		PanelInfo panelInfo = new PanelInfo();

		panelInfo.panel = GetPanelOfType<Menu>();
		panelInfo.title = GetRandomTitle();
		panelInfo.description = GetRandomDescription();
		panelInfo.headerColor = GetRandomColor();

		AddPanelToQueue(panelInfo);
	}

	public void AddBarPanelToQueue() {
		PanelInfo panelInfo = new PanelInfo();
		panelInfo.panel = GetPanelOfType<BarPanel>();
		AddPanelToQueue(panelInfo);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) AddRandomMenuToQueue();
		if (Input.GetKeyDown(KeyCode.B)) {
			if (RegisteredPanelIsOfType(typeof(BarPanel))) {
				BarPanel barPanel = GetPanelOfType<BarPanel>();
				barPanel.Hide();
			}
			else AddBarPanelToQueue();
		}
		BaseUpdate();
	}

	private string GetRandomTitle() {
		string[] titles = new string[] {
			"Fuck",
			"Poop",
			"Hello!",
			"What's up friend?",
			"Not sure",
			"Okay...",
			"What the fuck!",
			"Why did you do that?",
			"Okay I'm done here",
			"Fuck you"
		};

		return titles[Random.Range(0, titles.Length)];
	}

	private string GetRandomDescription() {
		string[] descriptions = new string[] {
			"What's going on in the world today?",
			"Don't you dare even think about doing that!",
			"Well well well, what do we have here? An imposter?",
			"Hello Mr. Nugget.",
			"I am a golden king with a golden crown and a golden ballsack.",
			"Why oh why oh why must I go to Ohio?",
			"Seventeen years after birth, I died a horrible death. This is my story.",
			"This is the definition of a fucker. Never underestimate them. They will fuck you. Over and over."
		};

		return descriptions[Random.Range(0, descriptions.Length)];
	}

	private Color GetRandomColor() {
		return new Color(Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f));
	}
}

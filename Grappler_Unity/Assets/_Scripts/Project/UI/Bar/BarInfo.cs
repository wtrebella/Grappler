using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BarInfo {
	public BarInfoType barInfoType {get; private set;}
	public BarButtonDelegate buttonDelegate {get; private set;}
	public string text {get; private set;}

	public BarInfo(BarInfoType barInfoType) {
		this.barInfoType = barInfoType;
		SetupBarInfo(barInfoType);
	}

	public BarType GetBarType() {
		return buttonDelegate == null ? BarType.PlainBar : BarType.ButtonBar;
	}

	private void SetupBarInfo(BarInfoType barInfoType) {
		switch (barInfoType) {
		case BarInfoType.Challenge:
			text = "Challenge";
			buttonDelegate = null;
			break;

		case BarInfoType.CoinsToGo:
			text = "Coins to Go";
			buttonDelegate = null;
			break;

		case BarInfoType.FreeGift:
			text = "Free Gift";
			buttonDelegate = TempBarButtonMethod;
			break;

		case BarInfoType.FreeGiftTimer:
			text = "Free Gift Timer";
			buttonDelegate = null;
			break;

		case BarInfoType.PiggyBank:
			text = "Piggy Bank";
			buttonDelegate = TempBarButtonMethod;
			break;

		case BarInfoType.PurchaseItem:
			text = "Purchase Item";
			buttonDelegate = TempBarButtonMethod;
			break;

		case BarInfoType.Rate:
			text = "Rate";
			buttonDelegate = TempBarButtonMethod;
			break;

		case BarInfoType.TryItem:
			text = "Try Item";
			buttonDelegate = TempBarButtonMethod;
			break;

		case BarInfoType.WatchAd:
			text = "Watch Ad";
			buttonDelegate = TempBarButtonMethod;
			break;

		default:
			Debug.LogError("invalid barInfoType: " + barInfoType.ToString());
			break;
		}
	}

	private void TempBarButtonMethod(Bar bar) {
		Debug.Log("Bar Info Type: " + bar.barInfo.barInfoType.ToString());
	}
}

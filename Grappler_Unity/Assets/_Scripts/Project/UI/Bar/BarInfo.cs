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
			buttonDelegate = OnButtonPressed_FreeGift;
			break;

		case BarInfoType.FreeGiftTimer:
			text = "Free Gift Timer";
			buttonDelegate = null;
			break;

		case BarInfoType.PiggyBank:
			text = "Piggy Bank";
			buttonDelegate = OnButtonPressed_PiggyBank;
			break;

		case BarInfoType.PurchaseItem:
			text = "Purchase Item";
			buttonDelegate = OnButtonPressed_PurchaseItem;
			break;

		case BarInfoType.Rate:
			text = "Rate";
			buttonDelegate = OnButtonPressed_Rate;
			break;

		case BarInfoType.TryItem:
			text = "Try Item";
			buttonDelegate = OnButtonPressed_TryItem;
			break;

		case BarInfoType.WatchAd:
			text = "Watch Ad";
			buttonDelegate = OnButtonPressed_WatchAd;
			break;

		default:
			Debug.LogError("invalid barInfoType: " + barInfoType.ToString());
			break;
		}
	}

	private static void OnButtonPressed_FreeGift(Bar bar) {
		GameSceneManager.instance.GoToGiftState();
	}

	private static void OnButtonPressed_PiggyBank(Bar bar) {

	}

	private static void OnButtonPressed_PurchaseItem(Bar bar) {

	}

	private static void OnButtonPressed_Rate(Bar bar) {

	}

	private static void OnButtonPressed_TryItem(Bar bar) {

	}

	private static void OnButtonPressed_WatchAd(Bar bar) {

	}
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BarInfo {
	public BarInfoType barInfoType {get; private set;}
	public BarButtonDelegate buttonDelegate {get; private set;}
	public string text {get; private set;}

	public static List<BarInfoType> GetBarInfoTypesToShow() {
		List<BarInfoType> barInfoTypeList = new List<BarInfoType>();
		if (GameStats.instance.CanAffordGift()) barInfoTypeList.Add(BarInfoType.Gift);
		else barInfoTypeList.Add(BarInfoType.CoinsToGo);
		barInfoTypeList.Add(BarInfoType.Challenge);
		barInfoTypeList.Add(BarInfoType.FreeCoinsTimer);
		barInfoTypeList.Add(BarInfoType.PiggyBank);
		barInfoTypeList.Add(BarInfoType.PurchaseItem);
		barInfoTypeList.Add(BarInfoType.Rate);
		barInfoTypeList.Add(BarInfoType.TryItem);
		barInfoTypeList.Add(BarInfoType.WatchAd);
		barInfoTypeList.Shuffle();
		barInfoTypeList = barInfoTypeList.GetRange(0, 3);
		return barInfoTypeList;
	}

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
			text = GameStats.instance.CoinsToGoUntilFreeGift() + " Coins Until Gift";
			buttonDelegate = null;
			break;

		case BarInfoType.Gift:
			text = "Free Gift";
			buttonDelegate = OnButtonPressed_Gift;
			break;

		case BarInfoType.FreeCoinsTimer:
			text = "Free Coins Timer";
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

	private static void OnButtonPressed_Gift(Bar bar) {
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

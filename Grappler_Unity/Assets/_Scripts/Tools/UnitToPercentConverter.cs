using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class UnitToPercentConverter {

}

[Serializable]
public class FloatToPercentConverter : UnitToPercentConverter {
	public FloatRange floatRange = new FloatRange(0.0f, 1.0f);

	public float ConvertToPercent(float unit) {
		unit = floatRange.Clamp(unit);
		float adjustedUnit = unit - floatRange.min;
		float difference = floatRange.difference;
		float percent = adjustedUnit / difference;
		return Mathf.Clamp01(percent);
	}
}

[Serializable]
public class IntToPercentConverter : UnitToPercentConverter {
	public IntRange intRange = new IntRange(0, 10);

	public float ConvertToPercent(int unit) {
		unit = intRange.Clamp(unit);
		int adjustedUnit = unit - intRange.min;
		int difference = intRange.difference;
		float percent = (float)adjustedUnit / (float)difference;
		return Mathf.Clamp01(percent);
	}
}

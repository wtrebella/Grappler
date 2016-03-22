using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable] public class UnityEventWithInt : UnityEvent<int> {}
[System.Serializable] public class UnityEventWithFloat : UnityEvent<float> {}
[System.Serializable] public class UnityEventWithList<T> : UnityEvent<List<T>> {}
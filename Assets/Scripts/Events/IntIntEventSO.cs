using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/IntInt Event SO")]
public class IntIntEventSO : ScriptableObject
{
    public Action<int, int> OnEventRaised;

    public void Raise(int value1, int value2)
    {
        OnEventRaised?.Invoke(value1, value2);
    }
}
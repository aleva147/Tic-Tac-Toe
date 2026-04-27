using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Int Event SO")]
public class IntEventSO : ScriptableObject
{
    public Action<int> OnEventRaised;

    public void Raise(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Void Event SO")]
public class VoidEventSO : ScriptableObject
{
    public Action OnEventRaised;

    public void Raise()
    {
        OnEventRaised?.Invoke();
    }
}
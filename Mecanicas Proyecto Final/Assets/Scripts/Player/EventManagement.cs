using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class Int2Event : UnityEvent<int, int> { } 

public class EventManagement : MonoBehaviour
{
    #region Signgleton

    public static EventManagement current;

    private void Awake()
    {
        if(current == null) { current = this; } else if (current != null) { Destroy(this); }
    }

    #endregion

    public Int2Event updateBulletsEvent = new Int2Event();
    public UnityEvent NewGunEvent = new UnityEvent();

}

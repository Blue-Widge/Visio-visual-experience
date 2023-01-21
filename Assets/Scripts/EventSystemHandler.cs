using System;
using UnityEngine;

public class EventSystemHandler : MonoBehaviour
{
    public static EventSystemHandler Current;
    private void Awake()
    {
        Current = this;
    }

    public event Action<int> OnFirstActionPerformed;

    public void ContainerDoorOpened(int id)
    {
        if (OnFirstActionPerformed != null)
            OnFirstActionPerformed(id);
    }
    public void CuttingBoardUsed(int id)
    {
        if (OnFirstActionPerformed != null)
            OnFirstActionPerformed(id);
    }
}


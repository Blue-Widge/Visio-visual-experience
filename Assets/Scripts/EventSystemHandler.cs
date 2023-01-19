using System;
using UnityEngine;

public class EventSystemHandler : MonoBehaviour
{
    public static EventSystemHandler current;
    private void Awake()
    {
        current = this;
    }

    public event Action<int> onFirstActionPerformed;

    public void ContainerDoorOpened(int id)
    {
        if (onFirstActionPerformed != null)
            onFirstActionPerformed(id);
    }
    public void CuttingBoardUsed(int id)
    {
        if (onFirstActionPerformed != null)
            onFirstActionPerformed(id);
    }
}


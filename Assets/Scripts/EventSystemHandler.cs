using System;
using UnityEngine;

/** @brief Class which contains custom events */
public class EventSystemHandler : MonoBehaviour
{
    /** @brief The event system handler variable, which is the same across the scenes and the scripts */
    public static EventSystemHandler Current;

    /** @brief Set the static variable to itself when awake */
    private void Awake()
    {
        Current = this;
    }

    /** @brief Action that redirects to methods when triggered */
    public event Action<int> OnFirstActionPerformed;

    /** @brief Method triggered when a container's door is opened 
     * \param[in] id The ID of the hand preview to disable
     */
    public void ContainerDoorOpened(int id)
    {
        if (OnFirstActionPerformed != null)
            OnFirstActionPerformed(id);
    }

    /** @brief Method triggered when the cutting board is being used
     * \param[in] id The ID of the hand preview to disable
     */
    public void CuttingBoardUsed(int id)
    {
        if (OnFirstActionPerformed != null)
            OnFirstActionPerformed(id);
    }
}


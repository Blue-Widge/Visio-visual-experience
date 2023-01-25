using UnityEngine;

/** @brief Class that handle the disabling of the hand preview animators */
public class PreviewDisabler : MonoBehaviour
{

    /** @brief The id of the hand preview */
    public int id;

    /** @brief Add the DisableAnimationPreview function to the OnFirstActionPerformed event */
    private void Start()
    {
        EventSystemHandler.Current.OnFirstActionPerformed += DisableAnimationPreview;
    }

    /** @brief Destroy the hand preview gameobject if the id of the hand is the same as the one passed on by the function
     * \param[in] pID Id of the hand preview that needs to be disabled 
     */
    private void DisableAnimationPreview(int pID)
    {
        if (id == pID)
            Destroy(gameObject);
    }

    /** @brief Remove the DisableAnimationPreview function to the OnFirstActionPerformed event */
    private void OnDestroy()
    {
        EventSystemHandler.Current.OnFirstActionPerformed -= DisableAnimationPreview;
    }
}

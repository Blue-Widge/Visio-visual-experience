using UnityEngine;

public class PreviewDisabler : MonoBehaviour
{
    public int id;
    private void Start()
    {
        EventSystemHandler.Current.OnFirstActionPerformed += disableAnimationPreview;
    }

    private void disableAnimationPreview(int p_id)
    {
        if (id == p_id)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventSystemHandler.Current.OnFirstActionPerformed -= disableAnimationPreview;
    }
}

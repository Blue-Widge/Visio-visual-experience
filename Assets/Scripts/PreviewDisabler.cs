using UnityEngine;

public class PreviewDisabler : MonoBehaviour
{
    public int id;
    private void Start()
    {
        EventSystemHandler.Current.OnFirstActionPerformed += DisableAnimationPreview;
    }

    private void DisableAnimationPreview(int pID)
    {
        if (id == pID)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventSystemHandler.Current.OnFirstActionPerformed -= DisableAnimationPreview;
    }
}

using UnityEngine;
using UnityEngine.Serialization;

public class LiquidHeightLevel : MonoBehaviour
{
    private GameObject _oil;
    private Renderer _renderer;
    [FormerlySerializedAs("_bottleHeight")] [SerializeField]
    private float bottleHeight;
    [FormerlySerializedAs("_minHeight")] [SerializeField]
    private float minHeight;
    private Vector3 _rotation;
    [SerializeField]
    float rotationVectorX;
    private static readonly int Level = Shader.PropertyToID("_Level");

    // Start is called before the first frame update
    void Start()
    {
        _oil = transform.gameObject;
        _renderer = GetComponent<Renderer>();
        if (!_oil || !_renderer)
            this.enabled = false;
        bottleHeight = minHeight = _renderer.material.GetFloat(Level);
    }

    private void Update()
    {
        rotationVectorX = 2.0f - Vector3.Distance(_oil.transform.up, Vector3.down);
        bottleHeight = Mathf.Lerp(minHeight, minHeight + 0.05f, rotationVectorX / 2.0f);
        _renderer.material.SetFloat(Level, bottleHeight); 
    }
}

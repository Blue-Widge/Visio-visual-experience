using UnityEngine;

public class LiquidHeightLevel : MonoBehaviour
{
    private GameObject _Oil;
    private Renderer _renderer;
    [SerializeField]
    private float _bottleHeight;
    [SerializeField]
    private float _minHeight;
    private Vector3 _rotation;
    [SerializeField]
    float rotationVectorX;
    private static readonly int _level = Shader.PropertyToID("_Level");

    // Start is called before the first frame update
    void Start()
    {
        _Oil = transform.gameObject;
        _renderer = GetComponent<Renderer>();
        if (!_Oil || !_renderer)
            this.enabled = false;
        _bottleHeight = _minHeight = _renderer.material.GetFloat(_level);
    }

    private void Update()
    {
        rotationVectorX = 2.0f - Vector3.Distance(_Oil.transform.up, Vector3.down);
        _bottleHeight = Mathf.Lerp(_minHeight, _minHeight + 0.05f, rotationVectorX / 2.0f);
        _renderer.material.SetFloat(_level, _bottleHeight); 
    }
}

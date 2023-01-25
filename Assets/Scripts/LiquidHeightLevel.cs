using UnityEngine;
using UnityEngine.Serialization;

/** @brief Class that handle the level of liquid in a bottle, so its volume appears constant whatever the bottle's rotation */
public class LiquidHeightLevel : MonoBehaviour
{
    /** @brief The oil gameobject in the bottle */
    private GameObject _oil;
    /** @brief The oil renderer in order to change the renderer's material properties*/
    private Renderer _renderer;
    [SerializeField]
    private float _liquidHeight;
    [SerializeField]
    private float _minliquidHeight;
    /** @brief The current rotation of the bottle */
    private Vector3 _rotation;
    /** @brief Value between 0 and 2 where 0 is when the bottle is facing up and 2 for down */
    [SerializeField]
    private float _facingDownRatio;
    private static readonly int Level = Shader.PropertyToID("_Level");

    /** @brief Set the gameobject and renderer, aswell as the current level of liquid, being also the min */
    void Start()
    {
        _oil = transform.gameObject;
        _renderer = GetComponent<Renderer>();
        if (!_oil || !_renderer)
            this.enabled = false;
        _liquidHeight = _minliquidHeight = _renderer.material.GetFloat(Level);
    }

    /** @brief Change the liquid height on the oil material value depending on how much the bottle is facing down */
    private void Update()
    {
        _facingDownRatio = 2.0f - Vector3.Distance(_oil.transform.up, Vector3.down);
        _liquidHeight = Mathf.Lerp(_minliquidHeight, _minliquidHeight + 0.05f, _facingDownRatio / 2.0f);
        _renderer.material.SetFloat(Level, _liquidHeight); 
    }
}

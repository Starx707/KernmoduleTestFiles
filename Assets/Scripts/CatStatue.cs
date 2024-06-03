using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatue : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private RenderTexture _renderTexture;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public bool IsOnScreen()
    {
        Vector3 screenPos = _camera.WorldToScreenPoint(transform.position);
        if ((screenPos.x > 0 && screenPos.x < _renderTexture.width) && (screenPos.y > 0 && screenPos.y < _renderTexture.height))
        {
            return true;
        }
        return false;
    }
}
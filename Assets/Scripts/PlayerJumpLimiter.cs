using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJumpLimiter : MonoBehaviour
{
    private List<CatStatue> _catStatues = new List<CatStatue>();

    private void Start()
    {
        _catStatues = FindObjectsOfType<CatStatue>().ToList();
    }

    public bool CanJump()
    {
        foreach (CatStatue statue in _catStatues)
        {
            if (statue.IsOnScreen())
            {
                return true;
            }
        }
        return false;
    }
}
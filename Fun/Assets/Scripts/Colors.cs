using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public Material mat;

    public Color color;

    private void Update()
    {
        mat.color = color;
    }
}

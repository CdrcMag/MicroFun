using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDetecter : MonoBehaviour
{
    public MainMenu menu;

    private Color originalColor;
    public Color32 newColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            originalColor = collision.GetComponent<Image>().color;
            collision.GetComponent<Image>().color = newColor;
            menu.nextTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            collision.GetComponent<Image>().color = originalColor;
            menu.nextTransform = null;
        }
    }
}

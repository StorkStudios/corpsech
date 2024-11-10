using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseController : MonoBehaviour
{
    [SerializeField]
    private Transform referencePoint;
    public Transform ReferencePoint => referencePoint;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color highlightedColor;

    private Color defaultColor;

    private void Start()
    {
        defaultColor = spriteRenderer.color;
    }

    public void SetHighlight(bool highlight)
    {
        spriteRenderer.color = highlight ? highlightedColor : defaultColor;
    }
}

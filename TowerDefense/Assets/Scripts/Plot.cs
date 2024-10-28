using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer; // Componente para alterar a aparência do plot.
    [SerializeField] private Color hoverColor;              // Cor ao passar o mouse sobre o plot.

    private GameObject tower;         // Torre construída neste plot.
    private Color initialColor;       // Cor inicial do plot.

    private void Start()
    {
        // Armazena a cor inicial do SpriteRenderer.
        initialColor = spriteRenderer.color;
    }
}

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
    private void OnMouseEnter()
    {
        // Muda a cor do plot para a cor de hover.
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        // Restaura a cor inicial do plot.
        spriteRenderer.color = initialColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return; // Se já houver uma torre, retorna.

        Tower selectedTower = BuildManager.Instance.GetSelectedTower(); // Obtém a torre do BuildManager.
        if (selectedTower == null) return; // Retorna se nenhuma torre estiver selecionada.

        // Instancia a torre na posição do plot.
        tower = Instantiate(selectedTower.prefab, transform.position, Quaternion.identity);
    }
}

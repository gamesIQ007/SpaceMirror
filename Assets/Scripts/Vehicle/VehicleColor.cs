using UnityEngine;
using Mirror;

/// <summary>
/// Цвет транспорта
/// </summary>
public class VehicleColor : NetworkBehaviour
{
    /// <summary>
    /// Спрайт
    /// </summary>
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    /// <summary>
    /// Цвет
    /// </summary>
    [SyncVar(hook = nameof(SetColor))]
    private Color m_Color;


    public override void OnStartServer()
    {
        base.OnStartServer();

        m_Color = Random.ColorHSV();
    }


    /// <summary>
    /// Задать цвет
    /// </summary>
    /// <param name="oldColor">Старый цвет</param>
    /// <param name="newColor">Новый цвет</param>
    private void SetColor(Color oldColor, Color newColor)
    {
        m_SpriteRenderer.color = newColor;
    }
}

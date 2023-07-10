using UnityEngine;
using Mirror;

/// <summary>
/// Цвет транспорта
/// </summary>
public class VehicleColor : NetworkBehaviour
{
    /// <summary>
    /// Транспорт
    /// </summary>
    [SerializeField] private Vehicle m_Vehicle;

    /// <summary>
    /// Спрайт
    /// </summary>
    [SerializeField] private SpriteRenderer m_SpriteRenderer;


    private void Start()
    {
        m_SpriteRenderer.color = m_Vehicle.Owner.GetComponent<Player>().PlayerColor;
    }
}

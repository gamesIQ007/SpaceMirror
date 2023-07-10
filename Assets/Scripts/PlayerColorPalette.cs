using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// Доступные для игроков цвета
/// </summary>
public class PlayerColorPalette : NetworkBehaviour
{
    /// <summary>
    /// Синглтон
    /// </summary>
    public static PlayerColorPalette Instance;

    /// <summary>
    /// Все цвета
    /// </summary>
    [SerializeField] private List<Color> m_AllColors;

    /// <summary>
    /// Все доступные цвета
    /// </summary>
    private List<Color> m_AvailableColors;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        m_AvailableColors = new List<Color>();
        m_AllColors.CopyTo(m_AvailableColors);
    }


    #region Public API

    /// <summary>
    /// Получить случайный цвет
    /// </summary>
    /// <returns>Цвет</returns>
    public Color TakeRandomColor()
    {
        int index = Random.Range(0, m_AvailableColors.Count);
        Color color = m_AvailableColors[index];

        m_AvailableColors.RemoveAt(index);

        return color;
    }

    /// <summary>
    /// Положить цвет к доступным
    /// </summary>
    /// <param name="color">Цвет</param>
    public void PutColor(Color color)
    {
        if (m_AllColors.Contains(color))
        {
            if (m_AvailableColors.Contains(color) == false)
            {
                m_AvailableColors.Add(color);
            }
        }
    }

    #endregion
}

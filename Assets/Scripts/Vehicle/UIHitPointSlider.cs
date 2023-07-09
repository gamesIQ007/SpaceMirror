using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображение слайдера со здоровьем
/// </summary>
public class UIHitPointSlider : MonoBehaviour
{
    /// <summary>
    /// Уничтожимый объект
    /// </summary>
    [SerializeField] private Destructible m_Destructible;

    /// <summary>
    /// Изображение
    /// </summary>
    [SerializeField] private Image m_FillImage;

    /// <summary>
    /// Слайдер
    /// </summary>
    [SerializeField] private Slider m_Slider;


    #region Unity Events

    private void Start()
    {
        m_Destructible.HitPointChange += OnHitPointChange;

        m_Slider.maxValue = m_Destructible.MaxHitPoint;
        m_Slider.value = m_Destructible.HitPoint;
    }

    private void OnDestroy()
    {
        m_Destructible.HitPointChange -= OnHitPointChange;
    }

    #endregion


    /// <summary>
    /// При изменении здоровья
    /// </summary>
    /// <param name="hitPoint">Здоровье</param>
    private void OnHitPointChange(int hitPoint)
    {
        m_Slider.value = hitPoint;
    }
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображение слайдера со здоровьем
/// </summary>
public class VehicleHitPointSlider : MonoBehaviour
{
    /// <summary>
    /// Уничтожимый объект
    /// </summary>
    [SerializeField] private Vehicle m_Vehicle;

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
        m_Vehicle.HitPointChange += OnHitPointChange;

        m_FillImage.color = m_Vehicle.Owner.GetComponent<Player>().PlayerColor;

        m_Slider.maxValue = m_Vehicle.MaxHitPoint;
        m_Slider.value = m_Vehicle.HitPoint;
    }

    private void OnDestroy()
    {
        m_Vehicle.HitPointChange -= OnHitPointChange;
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

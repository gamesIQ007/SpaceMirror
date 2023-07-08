using UnityEngine;

/// <summary>
/// Класс для слежения камеры за целью
/// </summary>
public class VehicleCamera : MonoBehaviour
{
    /// <summary>
    /// Синглтон
    /// </summary>
    public static VehicleCamera Instance;

    /// <summary>
    /// Камера
    /// </summary>
    [SerializeField] private Camera m_Camera;

    /// <summary>
    /// Линейная скорость
    /// </summary>
    [SerializeField] private float m_InterpolationLinear;

    /// <summary>
    /// Скорость поворота
    /// </summary>
    [SerializeField] private float m_InterpolationAngular;

    /// <summary>
    /// Отступ камеры по оси Z
    /// </summary>
    [SerializeField] private float m_CameraZOffset;

    /// <summary>
    /// Смещение камеры вперёд от цели
    /// </summary>
    [SerializeField] private float m_ForwardOffset;

    /// <summary>
    /// Цель слежения
    /// </summary>
    private Transform m_Target;


    #region Unity Events

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void FixedUpdate()
    {
        if (m_Camera == null || m_Target == null) return;

        Vector2 camPos = m_Camera.transform.position;
        Vector2 targetPos = m_Target.position + m_Target.transform.up * m_ForwardOffset;

        Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, m_InterpolationLinear * Time.deltaTime);

        m_Camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset);

        if (m_InterpolationAngular > 0)
        {
            m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
        }
    }

    #endregion

    /// <summary>
    /// Назначение новой цели отслеживания камерой
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetTarget(Transform newTarget)
    {
        m_Target = newTarget;
    }
}
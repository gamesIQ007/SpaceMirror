using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Зона передвижения
/// </summary>
public class MoveArea : MonoBehaviour
{
    /// <summary>
    /// Вектор, задающий область
    /// </summary>
    [SerializeField] private Vector2 m_Area;


    /// <summary>
    /// Получить случайную точку внутри области
    /// </summary>
    /// <returns>Точка внутри области</returns>
    public Vector2 GetRandomInsideZone()
    {
        Vector2 result = transform.position;

        result.x += Random.Range(-m_Area.x / 2, m_Area.x / 2);
        result.y += Random.Range(-m_Area.y / 2, m_Area.y / 2);

        return result;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireCube(transform.position, m_Area);
    }
#endif
}

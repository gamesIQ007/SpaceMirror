using UnityEngine;

/// <summary>
/// Снаряд
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Скорость перемещения
    /// </summary>
    [SerializeField] private float m_MovementSpeed;

    /// <summary>
    /// Урон
    /// </summary>
    [SerializeField] private int m_Damage;

    /// <summary>
    /// Время жизни
    /// </summary>
    [SerializeField] private float m_LifeTime;

    /// <summary>
    /// Эффект уничтожения
    /// </summary>
    [SerializeField] private GameObject m_DestroySFX;

    /// <summary>
    /// Родитель
    /// </summary>
    private Transform m_Parent;


    #region Unity Events

    private void Start()
    {
        Destroy(gameObject, m_LifeTime);
    }

    private void Update()
    {
        float stepLength = m_MovementSpeed * Time.deltaTime;
        Vector2 step = transform.up * stepLength;

        transform.position += new Vector3(step.x, step.y, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

        if (hit)
        {
            if (hit.collider.transform.root != m_Parent)
            {
                if (NetworkSessionManager.Instance.IsServer)
                {
                    Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                    if (dest != null)
                    {
                        dest.SvApplyDamage(m_Damage);
                    }
                }

                if (NetworkSessionManager.Instance.IsClient)
                {
                    if (m_DestroySFX != null)
                    {
                        Instantiate(m_DestroySFX, transform.position, Quaternion.identity);
                    }
                }

                Destroy(gameObject);
            }
        }
    }

    #endregion


    /// <summary>
    /// Задать родителя
    /// </summary>
    /// <param name="parent">Родитель</param>
    public void SetParent(Transform parent)
    {
        m_Parent = parent;
    }
}

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

/// <summary>
/// Область астероидов
/// </summary>
public class AsteroidsArea : MonoBehaviour
{
    /// <summary>
    /// Область перемещения
    /// </summary>
    [SerializeField] private MoveArea m_MovementArea;
    /// <summary>
    /// Скорость перемещения
    /// </summary>
    [SerializeField] private float m_MovementSpeed;

    /// <summary>
    /// Урон
    /// </summary>
    [SerializeField] private int m_Damage;
    /// <summary>
    /// Частота нанесения урона
    /// </summary>
    [SerializeField] private float m_DamageRate;

    /// <summary>
    /// Позиция, в которую перемещается
    /// </summary>
    private Vector3 movementPosition;

    /// <summary>
    /// Таймер
    /// </summary>
    private float timer = 0;
    /// <summary>
    /// Транспорт внутри поля
    /// </summary>
    private List<Vehicle> vehicles = new List<Vehicle>();


    #region Unity Events

    private void Update()
    {
        Move();

        if (vehicles.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= m_DamageRate)
            {
                MakeDamage();
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vehicle vehicle = collision.transform.root.GetComponent<Vehicle>();
        if (vehicle != null)
        {
            vehicles.Add(vehicle);
            timer = m_DamageRate;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Vehicle vehicle = collision.transform.root.GetComponent<Vehicle>();
        if (vehicle != null)
        {
            vehicles.Remove(vehicle);
        }
    }

    #endregion


    /// <summary>
    /// Движение
    /// </summary>
    private void Move()
    {
        if (transform.position == movementPosition)
        {
            movementPosition = m_MovementArea.GetRandomInsideZone();
        }

        transform.position = Vector3.MoveTowards(transform.position, movementPosition, Time.deltaTime * m_MovementSpeed);
    }

    /// <summary>
    /// Нанести урон
    /// </summary>
    private void MakeDamage()
    {
        for (int i = 0; i < vehicles.Count; i++)
        {
            vehicles[i].SvApplyDamage(m_Damage);
        }
    }
}

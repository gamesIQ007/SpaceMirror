﻿using UnityEngine;
using UnityEngine.Events;
using Mirror;

/// <summary>
/// Уничтожаемый объект
/// </summary>
public class Destructible : NetworkBehaviour
{
    /// <summary>
    /// Событие изменения здоровья
    /// </summary>
    public UnityAction<int> HitPointChange;

    /// <summary>
    /// Максимальное количество здоровья
    /// </summary>
    [SerializeField] private int m_MaxHitPoint;
    public int MaxHitPoint => m_MaxHitPoint;

    /// <summary>
    /// Эффект при смерти
    /// </summary>
    [SerializeField] private GameObject m_DestroySFX;

    /// <summary>
    /// Текущее количество здоровья
    /// </summary>
    private int m_CurrentHitPoint;
    public int HitPoint => m_CurrentHitPoint;
    [SyncVar(hook = nameof(ChangeHitPoint))]
    private int m_SyncCurrentHitPoint;


    public override void OnStartServer()
    {
        base.OnStartServer();

        m_SyncCurrentHitPoint = m_MaxHitPoint;
        m_CurrentHitPoint = m_MaxHitPoint;
    }


    /// <summary>
    /// Изменить количество здоровья
    /// </summary>
    /// <param name="oldValue">Старое значение</param>
    /// <param name="newValue">Новое значение</param>
    private void ChangeHitPoint(int oldValue, int newValue)
    {
        m_CurrentHitPoint = newValue;
        HitPointChange?.Invoke(newValue);
    }

    /// <summary>
    /// Нанести урон
    /// </summary>
    /// <param name="damage">Урон</param>
    [Server]
    public void SvApplyDamage(int damage)
    {
        m_SyncCurrentHitPoint -= damage;

        if (m_SyncCurrentHitPoint <= 0)
        {
            if (m_DestroySFX != null)
            {
                GameObject sfx = Instantiate(m_DestroySFX, transform.position, Quaternion.identity);
                NetworkServer.Spawn(sfx);
            }

            Destroy(gameObject);
        }
    }
}

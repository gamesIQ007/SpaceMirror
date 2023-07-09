﻿using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]

/// <summary>
/// Транспорт
/// </summary>
public class Vehicle : Destructible
{
    [Header("Vehicle")]
    /// <summary>
    /// Масса, для автоматической установки в ригиде
    /// </summary>
    [SerializeField] private float m_Mass;

    /// <summary>
    /// Толкающая вперёд сила
    /// </summary>
    [SerializeField] private float m_Thrust;

    /// <summary>
    /// Вращающая сила
    /// </summary>
    [SerializeField] private float m_Mobility;

    /// <summary>
    /// Максимальная линейная скорость
    /// </summary>
    [SerializeField] private float m_MaxLinearVelocity;
    public float MaxLinearVelocity => m_MaxLinearVelocity;

    /// <summary>
    /// Максимальная вращательная скорость в градусах/сек.
    /// </summary>
    [SerializeField] private float m_MaxAngularVelocity;
    public float MaxAngularVelocity => m_MaxAngularVelocity;

    /// <summary>
    /// Турель
    /// </summary>
    [SerializeField] private Turret m_Turret;

    /// <summary>
    /// Сохранённая ссылка на ригид
    /// </summary>
    private Rigidbody2D m_Rigid;


    #region Unity Events

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.mass = m_Mass;
        m_Rigid.inertia = 1; // иннерциальные силы, чтобы было проще балансировать соотношение сил и легче управлять
    }

    private void FixedUpdate()
    {
        if (isOwned || netIdentity.connectionToClient == null)
        {
            UpdateRigitBody();
        }
    }

    #endregion


    #region Public API

    /// <summary>
    /// Управление линейной тягой. От -1.0 до +1.0
    /// </summary>
    public float ThrustControl { get; set; }

    /// <summary>
    /// Управление вращательной тягой. От -1.0 до +1.0
    /// </summary>
    public float TorqueControl { get; set; }

    /// <summary>
    /// Выстрел
    /// </summary>
    public void Fire()
    {
        m_Turret.CmdFire();
    }

    #endregion


    /// <summary>
    /// Метод добавления сил кораблю для движения
    /// </summary>
    private void UpdateRigitBody()
    {
        m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
        m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
        m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
    }
}

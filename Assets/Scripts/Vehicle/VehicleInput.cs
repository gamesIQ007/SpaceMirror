using UnityEngine;

[RequireComponent(typeof(Player))]

/// <summary>
/// Управление транспортом
/// </summary>
public class VehicleInput : MonoBehaviour
{
    /// <summary>
    /// Игрок
    /// </summary>
    private Player m_Player;


    #region Unity Events

    private void Awake()
    {
        m_Player = GetComponent<Player>();
    }

    private void Update()
    {
        if (m_Player.isOwned && m_Player.isLocalPlayer)
        {
            UpdateControl();
        }
    }

    #endregion


    /// <summary>
    /// Реализация управления
    /// </summary>
    private void UpdateControl()
    {
        if (m_Player.ActiveVehicle == null) return;

        float thrust = 0;
        float torque = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            thrust = 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            thrust = -1.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            torque = 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            torque = -1.0f;
        }

        m_Player.ActiveVehicle.ThrustControl = thrust;
        m_Player.ActiveVehicle.TorqueControl = torque;
    }
}


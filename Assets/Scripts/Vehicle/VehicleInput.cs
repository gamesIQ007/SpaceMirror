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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            thrust = 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            thrust = -1.0f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            m_Player.ActiveVehicle.Fire();
        }

        m_Player.ActiveVehicle.ThrustControl = thrust;

        // Управление поворотом с клавиатуры
        /*float torque = 0;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            torque = 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            torque = -1.0f;
        }
        
        m_Player.ActiveVehicle.TorqueControl = torque;*/

        // Поворот в сторону курсора
        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = m_Player.ActiveVehicle.transform.position.z;

        Vector3 lookDirection = worldMousePosition - m_Player.ActiveVehicle.transform.position;
        lookDirection.Normalize();

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        m_Player.ActiveVehicle.transform.rotation = rotation;
    }
}


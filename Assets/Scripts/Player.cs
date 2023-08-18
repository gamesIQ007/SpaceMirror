using UnityEngine;
using Mirror;

/// <summary>
/// Игрок
/// </summary>
public class Player : NetworkBehaviour
{
    /// <summary>
    /// Префаб транспорта
    /// </summary>
    [SerializeField] private Vehicle[] m_VehiclePrefab;

    /// <summary>
    /// Цвет игрока
    /// </summary>
    [SyncVar]
    private Color m_PlayerColor;
    public Color PlayerColor => m_PlayerColor;

    /// <summary>
    /// Активный транспорт
    /// </summary>
    public Vehicle ActiveVehicle { get; set; }


    public override void OnStartServer()
    {
        base.OnStartServer();

        m_PlayerColor = PlayerColorPalette.Instance.TakeRandomColor();
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        PlayerColorPalette.Instance.PutColor(m_PlayerColor);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (isOwned)
        {
            CmdSpawnVehicle();
        }
    }


    [Command]
    /// <summary>
    /// Просьба к серверу заспавнить транспорт
    /// </summary>
    private void CmdSpawnVehicle()
    {
        SvSpawnClientVehicle();
    }


    [Server]
    /// <summary>
    /// Заспавнить клиентский транспорт
    /// </summary>
    public void SvSpawnClientVehicle()
    {
        if (ActiveVehicle != null) return;

        // Предотвращение спавна там, где уже что-то есть
        if (Physics2D.OverlapCircleAll(transform.position, 1.0f).Length > 0)
        {
            for (int i = 0; i < NetworkSessionManager.Instance.SpawnPoints.Length; i++)
            {
                Collider2D collider = Physics2D.OverlapCircle(NetworkSessionManager.Instance.SpawnPoints[i].position, 1.0f);

                if (collider == null)
                {
                    transform.position = NetworkSessionManager.Instance.SpawnPoints[i].position;
                    break;
                }
            }
        }

        GameObject playerVehicle = Instantiate(m_VehiclePrefab[Random.Range(0, m_VehiclePrefab.Length)].gameObject, transform.position, Quaternion.identity);
        NetworkServer.Spawn(playerVehicle, netIdentity.connectionToClient);
        
        ActiveVehicle = playerVehicle.GetComponent<Vehicle>();
        ActiveVehicle.Owner = netIdentity;

        RpcSetVehicle(ActiveVehicle.netIdentity);
    }

    [ClientRpc]
    /// <summary>
    /// Задать клиенту активный транспорт
    /// </summary>
    private void RpcSetVehicle(NetworkIdentity vehicle)
    {
        ActiveVehicle = vehicle.GetComponent<Vehicle>();

        if (ActiveVehicle != null && ActiveVehicle.isOwned && VehicleCamera.Instance != null)
        {
            VehicleCamera.Instance.SetTarget(ActiveVehicle.transform);
        }
    }
}

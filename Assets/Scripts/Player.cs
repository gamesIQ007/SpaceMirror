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
    [SerializeField] private Vehicle m_VehiclePrefab;

    /// <summary>
    /// Активный транспорт
    /// </summary>
    public Vehicle ActiveVehicle { get; set; }


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

        GameObject playerVehicle = Instantiate(m_VehiclePrefab.gameObject, transform.position, Quaternion.identity);
        NetworkServer.Spawn(playerVehicle, netIdentity.connectionToClient);

        ActiveVehicle = playerVehicle.GetComponent<Vehicle>();

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

using UnityEngine;
using Mirror;

/// <summary>
/// Менеджер сетевых сессий
/// </summary>
public class NetworkSessionManager : NetworkManager
{
    /// <summary>
    /// Синглтон
    /// </summary>
    public static NetworkSessionManager Instance => singleton as NetworkSessionManager;

    /// <summary>
    /// Являемся сервером?
    /// </summary>
    public bool IsServer => (mode == NetworkManagerMode.Host || mode == NetworkManagerMode.ServerOnly);
    /// <summary>
    /// Являемся клиентом?
    /// </summary>
    public bool IsClient => (mode == NetworkManagerMode.Host || mode == NetworkManagerMode.ClientOnly);

    /// <summary>
    /// Точки спавна
    /// </summary>
    public Transform[] SpawnPoints;
}

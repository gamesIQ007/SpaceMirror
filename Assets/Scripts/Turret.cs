using UnityEngine;
using Mirror;

/// <summary>
/// Турель
/// </summary>
public class Turret : NetworkBehaviour
{
    /// <summary>
    /// Снаряд
    /// </summary>
    [SerializeField] private GameObject m_Projectile;

    /// <summary>
    /// Скорострельность
    /// </summary>
    [SerializeField] private float m_FireRate;

    /// <summary>
    /// Текущее время
    /// </summary>
    private float m_CurrentTime;


    private void Update()
    {
        if (isServer)
        {
            m_CurrentTime += Time.deltaTime;
        }
    }


    /// <summary>
    /// Команда на сервер выстрелить
    /// </summary>
    [Command]
    public void CmdFire()
    {
        SvFire();
    }

    /// <summary>
    /// Выстрел на сервере
    /// </summary>
    [Server]
    private void SvFire()
    {
        if (m_CurrentTime < m_FireRate) return;

        GameObject projectile = Instantiate(m_Projectile, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().SetParent(transform);

        m_CurrentTime = 0;

        RpcFire();
    }

    /// <summary>
    /// Выстрел на клиенте
    /// </summary>
    [ClientRpc]
    private void RpcFire()
    {
        GameObject projectile = Instantiate(m_Projectile, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().SetParent(transform);
    }
}

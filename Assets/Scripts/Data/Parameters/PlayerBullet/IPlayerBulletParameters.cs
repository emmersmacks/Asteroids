using UnityEngine;

namespace Data.Parameters.PlayerBullet
{
    public interface IPlayerBulletParameters
    {
        LayerMask DamageLayerMask { get; }
    }
}
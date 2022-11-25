using Unity.VisualScripting;
using UnityEngine;

namespace Data.Parameters
{
    public interface IPlayerBulletParameters
    {
        LayerMask DamageLayerMask { get; }
    }
}
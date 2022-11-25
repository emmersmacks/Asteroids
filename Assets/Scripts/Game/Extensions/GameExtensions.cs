using Data;
using Game.Components;
using Game.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Extensions
{
    public static class GameExtensions
    {
        public static EcsEntity CreatePlayer(this EcsWorld world, Vector3 position)
        {
            var entity = world.NewEntity();
            
            var gameObject = entity.AddPrefab("Player", position);
            entity.Replace(new RigidbodyComponent() { Value = gameObject.GetComponent<Rigidbody2D>() });
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 3 });
            entity.Replace(new RotateSpeedComponent() { Value = 120 });
            entity.Get<UnitComponent>();
            entity.Get<ForceMoveComponent>();
            entity.Get<DirectionComponent>();
            entity.Get<PlayerTagComponent>();

            return entity;
        }

        public static EcsEntity CreatePlayerBullet(this EcsWorld world, Vector3 position, Quaternion rotation, LayerMask damageLayer)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("PlayerBullet", position, rotation);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 30 });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new DamageLayerComponent() { Value = damageLayer });
            entity.Replace(new OldPositionComponent() { Value = position });
            entity.Get<BulletComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateMainWeapon(this EcsWorld world, Vector3 position, EcsEntity player)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("MainGun", position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpawnPointsComponent() { Value = gameObject.GetComponent<WeaponView>().BulletSpawnPoints });
            entity.AddParent(player);
            entity.Get<MainWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }

        public static EcsEntity CreateLevel(this EcsWorld world)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("Level");
            entity.Replace(new AsteroidSpawnPointsComponent() { Value = gameObject.GetComponent<LevelView>().AsteroidSpawnPoints });
            entity.Get<LevelTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateBigAsteroid(this EcsWorld world, Vector3 position, Vector2 moveDirection)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("BigAsteroid", position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 2 });
            
            var transform = gameObject.transform;
            float signedAngle = Vector2.SignedAngle(transform.up, (moveDirection - (Vector2)transform.position));

            if (Mathf.Abs(signedAngle) >= 1e-3f)
            {
                var angles = transform.eulerAngles;
                angles.z += signedAngle;
                transform.eulerAngles = angles;
            }

            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new AsteroidSizeComponent() { Value = EAsteroidSizeType.Big });
            entity.Get<BulletComponent>();
            entity.Get<PlayerTagComponent>();
            entity.Get<AsteroidComponent>();
            return entity;
        }
        
        public static EcsEntity CreateSmallAsteroid(this EcsWorld world, Vector3 position, Quaternion rotation)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("SmallAsteroid", position, rotation);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 2 });

            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new AsteroidSizeComponent() { Value = EAsteroidSizeType.Small });

            entity.Get<BulletComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }
    }
}
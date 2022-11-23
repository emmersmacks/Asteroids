using Game.Components;
using Game.Providers;
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

        public static EcsEntity CreatePlayerBullet(this EcsWorld world, Vector3 position, Quaternion rotation)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("PlayerBullet", position, rotation);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 30 });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Get<BulletComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateMainWeapon(this EcsWorld world, Vector3 position)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("MainGun", position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpawnPointsComponent() { Value = gameObject.GetComponent<WeaponView>().BulletSpawnPoints });
            entity.Get<MainWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }
    }
}
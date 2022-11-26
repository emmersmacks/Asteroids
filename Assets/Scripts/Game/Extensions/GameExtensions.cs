using System.Collections.Generic;
using Data;
using Data.Bases;
using Game.Components;
using Game.Components.Asteroids;
using Game.Components.SpawnPoints;
using Game.Components.Tags;
using Game.Views;
using Game.Views.SpawnPoints;
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

        public static EcsEntity CreateBullet(this EcsWorld world, Vector3 position, Quaternion rotation, LayerMask damageLayer)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("DefaultBullet", position, rotation);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 30 });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new DamageLayerComponent() { Value = damageLayer });
            entity.Replace(new OldPositionComponent() { Value = position });
            entity.Get<BulletTagComponent>();
            entity.Get<TransformMoveComponent>();
            return entity;
        }
        
        public static EcsEntity CreateLaserBullet(this EcsWorld world, Vector3 startPosition, Vector3 direction, LayerMask damageLayer)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("LaserBullet", startPosition);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new DamageLayerComponent() { Value = damageLayer });
            var lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, direction);
            entity.Get<BulletTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateMainWeapon(this EcsWorld world, Vector3 position, EcsEntity player)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("MainGun", position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpawnPointsWithBoolComponent() { Value = gameObject.GetComponent<WeaponView>().BulletSpawnPoints });
            entity.AddParent(player);
            entity.Get<MainWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateLaserWeapon(this EcsWorld world, Vector3 position, EcsEntity player)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("LaserGun", position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpawnPointsWithBoolComponent() { Value = gameObject.GetComponent<WeaponView>().BulletSpawnPoints });
            entity.AddParent(player);
            entity.Get<AdditionalWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }

        public static EcsEntity CreateLevel(this EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.AddPrefab("Level");
            entity.Get<LevelTagComponent>();
            return entity;
        }
        
        public static EcsEntity InitializeAsteroidsSpawner(this EcsWorld world, AsteroidSpawnPointView[] spawnPoints)
        {
            var entity = world.NewEntity();
            var pointsBase = new List<DirectedSpawnPointBase>();
            
            foreach (var point in spawnPoints)
            {
                var newPoint = new DirectedSpawnPointBase()
                {
                    Direction = point.TargetMoveDirection,
                    Point = point.transform
                };
                pointsBase.Add(newPoint);
            }
            
            entity.Replace(new DirectedSpawnPointsComponent() { Value = pointsBase.ToArray() });
            entity.Get<AsteroidTagComponent>();
            return entity;
        }
        
        public static EcsEntity InitializeUFOSpawner(this EcsWorld world, UFOSpawnPointView[] spawnPoints)
        {
            var entity = world.NewEntity();
            var pointsBase = new List<Transform>();
            
            foreach (var point in spawnPoints)
            {
                var newPoint = point.transform;
                pointsBase.Add(newPoint);
            }
            
            entity.Replace(new SpawnPointsComponent() { Value = pointsBase.ToArray() });
            entity.Get<UFOTagComponent>();
            return entity;
        }

        public static EcsEntity CreateBigAsteroid(this EcsWorld world, Vector3 position, Vector3 spawnAngle)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("BigAsteroid", position);
            gameObject.transform.eulerAngles = spawnAngle;
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 2 });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new AsteroidSizeComponent() { Value = EAsteroidSizeType.Big });
            entity.Get<TransformMoveComponent>();
            entity.Get<AsteroidTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateSmallAsteroid(this EcsWorld world, Vector3 position, Quaternion rotation)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("SmallAsteroid", position, rotation);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = 2 });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Get<TransformMoveComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }

        public static EcsEntity CreateUFO(this EcsWorld world, Vector3 position)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab("UFO", position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new RigidbodyComponent() { Value = gameObject.GetComponent<Rigidbody2D>() });
            entity.Replace(new SpeedComponent() { Value = 3 });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Get<FollowPlayerComponent>();
            entity.Get<ForceMoveComponent>();
            entity.Get<UnitComponent>();
            return entity;
        }
    }
}
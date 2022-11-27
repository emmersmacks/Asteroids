using System.Collections.Generic;
using Data;
using Data.Bases;
using Data.Parameters;
using Data.Parameters.Asteroids;
using Data.Parameters.PlayerBullet.Impl;
using Data.Parameters.PlayerBullet.Impl.Charges;
using Game.Components;
using Game.Components.Asteroids;
using Game.Components.SpawnPoints;
using Game.Components.Tags;
using Game.Views;
using Game.Views.SpawnPoints;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Extensions
{
    public static class GameExtensions
    {
        public static EcsEntity CreatePlayer(this CustomEcsWorld world, Vector3 position, PlayerParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.Player, position);
            entity.Replace(new RigidbodyComponent() { Value = gameObject.GetComponent<Rigidbody2D>() });
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = parameters.Speed });
            entity.Replace(new RotateSpeedComponent() { Value = parameters.RotateSpeed });
            
            entity.Get<UnitComponent>();
            entity.Get<ForceMoveComponent>();
            entity.Get<DirectionComponent>();
            entity.Get<PlayerTagComponent>();

            return entity;
        }

        public static EcsEntity CreateBullet(this CustomEcsWorld world, Vector3 position, Quaternion rotation, PlayerBulletsParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.DefaultBullet, position, rotation);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpeedComponent() { Value = parameters.Speed });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new DamageLayerComponent() { Value = parameters.DamageLayerMask });
            entity.Replace(new OldPositionComponent() { Value = position });
            
            entity.Get<BulletTagComponent>();
            entity.Get<TransformMoveComponent>();
            return entity;
        }
        
        public static EcsEntity CreateLaserBullet(this CustomEcsWorld world, Vector3 startPosition, Vector3 direction, LayerMask damageLayer)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.LaserBullet, startPosition);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new DamageLayerComponent() { Value = damageLayer });
            var lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, direction);
            
            entity.Get<BulletTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateMainWeapon(this CustomEcsWorld world, Vector3 position, EcsEntity player)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.MainGun, position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new SpawnPointsWithBoolComponent() { Value = gameObject.GetComponent<WeaponView>().BulletSpawnPoints });
            entity.AddParent(player);
            
            entity.Get<MainWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateLaserWeapon(this CustomEcsWorld world, Vector3 position, EcsEntity player, ChargesParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.LaserGun, position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new ChargesComponent() { Value = parameters.MaxCharges });
            entity.Replace(new MaxChargesComponent() { Value = parameters.MaxCharges });
            world.LaserChargeChange(parameters.MaxCharges);
            entity.Replace(new SpawnPointsWithBoolComponent() { Value = gameObject.GetComponent<WeaponView>().BulletSpawnPoints });
            entity.AddParent(player);
            
            entity.Get<AdditionalWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            entity.Get<WeaponTagComponent>();
            return entity;
        }

        public static EcsEntity CreateLevel(this CustomEcsWorld world, EcsEntity entity)
        {
            entity.Get<ScoreComponent>();
            entity.Get<LevelTagComponent>();
            return entity;
        }
        
        public static EcsEntity InitializeAsteroidsSpawner(this CustomEcsWorld world, AsteroidSpawnPointView[] spawnPoints)
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
        
        public static EcsEntity InitializeUFOSpawner(this CustomEcsWorld world, UFOSpawnPointView[] spawnPoints)
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

        public static EcsEntity CreateBigAsteroid(this CustomEcsWorld world, Vector3 position, Vector3 spawnAngle, BigAsteroidParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.BigAsteroid, position);
            gameObject.transform.eulerAngles = spawnAngle;
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            var random = new System.Random();
            var speed = random.Next(parameters.MinSpeed, parameters.MaxSpeed);
            entity.Replace(new SpeedComponent() { Value = speed });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new AsteroidSizeComponent() { Value = EAsteroidSizeType.Big });
            entity.Replace(new CostInPointsComponent() { Value = parameters.CostInPoints });

            entity.Get<EnemyTagComponent>();
            entity.Get<TransformMoveComponent>();
            entity.Get<AsteroidTagComponent>();
            entity.Get<UnitComponent>();
            return entity;
        }
        
        public static EcsEntity CreateSmallAsteroid(this CustomEcsWorld world, Vector3 position, Quaternion rotation, SmallAsteroidsParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.SmallAsteroid, position, rotation);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            var random = new System.Random();
            var speed = random.Next(parameters.MinSpeed, parameters.MaxSpeed);
            entity.Replace(new SpeedComponent() { Value = speed });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new CostInPointsComponent() { Value = parameters.CostInPoints });

            entity.Get<EnemyTagComponent>();
            entity.Get<TransformMoveComponent>();
            entity.Get<UnitComponent>();
            return entity;
        }

        public static EcsEntity CreateUFO(this CustomEcsWorld world, Vector3 position, UFOParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.UFO, position);
            entity.Replace(new TransformComponent() { Value = gameObject.transform });
            entity.Replace(new RigidbodyComponent() { Value = gameObject.GetComponent<Rigidbody2D>() });
            entity.Replace(new SpeedComponent() { Value = parameters.Speed });
            entity.Replace(new DirectionComponent() { Value = Vector2.up });
            entity.Replace(new CostInPointsComponent() { Value = parameters.CostInPoints });

            entity.Get<EnemyTagComponent>();
            entity.Get<FollowPlayerComponent>();
            entity.Get<ForceMoveComponent>();
            entity.Get<UnitComponent>();
            return entity;
        }
    }
}
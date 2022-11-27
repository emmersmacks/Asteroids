using System.Collections.Generic;
using Data;
using Data.Bases;
using Data.Parameters.Asteroids;
using Data.Parameters.Charges;
using Data.Parameters.Player;
using Data.Parameters.PlayerBullets;
using Data.Parameters.UFO;
using Esc.Game.Components;
using Esc.Game.Components.Asteroids;
using Esc.Game.Components.SpawnPoints;
using Esc.Game.Components.Tags;
using Esc.Views;
using Esc.Views.SpawnPoints;
using Infrastructure;
using Leopotam.Ecs;
using UnityEngine;

namespace Esc.Game.Extensions
{
    public static class GameExtensions
    {
        public static EcsEntity CreatePlayer(this CustomEcsWorld world, Vector3 position, PlayerParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.Player, position);
            world.SetUnique(ref world.Player, ref entity);
            entity.ReplaceComponent(new RigidbodyComponent() { Value = gameObject.GetComponent<Rigidbody2D>() });
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            entity.ReplaceComponent(new SpeedComponent() { Value = parameters.Speed });
            entity.ReplaceComponent(new RotateSpeedComponent() { Value = parameters.RotateSpeed });
            
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
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            entity.ReplaceComponent(new SpeedComponent() { Value = parameters.Speed });
            entity.ReplaceComponent(new DirectionComponent() { Value = Vector2.up });
            entity.ReplaceComponent(new DamageLayerComponent() { Value = parameters.DamageLayerMask });
            entity.ReplaceComponent(new OldPositionComponent() { Value = position });
            
            entity.Get<BulletTagComponent>();
            entity.Get<TransformMoveComponent>();
            return entity;
        }
        
        public static EcsEntity CreateLaserBullet(this CustomEcsWorld world, Vector3 startPosition, Vector3 direction, LayerMask damageLayer)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.LaserBullet, startPosition);
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            entity.ReplaceComponent(new DamageLayerComponent() { Value = damageLayer });
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
            var view = gameObject.GetComponent<WeaponView>();
            view.Link(ref entity);
            entity.Replace(new ViewComponent() { Value = view });
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            entity.ReplaceComponent(new SpawnPointsWithBoolComponent() { Value = gameObject.GetComponent<WeaponBoolSpawnPointsView>().BulletSpawnPoints });
            entity.AddParent(player);
            
            entity.Get<MainWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            return entity;
        }
        
        public static EcsEntity CreateLaserWeapon(this CustomEcsWorld world, Vector3 position, EcsEntity player, ChargesParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.LaserGun, position);
            var view = gameObject.GetComponent<WeaponView>();
            view.Link(ref entity);
            entity.Replace(new ViewComponent() { Value = view });
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            entity.ReplaceComponent(new ChargesComponent() { Value = parameters.MaxCharges });
            entity.ReplaceComponent(new SpawnPointsWithBoolComponent() { Value = gameObject.GetComponent<WeaponBoolSpawnPointsView>().BulletSpawnPoints });
            entity.ReplaceComponent(new MaxChargesComponent() { Value = parameters.MaxCharges });
            world.LaserChargeChange(parameters.MaxCharges);
            entity.AddParent(player);
            
            entity.Get<AdditionalWeaponComponent>();
            entity.Get<PlayerTagComponent>();
            entity.Get<WeaponTagComponent>();
            return entity;
        }

        public static EcsEntity InitializeLevel(this CustomEcsWorld world, EcsEntity entity)
        {
            world.SetUnique(ref world.Level, ref entity);
            entity.Get<ScoreComponent>();
            world.ScoreChange(0);
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
            
            entity.ReplaceComponent(new DirectedSpawnPointsComponent() { Value = pointsBase.ToArray() });
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
            
            entity.ReplaceComponent(new SpawnPointsComponent() { Value = pointsBase.ToArray() });
            entity.Get<UFOTagComponent>();
            return entity;
        }

        public static EcsEntity CreateBigAsteroid(this CustomEcsWorld world, Vector3 position, Vector3 spawnAngle, BigAsteroidParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.BigAsteroid, position);
            gameObject.transform.eulerAngles = spawnAngle;
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            var random = new System.Random();
            var speed = random.Next(parameters.MinSpeed, parameters.MaxSpeed);
            entity.ReplaceComponent(new SpeedComponent() { Value = speed });
            entity.ReplaceComponent(new DirectionComponent() { Value = Vector2.up });
            entity.ReplaceComponent(new AsteroidSizeComponent() { Value = EAsteroidSizeType.Big });
            entity.ReplaceComponent(new CostInPointsComponent() { Value = parameters.CostInPoints });

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
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            var random = new System.Random();
            var speed = random.Next(parameters.MinSpeed, parameters.MaxSpeed);
            entity.ReplaceComponent(new SpeedComponent() { Value = speed });
            entity.ReplaceComponent(new DirectionComponent() { Value = Vector2.up });
            entity.ReplaceComponent(new CostInPointsComponent() { Value = parameters.CostInPoints });

            entity.Get<EnemyTagComponent>();
            entity.Get<TransformMoveComponent>();
            entity.Get<UnitComponent>();
            return entity;
        }

        public static EcsEntity CreateUFO(this CustomEcsWorld world, Vector3 position, UFOParameters parameters)
        {
            var entity = world.NewEntity();
            var gameObject = entity.AddPrefab(PrefabNames.UFO, position);
            entity.ReplaceComponent(new TransformComponent() { Value = gameObject.transform });
            entity.ReplaceComponent(new RigidbodyComponent() { Value = gameObject.GetComponent<Rigidbody2D>() });
            entity.ReplaceComponent(new SpeedComponent() { Value = parameters.Speed });
            entity.ReplaceComponent(new DirectionComponent() { Value = Vector2.up });
            entity.ReplaceComponent(new CostInPointsComponent() { Value = parameters.CostInPoints });

            entity.Get<EnemyTagComponent>();
            entity.Get<FollowPlayerComponent>();
            entity.Get<ForceMoveComponent>();
            entity.Get<UnitComponent>();
            return entity;
        }
    }
}
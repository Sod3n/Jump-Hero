using AleVerDes.LeoEcsLiteZoo;
using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;

namespace MovementByAIAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal class MoveBetweenPatrolPoints : IEcsRunSystem
    {
        EcsFilter _entities;
        EcsPool<PatrolPoints> _patrolPoints;
        EcsPool<Rigidbody2DRef> _rigidbody2DRefs;
        EcsPool<MovementSpeed> _movementSpeeds;
        EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
        }
        public void Run(IEcsSystems systems)
        {
            if (_entities is null) _entities = _world.Filter<PatrolPoints>().Inc<Rigidbody2DRef>().Inc<MovementSpeed>().End();
            if (_entities is null) return;

            foreach (int entity in _entities)
            {
                ref var patrolPoints = ref _patrolPoints.Get(entity);
                var movementSpeed = _movementSpeeds.Get(entity).Value;

                var body = _rigidbody2DRefs.Get(entity).Value;

                var nextPointPos = patrolPoints.Points[patrolPoints.NextPoint].position;
                var heading = nextPointPos - body.transform.position;
                var moveVec = heading.normalized * movementSpeed * Time.fixedDeltaTime;
                body.MovePosition(body.transform.position + moveVec);
                if(moveVec.sqrMagnitude > heading.sqrMagnitude)
                {
                    patrolPoints.NextPoint++;
                    if (patrolPoints.NextPoint >= patrolPoints.Points.Length)
                        patrolPoints.NextPoint = 0;
                    body.velocity = Vector3.zero;
                }
            }
        }
    }
}


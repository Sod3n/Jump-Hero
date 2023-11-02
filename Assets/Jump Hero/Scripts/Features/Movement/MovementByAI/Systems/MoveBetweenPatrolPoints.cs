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
        EcsQuery<PatrolPoints, Rigidbody2DRef, MovementSpeed> _entities;
        EcsPool<PatrolPoints> _patrolPoints;
        EcsPool<Rigidbody2DRef> _rigidbody2DRefs;
        EcsPool<MovementSpeed> _movementSpeeds;
        EcsWorld _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities)
            {
                ref var patrolPoints = ref _patrolPoints.Get(entity);
                var movementSpeed = _movementSpeeds.Get(entity).Value;
                Debug.Log(entity);
                var body = _rigidbody2DRefs.Get(entity).Value;
                Debug.Log(_rigidbody2DRefs.Get(entity).Value);
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


using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Pathfinding;
using UnityEngine;

namespace Runtime.Entity.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4.0f;
        [SerializeField] private Transform target;

        private Rigidbody2D _rb;
        private Pathfinder _pathfinder;
        private List<Node> _currentPath;
        private int _currentPathIndex;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _pathfinder = GetComponent<Pathfinder>();
        }

        private void Start()
        {
            StartCoroutine(UpdatePath());
        }

        private void FixedUpdate()
        {
            if (_currentPath == null || _currentPathIndex >= _currentPath.Count) return;

            var node = _currentPath[_currentPathIndex];
            var targetPosition = node.WorldPosition;
            var distance = Vector2.Distance(transform.position, targetPosition);
            var canMove = distance > 0.1f;
            
            if (canMove)
            {
                var direction = (targetPosition - (Vector2) transform.position).normalized;
                _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
            }
            else
            {
                _currentPathIndex++;
            }
        }

        private IEnumerator UpdatePath()
        {
            while (true)
            {
                if (target)
                {
                    _currentPath = _pathfinder.FindPath(target.position);
                    _currentPathIndex = 0;
                }

                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
using UnityEngine;

namespace Runtime.Entity.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10.0f;

        private Inputs _inputs;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _inputs = new Inputs();
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
            => _inputs.Enable();
        
        private void OnDisable()
            => _inputs.Disable();

        private void FixedUpdate()
        {
            var move = _inputs.Player.Movement.ReadValue<Vector2>();
            var movement = move * (moveSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + movement);
        }
    }
}

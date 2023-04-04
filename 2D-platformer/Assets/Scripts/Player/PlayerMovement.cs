using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        [Header("HorizontalMovement")] 
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jump")] [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _jumpableGround;
        

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _boxCollider;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }


        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }
        
        
        public void Jump()
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce);
            }
        }


        public void UpdateAnimation(float direction)
        {
            AnimationType state;
                
            if (_rigidbody.velocity.x > 0)
            {
                state = AnimationType.Running;
            }
            else if (_rigidbody.velocity.x < 0)
            {
                state = AnimationType.Running;
            }
            else 
                state = AnimationType.Idle;
            

            if (_rigidbody.velocity.y > .1f)
            {
                state = AnimationType.Jumping;
            }
            else if (_rigidbody.velocity.y < -.1f)
            {
                state = AnimationType.Falling;
            }
            
            _animator.SetInteger("state", (int)state);
        }
        

        private bool IsGrounded()
        {
            return Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
        }
        

        private void SetDirection(float direction)
        {
            if (_faceRight && direction < 0 || !_faceRight && direction > 0)
                Flip();
        }
        

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }

    }
}

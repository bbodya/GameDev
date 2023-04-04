using Player;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    private float _direction;

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        _playerMovement.UpdateAnimation(_direction);

        if (Input.GetButtonDown("Jump")) 
            _playerMovement.Jump();
    }

    private void FixedUpdate()
    {
        _playerMovement.MoveHorizontally(_direction);
    }
}

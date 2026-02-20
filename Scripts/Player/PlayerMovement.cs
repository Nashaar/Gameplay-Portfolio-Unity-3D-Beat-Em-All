using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Declarations
    [Header("References")]
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Transform _playerOrientation;
    [SerializeField] private InputController _inputController;

    [Space]

    [Header("Movement Variables")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _groundDrag;
    private float _movementSpeed;    
    
    [Space]

    [Header("Jump Variables")]
    [SerializeField] private float _walkJumpForce;
    [SerializeField] private float _sprintJumpForce;
    [SerializeField] private float _walkAirResistance;
    [SerializeField] private float _sprintAirResistance;
    [SerializeField] private float _fallMultiplier;
    private float _jumpForce;
    private float _airResistance;

    [Space]

    [Header("Ground Check")]
    public LayerMask groundLayers;
    [SerializeField] private float _playerHeight;
    [SerializeField] private bool _isGrounded;
    #endregion

    #region Unity Functions
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        SpeedSetter();
        MovePlayer();
        JumpPlayer();
    }

    void FixedUpdate()
    {
        GravityFall();
    }
    #endregion

    #region Move Functions
    private void MovePlayer()
    {
        Vector2 movementInputDirection = _inputController.MoveInput();
        Vector3 moveDirection = _playerOrientation.forward * movementInputDirection.y + _playerOrientation.right * movementInputDirection.x;

        if(_isGrounded)
        {
            _playerRb.linearVelocity = new Vector3 (moveDirection.x * _movementSpeed, _playerRb.linearVelocity.y, moveDirection.z * _movementSpeed);
        }
        else
        {
            _playerRb.linearVelocity = new Vector3 (moveDirection.x * _movementSpeed * _airResistance, _playerRb.linearVelocity.y, moveDirection.z * _movementSpeed * _airResistance);
        }  
    }

    public void SpeedSetter()
    {
        if(!_inputController.InputHeld(_inputController.sprintAction) || !_isGrounded)
        {
            _movementSpeed = _walkSpeed;
            _jumpForce = _walkJumpForce;
            _airResistance = _walkAirResistance;
        }
        else
        {
            _movementSpeed = _sprintSpeed;
            _jumpForce = _sprintJumpForce;
            _airResistance = _sprintAirResistance;
        }
    }
    #endregion

    #region Jump Functions
    private void JumpPlayer()
    {
        if(!_inputController.InputPressed(_inputController.jumpAction) || !_isGrounded)
        {
            return;
        }
        
        _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void GravityFall()
    {
        if(_playerRb.linearVelocity.y > 0)
        {
            return;
        }

        _playerRb.AddForce(Vector3.down * _fallMultiplier, ForceMode.Acceleration);
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, groundLayers);
        if(_isGrounded)
        {
            _playerRb.linearDamping = _groundDrag;
        }
        else
        {
            _playerRb.linearDamping = 0;
        }
    }
    #endregion
}

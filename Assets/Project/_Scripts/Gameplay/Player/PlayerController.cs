using UnityEngine;

namespace Project._Scripts.Gameplay.Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CameraController))]
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float baseSpeed = 5f;
        [SerializeField] float sprintMultiplier = 0.3f;
        [SerializeField] float sprintStaminaCost = 0.5f;
        
        [Header("Camera")]
        [SerializeField] float aimingZoom = 10f;
        
        PlayerInputHandler _inputHandler;
        Rigidbody2D _rigidbody;
        CameraController _camController;
        PlayerStats _playerStats;
        bool _isSprinting = false;
        bool _isAiming = false;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
            _inputHandler = GetComponent<PlayerInputHandler>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _camController = GetComponent<CameraController>();
        }

        // Update is called once per frame
        void Update()
        {
            _isSprinting = _inputHandler.IsSprinting;
            _isAiming = _inputHandler.IsAiming;
            
            _playerStats.staminaRegenEnabled = !_isSprinting;
        }

        private void FixedUpdate()
        {
            if (_isAiming)
            {
                _camController.SetZoom(aimingZoom);
                _camController.EnableOffset();
            }
            else
            {
                _camController.ResetZoom();
                _camController.DisableOffset();
            }
            
            HandleCharacterMovement(); 
        }

        float GetCurrentSpeed()
        {
            float moveSpeed = baseSpeed;
            if (_isSprinting && _playerStats.Stamina > 0f)
            {
                moveSpeed *= sprintMultiplier;
                _playerStats.Stamina -= sprintStaminaCost * Time.deltaTime;
            }
            
            return moveSpeed;
        }
        void HandleCharacterMovement()
        {
            _rigidbody.MovePosition(_rigidbody.position + _inputHandler.GetMoveInput() * (GetCurrentSpeed() * Time.fixedDeltaTime));
        }
    }
   
}
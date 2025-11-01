using Project._Scripts.Managers.Global;
using UnityEngine;

namespace Project._Scripts.Gameplay.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerInputHandler : MonoBehaviour
    {
        public bool IsAiming => CanProcessInput() && Input.GetMouseButton(1);
        public bool IsSprinting => CanProcessInput() && Input.GetKey(KeyCode.LeftShift);

        private PlayerHealth _health;
        private void Start()
        {
            _health = GetComponent<PlayerHealth>();
        }

        private bool CanProcessInput()
        {
            return !_health.IsDead && !UIManager.Instance.CurrentCanvasOptions.disableControl;
        }
        public Vector2 GetMoveInput()
        {
            if (CanProcessInput())
            {
                Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                return move.normalized;
            }

            return Vector2.zero;
        }
    }
}


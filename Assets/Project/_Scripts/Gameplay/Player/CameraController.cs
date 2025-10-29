using Unity.Cinemachine;
using UnityEngine;

namespace Project._Scripts.Gameplay.Player
{
    public class CameraController : MonoBehaviour
    {
        [Header("Zoom")]
        public float baseZoom = 5f;
        public float cameraSpeed = 5f;
        
        [Header("Offset")]
        public float maxOffset = 20f;
        public float offsetSpeed = 5f;
        
        bool _isCameraOffsetMouse;
        private CinemachinePositionComposer _composer;
        private CinemachineCamera _vCam;
        private Transform _playerTransform;
        private float _targetZoom;
    
        void Start()
        {
            _vCam = FindAnyObjectByType<CinemachineCamera>();
            _playerTransform = gameObject.transform;
            _composer = _vCam.gameObject.GetComponent<CinemachinePositionComposer>();
    
            _targetZoom = baseZoom;
            _vCam.Lens.OrthographicSize = _targetZoom;
            _vCam.Target.TrackingTarget = _playerTransform;
        }
        // Update is called once per frame
        void Update()
        {
            // Calculate offset for aiming to mouse side
            _composer.TargetOffset = Vector3.Lerp(_composer.TargetOffset, GetMouseOffset(), Time.deltaTime * offsetSpeed);
            
            // Calculate zoom to camera for aiming
            _vCam.Lens.OrthographicSize = Mathf.Lerp(_vCam.Lens.OrthographicSize, _targetZoom, cameraSpeed * Time.deltaTime);
        }
    
        public void SetZoom(float zoom) => _targetZoom = zoom;
        public void ResetZoom() => _targetZoom = baseZoom;
        public void EnableOffset() => _isCameraOffsetMouse = true;
        public void DisableOffset() => _isCameraOffsetMouse = false;
        
        Vector3 GetMouseOffset()
        {
            if (!_isCameraOffsetMouse)
                return Vector3.zero;
            
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Vector3 delta = (mousePos - screenCenter) / Screen.width;
            
            return new Vector3(delta.x * maxOffset, delta.y * maxOffset, 0);
        }
    }
}

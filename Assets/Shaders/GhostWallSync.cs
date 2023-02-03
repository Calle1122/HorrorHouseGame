using UnityEngine;

namespace Shaders
{
    public class GhostWallSync : MonoBehaviour
    {
        [SerializeField] private Transform wallOpenerFront, wallOpenerBack;
        private Transform _wallOpener;
        
        public static int PosID = Shader.PropertyToID("_Position");
        public static int SizeID = Shader.PropertyToID("_Size");

        public Material wallMat;
        public Camera cam;
        public LayerMask mask;

        private float _desiredSize = 0f;
        private float _currentSize = 0f;
        private float _animationSpeed = 5f;

        private void Start()
        {
            cam = Camera.main;
            _wallOpener = wallOpenerFront;
        }

        private void Update()
        {
            var dir = cam.transform.position - _wallOpener.position;
            var ray = new Ray(_wallOpener.position, dir.normalized);

            if (Physics.Raycast(ray, Mathf.Infinity, mask))
            {
                _wallOpener = wallOpenerBack;
                _desiredSize = 1f;
                _animationSpeed = 5f;
            }
            else
            {
                _wallOpener = wallOpenerFront;
                _desiredSize = 0f;
                _animationSpeed = 2f;
            }

            _currentSize = Mathf.Lerp(_currentSize, _desiredSize, Time.deltaTime * _animationSpeed);
            wallMat.SetFloat(SizeID, _currentSize);

            var view = cam.WorldToViewportPoint(transform.position);
            wallMat.SetVector(PosID, view);
        }
    }
}
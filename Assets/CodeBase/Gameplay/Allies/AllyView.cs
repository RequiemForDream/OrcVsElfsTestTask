using System;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem;
using CodeBase.Gameplay.Tiles;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Gameplay.Allies
{
    public class AllyView : TargetView, IEndDragHandler, IBeginDragHandler, IDragHandler
    {
        public event Action OnDestroyHandler;
        public override ITarget Target { get; set; }
        [SerializeField] private LayerMask _tileLayerMask;
        
        public Collider Collider;
        public Transform ArrowSpawnPoint;
        public TriggerObserver AttackRangeTrigger;
        public AllyAnimator AllyAnimator;

        public void SetTarget(ITarget target) => Target = target;
        
        private float _dragPlaneY;
        private Camera _camera;
        private Vector3 _startPosition;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Ray ray = _camera.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _tileLayerMask))
            {
                print(hit.collider.gameObject.name);
                if (hit.collider.TryGetComponent(out TileView tileView))
                {
                    print(tileView);
                    transform.position = tileView.transform.position;
                }
            }
            else
            {
                transform.position = _startPosition;
            }
            
            Collider.enabled = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
            _dragPlaneY = transform.position.y;
            Collider.enabled = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            
            Ray ray = _camera.ScreenPointToRay(eventData.position);
            Plane plane = new Plane(Vector3.up, new Vector3(0, _dragPlaneY, 0));

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPoint = ray.GetPoint(distance);
                transform.position = worldPoint;
            }
        }
    }
}
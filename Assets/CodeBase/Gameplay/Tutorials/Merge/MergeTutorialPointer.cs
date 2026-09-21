using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials.Merge
{
    public class MergeTutorialPointer : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 0.6f;
        [SerializeField] private float _pauseAtStart = 0.3f;
        [SerializeField] private float _pauseAtEnd = 0.3f;
        [SerializeField] private float _tiltAngle = 30f;

        private RectTransform _pointerRect;
        private RectTransform _canvasRect;
        private Camera _worldCamera;
        private Sequence _loopSequence;

        private void Awake()
        {
            _pointerRect = GetComponent<RectTransform>();
            _canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        }

        public void SetCamera(Camera mainCamera) => _worldCamera = mainCamera;

        public void PlayDragLoop(Transform from, Transform to)
        {
            Vector2 startPoint = WorldToCanvasPoint(from.position);
            Vector2 endPoint = WorldToCanvasPoint(to.position);

            _pointerRect.anchoredPosition = startPoint;
            _pointerRect.localRotation = Quaternion.identity;

            _loopSequence = DOTween.Sequence();

            _loopSequence
                .AppendInterval(_pauseAtStart)
                .Append(_pointerRect.DOAnchorPos(endPoint, _moveDuration).SetEase(Ease.InOutQuad))
                .Join(_pointerRect.DOLocalRotate(new Vector3(0, 0, -_tiltAngle), _moveDuration).SetEase(Ease.InOutQuad))
                .AppendInterval(_pauseAtEnd)
                .Append(_pointerRect.DOAnchorPos(startPoint, _moveDuration).SetEase(Ease.InOutQuad))
                .Join(_pointerRect.DOLocalRotate(Vector3.zero, _moveDuration).SetEase(Ease.InOutQuad))
                .AppendInterval(_pauseAtStart)
                .SetLoops(-1);
        }

        private void Stop()
        {
            _loopSequence?.Kill();
            _loopSequence = null;
        }

        private Vector2 WorldToCanvasPoint(Vector3 worldPosition)
        {
            Vector2 screenPoint = _worldCamera.WorldToScreenPoint(worldPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect,
                screenPoint,
                null,
                out Vector2 localPoint);

            return localPoint;
        }

        private void OnDestroy() => Stop();
    }
}
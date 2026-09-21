using System;
using System.Collections.Generic;
using CodeBase.Common.Extensions;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Merge;
using CodeBase.Gameplay.Tiles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Gameplay.Allies
{
    public class AllyDragHandler : IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly AllyView _allyView;
        private readonly IAlly _allyController;
        private readonly ILevelDataProvider _levelDataProvider;

        private Vector3 _startPosition;
        private readonly IMergeService _mergeService;

        public AllyDragHandler(AllyView allyView, IAlly allyController, ILevelDataProvider levelDataProvider,
            IMergeService mergeService)
        {
            _mergeService = mergeService;
            _levelDataProvider = levelDataProvider;
            _allyView = allyView;
            _allyController = allyController;
        }

        public void Initialize()
        {
            _allyView.GetComponent<ObservableBeginDragTrigger>().OnBeginDragAsObservable()
                .Subscribe(OnBeginDrag)
                .AddTo(_disposables);
            _allyView.GetComponent<ObservableDragTrigger>().OnDragAsObservable()
                .Subscribe(OnDrag)
                .AddTo(_disposables);
            _allyView.GetComponent<ObservableEndDragTrigger>().OnEndDragAsObservable()
                .Subscribe(OnEndDrag)
                .AddTo(_disposables);
        }

        private void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = _allyView.transform.position;
            _allyView.Collider.enabled = false;
            
            IReadOnlyList<IMergeable> mergeables = _mergeService.GetMergeableTiles(_allyController.Tile);

            foreach (IMergeable mergeable in mergeables)
                mergeable.SetMergeableHighlightActive(true);
        }

        private void OnDrag(PointerEventData eventData)
        {
            Ray ray = _levelDataProvider.MainCamera.ScreenPointToRay(eventData.position);
            Plane plane = new Plane(Vector3.up, new Vector3(0, _allyView.transform.position.y, 0));

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPoint = ray.GetPoint(distance);
                _allyView.transform.position = worldPoint;
            }
        }

        private void OnEndDrag(PointerEventData eventData)
        {
            ClearHighlights();
            
            Ray ray = _levelDataProvider.MainCamera.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, CollisionLayer.Tile.AsMask())
                && hit.collider.TryGetComponent(out TileView tileView))
            {
                ITile currentTile = _allyController.Tile;
                ITile targetTile = tileView.TileController;

                if (targetTile == currentTile)
                {
                    ReturnToStart();
                    return;
                }

                if (!targetTile.IsOccupied)
                {
                    MoveAllyToTile(tileView);
                    return;
                }

                if (_mergeService.TryMerge(targetTile, currentTile))
                    return;
            }

            ReturnToStart();
        }

        private void ReturnToStart()
        {
            _allyView.transform.position = _startPosition;
            _allyView.Collider.enabled = true;
        }

        private void MoveAllyToTile(TileView tileView)
        {
            tileView.TileController.SetAlly(_allyController);
            _allyController.Tile.Deoccupy();
            _allyController.SetTile(tileView.TileController);
            _allyView.transform.position = tileView.transform.position;
            _allyView.Collider.enabled = true;
        }
        
        private void ClearHighlights()
        {
            IReadOnlyList<IMergeable> mergeables = _mergeService.GetMergeableTiles(_allyController.Tile);

            foreach (IMergeable mergeable in mergeables)
                mergeable.SetMergeableHighlightActive(false);
        }

        public void Dispose() => _disposables.Dispose();
    }
}
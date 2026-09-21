using System;
using CodeBase.Gameplay.Common.Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Arrows
{
    public class Arrow : IArrow
    {
        public event Action OnHit;
        
        private readonly ArrowModel _arrowModel;
        private readonly ArrowView _arrowView;
        private readonly TickableManager _tickableManager;
        
        private Vector3 _startPos;
        private Vector3 _endPos;

        private float _t;

        private ITarget _target;

        public Arrow(ArrowModel arrowModel, ArrowView arrowView, TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
            _arrowModel = arrowModel;
            _arrowView = arrowView;
        }

        public void Initialize()
        {
            _tickableManager.Add(this);
            _arrowView.OnDestroyHandler += Destroy;
        }

        public void Release(ITarget target)
        {
            _target = target;
            _startPos = _arrowView.transform.position;
            _t = 0f;
        }

        public void Tick()
        {
            if (_target == null) 
                return;

            _t += Time.deltaTime * _arrowModel.Speed;

            float tClamped = Mathf.Clamp01(_t);

            float tEased = Mathf.SmoothStep(0f, 1f, tClamped);

            Vector3 pos = Vector3.Lerp(_startPos, _target.HitPosition, tEased);

            float height = 4f * _arrowModel.ArcHeight * tEased * (1f - tEased);
            pos.y += height;

            float nextT = Mathf.Clamp01(_t + 0.01f);
            float nextEased = Mathf.SmoothStep(0f, 1f, nextT);

            Vector3 nextPos = Vector3.Lerp(_startPos, _target.HitPosition, nextEased);
            nextPos.y += 4f * _arrowModel.ArcHeight * nextEased * (1f - nextEased);

            Vector3 dir = (nextPos - pos).normalized;
            if (dir != Vector3.zero)
            {
                _arrowView.transform.rotation = Quaternion.LookRotation(dir);
            }

            _arrowView.transform.position = pos;
            
            float distance = Vector3.Distance(
                _arrowView.transform.position,
                _target.HitPosition
            );

            if(distance < 0.3f)
            {
                OnHit?.Invoke();
                Object.Destroy(_arrowView.gameObject);
            }
        }

        public event Action OnDestroyHandler;

        public void Destroy()
        {
            _tickableManager.Remove(this);
            _arrowView.OnDestroyHandler -= Destroy;
        }
    }
}
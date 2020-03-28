using System;
using System.Collections;
using UnityEngine;

namespace Animations
{
    /// <summary>
    /// PropertyAnimation allows to describe and reuse
    /// some custom animation to work with 
    /// </summary>
    /// Usage example:
    /// <code>
    /// anim = PropertyAnimation(this, config, value -> property = value);
    /// anim.OnDone += OnDoneEvent;
    /// anim.StartValue = 1.0f;
    /// anim.EndValue = 5.0f;
    /// anim.Start(AnimationCallback);
    /// </code>
    public class PropertyAnimation : PropertyCurve
    {
        public event Action OnDone = null;
        
        private readonly AnimationConfig config = null;
        private readonly Routine animationRoutine = null;
        private float startTime = 0.0f;
        private float EndTime => startTime + config.m_duration;
        
        private IEnumerator AnimationCoroutine(Action callback)
        {
            Progress = 0.0f;
            startTime = Time.time;
            while (Time.time < EndTime)
            {
                Progress = Mathf.InverseLerp(startTime, EndTime, Time.time);
                yield return null;
            }

            Progress = 1.0f;
            callback?.Invoke();
            OnDone?.Invoke();
        }

        public PropertyAnimation(MonoBehaviour owner, AnimationConfig config, Action<float> setter) 
            : base(config, setter)
        {
            this.config = config;
            animationRoutine = new Routine(owner);
            if (config.m_loop) OnDone += Start;
        }
        
        public void Start(Action callback)
        {
            if (animationRoutine.Active && !(config.m_allowRestart || config.m_loop)) return;
            animationRoutine.Start(AnimationCoroutine(callback));
        }

        public void Start() => Start(null);

        public void Finish()
        {
            animationRoutine.Stop();
            Progress = 1.0f;
            OnDone?.Invoke();
        }

        public void Cancel()
        {
            animationRoutine.Stop();
            Progress = 0.0f;
        }
    }
}
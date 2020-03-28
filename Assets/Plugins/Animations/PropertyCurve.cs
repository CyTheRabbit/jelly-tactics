using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Animations
{
    /// <summary>
    /// PropertyCurve defines custom curve for given property
    /// and updates it when the progress property is updated.
    /// </summary>
    public class PropertyCurve
    {
        private readonly AnimationConfig config = null;

        private readonly Action<float> setter = null;
        [Range(0, 1)] private float progress = 0.0f;

        public float Progress
        {
            get => progress;
            set
            {
                progress = value;
                Update();
            }
        }

        public float StartValue { get; set; } = 0.0f;
        public float EndValue { get; set; } = 1.0f;
        public float Value => Mathf.LerpUnclamped(StartValue, EndValue, config.m_curve.Evaluate(progress));

        public PropertyCurve([NotNull] AnimationConfig animationConfig, [NotNull] Action<float> propertySetter)
        {
            config = animationConfig;
            setter = propertySetter;
            progress = 0.0f;
        }
        
        public void Update() => setter?.Invoke(Value);
    }
}
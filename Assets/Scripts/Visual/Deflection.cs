using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Visual
{
    [Serializable]
    [PostProcess(typeof(DeflectionRenderer), PostProcessEvent.AfterStack, "Custom/Deflection")]
    public sealed class Deflection : PostProcessEffectSettings
    {
        [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
        public FloatParameter m_timeScale = new FloatParameter {value = 0.5f};
        public FloatParameter m_waveScale = new FloatParameter {value = 4f};
        public Vector2Parameter m_offsetDirection = new Vector2Parameter {value = Vector2.zero};
        public TextureParameter m_noiseTexture = new TextureParameter();
    }

    public sealed class DeflectionRenderer : PostProcessEffectRenderer<Deflection>
    {
        private static readonly int T = Shader.PropertyToID("_T");
        private static readonly int Offset = Shader.PropertyToID("_Offset");
        private static readonly int NoiseTexture = Shader.PropertyToID("_NoiseTex");
        private static readonly int WaveScale = Shader.PropertyToID("_WaveScale");


        public override void Render(PostProcessRenderContext context)
        {
            PropertySheet sheet = context.propertySheets.Get(Shader.Find("Hidden/Deflection"));
            float t = Time.time * settings.m_timeScale;
            sheet.properties.SetFloat(T, t);
            sheet.properties.SetFloat(WaveScale, settings.m_waveScale);
            sheet.properties.SetVector(Offset, settings.m_offsetDirection);
            sheet.properties.SetTexture(NoiseTexture, settings.m_noiseTexture);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
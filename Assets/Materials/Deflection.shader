Shader "Hidden/Deflection"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
        
        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

        Texture2D _NoiseTex;
        float _T;        
        float2 _Offset;
        float _WaveScale;
        
        SamplerState my_linear_repeat_sampler;

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float2 noisePosition = i.texcoord / _WaveScale + _T.xx;
            float4 offsetValue = SAMPLE_TEXTURE2D(_NoiseTex, my_linear_repeat_sampler, noisePosition);
            
            float2 uv = i.texcoord + _Offset * offsetValue;
            float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
            return color;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}

// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Underwater_Light" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0


        _NoiseTex ("Noise (BW)", 2D) = "white" {}
        _WaveAmplitude ("Wave Amplitude", Range(0, 1)) = 0.1
        _WaveWidth ("Wave Width", Range(0, 0.5)) = 0.3
        _WaveOrigin ("Wave Origin", Range(0, 1)) = 0.3
        _WaveDim ("Wave Dim", Range(0, 1)) = 0.9
        _WaveLight ("Wave Light", Range(0, 1)) = 1.15
        _WaveScale ("Wave Scale", float) = 64
        _WaveSpeed ("Wave Speed", float) = 0.1
        _WaveCosSpeed ("Wave Cos Speed", float) = 0.025
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM


                #pragma surface surf Standard fullforwardshadows vertex:vert


        // Physically based Standard lighting model, and enable shadows on all light types
        // #pragma surface surf Standard fullforwardshadows
        

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;


        sampler2D _NoiseTex;
    
        half _WaveAmplitude;
        half _WaveWidth;
        half _WaveOrigin;
        half _WaveDim;
        half _WaveLight;
        half _WaveScale;
        half _WaveSpeed;
        half _WaveCosSpeed;


        half ClampNormalize(half noiseValue, half offset, half width)
        {
            half raw = clamp (noiseValue, offset, offset + width);
            return (raw - offset) / width;
        }
        
        half GradNormalize(half noiseValue, half offset, half width)
        {
            return ClampNormalize(noiseValue, offset, width)
                 - ClampNormalize(noiseValue, offset + width, width);
        }
        
        half WaveLight(half noiseValue, half offset, half width, half dim, half light)
        {
            return lerp( dim, light, GradNormalize(noiseValue, offset, width) );
        }


        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            
            // Wave diffraction
            half wavePos = _Time.x * _WaveSpeed + _CosTime.x * _WaveCosSpeed;
            half waveT = _WaveOrigin + _SinTime.w * _WaveAmplitude;
            half noise = tex2D (_NoiseTex, IN.worldPos.xz / _WaveScale + wavePos);
            o.Albedo *= WaveLight(noise, waveT, _WaveWidth, _WaveDim, _WaveLight);

        }
        
 
        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = mul( v.vertex.xyz, unity_ObjectToWorld );
        }
        
        ENDCG
    }
    FallBack "Diffuse"
}
Shader "Unlit/CustomShader"
{
	Properties
	{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo", 2D) = "white" {}

    _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
        _GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
        [Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel("Smoothness texture channel", Float) = 0

        [Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        _MetallicGlossMap("Metallic", 2D) = "white" {}

    [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

        _BumpScale("Scale", Float) = 1.0
        _BumpMap("Normal Map", 2D) = "bump" {}

    _Parallax("Height Scale", Range(0.005, 0.08)) = 0.02
        _ParallaxMap("Height Map", 2D) = "black" {}

    _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
        _OcclusionMap("Occlusion", 2D) = "white" {}

    _EmissionColor("Color", Color) = (0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}

    _DetailMask("Detail Mask", 2D) = "white" {}

    _DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
    _DetailNormalMapScale("Scale", Float) = 1.0
        _DetailNormalMap("Normal Map", 2D) = "bump" {}

    [Enum(UV0,0,UV1,1)] _UVSec("UV Set for secondary textures", Float) = 0


        // Blending state
        [HideInInspector] _Mode("__mode", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 1.0
        [HideInInspector] _DstBlend("__dst", Float) = 0.0
        [HideInInspector] _ZWrite("__zw", Float) = 1.0
    }

	SubShader
	{
        UsePass "Standard/FORWARD"
        UsePass "Standard/FORWARD_DELTA"
        UsePass "Standard/SHADOWCASTER"
        UsePass "Standard/DEFERRED"
        UsePass "Standard/META"

        Pass
		{
            Tags {"RenderType" = "Opaque"}
            LOD 200

            Name "AMBIENT_PASS"
            Cull Off
            ZWrite Off
            Blend SrcAlpha One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            #include "UnityCG.cginc"

            #define _DayColor fixed4(0.93,0.88,0.48,0.1)
            #define _NightColor fixed4(0.05,0.06,0.647,0.2)
            uniform int _Day = 1;


			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
                float depth : DEPTH;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                //return fixed4(1,1,1,0.3);
                if (_Day == 1) {
                    return _DayColor;
                } else {
                    return _NightColor;
                }
            }
			ENDCG
		}

        Pass {
            Name "FOG_PASS"
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define _FogColor fixed4(0.2,0.2,0.3,0.8)

            uniform int _Fog = 1;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float fogIntensity : FOG;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 cameraPos = UnityObjectToViewPos(v.vertex);
                o.fogIntensity = (_ProjectionParams.z * 0.5 - cameraPos.z) / ((_ProjectionParams.z - _ProjectionParams.y));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.fogIntensity * _FogColor * _Fog;
            }
            ENDCG
        }
         
        Pass{
            Name "FLASHLIGHT_PASS"

            Cull Off
            ZWrite Off
            Blend SrcAlpha One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define _LightColor fixed4(0.8,0.8,0.5,0.6)
            #define _LightRadius 0.13
            #define _Sharpness 2.1

            uniform int _Light = 0;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float lightIntensity : LIGHT;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 cameraPos = UnityObjectToViewPos(v.vertex);
                o.lightIntensity = ((_ProjectionParams.z - _ProjectionParams.y)) / (_ProjectionParams.z - cameraPos.z);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dist = distance(float2(0.25,-0.15), ComputeScreenPos(i.vertex).xy / _ScreenParams.x);
                return (1 - pow(dist / _LightRadius, _Sharpness)) * i.lightIntensity * _LightColor * _Light;
            }
            ENDCG
        }
	}
}

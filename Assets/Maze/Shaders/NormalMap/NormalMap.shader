Shader "COMP7051 Shader Demo/NormalMap" {
// Adapted from tutorials on Unity website
    Properties{
        _AmbientLightColor("Ambient Light Color", Color) = (1,1,1,1)
        _AmbientLighIntensity("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma target 2.0
            #pragma vertex vertexShader
            #pragma fragment fragmentShader
           // include file that contains UnityObjectToWorldNormal helper function
            #include "UnityCG.cginc"

            float4 _AmbientLightColor;
            float _AmbientLighIntensity;
 
 
            struct vsIn {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };
 
            struct vsOut {
                float4 position : SV_POSITION;
                half3 worldNormal : TEXCOORD0;
            };
 
            vsOut vertexShader(vsIn v)
            {
                vsOut o;
                o.position = UnityObjectToClipPos(v.position);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }
 
            float4 fragmentShader(vsOut psIn) : SV_Target
            {
            	fixed4 c = 0;
                // normal is a 3D vector with xyz components; in -1..1
                // range. To display it as color, bring the range into 0..1
                // and put into red, green, blue components
                c.rgb = psIn.worldNormal*0.5+0.5;
                return saturate(c + (_AmbientLightColor * _AmbientLighIntensity));
            }
 
            ENDCG
        }
    }
}
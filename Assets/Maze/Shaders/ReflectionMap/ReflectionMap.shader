Shader "COMP7051 Shader Demo/ReflectionMap" {
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
            #include "UnityCG.cginc"

            float4 _AmbientLightColor;
            float _AmbientLighIntensity;
 
 
            struct vsIn {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };
 
            struct vsOut {
                float4 position : SV_POSITION;
                half3 worldRefl : TEXCOORD0;
            };
 
            vsOut vertexShader(vsIn v)
            {
                vsOut o;
                o.position = UnityObjectToClipPos(v.position);
                // compute world space position of the vertex
                float3 worldPos = mul(unity_ObjectToWorld, v.position).xyz;
                // compute world space view direction
                float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                // world space normal
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                // world space reflection vector
                o.worldRefl = reflect(-worldViewDir, worldNormal);
                return o;
            }
 
            float4 fragmentShader(vsOut psIn) : SV_Target
            {
               // sample the default reflection cubemap, using the reflection vector
                half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, psIn.worldRefl);
                // decode cubemap data into actual color
                half3 skyColor = DecodeHDR (skyData, unity_SpecCube0_HDR);
                // output it!
                fixed4 c = 0;
                c.rgb = skyColor;
                return saturate(c + (_AmbientLightColor * _AmbientLighIntensity));
            }
 
            ENDCG
        }
    }
}
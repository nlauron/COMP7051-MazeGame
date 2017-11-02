Shader "Unlit/CustomShader"
{
	Properties
	{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo", 2D) = "white" {}
    }

	SubShader
	{
        Tags {"RenderType"="Opaque"}
        LOD 100
        Pass
		{
            Name "FOG_PASS"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
                half2 texcoord : TEXCOORD0;
                float depth : DEPTH;
			};
			
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

			v2f vert (appdata v)
			{
				v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.depth = -UnityObjectToViewPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                float inverse = 1 - i.depth;
                fixed4 col = tex2D(_MainTex, i.texcoord);
                return fixed4(inverse, inverse, inverse, 1) * col + _Color;
			}
			ENDCG
		}
	}
}

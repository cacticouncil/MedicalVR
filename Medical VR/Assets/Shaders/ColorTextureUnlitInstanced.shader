Shader "Custom/ColorTextureUnlitInstanced"
{
	Properties
	{
		//Make properties
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			//Include functions
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

			//Make structs
			struct appdata
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
                UNITY_INSTANCE_ID
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
                UNITY_INSTANCE_ID
			};

			//Import properties
            UNITY_INSTANCING_CBUFFER_START (MyProperties)
            //List what to instance
            UNITY_DEFINE_INSTANCED_PROP (float4, _Color)
            UNITY_INSTANCING_CBUFFER_END
			sampler2D _MainTex;
			
			v2f vert (appdata v)
			{
				v2f o;
                UNITY_SETUP_INSTANCE_ID (v);
                UNITY_TRANSFER_INSTANCE_ID (v, o);

				o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                UNITY_SETUP_INSTANCE_ID (i);

				// sample the texture
				fixed4 texColor = tex2D(_MainTex, i.uv);
				return texColor * UNITY_ACCESS_INSTANCED_PROP (_Color);
			}
			ENDCG
		}
	}
}

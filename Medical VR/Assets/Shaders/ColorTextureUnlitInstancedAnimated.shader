// Upgrade NOTE: replaced 'UNITY_INSTANCE_ID' with 'UNITY_VERTEX_INPUT_INSTANCE_ID'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorTextureUnlitInstancedAnimated"
{
	Properties
	{
		//Make properties
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_AnimationScale("Animation Scale", float) = 1
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
                UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			//Import properties
            UNITY_INSTANCING_CBUFFER_START (MyProperties)
            //List what to instance
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
			UNITY_DEFINE_INSTANCED_PROP(float, _AnimationScale)
            UNITY_INSTANCING_CBUFFER_END
			sampler2D _MainTex;
			
			v2f vert (appdata v)
			{
				v2f o;
                UNITY_SETUP_INSTANCE_ID (v);
                UNITY_TRANSFER_INSTANCE_ID (v, o);

				o.pos = UnityObjectToClipPos(v.pos);
				o.pos.x += _SinTime.w * _AnimationScale;
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

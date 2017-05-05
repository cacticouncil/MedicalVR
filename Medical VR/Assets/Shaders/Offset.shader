// Upgrade NOTE: replaced 'UNITY_INSTANCE_ID' with 'UNITY_VERTEX_INPUT_INSTANCE_ID'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Test/Offset"
{
	Properties
	{
		//Make properties
		_Color ("Color", Color) = (1, 1, 1, 1)
		_Tex1("Texture", 2D) = "white" {}
		_Tex2("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Pass
		{
			//Forward base applies the first light
			//If I had forward add it would apply all other lights in multiple passes
            Tags { "LightMode" = "ForwardBase" }

			//Include functions
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_instancing
            //This matches the "forward base" of the LightMode tag to ensure the shader compiles
            //properly for the forward bass pass. As with the LightMode tag, for any additional lights
            //this would be changed from _fwdbase to _fwdadd.
            #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"
			// color of light source (from "Lighting.cginc")
			uniform float4 _LightColor0;

			//Import user-specified properties
            UNITY_INSTANCING_CBUFFER_START (MyProperties)
            //List what to instance
            UNITY_DEFINE_INSTANCED_PROP (float4, _Color)
            UNITY_INSTANCING_CBUFFER_END
			sampler2D _Tex1;
			float4 _Tex1_ST;			
			sampler2D _Tex2;
			float4 _Tex2_ST;

			//Make structs
			struct appdata
			{
				float4 pos : POSITION;
				float2 uv1 : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv1 : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
                UNITY_SETUP_INSTANCE_ID (v);

				float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * UNITY_ACCESS_INSTANCED_PROP (_Color).rgb;

				o.pos = UnityObjectToClipPos(v.pos);
				o.uv1 = TRANSFORM_TEX(v.uv1, _Tex1);
				o.uv2 = TRANSFORM_TEX(v.uv2, _Tex2);
				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{
				return float4(UNITY_ACCESS_INSTANCED_PROP(_Color) * (tex2D(_Tex1, i.uv1) * tex2D(_Tex1, i.uv2)));
			}
			ENDCG
		}
	}
    //Fallback "Specular"
}

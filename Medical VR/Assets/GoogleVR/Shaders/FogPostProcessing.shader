Shader "Custom/PostProcessing/FogPostProcessing"
{
	Properties
	{
		_MainTex("", 2D) = "white" {}
	}
		SubShader
	{
	Pass
	{
		ZTest Always Cull Off ZWrite Off
		Fog{ Mode  Off }
		//LOD 200

		CGPROGRAM
		//#pragma vertex vert_img
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		sampler2D _CameraDepthTexture;
		sampler2D _MainTex;

		//uniform half4 unity_FogColor;
		uniform half4 unity_FogStart;
		uniform half4 unity_FogEnd;
		uniform half4 unity_FogDensity;

		struct v2f
		{
			float4 pos : SV_POSITION;
			float4 scrPos : TEXCOORD1;
		};

		v2f vert (appdata_base v)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.scrPos = ComputeScreenPos(o.pos);
			o.scrPos.y = 1 - o.scrPos.y;
			return o;
		}

		float4 frag (v2f i) : COLOR
		{
			//return tex2D(_MainTex, i.scrPos);

			float depthValue = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r);
			float cameraVertDist = saturate((_ProjectionParams.z - _ProjectionParams.y) * depthValue + _ProjectionParams.y);
			float f = cameraVertDist * unity_FogDensity;
			float fogFactor = saturate(1 / pow(2.71828, f * f));
			half4 color = tex2Dproj(_MainTex, i.scrPos);
			color.rgb = lerp(unity_FogColor.rgb, color.rgb, fogFactor);
			return color;
		}
		ENDCG
	}
	}
	Fallback off
}
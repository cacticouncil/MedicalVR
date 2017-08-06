Shader "Custom/FogCol" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		Fog {Mode  Off}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:fogVertex finalcolor:fogColor

		uniform float4 _Color;

		//uniform half4 unity_FogColor;
		uniform half4 unity_FogStart;
		uniform half4 unity_FogEnd;
		uniform half4 unity_FogDensity;

		struct Input 
		{
			half fogFactor;
		};

		void fogVertex(inout appdata_full v, out Input data) 
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			float cameraVertDist = length(mul(UNITY_MATRIX_MV, v.vertex).xyz);
			float f = cameraVertDist * unity_FogDensity;
			data.fogFactor = saturate(1 / pow(2.71828, f * f));			
		}

		void fogColor(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			color.rgb = lerp(unity_FogColor.rgb, color.rgb, IN.fogFactor);
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
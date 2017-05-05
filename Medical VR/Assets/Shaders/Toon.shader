// Upgrade NOTE: replaced 'UNITY_INSTANCE_ID' with 'UNITY_VERTEX_INPUT_INSTANCE_ID'

Shader "Test/Toon"
{
	Properties
	{
		//Make properties
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_RampTex ("Ramp", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		//Include functions
		CGPROGRAM
		#pragma surface surf Toon
        #pragma multi_compile_instancing
		// color of light source (from "Lighting.cginc")
		//uniform float4 _LightColor0;
		//Import user-specified properties
        UNITY_INSTANCING_CBUFFER_START (MyProperties)
        //List what to instance
        UNITY_DEFINE_INSTANCED_PROP (float4, _Color)
        UNITY_INSTANCING_CBUFFER_END
		sampler2D _MainTex;
		sampler2D _RampTex;

		struct Input
        {
            float2 uv_MainTex;
            INTERNAL_DATA
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

		//Make functions
		fixed4 LightingToon (SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
            UNITY_SETUP_INSTANCE_ID (s);
		  	half NdotL = dot(s.Normal, lightDir); 
		  	NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));
		  	
		  	fixed4 c;
		  	c.rgb = s.Albedo * UNITY_ACCESS_INSTANCED_PROP (_Color).rgb * _LightColor0.rgb * NdotL * atten;
		  	c.a = s.Alpha;
		  	
		  	return c;
		}

		void surf (Input IN, inout SurfaceOutput o)
        {
            UNITY_SETUP_INSTANCE_ID (IN);
            UNITY_TRANSFER_INSTANCE_ID(IN, o);
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
		ENDCG
	}
    Fallback "Bumped Specular"
}

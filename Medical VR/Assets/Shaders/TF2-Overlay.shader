Shader "Custom/TF2-Overlay"{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_RimColor("Rim Color", Color) = (0.97,0.88,1,0.75)
		_RimPower("Rim Power", Range(0.5,8.0)) = 3.0
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline Width", Range(.002, 0.03)) = .005
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_SpecularTex("Specular Map", 2D) = "gray" {}
		_RampTex("Shading Ramp", 2D) = "white" {}
	}

		SubShader{
			Tags { "RenderType" = "Overlay" "Queue" = "Overlay" }
			ZTest Always

			CGINCLUDE
	#include "UnityCG.cginc"

			struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};

		struct v2f {
			float4 pos : POSITION;
			float4 color : COLOR;
		};

		uniform float _Outline;
		uniform float4 _OutlineColor;

		v2f vert(appdata v) {
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

			float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
			norm.x *= UNITY_MATRIX_P[0][0];
			norm.y *= UNITY_MATRIX_P[1][1];

			o.pos.xy += norm.xy * _Outline;
			o.color = _OutlineColor;
			return o;
		}
		ENDCG

		Pass{
		Name "OUTLINE"
		Tags{ "LightMode" = "Always" }
		Cull Front
		ZWrite On
		ColorMask RGB
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		half4 frag(v2f i) :COLOR{ return i.color; }
		ENDCG
		}

		CGPROGRAM
		#pragma surface surf TF2
		#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
			float3 worldNormal;
			INTERNAL_DATA
		};

		sampler2D _MainTex, _SpecularTex, _BumpMap, _RampTex;
		float4 _RimColor;
		float  _RimPower;
		fixed4 _Color;

		inline fixed4 LightingTF2(SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
		{
			fixed3 h = normalize(lightDir + viewDir);

			fixed NdotL = dot(s.Normal, lightDir) * 0.5 + 0.5;
			fixed3 ramp = tex2D(_RampTex, float2(NdotL * atten, 0)).rgb;

			float nh = max(0, dot(s.Normal, h));
			float spec = pow(nh, s.Gloss * 128) * s.Specular * saturate(NdotL);

			fixed4 c;
			c.rgb = ((s.Albedo * _Color.rgb * ramp * _LightColor0.rgb + _LightColor0.rgb * spec) * (atten * 2));
			return c;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
			float3 specGloss = tex2D(_SpecularTex, IN.uv_MainTex).rgb;
			o.Specular = specGloss.r;
			o.Gloss = specGloss.g;
			_Color.rgb *= 2;

			half3 rim = pow(max(0, dot(float3(0, 1, 0), WorldNormalVector(IN, o.Normal))), _RimPower) * _RimColor.rgb * _RimColor.a * specGloss.b;
			o.Emission = rim;
		}
		ENDCG
	}
	Fallback "Bumped Specular"
}
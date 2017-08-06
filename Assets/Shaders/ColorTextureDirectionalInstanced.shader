// Upgrade NOTE: replaced 'UNITY_INSTANCE_ID' with 'UNITY_VERTEX_INPUT_INSTANCE_ID'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorTextureDirectionalInstanced"
{
	Properties
	{
		//Make properties
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Float) = 10
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
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
			sampler2D _MainTex;
			uniform float4 _SpecColor;
			uniform float _Shininess;

			//Make structs
			struct appdata
			{
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 diffuseColor : TEXCOORD1;
				float3 specularColor : TEXCOORD2;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
                UNITY_SETUP_INSTANCE_ID (v);

				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;
				
				float3 normalDirection = normalize(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(modelMatrix, v.pos).xyz);
				float3 lightDirection;

                if (0.0 == _WorldSpaceLightPos0.w) // directional light?
				{
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}

				float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * UNITY_ACCESS_INSTANCED_PROP (_Color).rgb;
				float3 diffuseReflection = _LightColor0.rgb * UNITY_ACCESS_INSTANCED_PROP (_Color).rgb * max(0.0, dot(normalDirection, lightDirection));
				float3 specularReflection;

				// light source on the wrong side?
				if (dot(normalDirection, lightDirection) < 0.0)
				{
					// no specular reflection
					specularReflection = float3(0.0, 0.0, 0.0);
				}
				// light source on the right side
				else 
				{
					specularReflection = _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
				}


				o.diffuseColor = ambientLighting + diffuseReflection;
				o.specularColor = specularReflection;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = v.uv;
				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{
				return float4(i.specularColor +	i.diffuseColor * tex2D(_MainTex, i.uv.xy), 1.0);
			}
			ENDCG
		}
	}
    Fallback "Specular"
}

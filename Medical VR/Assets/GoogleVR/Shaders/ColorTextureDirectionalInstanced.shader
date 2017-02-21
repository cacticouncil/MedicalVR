Shader "Custom/ColorTextureDirectionalInstanced"
{
	Properties
	{
		//Make properties
		_Color ("Color", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 80
		Pass
		{
			//This will be the base forward rendering pass in which ambient, vertex, and
            //main directional light will be applied. Additional lights will need additional passes
            //using the "ForwardAdd" lightmode.
            //see: http://docs.unity3d.com/Manual/SL-PassTags.html
			Name "FORWARD"
            Tags { "LightMode" = "ForwardBase" }

			//Include functions
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
            #pragma multi_compile_instancing
			#include "HLSLSupport.cginc"
            #include "UnityCG.cginc"

            //This matches the "forward base" of the LightMode tag to ensure the shader compiles
            //properly for the forward bass pass. As with the LightMode tag, for any additional lights
            //this would be changed from _fwdbase to _fwdadd.
            #pragma multi_compile_fwdbase
			#include "Lighting.cginc"
            //Reference the Unity library that includes all the lighting shadow macros
            #include "AutoLight.cginc"

            inline float3 LightingLambertVS (float3 normal, float3 lightDir)
			{
			fixed diff = max (0, dot (normal, lightDir));			
			return _LightColor0.rgb * diff;
			}

			//Import properties
            UNITY_INSTANCING_CBUFFER_START (MyProperties)
            //List what to instance
            UNITY_DEFINE_INSTANCED_PROP (float4, _Color)
            UNITY_INSTANCING_CBUFFER_END
			sampler2D _MainTex;

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
                //The LIGHTING_COORDS macro (defined in AutoLight.cginc) defines the parameters needed to sample 
                //the shadow map. The (x,y) specifies which unused TEXCOORD semantics to hold the sampled values - 
                //If you're not using any texcoords in this shader, you can use TEXCOORD0 and TEXCOORD1 for the shadow 
                //sampling. However I am using TEXCOORD0 for UV coordinates, so I specify
                //LIGHTING_COORDS(1,2) instead to use TEXCOORD1 and TEXCOORD2.
                LIGHTING_COORDS(1,2)
			};
			
			v2f vert (appdata v)
			{
				v2f o;
                UNITY_SETUP_INSTANCE_ID (v);
                UNITY_TRANSFER_INSTANCE_ID (v, o);

				o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.uv = v.uv;

				//The TRANSFER_VERTEX_TO_FRAGMENT macro populates the chosen LIGHTING_COORDS in the v2f structure
                //with appropriate values to sample from the shadow/lighting map
                TRANSFER_VERTEX_TO_FRAGMENT(o);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                UNITY_SETUP_INSTANCE_ID (i);

				// sample the texture
				fixed4 texColor = tex2D(_MainTex, i.uv);

				//The LIGHT_ATTENUATION samples the shadowmap (using the coordinates calculated by TRANSFER_VERTEX_TO_FRAGMENT
                //and stored in the structure defined by LIGHTING_COORDS), and returns the value as a float.
                float attenuation = LIGHT_ATTENUATION(i);
				return texColor * UNITY_ACCESS_INSTANCED_PROP (_Color) * attenuation;
			}
			ENDCG
		}
	}
	//To receive or cast a shadow, shaders must implement the appropriate "Shadow Collector" or "Shadow Caster" pass.
    //Although we haven't explicitly done so in this shader, if these passes are missing they will be read from a fallback
    //shader instead, so specify one here to import the collector/caster passes used in that fallback.
    Fallback "Diffuse"
}

Shader "Custom/deform" {
	//show values to edit in inspector
	Properties {
		_Color ("Tint", Color) = (0, 0, 0, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_MetalTex ("Texture", 2D) = "white" {}
		_NormalTex ("Texture", 2D) = "white" {}
		_Metallic ("Metal Factor", Range(0.000000,1.000000)) = 0.500000
		_GlossMapScale ("Smoothness Scale", Range(0.000000,1.000000)) = 1.000000
		[Enum(Metallic Alpha,0,Albedo Alpha,1)]  _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0.000000
		[HDR] _Emission ("Emission", color) = (0,0,0)

		_Amplitude ("Wave Size", Range(0,1)) = 0.4
		_Frequency ("Wave Freqency", Range(1, 8)) = 2
		_AnimationSpeed ("Animation Speed", Range(0,5)) = 1
	}
	SubShader {
		//the material is completely non-transparent and is rendered at the same time as the other opaque geometry
		Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

		CGPROGRAM

		//the shader is a surface shader, meaning that it will be extended by unity in the background 
		//to have fancy lighting and other features
		//our surface shader function is called surf and we use our custom lighting model
		//fullforwardshadows makes sure unity adds the shadow passes the shader might need
		//vertex:vert makes the shader use vert as a vertex shader function
		//addshadows tells the surface shader to generate a new shadow pass based on out vertex shader
		#pragma surface surf Standard fullforwardshadows vertex:vert addshadow
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MetalTex;
		sampler2D _NormalTex;
		fixed4 _Color;

		half _GlossMapScale;
		half3 _Emission;
		half _Metallic;

		float _Amplitude;
		float _Frequency;
		float _AnimationSpeed;

		//input struct which is automatically filled by unity
		struct Input {
			float2 uv_MainTex;
			float2 uv_MetalTex;
			float2 uv_NormalTex;
		};

		void vert(inout appdata_full data){
			float4 modifiedPos = data.vertex;
			modifiedPos.y += sin(data.vertex.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
			
			float3 posPlusTangent = data.vertex + data.tangent * 0.01;
			posPlusTangent.y += sin(posPlusTangent.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;

			float3 bitangent = cross(data.normal, data.tangent);
			float3 posPlusBitangent = data.vertex + bitangent * 0.01;
			posPlusBitangent.y += sin(posPlusBitangent.x * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;

			float3 modifiedTangent = posPlusTangent - modifiedPos;
			float3 modifiedBitangent = posPlusBitangent - modifiedPos;

			float3 modifiedNormal = cross(modifiedTangent, modifiedBitangent);
			data.normal = normalize(modifiedNormal);
			data.vertex = modifiedPos;
		}

		//the surface shader function which sets parameters the lighting function then uses
		void surf (Input i, inout SurfaceOutputStandard o) {
			//sample and tint albedo texture
			fixed4 col = tex2D(_MainTex, i.uv_MainTex);
			fixed4 metal_col = tex2D(_MetalTex, i.uv_MetalTex);
			col *= _Color;
			o.Albedo = col.rgb;
			//just apply the values for metalness, smoothness and emission
			o.Metallic = metal_col.r * _Metallic;
			o.Smoothness = metal_col.a * _GlossMapScale;
			o.Emission = _Emission;
			o.Normal = UnpackNormal(tex2D(_NormalTex, i.uv_NormalTex));
		}
		ENDCG
	}
	FallBack "Standard"
}
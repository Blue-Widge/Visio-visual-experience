        Shader "Unlit/HologramShader"
        {
            Properties
            {
                [HDR]_FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
                [HDR]_MainColor ("Main Color", Color) = (0, 0.33, 0.75, 1)
                _ScrollSpeed ("ScrollSpeed", Range(0,1)) = 0.05
                _LinesNumber( "Lines Number", Range(0,128)) = 32
            }
            SubShader
            {
                Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        
                Blend SrcAlpha OneMinusSrcAlpha
                Cull Back 
                ZTest LEqual
                ZWrite On
        

                Pass
                {
                    CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag

                    #include "UnityCG.cginc"
                    #define TAU 6.283185307179586
            
                    struct appdata
                    {
                        float4 vertex : POSITION;
                        float2 uv : TEXCOORD0;
                        float3 normal : NORMAL;
                    };

                    struct v2f
                    {
                        float2 uv : TEXCOORD0;
                        float4 vertex : SV_POSITION;
                        float3 wPos : TEXCOORD1;
                        float3 normal : NORMAL;
                    };
                    float4 _FresnelColor;
                    float4 _MainColor;
                    float _ScrollSpeed;
                    float _LinesNumber;
            
            
                    v2f vert (appdata v)
                    {
                        v2f o;
                        o.vertex = UnityObjectToClipPos(v.vertex);
                        o.uv = v.uv;
                        o.wPos = mul(unity_ObjectToWorld, v.vertex);
                        o.normal = UnityObjectToWorldNormal(v.normal);
                        return o;
                    }

                    fixed4 frag (v2f i) : SV_Target
                    {
                        float3 viewVector = normalize(_WorldSpaceCameraPos - i.wPos);
                        float3 normalVector = normalize(i.normal);
                        float4 fresnel = _FresnelColor * (1 - dot(viewVector, normalVector)) * 0.9;
                        float variable = length((i.wPos.y + (_Time.y * _ScrollSpeed)) * 2 - 1);
                        float4 wave = cos(TAU * _LinesNumber * variable) * (cos(TAU * _LinesNumber * variable * 0.2)) * 0.5 + 0.5;
                        bool topMask = (i.normal.y > 0.999);
                        variable = (length(i.uv * 2 - 1) - (_Time.y * _ScrollSpeed));
                        float topWave = cos(TAU * _LinesNumber * variable) * cos(TAU * _LinesNumber * variable * 0.2) * 0.5 + 0.5;
                        return saturate(_MainColor * (wave * !topMask + topWave * topMask) + fresnel);
                    }
                    ENDCG
                }
            }
        }

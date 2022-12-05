Shader "Visio/ActionPreview"
{
    Properties  //properties of my shader, it contains generally : a texture, a mesh, a color, values, etc
    {
        _MainTexture("Texture", 2D) = "white" {}
        _Color("Color", Color) = (0, 1, 1, 1)
    }
    SubShader   //the actual shader, a shader file can contain multiple subshader to apply a different shader depending of the needs
    {
        Pass    // a subshader can also contains multiple pass, if a light needs multiple effects on it for example 
        {
            CGPROGRAM
            #pragma vertex vertex_function       //depending on mesh
            #pragma fragment fragmentFunction   // depending on screen pix

            #include "UnityCG.cginc"
            //Data of my mesh
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            //vertex 2 fragment 
            struct v2f
            {
                float4 position : SV_POSITION;
                float2 uv : TEXTCOORD0;
            };
            //variables from Properties
            fixed4 _Color;
            sampler2D _MainTexture;
            
            //Convert data from vertex 2 pixel 
            v2f vertex_function(appdata IN)
            {
                v2f OUT;
                OUT.position = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                return OUT;
            }
            //apply things to my pixels
            fixed4 fragmentFunction(v2f IN) : SV_Target
            {
                const fixed4 pixel_color = tex2D(_MainTexture, IN.uv);
                return pixel_color * _Color;
            }
            ENDCG
        }
    }
}
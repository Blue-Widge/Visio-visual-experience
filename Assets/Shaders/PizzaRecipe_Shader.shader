Shader "Unlit/PizzaRecipe_Shader"
{
    Properties
    {
        _PizzaRecipeTex ("Pizza Recipe Texture", 2D) = "white" {}
        _PizzaRecipeRedBars ("Pizza Recipe Red Bars", 2D) = "white" {}
        _PizzaIngredientsDone ("Pizza Ingredients Done", int) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile_instancing
            #pragma multi_compile_stereo
            #include "UnityCG.cginc"

            #define UPPER_LIST_Y_COORD 0.72
            #define RED_BARS_DISTANCE 0.0967
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _PizzaRecipeTex;
            sampler2D _PizzaRecipeRedBars;
            int _PizzaIngredientsDone;
            
            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_PizzaRecipeTex, i.uv);
                //tex2dlod because otherwise redbars disappear from afar
                fixed4 redBars = tex2Dlod(_PizzaRecipeRedBars, fixed4(i.uv, 0, 0));

                float mask = (i.uv.y > (UPPER_LIST_Y_COORD - _PizzaIngredientsDone * RED_BARS_DISTANCE)) && redBars == fixed4(1,0,0,1);
                return mask ? redBars : col;
            }
            ENDCG
        }
    }
}

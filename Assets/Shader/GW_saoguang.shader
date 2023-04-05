// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// 注意：手动更改此数据可能会妨碍您在 Shader Forge 中打开它
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34798,y:32252,varname:node_9361,prsc:2|custl-6543-OUT,clip-6567-A;n:type:ShaderForge.SFN_Tex2d,id:8939,x:33817,y:32112,ptovrint:False,ptlb:node_8939,ptin:_node_8939,varname:node_8939,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ac6ad7729d1fdbb49a1646c21e046497,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2436,x:34298,y:32156,varname:node_2436,prsc:2|A-8939-RGB,B-6823-OUT;n:type:ShaderForge.SFN_Color,id:6567,x:34508,y:32527,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_6567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6543,x:34576,y:32347,varname:node_6543,prsc:2|A-5240-OUT,B-6567-RGB;n:type:ShaderForge.SFN_ValueProperty,id:6823,x:33821,y:32296,ptovrint:False,ptlb:node_6823,ptin:_node_6823,varname:node_6823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:7050,x:33933,y:32387,ptovrint:False,ptlb:node_7050,ptin:_node_7050,varname:node_7050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3e73a7f761422f04c98833db89ada343,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6112,x:33932,y:32579,ptovrint:False,ptlb:node_6112,ptin:_node_6112,varname:node_6112,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:df999be636f0b4a4b835a6db15788f04,ntxv:0,isnm:False|UVIN-683-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1052,x:34193,y:32403,varname:node_1052,prsc:2|A-7050-A,B-6112-RGB;n:type:ShaderForge.SFN_Add,id:5240,x:34347,y:32317,varname:node_5240,prsc:2|A-2436-OUT,B-1052-OUT;n:type:ShaderForge.SFN_TexCoord,id:4529,x:33502,y:32610,varname:node_4529,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:683,x:33731,y:32573,varname:node_683,prsc:2,spu:0.1,spv:0.2|UVIN-4529-UVOUT;proporder:8939-6567-6823-7050-6112;pass:END;sub:END;*/

Shader "Shader Forge/GW_saoguang" {
    Properties {
        _node_8939 ("node_8939", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _node_6823 ("node_6823", Float ) = 1
        _node_7050 ("node_7050", 2D) = "white" {}
        _node_6112 ("node_6112", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _node_8939; uniform float4 _node_8939_ST;
            uniform float4 _Color;
            uniform float _node_6823;
            uniform sampler2D _node_7050; uniform float4 _node_7050_ST;
            uniform sampler2D _node_6112; uniform float4 _node_6112_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                clip(_Color.a - 0.5);
////// Lighting:
                float4 _node_8939_var = tex2D(_node_8939,TRANSFORM_TEX(i.uv0, _node_8939));
                float4 _node_7050_var = tex2D(_node_7050,TRANSFORM_TEX(i.uv0, _node_7050));
                float4 node_1539 = _Time;
                float2 node_683 = (i.uv0+node_1539.g*float2(0.1,0.2));
                float4 _node_6112_var = tex2D(_node_6112,TRANSFORM_TEX(node_683, _node_6112));
                float3 finalColor = (((_node_8939_var.rgb*_node_6823)+(_node_7050_var.a*_node_6112_var.rgb))*_Color.rgb);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                clip(_Color.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

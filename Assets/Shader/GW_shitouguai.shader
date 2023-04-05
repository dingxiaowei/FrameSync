// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// 注意：手动更改此数据可能会妨碍您在 Shader Forge 中打开它
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34925,y:32293,varname:node_9361,prsc:2|custl-6543-OUT,clip-6567-A;n:type:ShaderForge.SFN_Tex2d,id:8939,x:34088,y:31939,ptovrint:False,ptlb:node_8939,ptin:_node_8939,varname:node_8939,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:26efbd0102f33cd4e891034ebd0f8f8a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2436,x:34421,y:32084,varname:node_2436,prsc:2|A-8939-RGB,B-6823-OUT;n:type:ShaderForge.SFN_Color,id:6567,x:34634,y:32536,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_6567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6543,x:34743,y:32378,varname:node_6543,prsc:2|A-5240-OUT,B-6567-RGB;n:type:ShaderForge.SFN_ValueProperty,id:6823,x:34128,y:32123,ptovrint:False,ptlb:node_6823,ptin:_node_6823,varname:node_6823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:7050,x:33733,y:32339,ptovrint:False,ptlb:node_7050,ptin:_node_7050,varname:node_7050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:93c662e96f2394d479db03aa7922ae9b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6112,x:33985,y:32656,ptovrint:False,ptlb:node_6112,ptin:_node_6112,varname:node_6112,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f21404077c3c8c344bc7c56b71d082f1,ntxv:0,isnm:False|UVIN-683-UVOUT;n:type:ShaderForge.SFN_Add,id:5240,x:34594,y:32220,varname:node_5240,prsc:2|A-2436-OUT,B-9704-OUT,C-422-OUT;n:type:ShaderForge.SFN_TexCoord,id:4529,x:33362,y:32595,varname:node_4529,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:683,x:33650,y:32631,varname:node_683,prsc:2,spu:0.05,spv:0.05|UVIN-4529-UVOUT;n:type:ShaderForge.SFN_Multiply,id:422,x:34165,y:32266,varname:node_422,prsc:2|A-6209-OUT,B-7050-RGB,C-7050-A,D-875-OUT;n:type:ShaderForge.SFN_Time,id:8657,x:33198,y:31851,varname:node_8657,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6676,x:33376,y:31829,varname:node_6676,prsc:2|A-1213-OUT,B-8657-TSL;n:type:ShaderForge.SFN_Sin,id:6287,x:33566,y:31901,varname:node_6287,prsc:2|IN-6676-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1213,x:33191,y:31728,ptovrint:False,ptlb:pinlv,ptin:_pinlv,varname:node_1213,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:100;n:type:ShaderForge.SFN_Multiply,id:9704,x:34260,y:32495,varname:node_9704,prsc:2|A-2382-OUT,B-6112-RGB;n:type:ShaderForge.SFN_Multiply,id:2382,x:34026,y:32448,varname:node_2382,prsc:2|A-7050-A,B-494-OUT;n:type:ShaderForge.SFN_ValueProperty,id:875,x:33843,y:32242,ptovrint:False,ptlb:liangdu,ptin:_liangdu,varname:node_875,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:494,x:33833,y:32535,varname:node_494,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp,id:6209,x:33846,y:31961,varname:node_6209,prsc:2|IN-6287-OUT,MIN-1152-OUT,MAX-2470-OUT;n:type:ShaderForge.SFN_Vector1,id:1152,x:33629,y:32031,varname:node_1152,prsc:2,v1:0.001;n:type:ShaderForge.SFN_Vector1,id:2470,x:33643,y:32183,varname:node_2470,prsc:2,v1:10;proporder:8939-6567-6823-7050-6112-1213-875;pass:END;sub:END;*/

Shader "Shader Forge/shitouren" {
    Properties {
        _node_8939 ("node_8939", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _node_6823 ("node_6823", Float ) = 1
        _node_7050 ("node_7050", 2D) = "white" {}
        _node_6112 ("node_6112", 2D) = "white" {}
        _pinlv ("pinlv", Float ) = 100
        _liangdu ("liangdu", Float ) = 1
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
            uniform float _pinlv;
            uniform float _liangdu;
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
                float4 node_351 = _Time;
                float2 node_683 = (i.uv0+node_351.g*float2(0.05,0.05));
                float4 _node_6112_var = tex2D(_node_6112,TRANSFORM_TEX(node_683, _node_6112));
                float4 node_8657 = _Time;
                float3 finalColor = (((_node_8939_var.rgb*_node_6823)+((_node_7050_var.a*2.0)*_node_6112_var.rgb)+(clamp(sin((_pinlv*node_8657.r)),0.001,10.0)*_node_7050_var.rgb*_node_7050_var.a*_liangdu))*_Color.rgb);
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

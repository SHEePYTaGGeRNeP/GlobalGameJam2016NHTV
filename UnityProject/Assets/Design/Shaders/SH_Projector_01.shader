// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1.029412,fgcg:1.019076,fgcb:0.9612889,fgca:1,fgde:0.006,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4318,x:34053,y:32791,varname:node_4318,prsc:2|diff-1542-OUT;n:type:ShaderForge.SFN_SceneDepth,id:8496,x:31727,y:32915,varname:node_8496,prsc:2;n:type:ShaderForge.SFN_Depth,id:1030,x:31727,y:33065,varname:node_1030,prsc:2;n:type:ShaderForge.SFN_Subtract,id:346,x:31911,y:32979,varname:node_346,prsc:2|A-8496-OUT,B-1030-OUT;n:type:ShaderForge.SFN_ViewVector,id:7301,x:31911,y:32842,varname:node_7301,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6450,x:32106,y:32908,varname:node_6450,prsc:2|A-7301-OUT,B-346-OUT;n:type:ShaderForge.SFN_Subtract,id:3205,x:32272,y:32849,varname:node_3205,prsc:2|A-1502-XYZ,B-6450-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:1502,x:32106,y:32761,varname:node_1502,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:2484,x:32272,y:32990,varname:node_2484,prsc:2;n:type:ShaderForge.SFN_Subtract,id:213,x:32463,y:32900,varname:node_213,prsc:2|A-3205-OUT,B-2484-XYZ;n:type:ShaderForge.SFN_Transform,id:8191,x:32646,y:32900,varname:node_8191,prsc:2,tffrom:0,tfto:1|IN-213-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9157,x:32829,y:32900,varname:node_9157,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8191-XYZ;n:type:ShaderForge.SFN_Add,id:388,x:33032,y:32842,varname:node_388,prsc:2|A-8790-OUT,B-9157-OUT;n:type:ShaderForge.SFN_Abs,id:2746,x:33045,y:32998,varname:node_2746,prsc:2|IN-9157-OUT;n:type:ShaderForge.SFN_Vector1,id:8790,x:32829,y:32842,varname:node_8790,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Step,id:6953,x:33252,y:33038,varname:node_6953,prsc:2|A-2746-OUT,B-2087-OUT;n:type:ShaderForge.SFN_Vector1,id:2087,x:33045,y:33141,varname:node_2087,prsc:2,v1:0.5;n:type:ShaderForge.SFN_ComponentMask,id:2318,x:33430,y:33038,varname:node_2318,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6953-OUT;n:type:ShaderForge.SFN_Multiply,id:259,x:33603,y:33038,varname:node_259,prsc:2|A-2318-R,B-2318-G;n:type:ShaderForge.SFN_Multiply,id:1542,x:33808,y:32936,varname:node_1542,prsc:2|A-9619-OUT,B-259-OUT;n:type:ShaderForge.SFN_Tex2d,id:1958,x:33276,y:32815,ptovrint:False,ptlb:node_1958,ptin:_node_1958,varname:node_1958,prsc:2,tex:6a985dfcd1d3ca94e9753748ed65c697,ntxv:0,isnm:False|UVIN-388-OUT;n:type:ShaderForge.SFN_Multiply,id:9619,x:33464,y:32783,varname:node_9619,prsc:2|A-7680-RGB,B-1958-RGB;n:type:ShaderForge.SFN_Color,id:7680,x:33263,y:32608,ptovrint:False,ptlb:node_7680,ptin:_node_7680,varname:node_7680,prsc:2,glob:False,c1:1,c2:0,c3:0,c4:1;proporder:1958-7680;pass:END;sub:END;*/

Shader "Shader Forge/SH_Projector_01" {
    Properties {
        _node_1958 ("node_1958", 2D) = "white" {}
        _node_7680 ("node_7680", Color) = (1,0,0,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _node_1958; uniform float4 _node_1958_ST;
            uniform float4 _node_7680;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float2 node_9157 = mul( _World2Object, float4(((i.posWorld.rgb-(viewDirection*(sceneZ-partZ)))-objPos.rgb),0) ).xyz.rgb.rg;
                float2 node_388 = (0.5+node_9157);
                float4 _node_1958_var = tex2D(_node_1958,TRANSFORM_TEX(node_388, _node_1958));
                float2 node_2318 = step(abs(node_9157),0.5).rg;
                float3 diffuseColor = ((_node_7680.rgb*_node_1958_var.rgb)*(node_2318.r*node_2318.g));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _node_1958; uniform float4 _node_1958_ST;
            uniform float4 _node_7680;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float2 node_9157 = mul( _World2Object, float4(((i.posWorld.rgb-(viewDirection*(sceneZ-partZ)))-objPos.rgb),0) ).xyz.rgb.rg;
                float2 node_388 = (0.5+node_9157);
                float4 _node_1958_var = tex2D(_node_1958,TRANSFORM_TEX(node_388, _node_1958));
                float2 node_2318 = step(abs(node_9157),0.5).rg;
                float3 diffuseColor = ((_node_7680.rgb*_node_1958_var.rgb)*(node_2318.r*node_2318.g));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

float map(vec3 p)
{
    
    return length(p)-0.4;
    
}

vec3 getNormal(vec3 p)
{
    return normalize(vec3(
        map(p+vec3(0.001,0.0,0.0))-map(p-vec3(0.001,0.0,0.0)),
        map(p+vec3(0.0,0.001,0.0))-map(p-vec3(0.001,0.0,0.0)),
        map(p+vec3(0.0,0.0,0.001))-map(p-vec3(0.001,0.0,0.0))
    ));
}
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = fragCoord/iResolution.xy;
	float angle=iTime;
    vec2 screenPosition=(2.0*fragCoord.xy-iResolution.xy)/iResolution.y;
    vec3 cameraOrigin=vec3(sin(angle),0.0,cos(angle));
    vec3 cameraTarget=vec3(0.0,0.0,0.0);
    vec3 cameraForward=normalize(cameraTarget-cameraOrigin);
    vec3 cameraRight=normalize(cross(cameraForward,vec3(0.0,1.0,0.0)));
    vec3 cameraUp=normalize(cross(cameraRight,cameraForward));
    
    vec3 rayOrigin=cameraOrigin;
    vec3 rayDistance=normalize(vec3(screenPosition.x*cameraRight+screenPosition.y*cameraUp+1.0*cameraForward));
    vec3 col =vec3(0,0,0);
           
      
    float t=0.0;
    for(int stepMarch=0;stepMarch<100;stepMarch++)
    {
        
        vec3 point=rayOrigin+t*rayDistance;
        float h =map(point);
        
        if(h<0.001)
        {
            //col=vec3(point.x);
            col=getNormal(point);
            
            break;
        }
        
        t+=h;
        
    }
        
        
        
    // Time varying pixel color

    // Output to screen
    fragColor = vec4(col,1.0);
}

void DrawShaderToySphere_float (float2 screenPosition, out float3 fragColor)
{ 

    ///??????
 
} 
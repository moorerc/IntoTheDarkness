float4x4 World;
float4x4 View;
float4x4 Projection;
float4 AmbientColor;
float4 points[8];
float times[8];
float radiuses[8];
float time_passed;

struct VertexShaderInput
{
    float4 Position : POSITION0;
};

struct VertexShaderOutput
{
	float4 tPosition : TEXCOORD0;
	float4 Position : POSITION0;
	float4 time : TEXCOORD1;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.tPosition = mul(input.Position, World);
   
	float4 diff = float4(10000.0f, 10000.0f, 10000.0f, 10000.0f);
	float time = float(10000.0f);
	//float max = 200.0f;
	float shade = float(0.0f);
	float t_shade = float(0.0f);
	// find max intensity based on points
	for (int i = 0; i < 8; i = i + 1)
	{
		float4 temp = points[i] - worldPosition;
		float time = times[i]/4.0f;
		if (length(temp) > radiuses[i])
			shade = 0.0f;
		else if (((length(temp) > time - 20) && (length(temp) < time + 20)) ||
			((length(temp)+75 > time - 10) && (length(temp)+75 < time + 10)) ||
			((length(temp)+150 > time - 5) && (length(temp)+150 < time + 5)))
			shade = 1.0f - length(temp)/radiuses[i];
		else
			shade = 0.0f;

		if (shade > t_shade)
			t_shade = shade;
	
	}
	time.x = t_shade;
	output.tPosition = diff;
	output.time = time;
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	return AmbientColor * input.time.x;	
}


technique Ambient
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
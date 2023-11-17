shader_type canvas_item;
uniform vec4 color : source_color;


void fragment() {
	vec4 pixelColor = texture(TEXTURE, UV);
	COLOR = pixelColor;
	//float colorStrobe = clamp(sin(color.g * TIME*2.0)/color.r,0,1);
	float colorStrobe = clamp(sin(TIME * 4.0 + VERTEX.x/12.0)/color.r, 0,1);
	//COLOR.r += colorStrobe/4.0;
	COLOR.g += colorStrobe/2.0;
	COLOR.b += colorStrobe;
	
}

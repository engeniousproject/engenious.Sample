#version 330

uniform mat4 ViewProj;
uniform mat4 World;

in vec3 position;
in vec3 normal;
in vec2 texCoord;

out vec3 Normal;
out vec2 TexCoords;
out vec3 WorldPos;

void main()
{
	Normal = normal;
	TexCoords = texCoord;
	WorldPos = vec3(World * vec4(position, 1.0));

	gl_Position = ViewProj * vec4(WorldPos, 1.0);
}
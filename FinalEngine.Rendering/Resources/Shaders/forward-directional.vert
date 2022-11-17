#version 450

layout (location = 0) in vec3 in_position;
layout (location = 1) in vec4 in_color;
layout (location = 2) in vec2 in_texCoord;
layout (location = 3) in vec3 in_normal;
layout (location = 4) in vec3 in_tangent;

layout (location = 0) out vec4 out_color;
layout (location = 1) out vec2 out_texCoord;
layout (location = 2) out vec3 out_fragPos;
layout (location = 3) out mat3 out_tbn;

uniform mat4 u_projection;
uniform mat4 u_view;
uniform mat4 u_transform;

void main()
{
	out_color = in_color;
	out_texCoord = in_texCoord;
    out_fragPos = vec3(u_transform * vec4(in_position, 1.0));

    vec3 n = normalize((u_transform * vec4(in_normal, 0.0)).xyz);
    vec3 t = normalize((u_transform * vec4(in_tangent, 0.0)).xyz); 
    t = normalize(t - dot(t, n) * n);

    vec3 b = cross(t, n);

    out_tbn = mat3(t, b, n);

	gl_Position = u_projection * u_view * u_transform * vec4(in_position, 1.0);
}

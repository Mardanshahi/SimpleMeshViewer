//using OpenTK.Mathematics;

namespace Library;

public static class Objects
{
    /// <summary>
    /// Stores data for a standard VAO
    /// </summary>
    public class Mesh
    {
        public float[] Vertices;
        public float[] TexCoords;
        public float[] Normals;
        public int[] Indices;

        public int VertexBinding = 0;
        public int TexCoordBinding = 1;
        public int NormalBinding = 2;
        
        public Mesh(float[] vertices = null, int[] indices = null, float[] texCoords = null, float[] normals = null)
        {
            Vertices = vertices;
            Indices = indices;
            TexCoords = texCoords;
            Normals = normals;
        }

    }


    public class Material
    {
        public OpenTK.Vector3 Ambient;
        public OpenTK.Vector3 Diffuse;
        public OpenTK.Vector3 Specular;
        public float Shininess;


        public Material(){}
            
        public Material(
            float ambientR, float ambientG, float ambientB,
            float diffuseR, float diffuseG, float diffuseB,
            float specularR, float specularG, float specularB,
            float shininess
        )
        {
            SetAmbient(ambientR, ambientG, ambientB);
            SetDiffuse(diffuseR, diffuseG, diffuseB);
            SetSpecular(specularR, specularG, specularB);
            SetShininess(shininess);
        }

        public Material SetAmbient(OpenTK.Vector3 ambient) { Ambient = ambient; return this; }
        public Material SetAmbient(float r, float g, float b) { Ambient = new OpenTK.Vector3(r,g,b); return this; }
        public Material SetAmbient(float value) { Ambient = new OpenTK.Vector3(value,value,value); return this; }
        public Material SetDiffuse(OpenTK.Vector3 diffuse) { Diffuse = diffuse; return this; }
        public Material SetDiffuse(float r, float g, float b) { Diffuse = new OpenTK.Vector3(r,g,b); return this; }
        public Material SetDiffuse(float value) { Diffuse = new OpenTK.Vector3(value,value,value); return this; }
        public Material SetSpecular(OpenTK.Vector3 specular) { Specular = specular; return this; }
        public Material SetSpecular(float r, float g, float b) { Specular = new OpenTK.Vector3(r,g,b); return this; }
        public Material SetSpecular(float value) { Specular = new OpenTK.Vector3(value,value,value); return this; }
        public Material SetShininess(float shininess) { Shininess = shininess; return this; }
            
    }

    public class Light
    {
        public OpenTK.Vector3 Position;
        public OpenTK.Vector3 Ambient = OpenTK.Vector3.One;
        public OpenTK.Vector3 Diffuse = OpenTK.Vector3.One;
        public OpenTK.Vector3 Specular = OpenTK.Vector3.One;
        public OpenTK.Vector3 Attenuation = OpenTK.Vector3.UnitX;
        public OpenTK.Vector3 Direction = OpenTK.Vector3.UnitZ;
        private float cutOff = 1f;
        private float outerCutOff = 0f;
        
        public float GetCutOff() => cutOff;
        public float GetOuterCutOff() => outerCutOff;
        
        public Light PointMode(){cutOff = 1f; return this;}
        public Light SunMode(){cutOff = 0f; return this;}
        public Light SpotlightMode(float angle){cutOff = MathF.Cos(angle); outerCutOff = 0; return this;}
        public Light SpotlightMode(float angle, float outerAngle){cutOff = MathF.Cos(angle);outerCutOff = MathF.Cos(outerAngle); return this;}
        
        
        
        public Light SetPosition(OpenTK.Vector3 position) { Position = position; return this; }
        public Light SetPosition(float x, float y, float z) { Position = new OpenTK.Vector3(x,y,z); return this; }

        public Light UpdatePosition(ref ShaderProgram shaderProgram, string name)
        {
            shaderProgram.Uniform3(name + ".position", Position);
            return this;
        }

        public Light SetDirection(OpenTK.Vector3 direction) { Direction = direction; return this; }
        public Light SetDirection(float x, float y, float z) { Direction = new OpenTK.Vector3(x,y,z); return this; }

        public Light UpdateDirection(ref ShaderProgram shaderProgram, string name)
        {
            shaderProgram.Uniform3(name + ".direction", Direction);
            return this;
        }

        public Light SetAmbient(OpenTK.Vector3 ambient) { Ambient = ambient; return this; }
        public Light SetAmbient(float r, float g, float b) { Ambient = new OpenTK.Vector3(r,g,b); return this; }
        public Light SetDiffuse(OpenTK.Vector3 diffuse) { Diffuse = diffuse; return this; }
        public Light SetDiffuse(float r, float g, float b) { Diffuse = new OpenTK.Vector3(r,g,b); return this; }
        public Light SetSpecular(OpenTK.Vector3 specular) { Specular = specular; return this; }
        public Light SetSpecular(float r, float g, float b) { Specular = new OpenTK.Vector3(r,g,b); return this; }
        public Light SetAttenuation(OpenTK.Vector3 attenuation) { Attenuation = attenuation; return this; }
        public Light SetAttenuation(float constant, float linear, float quadratic) { Attenuation = new OpenTK.Vector3(constant,linear,quadratic); return this; }
    }

}
<Effect>
    <Technique name="Ambient">
        <Pass name="Pass1">
            <Shader type="PixelShader" filename="simple/simple.ps">

            </Shader>
            <Shader type="VertexShader" filename="simple/simple.vs">

            </Shader>
            <Attributes>
              <attribute name="position">Position</attribute>
              <attribute name="normal">Normal</attribute>
              <attribute name="texCoord">TextureCoordinate</attribute>
            </Attributes>
            <Materials>
                <Material name="Simple">
                    <binding name="Texture">Albedo</binding>
                    <binding name="metallic">Metallic</binding>
                    <binding name="roughness">Roughness</binding>
                    <binding name="ao">AmbientOcclusion</binding>
                </Material>
            </Materials>
        </Pass>
    </Technique>
    <Technique name="Test">
        <Pass name="MainPass">
            <Shader type="PixelShader" filename="simple/test.ps">

            </Shader>
            <Shader type="VertexShader" filename="simple/simple.vs">

            </Shader>
            <Attributes>
              <attribute name="position">Position</attribute>
              <attribute name="normal">Normal</attribute>
              <attribute name="texCoord">TextureCoordinate</attribute>
            </Attributes>
        </Pass>
    </Technique>
    <!--<Technique name="Compute">
        <Pass name="p1">
            <Shader type="ComputeShader" filename="simple/simple.cs" />
        </Pass>
    </Technique>-->
</Effect>

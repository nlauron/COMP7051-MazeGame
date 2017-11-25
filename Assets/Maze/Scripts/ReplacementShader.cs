using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacementShader : MonoBehaviour {
    public Shader shaderFile;

    void OnEnable() {
        if (shaderFile != null)
            GetComponent<Camera>().SetReplacementShader(shaderFile, "");
    }

    void OnDisable() {
        GetComponent<Camera>().ResetReplacementShader();
    }
}

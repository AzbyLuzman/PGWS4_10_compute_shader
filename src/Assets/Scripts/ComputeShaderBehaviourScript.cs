using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderBehaviourScript : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;

    private void Start()
    {
        int x = 8;
        int y = 8;
        ComputeBuffer buffer = new ComputeBuffer(x * y, sizeof(float) * 4);

        int kernel = computeShader.FindKernel("CSMain");
        computeShader.SetBuffer(kernel, "Result", buffer);
        computeShader.Dispatch(kernel, x / 8, y / 8, 1);

        float[] data = new float[4 * x * y];
        buffer.GetData(data);
        buffer.Release();

        for (int i = 0; i < x * y; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var c0 = data[4 * i + 0];
            var c1 = data[4 * i + 1] * 10f;
            var c2 = data[4 * i + 2] * 10f;
            Debug.Log(c0 + " " + c1 + " " + c2);
            cube.transform.Translate(c0, c1, c2);
        }
    }
}

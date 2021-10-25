using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class TowerColor : MonoBehaviour
{
    private Material material;
    public Color[] sources;
    public Color[] targets;

    public static string ToMatrixString<T>(T[,] matrix, string delimiter = "\t")
    {
        var s = new StringBuilder();

        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                s.Append(matrix[i, j]).Append(delimiter);
            }

            s.AppendLine();
        }

        return s.ToString();
    }

    public static string ToVectorString<T>(T[] matrix, string delimiter = "\t")
    {
        var s = new StringBuilder();

        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            s.Append(matrix[i]).Append(delimiter);
        }

        return s.ToString();
    }

    void reduceColumn(float[,] A, float[] B, int index, int size){
      float scale = A[index, index];
      //normalize row
      for(int i = 0; i < size; i++){
        A[index, i] = A[index, i] / scale;
      }
      B[index] = B[index] / scale;
      //use that row to reduce column, so it has only 0s and one 1 for that row
      for(int i = 0; i < size; i++){
        if(index != i){
          float ratio = A[i, index];
          for(int j = 0; j < size; j++){
            A[i, j] = A[i, j] - A[index, j] * ratio;
          }
          B[i] -= B[index] * ratio;
        }
      }
    }

    void swapRow(float[,] A, float[] B, int j, int i){
      int size = B.Length;
      float tmp;
      for(int x = 0; x < size; x++){
        tmp = A[j,x];
        A[j,x] = A[i, x];
        A[i, x] = tmp;
      }
      tmp = B[j];
      B[j] = B[i];
      B[i] = tmp;
    }

    //B=A*X
    void findX(float[,] A, float[] B){
        int size = A.GetLength(0);
        if(A.GetLength(1)!=size) return;
        if(B.Length!=size) return;
        for(int i=0; i<size; i++){
          if(A[i,i] == 0){
            bool found = false;
            for(int j=i+1; j<size; j++){
              if(A[j,i]!=0 && !found){
                swapRow(A, B, j, i);
              }
            }
          }
          reduceColumn(A, B, i, size);
        }
    }

    void Start() {
      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
      material = new Material(spriteRenderer.material);
      spriteRenderer.material = material;
      //findX(new float[2,2] {{0,1},{2,3}}, new float[2]{2,8});
    }

    void Update() {
      Matrix4x4 m = new Matrix4x4();
      for(int i = 0; i < 4; i++){
        float[,] A = new float[4,4];
        float[] B = new float[4];
        for(int j = 0; j < 4; j++){
          A[j, 0] = sources[j].r;
          A[j, 1] = sources[j].g;
          A[j, 2] = sources[j].b;
          A[j, 3] = Mathf.Sin(sources[j].r * sources[j].g * sources[j].b);

          B[j] = targets[j][i];
        }
        findX(A, B);
        m[i,0] = B[0];
        m[i,1] = B[1];
        m[i,2] = B[2];
        m[i,3] = B[3];
      }
      //m[3,3] = 1;
      /*Vector3 test = new Vector3(sources[0].r, sources[0].g, sources[0].b);
      Debug.Log(m.MultiplyPoint3x4(test));
      Debug.Log(targets[0]);*/
      material.SetMatrix("linearTransform", m);
    }
}

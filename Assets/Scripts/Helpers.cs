using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static class Helpers
    {
        private static Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector2 ToIso(this Vector2 input) => isoMatrix.MultiplyPoint3x4(input);
    }

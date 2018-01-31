using UnityEngine;

public class MathHandler {

	public static bool IsPointInRange(Vector3 point, Vector3 center, float radius)
    {
        return Vector3.SqrMagnitude(point - center) <= radius * radius;
    }

}

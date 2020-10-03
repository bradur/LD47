using System.Collections;
using UnityEngine;

public class Tools
{
    public static bool RandomPercent(int percent)
    {
        return percent >= Random.Range(1, 101);
    }

    public static bool MouseCast(out RaycastHit hit, LayerMask targetLayer, Ray ray)
    {
        if (Physics.Raycast(ray, out hit, 10000f, targetLayer))
        {
            return true;
        }
        return false;
    }

    public static bool MouseCast(out RaycastHit hit, LayerMask targetLayer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return MouseCast(out hit, targetLayer, ray);
    }


    public static IEnumerator WaitForRealTime(float delay)
    
    {
        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }
    }
}
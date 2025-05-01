using UnityEngine;

/// <summary>
/// can grab by grabber
/// </summary>
public interface IGrabbable
{
    public void OnGrab(Transform grabPos, float grabRot = 0f);
    public void StopGrab();
}

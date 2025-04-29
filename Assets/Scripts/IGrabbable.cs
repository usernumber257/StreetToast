using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    public void OnGrab(Transform grabPos);
    public void StopGrab();
}

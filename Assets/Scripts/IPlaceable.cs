using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    public GameObject OnPlace(Transform pivot);
}

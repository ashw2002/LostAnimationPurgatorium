using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppCrasher : MonoBehaviour
{
    public void OnClickCrash()
    {
        UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.PureVirtualFunction);
    }
    
}

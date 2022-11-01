using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public static class CGUtils {

    public static void DebugLog(string log)
	{
		if(GameManager.isDebugEnabled)
		{
			Debug.Log($"({Time.time}) {log}");
		}
	}

	public static void DebugLogError(string log)
	{
        if (GameManager.isDebugEnabled)
        {
            Debug.LogError($"({Time.time}) {log}");
		}
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000049 RID: 73
[Component]
public class MainThreadDispatcherComponent : MonoBehaviour
{
	// Token: 0x06000201 RID: 513 RVA: 0x0002DF3C File Offset: 0x0002C13C
	public void Update()
	{
		Queue<Action> methodQueue = MainThreadDispatcherComponent.MethodQueue;
		lock (methodQueue)
		{
			while (MainThreadDispatcherComponent.MethodQueue.Count > 0)
			{
				MainThreadDispatcherComponent.MethodQueue.Dequeue()();
			}
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0002DF94 File Offset: 0x0002C194
	public static void InvokeOnMain(Action a)
	{
		Queue<Action> methodQueue = MainThreadDispatcherComponent.MethodQueue;
		lock (methodQueue)
		{
			MainThreadDispatcherComponent.MethodQueue.Enqueue(a);
		}
	}

	// Token: 0x04000111 RID: 273
	public static readonly Queue<Action> MethodQueue = new Queue<Action>();
}

using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[Component]
public class MirrorCameraComponent : MonoBehaviour
{
	// Token: 0x06000250 RID: 592 RVA: 0x00025856 File Offset: 0x00023A56
	[OnSpy]
	public static void Disable()
	{
		MirrorCameraComponent.WasEnabled = MirrorCameraOptions.Enabled;
		MirrorCameraOptions.Enabled = false;
		UnityEngine.Object.Destroy(MirrorCameraComponent.cam_obj);
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00025872 File Offset: 0x00023A72
	[OffSpy]
	public static void Enable()
	{
		MirrorCameraOptions.Enabled = MirrorCameraComponent.WasEnabled;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0002587E File Offset: 0x00023A7E
	public void Update()
	{
		if (MirrorCameraComponent.cam_obj && MirrorCameraComponent.subCam)
		{
			if (MirrorCameraOptions.Enabled)
			{
				MirrorCameraComponent.subCam.enabled = true;
				return;
			}
			MirrorCameraComponent.subCam.enabled = false;
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0002EA44 File Offset: 0x0002CC44
	public void OnGUI()
	{
		if (MirrorCameraOptions.Enabled)
		{
			GUI.color = new Color(1f, 1f, 1f, 0f);
			MirrorCameraComponent.viewport = GUILayout.Window(99, MirrorCameraComponent.viewport, new GUI.WindowFunction(this.DoMenu), "Mirror Camera", new GUILayoutOption[0]);
			GUI.color = Color.white;
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0002EAA8 File Offset: 0x0002CCA8
	public void DoMenu(int windowID)
	{
		if (MirrorCameraComponent.cam_obj == null || MirrorCameraComponent.subCam == null)
		{
			MirrorCameraComponent.cam_obj = new GameObject();
			if (MirrorCameraComponent.subCam != null)
			{
				UnityEngine.Object.Destroy(MirrorCameraComponent.subCam);
			}
			MirrorCameraComponent.subCam = MirrorCameraComponent.cam_obj.AddComponent<Camera>();
			MirrorCameraComponent.subCam.CopyFrom(OptimizationVariables.MainCam);
			MirrorCameraComponent.cam_obj.transform.position = OptimizationVariables.MainCam.gameObject.transform.position;
			MirrorCameraComponent.cam_obj.transform.rotation = OptimizationVariables.MainCam.gameObject.transform.rotation;
			MirrorCameraComponent.cam_obj.transform.Rotate(0f, 180f, 0f);
			MirrorCameraComponent.subCam.transform.SetParent(OptimizationVariables.MainCam.transform, true);
			MirrorCameraComponent.subCam.enabled = true;
			MirrorCameraComponent.subCam.rect = new Rect(0.6f, 0.6f, 0.4f, 0.4f);
			MirrorCameraComponent.subCam.depth = 99f;
			UnityEngine.Object.DontDestroyOnLoad(MirrorCameraComponent.cam_obj);
		}
		float x = MirrorCameraComponent.viewport.x / (float)Screen.width;
		float num = (MirrorCameraComponent.viewport.y + 25f) / (float)Screen.height;
		float width = MirrorCameraComponent.viewport.width / (float)Screen.width;
		float num2 = MirrorCameraComponent.viewport.height / (float)Screen.height;
		num = 1f - num;
		num -= num2;
		MirrorCameraComponent.subCam.rect = new Rect(x, num, width, num2);
		Drawing.DrawRect(new Rect(0f, 0f, MirrorCameraComponent.viewport.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
		Drawing.DrawRect(new Rect(0f, 20f, MirrorCameraComponent.viewport.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
		GUILayout.Space(-19f);
		GUILayout.Label("Mirror Camera", new GUILayoutOption[0]);
		GUI.DragWindow();
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0002ECD8 File Offset: 0x0002CED8
	public static void FixCam()
	{
		if (MirrorCameraComponent.cam_obj != null && MirrorCameraComponent.subCam != null)
		{
			MirrorCameraComponent.cam_obj.transform.position = OptimizationVariables.MainCam.gameObject.transform.position;
			MirrorCameraComponent.cam_obj.transform.rotation = OptimizationVariables.MainCam.gameObject.transform.rotation;
			MirrorCameraComponent.cam_obj.transform.Rotate(0f, 180f, 0f);
			MirrorCameraComponent.subCam.transform.SetParent(OptimizationVariables.MainCam.transform, true);
			MirrorCameraComponent.subCam.depth = 99f;
			MirrorCameraComponent.subCam.enabled = true;
		}
	}

	// Token: 0x0400013E RID: 318
	public static Rect viewport = new Rect(1075f, 10f, (float)(Screen.width / 4), (float)(Screen.height / 4));

	// Token: 0x0400013F RID: 319
	public static GameObject cam_obj;

	// Token: 0x04000140 RID: 320
	public static Camera subCam;

	// Token: 0x04000141 RID: 321
	public static bool WasEnabled;
}

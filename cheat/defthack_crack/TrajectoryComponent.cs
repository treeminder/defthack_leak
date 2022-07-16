using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

// Token: 0x020000B0 RID: 176
[Component]
public class TrajectoryComponent : MonoBehaviour
{
	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600053F RID: 1343 RVA: 0x000264AD File Offset: 0x000246AD
	// (set) Token: 0x06000540 RID: 1344 RVA: 0x000264B4 File Offset: 0x000246B4
	public static Highlighter Highlighted { get; private set; }

	// Token: 0x06000541 RID: 1345 RVA: 0x00024F8C File Offset: 0x0002318C
	[Initializer]
	public static void Initialize()
	{
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00036294 File Offset: 0x00034494
	public void OnGUI()
	{
		Player mainPlayer = OptimizationVariables.MainPlayer;
		object obj;
		if (!(mainPlayer == null))
		{
			PlayerEquipment equipment = mainPlayer.equipment;
			obj = ((equipment != null) ? equipment.useable : null);
		}
		else
		{
			obj = null;
		}
		UseableGun useableGun = obj as UseableGun;
		if (useableGun == null || TrajectoryComponent.spying || !WeaponOptions.EnableBulletDropPrediction || !Provider.modeConfigData.Gameplay.Ballistics)
		{
			if (TrajectoryComponent.Highlighted != null)
			{
				TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
				TrajectoryComponent.Highlighted = null;
				return;
			}
		}
		else
		{
			RaycastHit raycastHit;
			List<Vector3> list = TrajectoryComponent.PlotTrajectory(useableGun, out raycastHit, 255);
			bool flag = Vector3.Distance(list.Last<Vector3>(), OptimizationVariables.MainPlayer.look.aim.position) > useableGun.equippedGunAsset.range;
			ColorVariable color = ColorUtilities.getColor("_TrajectoryPredictionInRange");
			ColorVariable color2 = ColorUtilities.getColor("_TrajectoryPredictionOutOfRange");
			if (WeaponOptions.HighlightBulletDropPredictionTarget && raycastHit.collider != null)
			{
				Transform transform = raycastHit.transform;
				GameObject gameObject = null;
				if (DamageTool.getPlayer(transform) != null)
				{
					gameObject = DamageTool.getPlayer(transform).gameObject;
				}
				else if (DamageTool.getZombie(transform) != null)
				{
					gameObject = DamageTool.getZombie(transform).gameObject;
				}
				else if (DamageTool.getAnimal(transform) != null)
				{
					gameObject = DamageTool.getAnimal(transform).gameObject;
				}
				else if (DamageTool.getVehicle(transform) != null)
				{
					gameObject = DamageTool.getVehicle(transform).gameObject;
				}
				if (gameObject != null)
				{
					Highlighter highlighter = gameObject.GetComponent<Highlighter>() ?? gameObject.AddComponent<Highlighter>();
					if (!highlighter.enabled)
					{
						highlighter.occluder = true;
						highlighter.overlay = true;
						highlighter.ConstantOnImmediate(flag ? color2 : color);
					}
					if (TrajectoryComponent.Highlighted != null && highlighter != TrajectoryComponent.Highlighted)
					{
						TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
					}
					TrajectoryComponent.Highlighted = highlighter;
				}
				else if (TrajectoryComponent.Highlighted != null)
				{
					TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
					TrajectoryComponent.Highlighted = null;
				}
			}
			else if (!WeaponOptions.HighlightBulletDropPredictionTarget && TrajectoryComponent.Highlighted != null)
			{
				TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
				TrajectoryComponent.Highlighted = null;
			}
			ESPComponent.GLMat.SetPass(0);
			GL.PushMatrix();
			GL.LoadProjectionMatrix(OptimizationVariables.MainCam.projectionMatrix);
			GL.modelview = OptimizationVariables.MainCam.worldToCameraMatrix;
			GL.Begin(2);
			GL.Color(flag ? color2 : color);
			foreach (Vector3 v in list)
			{
				GL.Vertex(v);
			}
			GL.End();
			GL.PopMatrix();
		}
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x000264BC File Offset: 0x000246BC
	public static void RemoveHighlight(Highlighter h)
	{
		if (!(h == null))
		{
			h.occluder = false;
			h.overlay = false;
			h.ConstantOffImmediate();
		}
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00036574 File Offset: 0x00034774
	public static List<Vector3> PlotTrajectory(UseableGun gun, out RaycastHit hit, int maxSteps = 255)
	{
		hit = default(RaycastHit);
		Transform transform = (OptimizationVariables.MainPlayer.look.perspective == EPlayerPerspective.FIRST) ? OptimizationVariables.MainPlayer.look.aim : OptimizationVariables.MainCam.transform;
		Vector3 vector = transform.position;
		Vector3 forward = transform.forward;
		ItemGunAsset equippedGunAsset = gun.equippedGunAsset;
		float num = equippedGunAsset.ballisticDrop;
		Attachments attachments = (Attachments)TrajectoryComponent.thirdAttachments.GetValue(gun);
		List<Vector3> list = new List<Vector3>
		{
			vector
		};
		if (((attachments != null) ? attachments.barrelAsset : null) != null)
		{
			num *= attachments.barrelAsset.ballisticDrop;
		}
		for (int i = 1; i < maxSteps; i++)
		{
			vector += forward * equippedGunAsset.ballisticTravel;
			forward.y -= num;
			forward.Normalize();
			if (Physics.Linecast(list[i - 1], vector, out hit, RayMasks.DAMAGE_CLIENT))
			{
				list.Add(hit.point);
				break;
			}
			list.Add(vector);
		}
		return list;
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x000264DB File Offset: 0x000246DB
	[OnSpy]
	public static void OnSpy()
	{
		if (TrajectoryComponent.Highlighted != null)
		{
			TrajectoryComponent.RemoveHighlight(TrajectoryComponent.Highlighted);
		}
		TrajectoryComponent.spying = true;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x000264FA File Offset: 0x000246FA
	[OffSpy]
	public static void OffSpy()
	{
		TrajectoryComponent.spying = false;
	}

	// Token: 0x04000297 RID: 663
	public static readonly FieldInfo thirdAttachments = typeof(UseableGun).GetField("thirdAttachments", BindingFlags.Instance | BindingFlags.NonPublic);

	// Token: 0x04000298 RID: 664
	public static bool spying;
}

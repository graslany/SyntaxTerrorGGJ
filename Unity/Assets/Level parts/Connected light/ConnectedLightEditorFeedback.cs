using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedLightEditorFeedback : EditorFeedback<ConnectedLight> {

	// Configuration de la lampe cible ; elle est copiée
	// régulièrement afin de savoir quand elle change.
	private bool readConfig;
	private float switchedOffIntensity;
	private Color switchedOffColor;
	private float switchedOnIntensity;
	private Color switchedOnColor;
	private bool isSwitchedOn;

	protected override void Awake ()
	{
		base.Awake ();
		readConfig = false;
	}

	protected override bool TargetMayHaveChanged ()
	{
		// Si on n'a jamais consulté la configuration du contrôle cible, considrer qu'il a changé.
		bool res = !readConfig;

		// Copier tous les champs qui configurent la lampe, en regardant si certains ont changé.
		CompareAndSet (target.IsSwitchedOn, ref isSwitchedOn, ref res);
		CompareAndSet (target.SwitchedOnColor, ref switchedOnColor, ref res);
		CompareAndSet (target.SwitchedOnIntensity, ref switchedOnIntensity, ref res);
		CompareAndSet (target.SwitchedOffColor, ref switchedOffColor, ref res);
		CompareAndSet (target.SwitchedOffIntensity, ref switchedOffIntensity, ref res);

		readConfig = true;
		return res;
	}

	protected override void ApplyTargetConfiguration ()
	{
		target.ApplyState ();
	}

}

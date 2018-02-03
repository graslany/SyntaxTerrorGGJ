
/// <summary>
/// Mode d'activation d'un bouton ou d'une plaqe de pression
/// </summary>
public enum ButtonMode
{
	/// <summary>
	/// Le bidule est activé tant qu'on appuie dessus.
	/// </summary>
	ActiveWhenPressed,

	/// <summary>
	/// L'état d'activation change (ON - OFF) à chaque fois qu'on commence
	/// à appuyer sur le bidule.
	/// </summary>
	SwitchWhenPressed
}
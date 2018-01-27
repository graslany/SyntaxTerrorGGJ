using System;

/// <summary>
/// Interface des objets qui peuvent utiliser des valeurs.
/// </summary>
public interface IValuesUser<T>
{
	/// <summary>
	/// Notifie cet objet qu'une valeur qu'il peut utilser a changé.
	/// </summary>
	void OnValueChanged(ISingleValueReceiver<T> valueHolder);
}


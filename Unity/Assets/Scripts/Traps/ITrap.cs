using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface des pièges.
/// </summary>
public interface ITrap
{
	/// <summary>
	/// Etat courant du piège
	/// </summary>
	TrapState State { get; }

	/// <summary>
	/// (Ré)arme ce piège, en l'arrêtant s'il était actif.
	/// </summary>
	void Arm();

	/// <summary>
	/// Désarme ce piège, en l'arrêtant s'il était actif.
	/// </summary>
	void Disarm();

	/// <summary>
	/// Déclenche ce piège, BOOM !
	/// </summary>
	void Spring();
    
}

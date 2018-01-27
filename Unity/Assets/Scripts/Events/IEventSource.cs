using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Une source d'événements qui peut envoyer des messages sur le réseau aux autres joueurs.
/// </summary>
public interface IEventSource {

	/// <summary>
	/// Identifiant de cette source, permettant d'adresser ses messages aux bonnes sources
	/// </summary>
	string Identifier { get; }
}

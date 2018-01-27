using System;

public interface ISingleValueReceiver : IEventReceiver
{
	/// <summary>
	/// L'identifiant de la source que ce récepteur souhaite écouter.
	/// </summary>
	string SourceName { get; }
}

public interface ISingleValueReceiver<T> : ISingleValueReceiver
{

	/// <summary>
	/// Accepte la nouvelle valeur prise par la source que ce récepteur souhaite écouter.
	/// </summary>
	void ReceiveNewValue(string variableName, T newValue);
}


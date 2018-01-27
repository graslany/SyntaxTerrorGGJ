using System;

public interface IValueSource<T> : IEventSource
{
	/// <summary>
	/// Valeur de cette variable
	/// </summary>
	T StoredValue { get; }
}


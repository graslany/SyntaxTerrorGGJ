using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SimpleValueSource<T> : IValueSource<T>
{

	public string Identifier {
		get {
			return identifier;
		}
	}
	private string identifier = "NON RENSEIGNE";

	public T StoredValue {
		get {
			return _storedValue;
		}
		set {
			if (!Equals (_storedValue, value)) {
				_storedValue = value;
				ValueSourcesSender.SendNewValue (this);
			}
		}
	}
	private T _storedValue;

	T IValueSource<T>.StoredValue {
		get {
			return StoredValue;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleValueSource<T> : IValueSource<T>
{

	[SerializeField]
	private string identifier = "NON RENSEIGNE";
	public string Identifier {
		get {
			return identifier;
		}
	}

	[SerializeField]
	private T storedValue;
	public T StoredValue {
		get {
			return storedValue;
		}
		set {
			if (!Equals (storedValue, value)) {
				storedValue = value;
				PlayerObject.Instance.SignalVariableChangeToServer(this);
			}
		}
	}

	T IValueSource<T>.StoredValue {
		get {
			return StoredValue;
		}
	}

	public SimpleValueSource():this(null, default(T)) { }

	public SimpleValueSource(string identifier, T value) {
		this.identifier = identifier;
		this.storedValue = value;
	}

}

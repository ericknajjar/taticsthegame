using UnityEngine;
using System.Collections;
using u3dExtensions.IOC;
using u3dExtensions.Engine.Runtime;

public class Bootstrap : MonoBehaviour {

	IBindingContext m_masterContext;

	void Start()
	{
		DontDestroyOnLoad (gameObject);

		var bindingFinder = new ReflectiveBindingFinder (GetType ().Assembly);

		m_masterContext = new ReflectiveBindingContextFactory (bindingFinder).CreateContext();
	}
}

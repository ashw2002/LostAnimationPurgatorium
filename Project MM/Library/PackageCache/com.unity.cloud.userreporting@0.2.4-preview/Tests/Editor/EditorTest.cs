using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

class EditorExampleTest
{
	[UnityTest]
	public IEnumerator EditorSampleTestWithEnumeratorPasses() {
		Menus.ImportPackageMenuItem();
		yield return null;
	}
}

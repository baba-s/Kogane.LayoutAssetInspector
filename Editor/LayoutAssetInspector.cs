using System.IO;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	[CustomEditor( typeof( DefaultAsset ) )]
	internal sealed class LayoutAssetInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var defaultAsset = ( DefaultAsset ) target;
			var assetPath    = AssetDatabase.GetAssetPath( defaultAsset );
			var extension    = Path.GetExtension( assetPath );

			if ( !extension.EndsWith( ".wlt" ) ) return;

			var oldEnabled = GUI.enabled;
			GUI.enabled = true;

			if ( GUILayout.Button( "Load" ) )
			{
				var assembly   = typeof( Editor ).Assembly;
				var type       = assembly.GetType( "UnityEditor.WindowLayout" );
				var methodInfo = type.GetMethod( "LoadWindowLayout", new[] { typeof( string ), typeof( bool ) } );

				var fullPath = Path.GetFullPath( assetPath );

				methodInfo.Invoke( null, new object[] { fullPath, false } );
			}

			GUI.enabled = oldEnabled;
		}
	}
}
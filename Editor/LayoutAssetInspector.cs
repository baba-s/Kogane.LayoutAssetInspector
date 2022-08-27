using System.IO;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal sealed class LayoutAssetInspector : IDefaultAssetInspector
    {
        static LayoutAssetInspector()
        {
            DefaultAssetInspector.Add( new LayoutAssetInspector() );
        }

        void IDefaultAssetInspector.OnInspectorGUI( Object target )
        {
            var defaultAsset = ( DefaultAsset )target;
            var assetPath    = AssetDatabase.GetAssetPath( defaultAsset );
            var extension    = Path.GetExtension( assetPath );

            if ( !extension.EndsWith( ".wlt" ) ) return;

            var oldEnabled = GUI.enabled;
            GUI.enabled = true;

            try
            {
                if ( GUILayout.Button( "Load" ) )
                {
                    var assembly   = typeof( Editor ).Assembly;
                    var type       = assembly.GetType( "UnityEditor.WindowLayout" );
                    var methodInfo = type.GetMethod( "LoadWindowLayout", new[] { typeof( string ), typeof( bool ) } );
                    var fullPath   = Path.GetFullPath( assetPath );

                    methodInfo.Invoke( null, new object[] { fullPath, false } );
                }
            }
            finally
            {
                GUI.enabled = oldEnabled;
            }
        }
    }
}
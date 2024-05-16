using System.Xml;

namespace AndroidIAttributeSetBug {
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity {
        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);

            string xmlString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                "<Com.Google.Android.Exoplayer2.UI.StyledPlayerView " +
                //"<com.google.android.exoplayer2.ui.PlayerView " +
                "android:id=\"@+id/player_view\" " +
                "app:surface_type=\"texture_view\" " +
                "android:layout_width=\"match_parent\" " +
                "android:layout_height=\"match_parent\"/>";

            XmlReader xmlReader = XmlReader.Create(new StringReader(xmlString)); //dispose of xmlReader when done or use "using"
            xmlReader.Read(); // https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlreader.read?view=net-8.0

            Android.Util.IAttributeSet attributes = Android.Util.Xml.AsAttributeSet(xmlReader);

            //BUILD EXOPLAYER
            //Com.Google.Android.Exoplayer2.UI.StyledPlayerView styledPlayerView = new(this); //constructor with no attributes will make SurfaceView successfully
            Com.Google.Android.Exoplayer2.UI.StyledPlayerView styledPlayerView = new(this, attributes); //constructor with attributes will give casting error and fail
            System.Diagnostics.Debug.WriteLine("SURFACE TYPE " + styledPlayerView.VideoSurfaceView.GetType()); //default is SurfaceView, need to make TextureView

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
    }
}
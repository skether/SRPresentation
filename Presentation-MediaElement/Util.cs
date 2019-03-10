using System.Windows.Controls;
using System.Reflection;


public static class Util
{
    public static MediaState GetMediaState(this MediaElement myMedia)
    {
        FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
        object helperObject = hlp.GetValue(myMedia);
        FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
        MediaState state = (MediaState)stateField.GetValue(helperObject);
        return state;
    }
}
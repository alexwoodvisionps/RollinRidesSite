using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RollingRides.WebApp.Controls
{
    public class MovieControl : Control
    {
        public override void RenderControl(HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            //writer.WriteBeginTag("object");
            if (Url.ToLower().EndsWith(".flv") || Url.ToLower().EndsWith(".swf"))
                RenderFlashControl(writer);
            else if (Url.ToLower().EndsWith(".mov") || Url.ToLower().EndsWith(".mp4"))
                RenderApple(writer);
            else if(Url.ToLower().EndsWith(".wmv"))
            {
                RenderWmv(writer);
            }
            else
            {
                writer.WriteLine("<h2>Bad Video File</h2>");
            }
            writer.Flush();
            //RenderMp4(writer);
            //writer.WriteLine("<object></object>");
            //writer.WriteEndTag("object");
        }
        
        private void RenderWmv(HtmlTextWriter writer)
        {
            writer.WriteLine("<div>Requires Windows Media Player To Be Installed</div>");
            writer.WriteLine("<object width='" + Width + "%' height='" + Height + "%' type=\"video/x-ms-asf\" url='" + Url + "' data='" + Url +"' classid=\"CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6\"><param name=\"url\" value='"+ Url +"'><param name=\"filename\" value='" + Url + "'><param name=\"autostart\" value=\"0\"><param name=\"uiMode\" value=\"full\" /><param name=\"autosize\" value=\"0\"><param name=\"playcount\" value=\"1\"><embed type=\"application/x-mplayer2\" src='"+ Url +"' width='"+Width+"%' height='"+ Height + "%' autostart=\"false\" showcontrols=\"true\" pluginspage=\"http://www.microsoft.com/Windows/MediaPlayer/\"></embed></object>");
        }

        private void RenderFlashControl(HtmlTextWriter writer)
        {
            writer.WriteLine("<div>Requires Flash To Be Installed</div>");
            writer.WriteLine("<object width='" + Width + "' height='" + Height + "' classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\"><param name=\"SRC\" value='" + Url + "'><embed src='" + Url + "' width='" + Width + "' height='" + Height + "'></embed></object> ");
        }


        private void RenderApple(HtmlTextWriter writer)
        {
            writer.WriteLine("<div>Requires Quicktime and a Html 5 browser such as Firefox 10+ or IE 9+</div>");
            writer.WriteLine("<object data='" + Url + "' width='" + Width +"' height='" + Height + "'><embed src='" + Url + "' width='"+Width+"' height='" + Height + "' /></object> ");
            //writer.WriteLine("<EMBED SRC='"+ Url + "' WIDTH=" + Width+ " HEIGHT = '"+ Height +"' AUTOPLAY='false' CONTROLLER='true' LOOP=\"false\" PLUGINSPAGE=\"http://www.apple.com/quicktime/\">");
            //writer.WriteLine("<video width='" + Width + "' height='" + Height + " autoplay=\"0\" controls=\"1\" tabindex=\"0\"><source type=\"video/mp4\" src='" + Url +"'></source></video>");
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
    }
}
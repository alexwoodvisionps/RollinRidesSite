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
            writer.WriteLine("<object width='" + Width + "%' height='" + Height + "%' type=\"video/x-ms-asf\" url='" + Url + "' data='" + Url +"' classid=\"CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6\"><param name=\"url\" value='"+ Url +"'><param name=\"filename\" value='" + Url + "'><param name=\"autostart\" value=\"0\"><param name=\"uiMode\" value=\"full\" /><param name=\"autosize\" value=\"0\"><param name=\"playcount\" value=\"1\"><embed type=\"application/x-mplayer2\" src='"+ Url +"' width='"+Width+"%' height='"+ Height + "%' autostart=\"false\" showcontrols=\"true\" pluginspage=\"http://www.microsoft.com/Windows/MediaPlayer/\"></embed></object>");
        }

        private void RenderFlashControl(HtmlTextWriter writer)
        {
            writer.WriteLine("<object width='" +Width + "' height='" + Height + "' classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\"><param name=\"SRC\" value='" + Url + "'><embed src='" + Url + "' width='" + Width + "' height='" + Height + "'></embed></object> ");
        }


        private void RenderApple(HtmlTextWriter writer)
        {
            writer.WriteLine("<object width='" + Width + "' height='" + Height + "' classid=\"clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B\" codebase=\"http://www.apple.com/qtactivex/qtplugin.cab\"><param name=\"src\" value='" + Url + "' /><param name=\"controller\" value=\"true\" /></object> ");
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
    }
}
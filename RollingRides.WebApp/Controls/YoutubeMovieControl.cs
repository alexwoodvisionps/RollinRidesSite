using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RollingRides.WebApp.Controls
{
    public class YoutubeMovieControl : Control
    {
        public override void RenderControl(HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            RenderYouTube(writer);
        }
        private void RenderYouTube(HtmlTextWriter writer)
        {
            writer.WriteLine("<iframe width=\"" + Width +"\" height=\""+ Height +"\" src='" + Url + "' frameborder=\"0\" allowfullscreen='true'></iframe>");
            //writer.WriteLine("<object width='" + Width +" height='" + Height + "'>" +    
            //              "<param name=\"movie\" value='" + Url + "'></param>" +   
            //              "<param name=\"allowFullScreen\" value=\"true\"></param>" +   
            //              "<param name=\"allowscriptaccess\" value=\"always\"></param>" +   
            //              "<embed src='" + Url + "' type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" width='" + Width +"' height='" + Height +"'></embed>" +   
            //              "</object>");
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public string Url { get; set; }
    }
}
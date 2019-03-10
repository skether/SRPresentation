using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentation_MediaElement
{
    class Chapter
    {
        public string name;
        public TimeSpan time;

        public Chapter(XElement element)
        {
            string[] timePieces = (element.FirstNode as XElement).Value.Split(new char[] { ':', '.'});
            this.time = new TimeSpan(0, Convert.ToInt32(timePieces[0]), Convert.ToInt32(timePieces[1]), Convert.ToInt32(timePieces[2]), Convert.ToInt32(timePieces[3].Substring(0, 3)));
            XElement next = (element.FirstNode.NextNode as XElement);
            if (next.Name.LocalName != "ChapterDisplay") next = next.NextNode as XElement;
            this.name = (next.FirstNode as XElement).Value;
        }

        public override string ToString()
        {
            return $"{name} - {time.ToString()}";
        }
    }
}

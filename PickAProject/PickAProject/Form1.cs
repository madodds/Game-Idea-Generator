using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PickAProject
{
    public partial class mainForm : Form
    {
        private string engineURL = String.Empty;

        public mainForm()
        {
            InitializeComponent();
        }

        private void generationButton_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// Creates a couple sentences of randomly selected game design attributes.
            /// 
            /// Format:
            ///  Create a(n) {genre} {genre} game that utilizes {perk}, {perk}, and {perk}. 
            ///  This game has a(n) {setting} setting with a {style} art style.
            ///  Try using {engine} to build this game, which can be found at {engine.url}. {engine} uses {engine.language} as its language.
            ///
            /// Example: 
            ///  Create a role playing horror game that utilizes character customization, destructible environments, and turn-based gameplay.
            ///  This game has an underwater setting with a gothic art style.
            ///  Need an engine? Try using Unity to build this game, which can be found at http://unity3d.com. Unity uses C# / JavaScript as its language.
            /// </summary>

            List<string> output = new List<string>();
            try
            {
                // Load the xml and put its nodes into lists.
                XDocument xmlDoc = XDocument.Load("Attributes.xml");
                List<XElement> genreList = QueryXML("genres", xmlDoc);
                List<XElement> engineList = QueryXML("engines", xmlDoc);
                List<XElement> perkList = QueryXML("perks", xmlDoc);
                List<XElement> settingList = QueryXML("settings", xmlDoc);
                List<XElement> styleList = QueryXML("styles", xmlDoc);
                string genres = "", engine = "", perks = "", setting = "", style = "";

                Random rand = new Random();
                List<int> randResults;

                // Write genres
                int randRequests = 2;
                if (genreList.Count >= randRequests)
                {
                    // The "an" attribute tells the code whether or not to write "a" or "an" for proper grammar.
                    randResults = Enumerable.Range(0, genreList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    genres = string.Format("Create {0} {1} {2} game", 
                        genreList[randResults[0]].Attribute("an").Value == "true" ? "an" : "a",
                        genreList[randResults[0]].Value, 
                        genreList[randResults[1]].Value);
                }

                // Write perks
                randRequests = 3;
                if (perkList.Count >= randRequests)
                {
                    randResults = Enumerable.Range(0, perkList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    perks = string.Format(" that utilizes {0}, {1}, and {2}.",
                        perkList[randResults[0]].Value,
                        perkList[randResults[1]].Value,
                        perkList[randResults[2]].Value);
                }

                // Write setting
                randRequests = 1;
                if (settingList.Any())
                {
                    randResults = Enumerable.Range(0, settingList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    setting = string.Format("This game has {0} {1} setting",
                        settingList[randResults[0]].Attribute("an").Value == "true" ? "an" : "a",
                        settingList[randResults[0]].Value);
                }

                // Write style
                randRequests = 1;
                if (styleList.Any())
                {
                    randResults = Enumerable.Range(0, styleList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    style = string.Format(" with {0} {1} art style.",
                        styleList[randResults[0]].Attribute("an").Value == "true" ? "an" : "a",
                        styleList[randResults[0]].Value);
                }

                // Write engine and get its URL
                randRequests = 1;
                if (engineList.Count >= randRequests)
                {
                    randResults = Enumerable.Range(0, engineList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    engine = string.Format("Need an engine? Try using {0} to build this game, which can be found at {1}. {0} uses {2} as its language.",
                        engineList[randResults[0]].Value,
                        engineList[randResults[0]].Attribute("url").Value,
                        engineList[randResults[0]].Attribute("language").Value);
                    engineURL = engineList[randResults[0]].Attribute("url").Value;
                }

                // add the strings to the output to make sentences.
                output.Add(genres + perks); 
                output.Add(setting + style);
                output.Add(engine);

                websiteButton.Enabled = true;
            }
            catch(Exception ex)
            {
                output.Clear();
                output.Add(ex.Message);
            }

            // output the sentences to the text box.
            textOutput.Clear();
            foreach (string sentence in output)
            {
                textOutput.AppendText(sentence);
                if (sentence != output.Last())
                {
                    textOutput.AppendText(Environment.NewLine);
                    textOutput.AppendText(Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Queries the xml using the provided string. 
        /// </summary> 
        /// <param name="name">The name of the group of nodes you want to access.</param>
        /// <param name="xml">The loaded xml document.</param>
        /// <returns>The list of nodes from that query.</returns>
        private List<XElement> QueryXML(string name, XDocument xml)
        {
            List<XElement> list = new List<XElement>();
            var query = from k in xml.Root.Elements(name)
                        select k.Descendants();

            // If there were no results, return an empty list.
            if (!query.Any()) return list;

            // Build the results into a list and return it.
            foreach (XElement el in query.ToList<IEnumerable<XContainer>>()[0]) list.Add(el);

            return list;
        }

        private void websiteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Opens the webpage
                ProcessStartInfo sInfo = new ProcessStartInfo(engineURL);
                Process.Start(sInfo);
            }
            catch
            {
                textOutput.AppendText(Environment.NewLine);
                textOutput.AppendText("Could not open the Webpage. Do it yourself.");
            }
        }
    }
}

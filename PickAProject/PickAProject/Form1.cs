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
            /// Creates a sentence with the following format and a URL to the engine's source:
            ///  Create a(n) {Genre} {Genre} in {Game Engine} that utilizes {Design Perk}, {Design Perk}, and {Design Perk} that takes place {Theme}.
            ///  {Game Engine} uses {language} and can be found at {url}.
            ///
            /// Example: 
            ///  Create a roguelike top-down shooter in Unity that utilizes Skill Trees, Pathfinding, and Synchronized Music that takes place underwater.
            ///  Unity uses C# / JavaScript and can be found at: http://unity3d.com
            /// </summary>

            string output = "Unthrown Error";
            string engineDetails = "Could not generate project.";
            try
            {
                // Load the xml and put its nodes into lists.
                XDocument xmlDoc = XDocument.Load("Attributes.xml");
                List<XElement> genreList = QueryXML("genres", xmlDoc);
                List<XElement> engineList = QueryXML("engines", xmlDoc);
                List<XElement> perkList = QueryXML("perks", xmlDoc);
                List<XElement> themeList = QueryXML("themes", xmlDoc);
                string genres = "", engineName = "", perks = "", theme = "";

                Random rand = new Random();
                List<int> randResults;

                // Write genres
                int randRequests = 2;
                if (genreList.Count >= randRequests)
                {
                    // The "an" attribute tells the code whether or not to write "a" or "an" for proper grammer.
                    randResults = Enumerable.Range(0, genreList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    genres = string.Format("Create {0} {1} {2}", 
                        genreList[randResults[0]].Attribute("an").Value == "true" ? "an" : "a",
                        genreList[randResults[0]].Value, 
                        genreList[randResults[1]].Value);
                }

                // Write engine, its details, and its URL
                randRequests = 1;
                if (engineList.Count >= randRequests)
                {
                    randResults = Enumerable.Range(0, engineList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    engineName = string.Format(" in {0}", 
                        engineList[randResults[0]].Value);
                    engineDetails = string.Format("{0} uses {1} and can be found at {2}.", 
                        engineList[randResults[0]].Value,
                        engineList[randResults[0]].Attribute("language").Value, 
                        engineList[randResults[0]].Attribute("url").Value);
                    engineURL = engineList[randResults[0]].Attribute("url").Value;
                }

                // Write perks
                randRequests = 3;
                if (perkList.Count >= randRequests)
                {
                    randResults = Enumerable.Range(0, perkList.Count).OrderBy(x => rand.Next()).Take(randRequests).ToList<int>();
                    perks = string.Format(" that utilizes {0}, {1}, and {2},", 
                        perkList[randResults[0]].Value,
                        perkList[randResults[1]].Value, 
                        perkList[randResults[2]].Value);
                }

                // Write theme
                if (themeList.Any())
                {
                    theme = string.Format(" that takes place {0}.", 
                        themeList[rand.Next(0, themeList.Count)].Value);
                }

                output = genres + engineName + perks + theme;

                websiteButton.Enabled = true;
            }
            catch(Exception ex)
            {
                output = ex.Message;
            }

            textOutput.Clear();
            textOutput.AppendText(output);
            textOutput.AppendText(Environment.NewLine);
            textOutput.AppendText(Environment.NewLine);
            textOutput.AppendText(engineDetails);
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

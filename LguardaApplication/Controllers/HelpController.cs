using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;
using LgurdaApp.Model.ControllerModels;
using System.Xml.Linq;
using System.Linq;

namespace LguardaApplication.Controllers
{
   



    public class HelpController : Controller
    {
        public ActionResult List()
        {
           
            var helpListCntrl = GetControllerName();
            return View(helpListCntrl);
        }

        public ActionResult GetHelp(string cntrlName)
        {
            Help helpObjText;
            var helpListText = new List<Help>();

            XDocument doc = XDocument.Load("C:\\help2.xml");

            var selectors = (from elements in doc.Elements("LgurdaHelp").Elements("Controller")
                             where elements.Attribute("name").Value == cntrlName
                             select elements).FirstOrDefault();
            var list = selectors.Elements("Text").ToList();

            for (int i = 0; i < list.Count; i++)
            {
                helpObjText = new Help();
                helpObjText.text = (string)list[i];
                //helpObjText.cntrlName = cntrlName;
                helpListText.Add(helpObjText);
            }

            ViewData["ControllerName"] = "How to navigate" + " " + cntrlName;
            var helpListCntrl = GetControllerName();
            ViewData["Contollers"] = helpListCntrl;

            return View(helpListText);

        }

        private static List<Help> GetControllerName()
        {
            Help helpObjCntrl;
            var helpListCntrl = new List<Help>();
            var doc = new XmlDocument();
            doc.Load("C:\\help2.xml");
            var items = doc.GetElementsByTagName("Controller");
            var xmlActions = new string[items.Count];
            var xmlFileNames = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                var xmlAttributeCollection = items[i].Attributes;
                if (xmlAttributeCollection != null)
                {
                    helpObjCntrl = new Help();
                    var action = xmlAttributeCollection["name"];
                    xmlActions[i] = action.Value;
                    helpObjCntrl.cntrlName = xmlActions[i];
                    //helpObjCntrl.text = items[i].InnerText;
                    helpListCntrl.Add(helpObjCntrl);
                }
            }
            return helpListCntrl;
        }
    }
}
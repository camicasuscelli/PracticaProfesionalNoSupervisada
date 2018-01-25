using System;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace MLM.Business.Configurations
{
    internal sealed class XmlConfigurator : IConfigurationSectionHandler
    {
        #region --Constructors & Destructors--

        #endregion --Constructors & Destructors--

        #region --Methods--

        public object Create(object parent, object configContext, XmlNode section)
        {
            Object settings = null;

            if (section == null)
            {
                return settings;
            }

            XPathNavigator navigator = section.CreateNavigator();
            if (navigator != null)
            {
                String typeName = (string)navigator.Evaluate("string(@type)");
                Type sectionType = Type.GetType(typeName);

                XmlSerializer xs = new XmlSerializer(sectionType);
                XmlNodeReader reader = new XmlNodeReader(section);

                try
                {
                    settings = xs.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return settings;
        }

        #endregion --Methods--
    }
}

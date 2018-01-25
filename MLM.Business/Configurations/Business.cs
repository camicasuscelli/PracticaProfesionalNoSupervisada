using System.Xml.Serialization;

namespace MLM.Business.Configurations
{
    public class Business
    {
        #region --Attributes--

        private string _assembly;
        private string _assemblyName;
        private string _interface;

        #endregion --Attributes--

        #region --Constructors & Destructors--

        public Business()
        {
        }

        public Business(string @interface)
        {
            _interface = @interface;
        }

        #endregion --Constructors & Destructors--

        #region --Properties--

        [XmlAttribute]
        public string Interface
        {
            get { return _interface; }
            set { _interface = value; }
        }

        [XmlAttribute]
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        [XmlAttribute]
        public string Assembly
        {
            get { return _assembly; }
            set { _assembly = value; }
        }

        #endregion --Properties--
    }
}

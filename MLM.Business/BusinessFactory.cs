using System;
using System.Configuration;
using System.Reflection;
using MLM.Business.Configurations;

namespace MLM.Business
{
    public class BusinessFactory
    {
        #region --Attributes--

        private readonly BusinessSettings _businessSettings;

        #endregion --Attributes--

        #region --Singleton--

        private static volatile BusinessFactory _instance;

        public static BusinessFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(BusinessFactory))
                    {
                        if (_instance == null)
                            _instance = new BusinessFactory();
                    }
                }
                return _instance;
            }
        }

        #endregion --Singleton--

        #region --Constructors & Destructors--

        private BusinessFactory()
        {
            try
            {
                _businessSettings = (BusinessSettings)ConfigurationManager.GetSection("BusinessSettings");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion --Constructors & Destructors--

        #region --Factory--

        public object CreateBusiness(Type type, object[] args)
        {
            int index = -1;
            try
            {
                index = _businessSettings.Objects.IndexOf(new MLM.Business.Configurations.Business(type.Name));
                //Array.Resize<object>(ref args, args.Length + 1);
                //args[args.Length - 1] = _businessSettings.Objects[index].PerformanceLimit;
                return System.Reflection.Assembly.Load(new AssemblyName(_businessSettings.Objects[index].AssemblyName)).CreateInstance(_businessSettings.Objects[index].Assembly, false, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public, null, new object[] { args }, null, null);
            }
            catch (Exception e)
            {
                if (index == -1)
                    throw new Exception("No se encontro entrada en el config para el tipo '" + type.Name + "'", e);
                throw e;
            }
        }

        public object CreateBusiness(Type type)
        {
            int index = -1;
            try
            {
                index = _businessSettings.Objects.IndexOf(new MLM.Business.Configurations.Business(type.Name));
                return System.Reflection.Assembly.Load(new AssemblyName(_businessSettings.Objects[index].AssemblyName)).CreateInstance(_businessSettings.Objects[index].Assembly, false, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public, null, null, null, null);
            }
            catch (Exception e)
            {
                if (index == -1)
                    throw new Exception("No se encontro entrada en el config para el tipo '" + type.Name + "'", e);
                throw e;
            }
        }

        #endregion --Factory--
    }
}

namespace MLM.Common
{
    public class Logging
    {
        #region --Attributes--

        private static volatile Logging _instance;

        #endregion --Attributes--

        #region --Singleton--

        public static Logging Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(Logging))
                    {
                        if (_instance == null)
                            _instance = new Logging();
                    }
                }
                return _instance;
            }
        }

        #endregion --Singleton--

        #region --Constructors & Destructors--

        private Logging()
        {
            try
            {

            }
            catch
            {
                //Si no hay seteado un logger, continua y listo....!!!!
            }
        }

        #endregion --Constructors & Destructors--

        #region --Methods--

        public void LogError(string message)
        {

        }

        public void LogError(string message, System.Exception ex)
        {

        }

        public void LogError(System.Exception ex)
        {

        }

        #endregion
    }
}

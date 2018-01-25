namespace MLM.Business.Configurations
{
    public class BusinessSettings
    {
        #region --Attributes--

        private BusinessCollection _collection;

        #endregion --Attributes--

        #region --Properties--

        public BusinessCollection Objects
        {
            get
            {
                if (_collection == null)
                {
                    _collection = new BusinessCollection();
                }
                return _collection;
            }
            set { _collection = value; }
        }

        #endregion --Properties--
    }
}

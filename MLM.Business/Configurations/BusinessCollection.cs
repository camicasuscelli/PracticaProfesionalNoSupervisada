using System.Collections;

namespace MLM.Business.Configurations
{
    public sealed class BusinessCollection : CollectionBase
    {
        #region --Methods--

        public int Add(Business value)
        {
            return (List.Add(value));
        }

        public int IndexOf(Business value)
        {
            int index = List.IndexOf(value);
            if (index == -1)
            {
                foreach (Business oBusiness in List)
                {
                    if (value.Interface.Equals(oBusiness.Interface))
                    {
                        index = List.IndexOf(oBusiness);
                        break;
                    }
                }
            }
            return index;
        }

        public void Insert(int index, Business value)
        {
            List.Insert(index, value);
        }

        public void Remove(Business value)
        {
            List.Remove(value);
        }

        public bool Contains(Business value)
        {
            return List.Contains(value);
        }

        #endregion --Methods--

        #region --Properties--

        public Business this[int index]
        {
            get { return ((Business)List[index]); }
            set { List[index] = value; }
        }

        #endregion --Properties--
    }
}

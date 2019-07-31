using System;

namespace Aafp.Also.Web.Models
{
    /// <author>ijoyce@aafp.org</author>
    /// <version>$Id: AbstractEntity.cs 21721 2011-01-05 21:52:09Z ylyeshchenko $</version>
    [Serializable]
    public abstract class AbstractEntity
    {
        #region Fields

        private Guid key;
        private string addUser;
        private DateTime addDate;
        private string changeUser;
        private DateTime? changeDate;
        private bool deleteFlag;
        private Guid? entityKey;

        #endregion

        #region Properties

        public virtual Guid Key
        {
            get { return key; }
            set { key = value; }
        }

        public virtual string AddUser
        {
            get { return addUser; }
            set { addUser = value; }
        }

        public virtual DateTime AddDate
        {
            get { return addDate; }
            set { addDate = value; }
        }

        public virtual string ChangeUser
        {
            get { return changeUser; }
            set { changeUser = value; }
        }

        public virtual DateTime? ChangeDate
        {
            get { return changeDate; }
            set { changeDate = value; }
        }

        public virtual bool DeleteFlag
        {
            get { return deleteFlag; }
            set { deleteFlag = value; }
        }

        public virtual Guid? EntityKey
        {
            get { return entityKey; }
            set { entityKey = value; }
        }

        #endregion

        #region Public Methods

        public virtual bool IsNew()
        {
            return Guid.Empty.Equals(Key);
        }

        public virtual void MarkAsChanged(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("ChangeUser cannot be null or empty when marking entity as changed.");
            }

            ChangeUser = user;
            ChangeDate = DateTime.Now;
        }

        #endregion
    }
}
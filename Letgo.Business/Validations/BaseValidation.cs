using Letgo.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Validations
{
	public enum Reason
	{
		None = 0,
		New = 1,
		Update = 2,
		Delete = 3,
	}
	public abstract class BaseValidation<T> where T : class
	{
		public bool IsValid { get; private set; }
		public string ErrorMessage { get; private set; }
		public string SuccessMessage { get; private set; }
		public Reason ValidationReason { get; set; }

		protected BaseValidation()
		{
			IsValid = false;
			ErrorMessage = "";
			SuccessMessage = "";
			ValidationReason = Reason.None;
		}

		public virtual void Validate(T entity, Reason reason)
		{
			IsValid = true;
			ErrorMessage = "";
			SuccessMessage = "";
			ValidationReason = reason;
		}

		protected void SetInvalid(string errorMessage)
		{
			IsValid = false;
			ErrorMessage = errorMessage;
		}

		protected void SetValid(string successMessage)
		{
			IsValid = true;
			SuccessMessage = successMessage;
		}
	}

}

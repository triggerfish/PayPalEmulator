using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayPalEmulator
{
	public class BinderResolver : DefaultModelBinder
	{
		private Func<Type, IModelBinder> m_resolver;
		private readonly Type m_binderOpenType = typeof(ModelBinder<>);

		public BinderResolver(Func<Type, IModelBinder> resolver)
		{
			m_resolver = resolver;
		}

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.ModelType.IsValueType)
			{
				return base.BindModel(controllerContext, bindingContext);
			}

			Type t = m_binderOpenType.MakeGenericType(bindingContext.ModelType);
			var binder = m_resolver(t);

			if (null == binder)
			{
				return base.BindModel(controllerContext, bindingContext);
			}

			return binder.BindModel(controllerContext, bindingContext);
		}
	}
}

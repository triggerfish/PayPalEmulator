using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Validator;
using Triggerfish.Ninject;
using System.Collections.Specialized;
using System.Text;
using Triggerfish.NHibernate;
using System.Web.Routing;

namespace PayPalEmulator
{
	public class BuyNowClickHandler : ICgiHandler
	{
		private Repository<PDT> m_pdtRepository;

		public BuyNowClickHandler(Repository<PDT> pdtRepository)
		{
			m_pdtRepository = pdtRepository;
		}

		public ActionResult Process(HttpRequestBase request, ModelStateDictionary modelState)
		{
			BuyNowBinder binder = new BuyNowBinder();
			PDT pdt = binder.Bind(request.Form, modelState);

			if (!modelState.IsValid)
			{
				throw new ErrorDataException(modelState);
			}

			m_pdtRepository.Insert(pdt);

			return new RedirectToRouteResult(new RouteValueDictionary(new {
				controller = "BuyNow",
				action = "BuyNow",
				pdtId = pdt.Id
			}));
		}
	}
}

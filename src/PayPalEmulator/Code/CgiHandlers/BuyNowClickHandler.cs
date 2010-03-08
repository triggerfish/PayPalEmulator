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
		private Repository<Transaction> m_txRepository;

		public BuyNowClickHandler(Repository<Transaction> txRepository)
		{
			m_txRepository = txRepository;
		}

		public ActionResult Process(HttpRequestBase request, ModelStateDictionary modelState)
		{
			BuyNowBinder binder = new BuyNowBinder();
			Transaction tx = binder.Bind(request.Form, modelState);

			if (!modelState.IsValid)
			{
				throw new ErrorDataException(modelState);
			}

			m_txRepository.Insert(tx);

			return new RedirectToRouteResult(new RouteValueDictionary(new {
				controller = "BuyNow",
				action = "BuyNow",
				txId = tx.Id
			}));
		}
	}
}

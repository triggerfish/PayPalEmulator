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
	public class AuthorisePdtHandler : ICgiHandler
	{
		private Repository<PDT> m_pdtRepository;

		public AuthorisePdtHandler(Repository<PDT> pdtRepository)
		{
			m_pdtRepository = pdtRepository;
		}

		public ActionResult Process(HttpRequestBase request, ModelStateDictionary modelState)
		{
			PdtVerificationBinder binder = new PdtVerificationBinder();
			PDT pdt = binder.Bind(request.Form, modelState);

			ContentResult cr = new ContentResult();
			cr.ContentEncoding = Encoding.UTF8;
			cr.ContentType = "text/html";
			cr.Content = "FAIL";

			if (pdt != null)
			{
				PDT dbPDT = m_pdtRepository.GetAll().Where(x => x.Tx == pdt.Tx).FirstOrDefault();
				if (dbPDT != null && dbPDT.AuthToken == pdt.AuthToken)
				{
					StringBuilder sb = new StringBuilder();
					sb.AppendLine("SUCCESS");

					decimal amount = Decimal.Parse(dbPDT.Amount);
					decimal commission = Math.Round((amount * 0.034m), 2, MidpointRounding.AwayFromZero) + 0.2m;

					sb.AppendLine(String.Format("{0}={1}", "txn_type", "web_accept"));
					sb.AppendLine(String.Format("{0}={1}", "txn_id", HttpUtility.UrlEncodeUnicode(dbPDT.Tx)));
					sb.AppendLine(String.Format("{0}={1}", "payment_status", dbPDT.State));
					sb.AppendLine(String.Format("{0}={1}", "mc_fee", commission.ToString()));
					sb.AppendLine(String.Format("{0}={1}", "mc_gross", HttpUtility.UrlEncodeUnicode(dbPDT.Amount)));
					sb.AppendLine(String.Format("{0}={1}", "custom", HttpUtility.UrlEncodeUnicode(dbPDT.Custom)));
					sb.AppendLine(String.Format("{0}={1}", "business", HttpUtility.UrlEncodeUnicode(dbPDT.Account)));

					cr.Content = sb.ToString();
				}
			}

			return cr;		
		}
	}
}

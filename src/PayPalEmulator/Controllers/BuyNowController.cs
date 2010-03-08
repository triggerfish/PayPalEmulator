using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Triggerfish.NHibernate;
using Triggerfish.Web.Mvc;
using Triggerfish.Web;

namespace PayPalEmulator.Controllers
{
	public enum BuyNowAction
	{
		Succeed,
		Fail,
		Corrupt
	}

	[HandleError]
	public class BuyNowController : Controller
	{
		private Repository<Transaction> m_txRepository;
		private IPostSubmitter m_postSubmitter;

		public BuyNowController(Repository<Transaction> txRepository, IPostSubmitter postSubmitter)
		{
			m_txRepository = txRepository;
			m_postSubmitter = postSubmitter;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult BuyNow(int txId)
		{
			Transaction tx = GetTx(txId);

			m_txRepository.Insert(tx);

			return View(new PaymentViewData { Tx = tx });
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Transaction]
		public ActionResult PayNow(int txId, BuyNowAction action)
		{
			Transaction tx = GetTx(txId);

			tx.Tx = Regex.Replace(Guid.NewGuid().ToString(), "-", String.Empty);
			tx.VerifySign = Regex.Replace(Guid.NewGuid().ToString(), "-", String.Empty);

			switch (action)
			{
				case BuyNowAction.Succeed:
					tx.State = "Completed";
					break;
				case BuyNowAction.Fail:
					tx.State = "Failed";
					break;
				case BuyNowAction.Corrupt:
					tx.State = "Completed";
					tx.Tx = null;
					break;
			}

			if (!String.IsNullOrEmpty(tx.IpnReturnUrl))
			{
				m_postSubmitter.Url = tx.IpnReturnUrl;
				m_postSubmitter.PostItems = tx.ToIpnQueryString();
				m_postSubmitter.BeginPost(null, null);
			}

			return RedirectToAction("Paid", new { txId = txId });
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult Paid(int txId)
		{
			return View(new PaymentViewData { Tx = GetTx(txId) });
		}

		private Transaction GetTx(int txId)
		{
			Transaction tx = m_txRepository.Get(txId);

			if (null == tx)
			{
				ModelState.AddModelError("txId", String.Format("Invalid ID: {0}", txId));
				throw new ErrorDataException(ModelState);
			}

			return tx;
		}
	}
}
